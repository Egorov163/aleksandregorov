using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stocks = new List<Stocks>();
            stocks.Add(new Stocks
            {
                Price = 1000, Name = "Sber" 
            });
            stocks.Add(new Stocks
            {
                Price = 1000,
                Name = "Lukoil"
            });
            stocks.Add(new Stocks
            {
                Price = 1000,
                Name = "MTS"
            });

            //Portfolio portfolio = new Portfolio();
            //Console.WriteLine(portfolio.PortfoliValue(stocks));

            //total = myList.Sum(item => item.Amount);
            int total = stocks.Sum(i=>i.Price);
            Console.WriteLine(total);
        }
        public class Portfolio
        {
            public int PortfoliValue(List<Stocks> stocks)
            {
                int value = 0;
                foreach (var stock in stocks)
                {
                    value =+ stock.Price;
                }
                return value;
            }
        }
    }

    public class Stocks
    {
        public int Price { get; set; }
        public string Name { get; set; }
    }
}
