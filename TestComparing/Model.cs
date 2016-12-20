using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestComparing
{
    internal class Model
    {
        public String Name;
        public Int32 MaximumCost { get; set; }
        public List<NestedClass> NestedClass { get; set; } 
    }

    internal class NestedClass : IComparable<NestedClass>, IComparable
    {
        public Decimal Decimal_Speed;
        public NestedClass1 NestedClass1_Property { get; set; }

        public int CompareTo(object obj)
        {
            NestedClass other = obj as NestedClass;
            return Decimal_Speed.CompareTo(other.Decimal_Speed);
        }

        public int CompareTo(NestedClass other)
        {
            return Decimal_Speed.CompareTo(other.Decimal_Speed);
        }
    }

    public class NestedClass1
    {
        public Boolean boolean_field_IsTrue;
    }
}
