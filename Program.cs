using System.Collections;
using System.Numerics;

namespace Otus_Collection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dictionary = new OtusDictionary(32);

            try
            {
                Console.WriteLine(dictionary.Add(0, "Стул"));
                Console.WriteLine(dictionary.Add(1, "Стол"));
                Console.WriteLine(dictionary.Add(1, "Стол"));
                Console.WriteLine(dictionary.Add(2, "Шкаф"));
                Console.WriteLine(dictionary.Add(2, "Шкаф"));
                Console.WriteLine(dictionary.Add(3, "Буфет"));
                Console.WriteLine(dictionary.Add(3, null));
                Console.WriteLine(dictionary.Add(4, null));
                Console.WriteLine(dictionary.Add(4, "Сервиз"));


                Console.WriteLine("\nТестируем метод GET");
                Console.WriteLine(dictionary.Get(2));
                Console.WriteLine(dictionary.Get(3));

                Console.WriteLine("\nТестируем геттер в итераторе");
                Console.WriteLine(dictionary[1]);
                Console.WriteLine(dictionary[4]);
                Console.WriteLine(dictionary[0]);                

                Console.WriteLine("\nТестируем сеттер в итераторе");
                dictionary[4] = "кросевый сервиз";
                Console.WriteLine(dictionary[4]);

                Console.WriteLine("\nТестируем for");
                for (var i = 0; i < dictionary.currentIndex; i++)
                {
                    var item = dictionary[i];
                    Console.WriteLine($"{dictionary.KeyArray[i]}: {dictionary.ValueArray[i]}");
                }

                Console.WriteLine("\nТестируем foreach");
                foreach (var item in dictionary)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
    }
}
