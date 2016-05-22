using System.Data.SqlTypes;

namespace BalticSoft_DevTest
{
    public abstract class Order: IProcessable
    {
        public int IDOrder { get; set; }
        public int DocNumber { get; set; }
        public SqlMoney TotalAmountOrder { get; set; }
        public SqlInt32 IDClient { get; set; }
        public SqlInt32 IDSupplier { get; set; }
        public string Status { get; set; }

        public abstract void  Process();
    }
}
