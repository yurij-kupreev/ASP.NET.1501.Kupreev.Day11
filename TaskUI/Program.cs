using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookService;

namespace TaskUI
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Book a = new Book("Tolstoy", "War and Peace", 1865, "St.Petersburg");
            Book b = new Book("Lermontov", "Hero of Our Time", 1840, "St.Petersburg");
            Book c = new Book("Lermontov", "Daemon", 1842, "Moscow");
            Book d = new Book("Tolstoy", "War and Peace", 1865, "St.Petersburg");

            BinaryFileRepository bookRepository = new BinaryFileRepository("fileBooks");
            BookListService bookService = new BookListService(bookRepository);

            bookService.AddBook(a);
            bookService.AddBook(b);
            bookService.AddBook(c);
            try
            {
                bookService.AddBook(d);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            bookService.SaveBooks();
            bookService.LoadBooks();

            foreach (Book element in bookService)
            {
                Console.WriteLine(element.ToString());
            }

            Console.WriteLine("\nSorted with YearIncrease mode:");
            bookService.SortBooks(new YearIncrease());
            foreach (Book element in bookService)
            {
                Console.WriteLine(element.ToString());
            }

            Console.WriteLine();
            List<Book> bookList = bookService.GiveBooksToParameter(publishedBy: "St.Petersburg");
            foreach (Book element in bookList)
            {
                Console.WriteLine(element.ToString());
            }

            BinarySerializerRepository repository = new BinarySerializerRepository("fileBooksSerialize");
            BookListService bookService2 = new BookListService(repository, bookService.GetBookList(), new LinqToXmlExporter());
            bookService.SaveBooks();
            bookService.LoadBooks();
            Console.WriteLine("\nAfter serialization:");
            foreach (Book element in bookService)
            {
                Console.WriteLine(element.ToString());
            }
            Console.WriteLine(bookService2.Export(@"xmlBooks.xml"));

            bookService2 = new BookListService(repository, bookService.GetBookList(), new XmlWriterExporter());
            bookService2.Export(@"xmlBooks2.xml");
            
        }
    }
}
