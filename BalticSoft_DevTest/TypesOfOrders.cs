using System;
using System.Data.SqlTypes;

namespace BalticSoft_DevTest
{
    public class OrderToTheSupplier : Order
    {
        public int INN { get; set; }
        public string PhysicalAdress { get; set; }
        public string LegalAdress { get; set; }

        public override void Process()
        {
            this.TotalAmountOrder = (SqlMoney)(((double)this.TotalAmountOrder.Value) * 1.1);
            Console.WriteLine("OrderToTheSupplier");
        }
    }

    public class OrderFromBuyer : Order
    {
        public string NameClient { get; set; }
        public string AdressClient { get; set; }

        public override void Process()
        {
            this.TotalAmountOrder = (SqlMoney)(((double)this.TotalAmountOrder.Value) * 0.95);
            Console.WriteLine("OrderFromBuyer");
        }
    }
}
