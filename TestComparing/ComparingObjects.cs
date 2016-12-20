using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestComparing
{
    class ComparingObjects
    {
        private StringBuilder _builder = new StringBuilder();

        /// <summary>
        /// Method for comparing two the same objects, returns null if they are equal or difference between objects
        /// </summary>
        /// <param name="expected">expected object</param>
        /// <param name="actual">actual object</param>
        /// <returns>null if objects are equal and string diffrence if they are not equal</returns>
        public String GetDifferenceBetweenObjects(object expected, object actual)
        {
            CompareObjects(expected, actual, "base");
            String diffrence = _builder.ToString();
            _builder.Clear();
            return diffrence;
        }

        private void CompareClassObjects(object expected, object actual)
        {
            Type expectedType = expected.GetType();
            PropertyInfo[] properties = expectedType.GetProperties();
            FieldInfo[] fields = expectedType.GetFields();
            CompareProperties(properties, expected, actual);
            CompareFields(fields, expected, actual);
        }

        private void CompareProperties(PropertyInfo[] properties, object expected, object actual)
        {
            foreach (PropertyInfo property in properties)
            {
                CompareObjects(property.GetValue(expected), property.GetValue(actual), $"{property.ReflectedType.Name}.{property.Name}");
            }
        }

        private void CompareFields(FieldInfo[] fields, object expected, object actual)
        {
            foreach (FieldInfo field in fields)
            {
                CompareObjects(field.GetValue(expected), field.GetValue(actual), $"{field.ReflectedType.Name}.{field.Name}");
            }
        }

        private void CompareCollections(object expected, object actual)
        {
            List<object> expectedCollection = ((IEnumerable)expected).Cast<object>().ToList();
            List<object> actualCollection = ((IEnumerable)actual).Cast<object>().ToList();
            if (expectedCollection.Count == actualCollection.Count)
            {
                expectedCollection.Sort();
                actualCollection.Sort();
                for (Int32 i = 0; i < expectedCollection.Count; i++)
                {
                    CompareObjects(expectedCollection[i], actualCollection[i], expectedCollection[i].GetType().Name);
                }
            }
            else
            {
                _builder.AppendLine($"Count for object {expected.GetType().Name} aren't equal");
            }
        }

        private void CompareObjects(object expected, object actual, string fullName)
        {
            if (expected == null && actual == null)
            {
                return;
            }
            else if (expected == null || actual == null)
            {
                if (expected == null)
                {
                    _builder.AppendLine($"Expected object {fullName} was null");
                }
                else
                {
                    _builder.AppendLine($"Actual object {fullName} was null");
                }
            }
            else
            {
                Type fieldType = expected.GetType();

                if (IsSimple(fieldType))
                {
                    ComparePrimitiveType(fullName, expected, actual);
                }
                else if (fieldType.GetInterface(nameof(IEnumerable)) != null)
                {
                    CompareCollections(expected, actual);
                }
                else if (fieldType.IsClass)
                {
                    CompareClassObjects(expected, actual);
                }
            }
        }

        private bool IsSimple(Type type)
        {
            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal));
        }

        private void ComparePrimitiveType(String fieldName, object expected, object actual)
        {
            if (expected.ToString() != actual.ToString())
            {
                _builder.AppendLine($"Values for {fieldName} are not equal. Expected {expected}, actual: {actual}");
            }
        }
    }
}
