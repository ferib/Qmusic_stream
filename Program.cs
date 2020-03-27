using System;
using System.Threading;

namespace Qmusic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Qmusic");
            Qmusic q = new Qmusic();

            while(true)
            {
                //Sleep XX minutes until exactly x'O'Clock
                Console.WriteLine($"Sleeping for {(((59 - DateTime.UtcNow.Minute) * 60) + 50 - DateTime.UtcNow.Second) * 1000}ms");
                Thread.Sleep((((59 - DateTime.UtcNow.Minute) * 60) + 50 - DateTime.UtcNow.Second) * 1000); //10sec might be ads
                //string url = "https://edtrfyguhijokpl/rtfyguhiokp";
                Console.WriteLine($"[{DateTime.UtcNow.ToString("dd/MM/yyyy_HH:mm")}]: Saving to disk");
                q.SaveMusicStream($"qmusic_{DateTime.UtcNow.ToString("dd/MM/yyyy_HH:mm")}.mp4", 5 * 60);
                Console.WriteLine($"[{DateTime.UtcNow.ToString("dd/MM/yyyy_HH:mm")}]: Saving complete");


                Thread.Sleep(5 * 60 * 1000);
            }
            
            
            Console.ReadKey();
        }
    }
}
