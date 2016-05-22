using System;
using System.Data.SqlTypes;

namespace BalticSoft_DevTest
{
    public class EnterData
    {
        static public void EnterDataOrder(ref Order order)
        {
            Console.WriteLine("Введите данные: \nID Документа ");
            order.DocNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Общая сумма заказа");

            order.TotalAmountOrder = (SqlMoney.Parse(Console.ReadLine()));
            Console.WriteLine("Статус");
            order.Status = Console.ReadLine();

        }

        static public Order EnterDataSupplier(OrderToTheSupplier order)
        {
            Console.WriteLine("Введите ИНН поставщика.");
            order.INN = int.Parse(Console.ReadLine());

            Console.WriteLine("Физический адрес");
            order.PhysicalAdress = Console.ReadLine();

            Console.WriteLine("Юридический адрес");
            order.LegalAdress = Console.ReadLine();

            return order;
        }

        static public Order EnterDataClient(OrderFromBuyer order)
        {
            Console.WriteLine("Имя Клиента");
            order.NameClient = Console.ReadLine();

            Console.WriteLine("Адрес Клиента");
            order.AdressClient = Console.ReadLine();

            return order;
        }

        static public int IDOrder()
        {
            int ID;
            try
            {
                Console.WriteLine("Введите ID заказа");
                ID = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                throw new FormatException("Неверно введены данные");
            }
            return ID;
        }

        static public int ChoiceCustomer()
        {
            int who = 0;
            Console.WriteLine("1. Создние заказа в таблице. \n 1 - Оформить заказ от покупателя \n 2 - Оформить заказ поставщику");
            try
            {
                who = (int.Parse(Console.ReadLine())) == 1 ? 1 : 2;
            }
            catch (Exception)
            {
                Show.ShowException(new Exception("Неверный формат ввода. Выберем сами: Оформить заказ поставщику"));
                who = 1;
            }
            return who;
        }

    }
}
