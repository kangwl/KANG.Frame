using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Interop.Word;

namespace KANG.Office {
    public class Word2Pdf {
        /// <summary>
        /// 把Word文件转换成pdf文件2
        /// </summary>
        /// <param name="sourcePath">需要转换的文件路径和文件名称</param>
        /// <param name="targetPath">转换完成后的文件的路径和文件名名称</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool WordToPdf(object sourcePath, string targetPath) {
            bool result = false;
            WdExportFormat wdExportFormatPDF = WdExportFormat.wdExportFormatPDF;
            object missing = Type.Missing;
            Microsoft.Office.Interop.Word.ApplicationClass applicationClass = null;
            Microsoft.Office.Interop.Word.Document document = null;
            try {
                applicationClass = new Microsoft.Office.Interop.Word.ApplicationClass();
                document = applicationClass.Documents.Open(ref sourcePath, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                if (document != null) {
                    document.ExportAsFixedFormat(targetPath, wdExportFormatPDF, false, WdExportOptimizeFor.wdExportOptimizeForPrint, WdExportRange.wdExportAllDocument, 0, 0, WdExportItem.wdExportDocumentContent, true, true, WdExportCreateBookmarks.wdExportCreateWordBookmarks, true, true, false, ref missing);
                }
                result = true;
            }
            catch {
                result = false;
            }
            finally {
                if (document != null) {
                    document.Close(ref missing, ref missing, ref missing);
                    document = null;
                }
                if (applicationClass != null) {
                    applicationClass.Quit(ref missing, ref missing, ref missing);
                    applicationClass = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 把ppt文件转换成pdf文件2
        /// </summary>
        /// <param name="sourcePath">需要转换的文件路径和文件名称</param>
        /// <param name="targetPath">转换完成后的文件的路径和文件名名称</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool PPTConvertToPDF(string sourcePath, string targetPath) {
            bool result;
            PowerPoint.PpSaveAsFileType ppSaveAsFileType = PowerPoint.PpSaveAsFileType.ppSaveAsPDF;//转换成pdf
            object missing = Type.Missing;
            Microsoft.Office.Interop.PowerPoint.ApplicationClass application = null;
            PowerPoint.Presentation persentation = null;
            try {
                application = new Microsoft.Office.Interop.PowerPoint.ApplicationClass();
                persentation = application.Presentations.Open(sourcePath, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
                if (persentation != null) {
                    persentation.SaveAs(targetPath, ppSaveAsFileType, MsoTriState.msoTrue);
                }
                result = true;
            }
            catch {
                result = false;
            }
            finally {
                if (persentation != null) {
                    persentation.Close();
                    persentation = null;
                }
                if (application != null) {
                    application.Quit();
                    application = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 打开pdf文件方法
        /// </summary>
        /// <param name="p"></param>
        /// <param name="inFilePath">文件路径及文件名</param>
        public static void Priview(System.Web.UI.Page p, string inFilePath) {
            p.Response.ContentType = "Application/pdf";

            string fileName = inFilePath.Substring(inFilePath.LastIndexOf('\\') + 1);
            p.Response.AddHeader("content-disposition", "filename=" + fileName);
            p.Response.WriteFile(inFilePath);
            p.Response.End();
        }
    }
}
