using System;
using Lib;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                System.Console.WriteLine("Aranacak noktanın belirlenmesi için klavyeden 2 tane input giriniz.");
                System.Console.WriteLine("Uygulamadan çıkmak için exit yazınız..\n");

                var result = WhichPolygon.Find(ReadInput("x"), ReadInput("y"));
                System.Console.WriteLine($"\nSonuç = {result ?? "Sonuç bulunamadı."}\n");
            }
        }

        static double ReadInput(string label)
        {
            while (true)
            {
                System.Console.Write($"{label} giriniz (örn 38.77) : ");
                var str = System.Console.ReadLine();

                if (str == "exit")
                    Environment.Exit(0);

                if (double.TryParse(str, out var _result))
                    return _result;

                System.Console.Write("Geçersiz değer. ");
            }
        }
    }
}
