using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalticSoft_DevTest
{
    public class Handler
    {
        DBWork dbWork { get; set; }
        Order order { get; set; }

        public Handler()
        {
            dbWork = new DBWork();
            order = null;
        }
        
        public void LoadOrder()
        {
            int ID = EnterData.IDOrder();
            order = dbWork.LoadOrder(ID);

            Show.ShowOrder(order);
        }

        public void UpdateOrder()
        {

            int ID = EnterData.IDOrder();
            dbWork.ChangeOrder(ID);

        }

        public void CreateOrder()
        {
            int who = EnterData.ChoiceCustomer();
            dbWork.CreateOrder(who);
        }
    }
}
