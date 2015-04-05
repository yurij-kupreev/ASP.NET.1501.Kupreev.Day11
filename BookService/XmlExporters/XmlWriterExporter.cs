using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace BookService
{
    public class XmlWriterExporter : IXmlFormatExporter
    {
        public void Export(IEnumerable<Book> books, String fileName)
        {
            XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8)
            {
                Formatting = Formatting.Indented,
                Indentation = 2
            };

            writer.WriteStartDocument();

            writer.WriteStartElement("books");

            foreach (Book book in books)
            {
                writer.WriteStartElement("book");                
                writer.WriteElementString("title", book.Title);
                writer.WriteElementString("author", book.Author);
                writer.WriteElementString("year", book.Year.ToString());
                writer.WriteElementString("publisher", book.PublishedBy);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteEndDocument();

            writer.Flush();
        }
    }
}
