namespace Exchange {
    using System;

    public class Order : IComparable {
        private static uint _count = 0;
        public readonly uint id;
        public readonly Type type;
        public uint amount;
        public readonly decimal limit;
        public readonly long ticks;
        public readonly DateTime created;

        public Order(Type type, uint amount, decimal limit, long ticks) {
            id = _count++;
            this.type = type;
            this.amount = amount;
            this.limit = limit;
            this.ticks = ticks;
            this.created = DateTime.Now;
        }

        public bool Match(Order order) {
            if (type == Type.Buy)
                return limit >= order.limit;
            else
                return limit <= order.limit;
        }

        public int CompareTo(object obj) {
            if (obj == null)
                return 1;
            Order ordobj = obj as Order;
            if (ordobj.id == this.id)
                return 0;
            else
                return 1;
        }
        
        public enum Type : byte {
            Buy = 0,
            Sell = 1
        }
    }
}
