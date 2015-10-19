using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using KANG.Common;
using KANG.Common.json;
using KANG.SearchEngine.Model;

namespace KANG.SearchEngine {
    public class DocSearch : DocIndex {
        public DocSearch(Model.Search_Model searchModel, string filePath, string baseDataPath = Static.Config.LuceneBasePath)
            : base(filePath, baseDataPath) {
            InitSearchModel(searchModel);
        }

        private Search_Model SearchModel { get; set; }

        private void InitSearchModel(Search_Model searchModel) {
            SearchModel = searchModel;
            SearchModel.Words = GetKeyWordsSplitBySpace(SearchModel.Words);
        }

        /// <summary>
        /// 返回json
        /// </summary>
        /// <returns></returns>
        public string SearchJson() {
            if (string.IsNullOrEmpty(SearchModel.Words)) {
                return "";
            }
            SearchResult_Model<IEnumerable<Dictionary<string, string>>> searchResult = SearchDic();
            return searchResult.ToJson();
        }

        /// <summary>
        /// 返回list dic
        /// </summary>
        /// <returns></returns>
        public SearchResult_Model<IEnumerable<Dictionary<string, string>>> SearchDic() {
            SearchResult_Model<IEnumerable<Dictionary<string, string>>> searchResult =
                new SearchResult_Model<IEnumerable<Dictionary<string, string>>>();
            if (string.IsNullOrEmpty(SearchModel.Words)) {
                return searchResult;
            }

            SearchedDocResult searchedDoc = SearchInter();

            searchResult.Data = Docs2Dic(searchedDoc.Documents);
            searchResult.Total = searchedDoc.Total;
            return searchResult;
        }

        /// <summary>
        /// 返回强类型
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public SearchResult_Model<List<TModel>> Search<TModel>() {
            SearchResult_Model<List<TModel>> searchResultModel = new SearchResult_Model<List<TModel>>();
            if (string.IsNullOrEmpty(SearchModel.Words)) {
                return searchResultModel;
            }

            SearchedDocResult searchedDoc = SearchInter();

            List<TModel> models = searchedDoc.Documents.Select(Doc2TModel<TModel>).ToList();
            searchResultModel.Data = models;
            searchResultModel.Total = searchedDoc.Total;

            return searchResultModel;
        }

        private void SetDocBoost(Lucene.Net.Documents.Document document, float boost) {
            document.Boost = boost;
        }
        /// <summary>
        /// 设置权重
        /// 值越大，显示越靠前
        /// </summary>
        /// <param name="boost"></param>
        public bool SetBoost(float boost) { 
            SearchedDocResult result = SearchInter();
            List<Document> documents = result.Documents;
            documents.ForEach(doc => SetDocBoost(doc, boost));
            return true;
        }

        /// <summary>
        /// 根据field,fieldVal更新权重boost
        /// </summary>
        /// <param name="ID">field</param>
        /// <param name="IDVal">fieldVal</param>
        /// <param name="boost"></param>
        /// <returns></returns>
        public bool SetDocBoost(string ID, string IDVal, float boost) {
            using (var directory = GetLuceneDirectory()) {
                using (var searcher = new IndexSearcher(directory, true)) {
                    Analyzer analyzer = GetAnalyzer();
                    Filter filter = new QueryWrapperFilter(new TermQuery(new Term(ID, IDVal)));
      
                    TopDocs topDocs = searcher.Search(Query(analyzer), filter, 10);
                    ScoreDoc[] scoreDocs = topDocs.ScoreDocs;
                    List<Document> documents = ScoreDocs2Doc(searcher, scoreDocs);
                   
                    using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED)) {
                        documents.ForEach(doc => {
                            doc.Boost = boost;
                            writer.DeleteDocuments(new Term(ID, IDVal));
                            writer.AddDocument(doc); 
                        });
                        writer.Commit();
                        writer.Optimize();
                    }
                    analyzer.Close();
                    return true;
                }
            }
        }

        /// <summary>
                /// 不对外开放
                /// </summary>
                /// <returns></returns>
            private
            SearchedDocResult SearchInter() {

            SearchedDocResult searchResult = new SearchedDocResult();
            using (var directory = GetLuceneDirectory()) {
                using (var searcher = new IndexSearcher(directory, true)) {
                    var hitsLimit = 1000;

                    TopScoreDocCollector collector = TopScoreDocCollector.Create(hitsLimit, true);
                    Analyzer analyzer = GetAnalyzer();
                    searcher.Search(Query(analyzer), null, collector);

                    searchResult.Total = collector.TotalHits;

                    //TopDocs 指定0到GetTotalHits() 即所有查询结果中的文档 如果TopDocs(20,10)则意味着获取第20-30之间文档内容 达到分页的效果
                    int start = SearchModel.PageIndex*SearchModel.PageSize;
                    ScoreDoc[] scoreDocs = collector.TopDocs(start, SearchModel.PageSize).ScoreDocs;
                   
                    searchResult.Documents = ScoreDocs2Doc(searcher, scoreDocs);

                    analyzer.Close();
                    return searchResult;
                }
            }
        }

        private Query Query(Analyzer analyzer) {
            var parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, SearchModel.Fields, analyzer);
            return ParseQuery(SearchModel.Words, parser);
        }

        private IEnumerable<Dictionary<string, string>> Docs2Dic(List<Document> documents) {
            return documents.Select(Doc2Dic);
        }

        private List<Document> ScoreDocs2Doc(IndexSearcher searcher, IEnumerable<ScoreDoc> scoreDocs) {
            Lucene.Net.Search.Highlight.SimpleHTMLFormatter htmlFormatter =
                new SimpleHTMLFormatter("<b class='search_key'>", "</b>");
            Analyzer analyzer = GetAnalyzer();
            Highlighter highlighter = new Highlighter(htmlFormatter, new QueryScorer(Query(analyzer)));

            using (analyzer) {
                return scoreDocs.Select(one => ScoreDoc2Document(searcher, one, highlighter, analyzer)).ToList();
            }
        }

        private Document ScoreDoc2Document(IndexSearcher searcher, ScoreDoc scoreDoc, Highlighter highlighter,
            Analyzer analyzer) {
            Document newdoc = new Document();
            Document document = searcher.Doc(scoreDoc.Doc);
            var fields = document.GetFields();
            foreach (IFieldable field in fields) {
                string name = field.Name;
                string value = field.StringValue;
                string text = highlighter.GetBestFragment(analyzer, name, value) ?? (value ?? "");

                newdoc.Add(new Field(name, text, Field.Store.YES, Field.Index.ANALYZED));
            }
            return newdoc;
        }

        private TModel Doc2TModel<TModel>(Document document) {
            string json = Doc2Dic(document).ToJson();
            return JsonHelper<TModel>.DeserializeFromStr(json);
        }

        private Dictionary<string, string> Doc2Dic(Document document) {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var fields = document.GetFields();
            foreach (IFieldable field in fields) {
                string name = field.Name;
                string value = field.StringValue;
                dic.Add(name, value);
            }
            return dic;
        }

        /// <summary>
        /// Document 模型
        /// </summary>
        private class SearchedDocResult {
            public SearchedDocResult() {
                Total = 0;
                Documents = new List<Document>();
            }

            /// <summary>
            /// 搜索总数
            /// </summary>
            public int Total { get; set; }

            public List<Document> Documents { get; set; }
        }
    }
}
