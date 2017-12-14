using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace TestUnitConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = UnitConverter.ConvertByAbbreviation(5, "Length", "m", "cm");
            QuantityType[] quantityTypes = Enum.GetValues(typeof(QuantityType)).Cast<QuantityType>().ToArray();

            Mass.ParseUnit("kg");
            Console.WriteLine(test);
            Console.ReadKey();
        }
    }
}
