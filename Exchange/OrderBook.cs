namespace Exchange {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class OrderBook {
        private SortedSet<Order> buys;
        private SortedSet<Order> sells;
        private Stopwatch watch;
        private static int _trades = 0;
        public int trades { get { return _trades; } }

        public OrderBook() {
            buys = new SortedSet<Order>(new ByLimit());
            sells = new SortedSet<Order>(new ByLimit());
            watch = new Stopwatch();
            watch.Start();
        }

        public void SubmitOrder(Order.Type type, uint amount, decimal limit) {
            if (type == Order.Type.Buy)
                buys.Add(new Order(type, amount, limit, watch.ElapsedTicks));
            else
                sells.Add(new Order(type, amount, limit, watch.ElapsedTicks));
            while (buys.Count > 0 && sells.Count > 0 && sells.Min.Match(buys.Max))
                ExecuteTrade(buys.Max, sells.Min);
        }

        public void ExecuteTrade(Order buy, Order sell) {
            _trades++;
            if (buy.amount == sell.amount) {
                sells.Remove(sell);
                buys.Remove(buy);
            }
            else if (buy.amount > sell.amount) {
                buy.amount -= sell.amount;
                sells.Remove(sell);
            }
            else if (buy.amount < sell.amount) {
                sell.amount -= buy.amount;
                buys.Remove(buy);
            }
        }

        public void PrintOrderBooks() {
            Console.WriteLine(String.Format("|{0,5}|{1,6}|{2,6}|{3,6}|{4,20}|", "type", "id", "limit", "amt", "ticks"));
            foreach (var i in buys) {
                Console.WriteLine(String.Format("|{0,5}|{1,6}|{2,6}|{3,6}|{4,20}|", "BUY", i.id, i.limit, i.amount, i.ticks));
            }
            foreach (var i in sells) {
                Console.WriteLine(String.Format("|{0,5}|{1,6}|{2,6}|{3,6}|{4,20}|", "SELL", i.id, i.limit, i.amount, i.ticks));
            }
        }
    }
}
