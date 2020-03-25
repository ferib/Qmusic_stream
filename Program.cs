using System;

namespace Qmusic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Qmusic");
            Qmusic q = new Qmusic();
            //string url = q.GetStreamURL();
            string url = "https://edtrfyguhijokpl/rtfyguhiokp";
            q.ReadMusicStream(url);
            Console.ReadKey();
        }
    }
}
