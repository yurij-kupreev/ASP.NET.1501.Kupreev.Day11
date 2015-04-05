using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BookService
{
    public class BinarySerializerRepository : IBookRepository
    {
        public static string FileName { get; private set; }
        public BinarySerializerRepository(String fileName)
        {
            FileName = fileName;
        }


        public void SaveBooks(IEnumerable<Book> bookList)
        {
            if (bookList == null) throw new ArgumentNullException();

            long position = 0;
            FileStream fs = null;
            try
            {
                using (fs = new FileStream(FileName, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    foreach (var element in bookList)
                    {
                        bf.Serialize(fs, element);
                    }
                }
            }
            catch (Exception)
            {
                if (fs != null)
                    fs.Position = position;
                throw;
            }
        }

        public IEnumerable<Book> LoadBooks()
        {
            List<Book> bookList = new List<Book>();
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    while (fs.Position < fs.Length)
                    {
                        bookList.Add((Book)bf.Deserialize(fs));
                    }
                    return bookList;
                }
            }
            catch (Exception e)
            {
                throw new IOException("Error while loading books from file.", e);
            }
        }
    }
}
