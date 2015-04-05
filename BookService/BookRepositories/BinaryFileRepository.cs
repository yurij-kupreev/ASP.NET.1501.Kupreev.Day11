using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BookService
{
    public class BinaryFileRepository : IBookRepository
    {
        public static string FileName { get; private set; }
        public BinaryFileRepository(String fileName)
        {
            FileName = fileName;
        }


        public void SaveBooks(IEnumerable<Book> bookList)
        {
            if (bookList == null) throw new ArgumentNullException();
            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
                bw = new BinaryWriter(fs);
                foreach (Book element in bookList)
                {
                    bw.Write(element.Author);
                    bw.Write(element.Title);
                    bw.Write(element.Year);
                    bw.Write(element.PublishedBy);
                }
            }
            catch { throw; }
            finally
            {
                if (fs != null) { fs.Close(); }
                if (bw != null) { bw.Close(); }
            }
        }

        public IEnumerable<Book> LoadBooks()
        {
            List<Book> listBooks = new List<Book>();
            FileStream fs = null;
            BinaryReader br = null;
            try
            {
                fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read);
                br = new BinaryReader(fs);
                while (fs.Position < fs.Length)
                {
                    listBooks.Add(new Book(br.ReadString(), br.ReadString(), br.ReadInt32(), br.ReadString()));
                }
            }
            catch { throw; }
            finally
            {
                if (fs != null) { fs.Close(); }
                if (br != null) { br.Close(); }
            }
            return listBooks;
        }
    }
}
