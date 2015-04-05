using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService
{
    public class LinqToXmlExporter : IXmlFormatExporter
    {
        public void Export(IEnumerable<Book> books, String fileName)
        {
            XElement booksNode = new XElement("books");
            foreach(Book book in books)
            {
                XElement bookNode = new XElement("book");
                bookNode.Add(new XElement("title", book.Title));
                bookNode.Add(new XElement("author", book.Author));
                bookNode.Add(new XElement("year", book.Year.ToString()));
                bookNode.Add(new XElement("publisher", book.PublishedBy));
                booksNode.Add(bookNode);
            }
            XDocument xDoc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), booksNode);
            xDoc.Save(fileName);
        }
    }
}
