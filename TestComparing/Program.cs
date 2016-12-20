using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestComparing
{
    class Program
    {
        static void Main(string[] args)
        {
            #region models
            Model model = new Model()
            {
                Name = "test_String_field",
                MaximumCost = 100,
                NestedClass = new List<NestedClass>()
                {
                    new NestedClass()
                    {
                        Decimal_Speed = 100.01M,
                        NestedClass1_Property = new NestedClass1()
                        {
                            boolean_field_IsTrue = true
                        }
                    },
                    new NestedClass()
                    {
                        Decimal_Speed = 101.01M,
                        NestedClass1_Property = new NestedClass1()
                        {
                            boolean_field_IsTrue = false
                        }
                    }
                }
            };

            Model model1 = new Model()
            {
                Name = "test_string_field",
                MaximumCost = 1001,
                NestedClass = new List<NestedClass>()
                {
                    new NestedClass()
                    {
                        Decimal_Speed = 101.01M,
                        NestedClass1_Property = new NestedClass1()
                        {
                            boolean_field_IsTrue = true
                        }
                    },
                    new NestedClass()
                    {
                        Decimal_Speed = 100.02M,
                        NestedClass1_Property = new NestedClass1()
                        {
                            boolean_field_IsTrue = false
                        }
                    },
                    
                }
            };
            #endregion

            ComparingObjects comparator = new ComparingObjects();
            String diffrecnce = comparator.GetDifferenceBetweenObjects(model, model1);
            Console.WriteLine(diffrecnce);
            Console.WriteLine("---------------------------------");
            NestedClass class1 = new NestedClass()
            {
                NestedClass1_Property = new NestedClass1()
                {
                    boolean_field_IsTrue = false
                },
                Decimal_Speed = 100.01M
            };
            NestedClass class2 = new NestedClass()
            {
                NestedClass1_Property = new NestedClass1()
                {
                    boolean_field_IsTrue = true
                },
                Decimal_Speed = 100.01M
            };

            diffrecnce = comparator.GetDifferenceBetweenObjects(class1, class2);
            Console.WriteLine(diffrecnce);

            Console.ReadLine();
        }
    }
}
