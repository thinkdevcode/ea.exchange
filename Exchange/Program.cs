namespace Exchange {
    using System;

    class Program {
        static void Main(string[] args) {
            OrderBook book = new OrderBook();
            book.SubmitOrder(Order.Type.Buy, 10, 11.12m);
            book.SubmitOrder(Order.Type.Buy, 10, 11.00m);
            book.SubmitOrder(Order.Type.Buy, 20, 10.50m);
            book.SubmitOrder(Order.Type.Buy, 1, 11.20m);
            book.SubmitOrder(Order.Type.Buy, 7, 11.15m);
            book.SubmitOrder(Order.Type.Buy, 100, 10.05m);
            book.SubmitOrder(Order.Type.Buy, 2, 11.10m);
            book.SubmitOrder(Order.Type.Sell, 10, 11.24m);
            book.SubmitOrder(Order.Type.Sell, 10, 11.30m);
            book.SubmitOrder(Order.Type.Sell, 20, 11.50m);
            book.SubmitOrder(Order.Type.Sell, 1, 11.74m);
            book.SubmitOrder(Order.Type.Sell, 7, 11.60m);
            book.SubmitOrder(Order.Type.Sell, 100, 12.05m);
            book.SubmitOrder(Order.Type.Sell, 3, 11.10m);

            bool exit = false;
            Random rnd = new Random();
            DateTime now = DateTime.Now.AddSeconds(10);

            while (!exit) {
                int rndl = rnd.Next(1120, 1125);
                int rnda = rnd.Next(1, 5);
                byte rndt = rnd.Next(0, 100) < 50 ? (byte)0 : (byte)1;

                Order.Type t = (Order.Type)rndt;
                uint a = (uint)rnda;
                decimal l = (decimal)rndl / 100m;

                book.SubmitOrder(t, a, l);

                //Thread.Sleep(1);
                //Console.Clear();
                //book.PrintOrderBooks();

                if (DateTime.Now == now)
                    exit = true;
            }
            Console.WriteLine(book.trades);
            Console.ReadLine();
        }
    }
}
