namespace Exchange {
    using System.Collections.Generic;

    public class ByLimit : IComparer<Order> {
        public int Compare(Order x, Order y) {
            if (x.id == y.id)
                return 0;
            if (x.limit > y.limit)
                return 1;
            else if (x.limit == y.limit) {
                if (x.ticks > y.ticks)
                    return 1;
                else
                    return -1;
            }
            else
                return -1;
        }
    }
}
