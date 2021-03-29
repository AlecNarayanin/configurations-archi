using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AppliAuth.Extensions
{
    public static class TypeExtension
    {


        public static List<FieldInfo> GetAllFields(this Type t)
        {
            
            List<FieldInfo> fields = new List<FieldInfo>();

            fields.AddRange(t.GetFields((BindingFlags.Instance |
                       BindingFlags.Static |
                       BindingFlags.NonPublic |
                       BindingFlags.Public)));

            fields.AddRange(t.BaseType.GetFields((BindingFlags.Instance |
                       BindingFlags.Static |
                       BindingFlags.NonPublic |
                       BindingFlags.Public)));

            return fields;
        }
    }
}
