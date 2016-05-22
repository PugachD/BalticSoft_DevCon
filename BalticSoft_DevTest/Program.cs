using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalticSoft_DevTest
{
    class Program
    {
        private static Random random;
        static void Main(string[] args)
        {
            int i;
            char symbol = 'N';
            random = new Random();
            Handler handler = new Handler();
            do
            {
                Console.WriteLine(Show.ShowInfo());
                try
                {
                    i = int.Parse(Console.ReadLine());
                    switch (i)
                    {
                        case 1:
                            handler.CreateOrder();
                            break;
                        case 2:
                            handler.UpdateOrder();
                            break;
                        case 3:
                            handler.LoadOrder();
                            break;
                        default:
                            i = random.Next(1, 3);
                            Console.WriteLine("Такого пункта нет. Тогда выберу я. Пункт номер {0}", i);
                            break;
                    }
                }
                catch (Exception exc)
                {
                    Show.ShowException(exc);
                }
                Console.WriteLine("\nЖелаете повторить выполнение программы?\nY - да. Любая другая клавиша - нет.");
                symbol = Console.ReadKey(true).KeyChar;
                Console.Clear();
            } while (symbol == 'Y');
        }
    }
}
