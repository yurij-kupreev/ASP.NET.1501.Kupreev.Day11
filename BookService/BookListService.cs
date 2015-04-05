using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BookService
{
    public class BookListService : IEnumerable<Book>
    {
        private readonly IBookRepository bookRepository;
        private readonly IXmlFormatExporter xmlExporter;
        private Logger logger;

        private List<Book> bookList = new List<Book>();

        public BookListService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
            logger = LogManager.GetCurrentClassLogger();
        }

        public BookListService(IBookRepository bookRepository, IXmlFormatExporter xmlExporter) 
            : this (bookRepository)
        {
            this.xmlExporter = xmlExporter;
        }

        public BookListService(IBookRepository bookRepository, IEnumerable<Book> books)
            : this(bookRepository, books, null)
        {     
        }

        public BookListService(IBookRepository bookRepository, IEnumerable<Book> books, IXmlFormatExporter xmlExporter)
            : this(bookRepository)
        {
            foreach (var a in books)
            {
                this.AddBook(a);
            }
            if (xmlExporter != null)
            {
                this.xmlExporter = xmlExporter;
            }
        }
        public void AddBook(Book book)
        {
            if (book == null) throw new ArgumentNullException("null-parameters in AddBook");

            foreach (Book element in bookList)
            {
                if (element.Equals(book)) throw new ArgumentException("This book already exists.");
            }

            bookList.Add(book);
        }

        public void Clear()
        {
            bookList.Clear();
        }

        public void SortBooks(IComparer<Book> compareLogic)
        {
            if (bookList == null || compareLogic == null) throw new ArgumentNullException("null-parameters in SortBooks");
            bookList.Sort(compareLogic);
        }

        public void BooksFilter(Predicate<Book> predicate, BookListService newList)
        {
            if (newList == null || predicate == null) throw new ArgumentNullException();
            newList.Clear();
            foreach (Book book in bookList)
            {
                if (predicate(book))
                    newList.AddBook(book);
            }
        }

        public List<Book> GiveBooksToParameter(String author = null, String title = null, int year = 0, String publishedBy = null)
        {
            if (author == null && title == null && year == 0 && publishedBy == null)
                throw new ArgumentNullException("null-parameters in GiveBooksToParameter");
            List<Book> newBookList = new List<Book>();
            foreach (Book element in bookList)
            {
                bool parameterEquals = true;
                if (author != null)
                    if (element.Author != author) { parameterEquals = false; continue; }
                if (title != null)
                    if (element.Title != title) { parameterEquals = false; continue; }
                if (year != 0)
                    if (element.Year != year) { parameterEquals = false; continue; }
                if (publishedBy != null)
                    if (element.PublishedBy != publishedBy) { parameterEquals = false; continue; }
                if (parameterEquals) newBookList.Add(element);
            }
            if (newBookList.Count == 0) throw new ArgumentException("There are no books with such specification in the store.");
            else return newBookList;
        }

        public List<Book> GetBookList()
        {
            return bookList;
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return bookList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void LoadBooks()
        {
            this.Clear();
            try
            {
                foreach (var element in bookRepository.LoadBooks())
                {
                    bookList.Add(element);
                }
            }
            catch (Exception e)
            {
                logger.Error("Reading file error.");
                logger.Error(e.StackTrace);
            }
        }

        public void SaveBooks()
        {
            try
            {
                bookRepository.SaveBooks(bookList);
            }
            catch (Exception e)
            {
                logger.Error("Writing file error.");
                logger.Error(e.StackTrace);
            }
        }

        public bool Export(string fileName)
        {
            if (xmlExporter != null)
            {
                xmlExporter.Export(bookList, fileName);
                return true;
            }
            return false;
        }

    }
}
