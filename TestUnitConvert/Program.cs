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
            Type[] _types=GetQuantityTypes(GetTypesInNamespace(UnitsNetAssembly,QuantityNamespace));
            Type[] _unitTypes=GetUnitTypes(GetTypesInNamespace(UnitsNetAssembly,UnitTypeNamespace));
            foreach (Type t in _types)
            {
                //Console.WriteLine(t.Name);
                MethodInfo methodInfo = t.GetMethod("GetAbbreviation");
                ParameterInfo[] parameters = methodInfo.GetParameters();
                object[] parametersArray = { "Killogram" };
                var result= methodInfo.Invoke(t,parametersArray);
                Console.WriteLine(result);
            }
            foreach (Type t in _unitTypes)
            {
                Console.WriteLine(t.GetEnumValues().GetValue(1));
            }

            //Console.ReadKey();
        }

        private static Type[] GetQuantityTypes(Type[] tp)
        {
            List<Type> arr=new List<Type>();
                        foreach (Type t in tp)
                        {
                            if (t.IsValueType)
                            {
                                arr.Add(t);
                            }
                        }
            
                        return arr.ToArray();
        }

        private static Type[] GetUnitTypes(Type [] utp)
        {
            List<Type> arr=new List<Type>();
            foreach (Type t in utp)
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
