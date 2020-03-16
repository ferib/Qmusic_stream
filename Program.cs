using System;

namespace Qmusic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Qmusic");
            Qmusic q = new Qmusic();
            Console.WriteLine(q.GetStreamURL());
            Console.ReadKey();
        }
    }
}
