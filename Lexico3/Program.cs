using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexico3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (Lexico l = new Lexico())
                {
                    while (!l.FinArchivo())
                    {
                        l.NextToken();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
