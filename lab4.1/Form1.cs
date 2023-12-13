using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

namespace lab4._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string xmlfile = @"d:\DvdList.xml";

            XmlTextWriter writer = new XmlTextWriter(xmlfile, null);

            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;
            writer.WriteStartDocument();
            writer.WriteComment("Created @ " + DateTime.Now.ToString());
            // Корневой элемент <DVDList>
            writer.WriteStartElement("DvdList");
            // Новый элемент <DVD>
            writer.WriteStartElement("DVD");
            // Добавление атрибутов
            writer.WriteAttributeString("ID", "1");
            writer.WriteAttributeString("Category", "Science Fiction");
            // Добавление информации о DVD-диске внутрь элемента
            writer.WriteElementString("Title", "The Matrix");
            writer.WriteElementString("Director", "Larry Wachowski");
            writer.WriteElementString("Price", "18,74");
            // Добавление списка актеров
            writer.WriteStartElement("Starring");
            writer.WriteElementString("Star", "Keanu Reeves");
            writer.WriteElementString("Star", "Laurence Fishburne");
            // Закрыть элемент <Starring>
            writer.WriteEndElement();
            // Закрыть элемент <DVD>.
            writer.WriteEndElement();
            // Новый элемент <DVD>
            writer.WriteStartElement("DVD");
            // Добавлени еатрибутов
            writer.WriteAttributeString("ID", "2");
            writer.WriteAttributeString("Category", "Drama");
            // Добавление информации о DVD-диске внутрь элемента
            writer.WriteElementString("Title", "Forrest Gump");
            writer.WriteElementString("Director", "Robert Zemeckis");
            writer.WriteElementString("Price", "23,99");
            // Добавление списка актеров
            writer.WriteStartElement("Starring");
            writer.WriteElementString("Star", "Tom Hanks");
            writer.WriteElementString("Star", "Robin Wright");
            // Закрыть элемент <Starring>
            writer.WriteEndElement();
            // Закрыть элемент <DVD>
            writer.WriteEndElement();
            // Закрыть элемент <DVDList>
            writer.WriteEndElement();
            writer.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string xmlfile = @"d:\DvdList.xml";

            XmlTextReader reader = new XmlTextReader(xmlfile);

            listBox1.Items.Clear();

            reader.ReadStartElement("DvdList");

            while (reader.Read())
            {
                if((reader.Name == "DVD") && (reader.NodeType == XmlNodeType.Element)) 
                {
                    reader.ReadStartElement("DVD");
                    listBox1.Items.Add(reader.ReadElementString("Title"));
                    listBox1.Items.Add(reader.ReadElementString("Director"));
                    listBox1.Items.Add(String.Format("{0:C}", Decimal.Parse(reader.ReadElementString("Price"))));
                    listBox1.Items.Add("");
                }
            }
            reader.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string xmlFile = @"d:\DvdList.xml";
            // Загрузить XML-файлв XmlDocument
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);
            // Получить текст описания XmlDocument
            listBox1.Items.Clear();
            GetChildNodesDescr(doc.ChildNodes, 0);
        }
        // Рекурсивныйметод посещения узлов дерева XML с целью их идентификации
        private void GetChildNodesDescr(XmlNodeList nodeList, int level)
        {
            string indent = "";
            foreach (XmlNode node in nodeList)
            {
                switch (node.NodeType)
                { 
                case XmlNodeType.XmlDeclaration:
                    listBox1.Items.Add("XML декларация: ");
                    listBox1.Items.Add(node.Name);
                    listBox1.Items.Add(node.Value);
                    break;

                case XmlNodeType.Element:
                    listBox1.Items.Add(indent);
                    listBox1.Items.Add("Элемент: " + node.Name);
                    break;

                case XmlNodeType.Text:
                    listBox1.Items.Add(" - Значение: ");
                    listBox1.Items.Add(node.Value);
                    break;

                case XmlNodeType.Comment:
                    listBox1.Items.Add(indent);
                    listBox1.Items.Add("Комментарии: ");
                    listBox1.Items.Add(node.Value);
                    break;
                }
                if (node.Attributes != null)
                {
                    foreach (XmlAttribute attrib in node.Attributes)
                    {
                        listBox1.Items.Add(" - Атрибут: " + attrib.Name);
                        listBox1.Items.Add("Значение: ");
                        listBox1.Items.Add(attrib.Value);
                    }
                }
                if (node.HasChildNodes)
                    GetChildNodesDescr(node.ChildNodes, level + 1);
                
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            XDocument doc = new XDocument(
new XDeclaration("1.0", "utf-8", "yes"),
new XComment("Created: " + DateTime.Now.ToString()),
new XElement("DvdList",
new XElement("DVD",
new XAttribute("ID", "1"),
new XAttribute("Category", "Science Fiction"),
new XElement("Title", "The Matrix"),
new XElement("Country", "USA"),
new XElement("Director", "Larry Wachowski"),
new XElement("Price", "18.74"),
new XElement("Starring",
new XElement("Star", "Keanu Reeves"),
new XElement("Star", "Laurence Fishburne")
          )),
new XElement("DVD",
new XAttribute("ID", "2"),
new XAttribute("Category", "Drama"),
new XElement("Title", "Forrest Gump"),
new XElement("Country", "USA"),
new XElement("Director", "Robert Zemeckis"),
new XElement("Price", "23.99"),
new XElement("Starring",
new XElement("Star", "Tom Hanks"),
new XElement("Star", "Robin Wright")
          )),
new XElement("DVD",
new XAttribute("ID", "3"),
new XAttribute("Category", "Horror"),
new XElement("Title", "The Others"),
new XElement("Country", "USA"),
new XElement("Director", "Alejandro Amenбbar"),
new XElement("Price", "22.49"),
new XElement("Starring",
new XElement("Star", "Nicole Kidman"),
new XElement("Star", "Christopher Eccleston")
)),
new XElement("DVD",
new XAttribute("ID", "4"),
new XAttribute("Category", "Mystery"),
new XElement("Title", "Mulholland Drive"),
new XElement("Country", "USA"),
new XElement("Director", "David Lynch"),
new XElement("Price", "25.74"),
new XElement("Starring",
new XElement("Star", "Laura Harring")
          )),
new XElement("DVD",
new XAttribute("ID", "5"),
new XAttribute("Category", "Science Fiction"),
new XElement("Title", "A.I. Artificial Intelligence"),
new XElement("Country", "USA"),
new XElement("Director", "Steven Spielberg"),
new XElement("Price", "23.99"),
new XElement("Starring",
new XElement("Star", "Haley Joel Osment"),
new XElement("Star", "Jude Law")
          )))
    );
            doc.Save(@"d:\DvdList_X.xml");
            webBrowser1.Navigate(@"d:\DvdList_X.xml");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            XDocument myDoc = XDocument.Load(@"d:\DvdList_X.xml");
            // Удалить элемент Country  из дерева
            myDoc.Descendants("Country").Remove();
            myDoc.Save(@"d:\DvdList_XX.xml");
            webBrowser1.Navigate(@"d:\DvdList_XX.xml");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string s;
            double x;
            decimal y;
            string xmlFile = @"d:\DvdList_X.xml";
            // Создать  читателя
            XDocument doc = XDocument.Load(xmlFile);
            listBox1.Items.Clear();
            foreach (XElement element in doc.Element("DvdList").Elements())
            {
                listBox1.Items.Add((string)element.Element("Title"));
                listBox1.Items.Add((string)element.Element("Director"));
                listBox1.Items.Add(String.Format("{0:C}", (decimal)element.Element("Price")));
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            XNamespace ns = "http://www.somecompany.com/DVDList";
            XDocument doc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XComment("Created: " + DateTime.Now.ToString()),
            new XElement(ns + "DvdList",
            new XElement(ns + "DVD", new XAttribute("ID", "1"),
            new XAttribute("Category", "Science Fiction"),
            new XElement(ns + "Title", "The Matrix"),
            new XElement(ns + "Director", "Larry Wachowski"),
            new XElement(ns + "Price", "18.74"),
            new XElement(ns + "Starring",
            new XElement(ns + "Star", "Keanu Reeves"),
            new XElement(ns + "Star", "Laurence Fishburne")
                      ))));
            doc.Save(@"d:\DvdList_X1.xml"); webBrowser1.Navigate(@"d:\DvdList_X1.xml");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Загрузить XML-файл
            string xmlFile = @"d:\DvdList.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);
            // Найти все элементы <Title> по всему документу
            listBox1.Items.Clear();
            XmlNodeList nodes = doc.GetElementsByTagName("Title");
            foreach (XmlNode node in nodes)
            {
                listBox1.Items.Add("Результат поиска : ");
                // Показать текст, содержащийся в элементе <Title>
                listBox1.Items.Add(node.ChildNodes[0].Value);
                listBox1.Items.Add("");
            }

        }
    }
}
