#region

using System;
using System.IO;
using System.Xml;

#endregion

namespace KANG.Common {
    public class XmlHelper {
        #region XML文档节点查询和读取

        public static XmlNode GetXmlDoc(string xmlFileName) {
            var xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(xmlFileName); //加载XML文档
            }
            catch (Exception ex) {
                return null;
                //throw ex; //这里可以定义你自己的异常处理
            }
            return xmlDoc;
        }

        public static XmlDocument GetXmlDoc(Stream xmlStream) {
            var xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(xmlStream); //加载XML文档
            }
            catch (Exception ex) {
                return null;
                //throw ex; //这里可以定义你自己的异常处理
            }
            return xmlDoc;
        }

        /// <summary>
        ///     选择匹配XPath表达式的第一个节点XmlNode.
        /// </summary>
        /// <param name="xmlDoc">XML文档</param>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <returns>返回XmlNode</returns>
        public static XmlNode GetXmlNodeByXpath(XmlDocument xmlDoc, string xpath) {
            try {
                var xmlNode = xmlDoc.SelectSingleNode(xpath);
                return xmlNode;
            }
            catch (Exception ex) {
                return null;
                //throw ex; //这里可以定义你自己的异常处理
            }
        }

        public static string GetXmlNodeTextByXpath(XmlDocument xmlDoc, string xpath) {
            var ret = "";
            try {
                var xmlNode = xmlDoc.SelectSingleNode(xpath);
                if (xmlNode != null) ret = xmlNode.InnerText;
            }
            catch (Exception ex) {
                return null;
                //throw ex; //这里可以定义你自己的异常处理
            }
            return ret;
        }

        /// <summary>
        ///     选择匹配XPath表达式的节点列表XmlNodeList.
        /// </summary>
        /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <returns>返回XmlNodeList</returns>
        public static XmlNodeList GetXmlNodeListByXpath(string xmlFileName, string xpath) {
            var xmlDoc = new XmlDocument();

            try {
                xmlDoc.Load(xmlFileName); //加载XML文档
                var xmlNodeList = xmlDoc.SelectNodes(xpath);
                return xmlNodeList;
            }
            catch (Exception ex) {
                return null;
                //throw ex; //这里可以定义你自己的异常处理
            }
        }

        /// <summary>
        ///     选择匹配XPath表达式的第一个节点的匹配xmlAttributeName的属性XmlAttribute.
        /// </summary>
        /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
        /// <param name="xmlAttributeName">要匹配xmlAttributeName的属性名称</param>
        /// <returns>返回xmlAttributeName</returns>
        public static XmlAttribute GetXmlAttribute(string xmlFileName, string xpath, string xmlAttributeName) {
            var content = string.Empty;
            var xmlDoc = new XmlDocument();
            XmlAttribute xmlAttribute = null;
            try {
                xmlDoc.Load(xmlFileName); //加载XML文档
                var xmlNode = xmlDoc.SelectSingleNode(xpath);
                if (xmlNode != null) {
                    if (xmlNode.Attributes.Count > 0) {
                        xmlAttribute = xmlNode.Attributes[xmlAttributeName];
                    }
                }
            }
            catch (Exception ex) {
                throw ex; //这里可以定义你自己的异常处理
            }
            return xmlAttribute;
        }

        #endregion

        #region XML文档创建和节点或属性的添加、修改

        /// <summary>
        ///     创建一个XML文档
        /// </summary>
        /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        /// <param name="rootNodeName">XML文档根节点名称(须指定一个根节点名称)</param>
        /// <param name="version">XML文档版本号(必须为:"1.0")</param>
        /// <param name="encoding">XML文档编码方式</param>
        /// <param name="standalone">该值必须是"yes"或"no",如果为null,Save方法不在XML声明上写出独立属性</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CreateXmlDocument(string xmlFileName, string rootNodeName, string version, string encoding,
            string standalone) {
            var isSuccess = false;
            try {
                var xmlDoc = new XmlDocument();
                var xmlDeclaration = xmlDoc.CreateXmlDeclaration(version, encoding, standalone);
                XmlNode root = xmlDoc.CreateElement(rootNodeName);
                xmlDoc.AppendChild(xmlDeclaration);
                xmlDoc.AppendChild(root);
                xmlDoc.Save(xmlFileName);
                isSuccess = true;
            }
            catch (Exception ex) {
                throw ex; //这里可以定义你自己的异常处理
            }
            return isSuccess;
        }

        /// <summary>
        ///     依据匹配XPath表达式的第一个节点来创建它的子节点(如果此节点已存在则追加一个新的同名节点
        /// </summary>
        /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
        /// <param name="xmlNodeName">要匹配xmlNodeName的节点名称</param>
        /// <param name="innerText">节点文本值</param>
        /// <param name="xmlAttributeName">要匹配xmlAttributeName的属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CreateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText,
            string xmlAttributeName, string value) {
            var isSuccess = false;
            var xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(xmlFileName); //加载XML文档
                var xmlNode = xmlDoc.SelectSingleNode(xpath);
                if (xmlNode != null) {
                    //存不存在此节点都创建
                    var subElement = xmlDoc.CreateElement(xmlNodeName);
                    subElement.InnerXml = innerText;

                    //如果属性和值参数都不为空则在此新节点上新增属性
                    if (!string.IsNullOrEmpty(xmlAttributeName) && !string.IsNullOrEmpty(value)) {
                        var xmlAttribute = xmlDoc.CreateAttribute(xmlAttributeName);
                        xmlAttribute.Value = value;
                        subElement.Attributes.Append(xmlAttribute);
                    }

                    xmlNode.AppendChild(subElement);
                }
                xmlDoc.Save(xmlFileName); //保存到XML文档
                isSuccess = true;
            }
            catch (Exception ex) {
                throw ex; //这里可以定义你自己的异常处理
            }
            return isSuccess;
        }

        /// <summary>
        ///     依据匹配XPath表达式的第一个节点来创建或更新它的子节点(如果节点存在则更新,不存在则创建)
        /// </summary>
        /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
        /// <param name="xmlNodeName">要匹配xmlNodeName的节点名称</param>
        /// <param name="innerText">节点文本值</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CreateOrUpdateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName,
            string innerText) {
            var isSuccess = false;
            var isExistsNode = false; //标识节点是否存在
            var xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(xmlFileName); //加载XML文档
                var xmlNode = xmlDoc.SelectSingleNode(xpath);
                if (xmlNode != null) {
                    //遍历xpath节点下的所有子节点
                    foreach (XmlNode node in xmlNode.ChildNodes) {
                        if (node.Name.ToLower() == xmlNodeName.ToLower()) {
                            //存在此节点则更新
                            node.InnerXml = innerText;
                            isExistsNode = true;
                            break;
                        }
                    }
                    if (!isExistsNode) {
                        //不存在此节点则创建
                        var subElement = xmlDoc.CreateElement(xmlNodeName);
                        subElement.InnerXml = innerText;
                        xmlNode.AppendChild(subElement);
                    }
                }
                xmlDoc.Save(xmlFileName); //保存到XML文档
                isSuccess = true;
            }
            catch (Exception ex) {
                throw ex; //这里可以定义你自己的异常处理
            }
            return isSuccess;
        }


        /// <summary>
        ///     依据匹配XPath表达式的第一个节点来创建或更新它的属性(如果属性存在则更新,不存在则创建)
        /// </summary>
        /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
        /// <param name="xmlAttributeName">要匹配xmlAttributeName的属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CreateOrUpdateXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName,
            string value) {
            var isSuccess = false;
            var isExistsAttribute = false; //标识属性是否存在
            var xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(xmlFileName); //加载XML文档
                var xmlNode = xmlDoc.SelectSingleNode(xpath);
                if (xmlNode != null) {
                    //遍历xpath节点中的所有属性
                    foreach (XmlAttribute attribute in xmlNode.Attributes) {
                        if (attribute.Name.ToLower() == xmlAttributeName.ToLower()) {
                            //节点中存在此属性则更新
                            attribute.Value = value;
                            isExistsAttribute = true;
                            break;
                        }
                    }
                    if (!isExistsAttribute) {
                        //节点中不存在此属性则创建
                        var xmlAttribute = xmlDoc.CreateAttribute(xmlAttributeName);
                        xmlAttribute.Value = value;
                        xmlNode.Attributes.Append(xmlAttribute);
                    }
                }
                xmlDoc.Save(xmlFileName); //保存到XML文档
                isSuccess = true;
            }
            catch (Exception ex) {
                throw ex; //这里可以定义你自己的异常处理
            }
            return isSuccess;
        }

        #endregion

        #region XML文档节点或属性的删除

        /// <summary>
        ///     删除匹配XPath表达式的第一个节点(节点中的子元素同时会被删除)
        /// </summary>
        /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DeleteXmlNodeByXPath(string xmlFileName, string xpath) {
            var isSuccess = false;
            var xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(xmlFileName); //加载XML文档
                var xmlNode = xmlDoc.SelectSingleNode(xpath);
                if (xmlNode != null) {
                    //删除节点
                    xmlNode.ParentNode.RemoveChild(xmlNode);
                }
                xmlDoc.Save(xmlFileName); //保存到XML文档
                isSuccess = true;
            }
            catch (Exception ex) {
                throw ex; //这里可以定义你自己的异常处理
            }
            return isSuccess;
        }

        /// <summary>
        ///     删除匹配XPath表达式的第一个节点中的匹配参数xmlAttributeName的属性
        /// </summary>
        /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
        /// <param name="xmlAttributeName">要删除的xmlAttributeName的属性名称</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DeleteXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName) {
            var isSuccess = false;
            var isExistsAttribute = false;
            var xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(xmlFileName); //加载XML文档
                var xmlNode = xmlDoc.SelectSingleNode(xpath);
                XmlAttribute xmlAttribute = null;
                if (xmlNode != null) {
                    //遍历xpath节点中的所有属性
                    foreach (XmlAttribute attribute in xmlNode.Attributes) {
                        if (attribute.Name.ToLower() == xmlAttributeName.ToLower()) {
                            //节点中存在此属性
                            xmlAttribute = attribute;
                            isExistsAttribute = true;
                            break;
                        }
                    }
                    if (isExistsAttribute) {
                        //删除节点中的属性
                        xmlNode.Attributes.Remove(xmlAttribute);
                    }
                }
                xmlDoc.Save(xmlFileName); //保存到XML文档
                isSuccess = true;
            }
            catch (Exception ex) {
                throw ex; //这里可以定义你自己的异常处理
            }
            return isSuccess;
        }

        /// <summary>
        ///     删除匹配XPath表达式的第一个节点中的所有属性
        /// </summary>
        /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DeleteAllXmlAttributeByXPath(string xmlFileName, string xpath) {
            var isSuccess = false;
            var xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(xmlFileName); //加载XML文档
                var xmlNode = xmlDoc.SelectSingleNode(xpath);
                if (xmlNode != null) {
                    //遍历xpath节点中的所有属性
                    xmlNode.Attributes.RemoveAll();
                }
                xmlDoc.Save(xmlFileName); //保存到XML文档
                isSuccess = true;
            }
            catch (Exception ex) {
                throw ex; //这里可以定义你自己的异常处理
            }
            return isSuccess;
        }

        #endregion
    }
}