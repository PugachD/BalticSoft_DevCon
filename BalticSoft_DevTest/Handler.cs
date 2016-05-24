using System;
using System.Reflection;

namespace BalticSoft_DevTest
{
    public class Handler
    {
        Order order { get; set; }

        public Handler()
        {
            order = null;
        }

        public void LoadOrder()
        {
            int ID = EnterData.IDOrder();
            order = DBWork.LoadOrder(ID);

            Show.ShowOrder(order);
        }

        public void UpdateOrder()
        {
            int ID = EnterData.IDOrder();
            order = DBWork.LoadOrder(ID);
            if (order != null)
            {
                Console.WriteLine("Новые данные заказа\n");
                order = ReturnTypeOrder(!order.IDSupplier.IsNull);

                GoMethod();
                EnterData.EnterDataOrder(order);
                //Изменяем общую сумму заказа
                order.Process();
                order.IDOrder = ID;
                DBWork.UpdateOrder(order);
            }
            Show.ShowOrder(order);
        }

        public void CreateOrder()
        {
            int who = EnterData.ChoiceCustomer();

            order = ReturnTypeOrder(who == 1);

            GoMethod();

            EnterData.EnterDataOrder(order);
            //Изменяем общую сумму заказа
            order.Process();

            DBWork.InsertOrderInDB(order);
            Show.ShowOrder(order);
        }

        private void GoMethod()
        {
            //Вызывается метод с именем Create + именем класса типа переменной для создания нового элемента этого типа заказа
            MethodInfo Method = Type.GetType("BalticSoft_DevTest.DBWork").GetMethod("Create" + order.GetType().Name);
            Method.Invoke(null, new Object[] { order });
        }

        private Order ReturnTypeOrder(bool b)
        {
            Order ord = null;
            if (b)
            {
                ord = new OrderFromBuyer();
            }
            else
            {
                ord = new OrderToTheSupplier();
            }
            return ord;
        }
    }
}
