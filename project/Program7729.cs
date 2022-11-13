// See https://aka.ms/new-console-template for more information

namespace Targil0
{
   partial class Program
    {
        static void Main(string[] arg)
        {
            Welcome7729();
            Welcome0796();
            Console.ReadKey();
        }
        private static void Welcome0796() {
            Console.WriteLine("noa! kol hakavod! eich korim lachotech?");
            string sister = Console.ReadLine();
            Console.WriteLine(" noa, tari le{0} eize yofi!", sister);

        }
        private static void Welcome7729()
        {
            Console.WriteLine("enter your name");
            string userName = Console.ReadLine();
            Console.WriteLine("{0} welcome to our first console", userName);
        }
    }
}
