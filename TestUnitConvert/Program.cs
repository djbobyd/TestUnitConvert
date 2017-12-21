using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace TestUnitConvert
{
    class Program
    {
        private static readonly string QuantityNamespace = typeof(Length).Namespace;
        private static readonly string UnitTypeNamespace = typeof(LengthUnit).Namespace;
        private static readonly Assembly UnitsNetAssembly = Assembly.GetAssembly(typeof(Length));

        static void Main(string[] args)
        {
            Console.WriteLine(Calculate("SquareInch","SquareMeter",4));
            Type[] _types=GetUnitTypes(GetTypesInNamespace(UnitsNetAssembly,"UnitsNet.Units"));
            foreach (Type t in _types)
            {
                Console.WriteLine(t.Name);
            }
            //Console.ReadKey();
        }

        private static Type[] GetUnitTypes(Type [] tp)
        {
            List<Type> arr=new List<Type>();
            foreach (Type t in tp)
            {
                //if (t.BaseType.Name.Equals("ValueType"))
                {
                    arr.Add(t);
                }
            }

            return arr.ToArray();
        }
        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }
        private static double Calculate(string fromUnit, string toUnit, double quantity)
        {
            double result = 0.0;
            bool isMatch = false;
            Type[] tp = GetTypesInNamespace(UnitsNetAssembly, "UnitsNet");

            foreach (Type t in tp)
            {
                isMatch = UnitConverter.TryConvertByAbbreviation(quantity, t.Name.ToString(), fromUnit, toUnit, out result);
                if (isMatch)
                {
                    break;
                }
                isMatch = UnitConverter.TryConvertByName(quantity, t.Name.ToString(), fromUnit, toUnit, out result);
                if (isMatch)
                {
                    break;
                }
            }
            return result;
        }
    }
}
