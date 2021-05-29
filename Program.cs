using System;

namespace Finder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the text to searchf");
            string text = Console.ReadLine();

            Console.WriteLine("Enter the directory name");
            string directory = Console.ReadLine();

            Console.WriteLine("Enter the file name");
            string fileName = Console.ReadLine();

            ISearch obj = new TextSearch(text, fileName, directory);
            obj.SearchAsync(text);

            Console.Read();
        }
    }
}
