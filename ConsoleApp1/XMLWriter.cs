using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp1
{
    public class XMLWriter
    {

        public static void WriteXML(XMLNode node)
        {
            if (node == null)
            {
                return;
            }

            //Declare a new XMLDocument object
            XmlDocument doc = new XmlDocument();

            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            //create the root element
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement element = doc.CreateElement(string.Empty, node.Name, string.Empty);
            doc.AppendChild(element);

            if (node.ChildNodes.Count > 0)
            {
                foreach(var child in node.ChildNodes)
                {
                    MakeNodeTree(doc, element, child);
                }
            }
#pragma warning disable CS8604 // Possible null reference argument.
            doc.Save(new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../../Document.xml")).LocalPath);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        private static void MakeNodeTree (XmlDocument doc, XmlElement element, XMLNode node)
        {
            XmlElement newElement = doc.CreateElement (string.Empty, node.Name, string.Empty);
            
            if (node.ChildNodes.Count > 0)
            {
                foreach(var child in node.ChildNodes)
                {
                    MakeNodeTree(doc, newElement, child);
                }
            }

            // If node doesn't have at least one child and no value then it won't be added to the tree
            // to avoid having empty tag elements in the xml file 
            if (!((node.Value == null || node.Value == "") && node.ChildNodes.Count == 0))
            {
                newElement.AppendChild(doc.CreateTextNode(node.Value));
                element.AppendChild(newElement);
            }
        }
    }
}
