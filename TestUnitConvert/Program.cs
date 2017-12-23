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
            String[] parametersArray = { "Killogram" };
            Type ppp=UnitsNetAssembly.GetType("UnitsNet.Length");
            MethodInfo mmm=ppp.GetMethods().First(m =>
                m.Name == "GetAbbreviation" && m.IsStatic && m.IsPublic &&
                m.ReturnType == typeof(string));
            //mmm.Invoke("")
            Type vvv;
            Mass.TryParse("gram",out vvv);
            Mass.GetAbbreviation(vvv);
            foreach (Type t in _types)
            {
                
                //Console.WriteLine(t.Name);
                //Console.WriteLine(_unitTypes[0].BaseType);

                //t.GetDeclaredMethods().Single(m => m.Name == "As" && !m.IsStatic && m.IsPublic && HasParameterTypes(m, _unitTypes[0]) && m.ReturnType == typeof(double));
                //MethodInfo method = t.GetMethod("GetAbbreviation",new [] {typeof ()});
                //ParameterInfo[] param = method.GetParameters();

                //Console.WriteLine(typeof(LengthUnit).BaseType.Name);
                //Console.WriteLine(typeof(AmplitudeRatioUnit));
                //MethodInfo methodInfo = t.GetMethod("GetAbbreviation", new [] {typeof (UnitsNet.)});
                //ParameterInfo[] parameters = methodInfo.GetParameters();
                //parameters.GetLength(0);
                //var result= methodInfo.Invoke(t,parametersArray);
                //Console.WriteLine(result);
            }
            foreach (Type t in _unitTypes)
            {
                if (!t.Name.Equals("Length" + "Unit")) continue;
                var units = t.GetEnumValues();
                foreach (var unit in units){
                    if (unit.ToString().Equals("Undefined")) continue;
                    //Console.WriteLine(unit);
                }
            }

            //Console.ReadKey();
        }

        private static Type[] GetQuantityTypes(Type[] tp)
        {
            List<Type> arr=new List<Type>();
                        foreach (Type t in tp)
                        {
                            if (!t.BaseType.Name.Equals("ValueType") || t.Name.Equals("Vector2") ||
                                t.Name.Equals("Vector3") ||
                                t.Name.Equals("QuantityValue") || t.Name.Equals("Length2d")) continue;
                            arr.Add(t);
                        }
            
                        return arr.ToArray();
        }

        private static Type[] GetUnitTypes(Type [] utp)
        {
            List<Type> arr=new List<Type>();
            foreach (Type t in utp)
            {
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
