using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalticSoft_DevTest
{
    public class Show
    {
        /// <summary>
        /// Вывести информацию о заказе
        /// </summary>
        static public void ShowOrder(Order order)
        {
            if (order == null)
                Console.WriteLine("\nЗаказа с таким идентификатором нет!");
            else
            {
                Console.WriteLine(String.Format("\nID заказа: " + order.IDOrder + "\nНомером документа: " + order.DocNumber +
                    "\nОбщая сумма заказа: " + order.TotalAmountOrder + "\nID клиента: " + order.IDClient +
                    "\nСтатус заказа: " + order.Status + "\nID поставщика: " + order.IDSupplier));
            }
        }

        static public void ShowException(Exception exc)
        {
            Console.WriteLine("Ошибка: "+exc.Message);
        }
        
        static public string ShowInfo()
        {
            return String.Format("Нажмите:\n"
                + "1) Создание нового заказа и запись в БД\n"
                + "2) Изменение заказа\n"
                + "3) Извлечение заказа из БД по ID заказа");
        }
    }
}
