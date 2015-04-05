using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService
{
    public class AuthorsIncrease: IComparer<Book>
    {
        public int Compare(Book left, Book right)
        {
            return left.Author.CompareTo(right.Author);
        }
    }

    public class AuthorsDecrease : IComparer<Book>
    {
        public int Compare(Book left, Book right)
        {
            return right.Author.CompareTo(left.Author);
        }
    }

    public class TitleIncrease : IComparer<Book>
    {
        public int Compare(Book left, Book right)
        {
            return left.Title.CompareTo(right.Title);
        }
    }

    public class TitleDecrease : IComparer<Book>
    {
        public int Compare(Book left, Book right)
        {
            return right.Title.CompareTo(left.Title);
        }
    }

    public class YearIncrease : IComparer<Book>
    {
        public int Compare(Book left, Book right)
        {
            return left.Year.CompareTo(right.Year);
        }
    }

    public class YearDecrease : IComparer<Book>
    {
        public int Compare(Book left, Book right)
        {
            return right.Year.CompareTo(left.Year);
        }
    }

    public class PublishedByIncrease : IComparer<Book>
    {
        public int Compare(Book left, Book right)
        {
            return left.PublishedBy.CompareTo(right.PublishedBy);
        }
    }

    public class PublishedByDecrease : IComparer<Book>
    {
        public int Compare(Book left, Book right)
        {
            return right.PublishedBy.CompareTo(left.PublishedBy);
        }
    }
}
