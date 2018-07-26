using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace test_cast
{
    class Program
    {
        public class ConsumptionWithDateLite
        {
            public DateTime Time { get; set; }
            public DayOfWeek? DayOfWeek { get; set; }
            public DayOfWeek DayOfWeek2 { get; set; }
        }

        static void Main(string[] args)
        {
            try
            {
                var a1 = Enumerable.Repeat(0, 100)
                    .Select((d, i) => new DateTime(2000, 1, 1).AddDays(i))
                    .AsQueryable()
                    .Select<ConsumptionWithDateLite>("new (it as Time, DayOfWeek as DayOfWeek, DayOfWeek as DayOfWeek2)");

                var z1 = a1.ToList();
                if (z1.Any(zx => zx.DayOfWeek == null))
                {
                    //You can set an bp here to view the data of z1 in netcoreapp
                    throw new InvalidOperationException("Bad data");
                }

                Console.WriteLine("Success");
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}
