using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace test_cast
{
    class Program
    {
        public class Example1
        {
            public DateTime Time { get; set; }
            public DayOfWeek? DOWNull { get; set; }
            public DayOfWeek DOW { get; set; }
            public int Sec { get; set; }
            public int? SecNull { get; set; }
        }

        public class Example2
        {
            public DateTime Time { get; set; }
            public DayOfWeek? DOWNull { get; set; }
            public DayOfWeek DOW { get; set; }
            public int Sec { get; set; }
            public int? SecNull { get; set; }

            public Example2(DateTime t, DayOfWeek? dn, DayOfWeek d, int s, int? sn)
            {
                Time = t;
                DOWNull = dn;
                DOW = d;
                Sec = s;
                SecNull = sn;
            }
        }

        static void Main(string[] args)
        {
            try
            {
                IQueryable<DateTime> a0 = Enumerable.Repeat(0, 7)
                    .Select((d, i) => new DateTime(2000, 1, 1).AddDays(i).AddSeconds(i))
                    .AsQueryable();

                IQueryable<Example1> a1 = a0
                    .Select<Example1>("new (it as Time, DayOfWeek as DOWNull, DayOfWeek as DOW, Second as Sec, int?(Second) as SecNull)");

                List<Example1> z1 = a1.ToList();
                if (z1.Any(c => c.DOWNull == null))
                {
                    // You can set an bp here to view the data of z1 in netcoreapp
                    throw new InvalidOperationException("Bad data 1");
                }

                Console.WriteLine(JsonConvert.SerializeObject(z1, Formatting.Indented));

                Console.WriteLine(new string('-', 80));

                IQueryable<Example2> b1 = a0
                    .Select<Example2>("new (it as Time, DayOfWeek as DOWNull, DayOfWeek as DOW, Second as Sec, int?(Second) as SecNull)");

                List<Example2> x1 = b1.ToList();
                if (x1.Any(c => c.DOWNull == null))
                {
                    // You can set an bp here to view the data of x1 in netcoreapp
                    throw new InvalidOperationException("Bad data 2");
                }

                Console.WriteLine(JsonConvert.SerializeObject(x1, Formatting.Indented));

                Console.WriteLine("Success");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}
