using System;
using System.Reflection;
using UnityEngine;

namespace Agile.Ksp.Extensions
{
    public static class ReflectionExtensions
    {
        public static object InvokeMethod(this object o, string name, object[] arguments, Type[] argumentTypes)
        {
            Type type = o.GetType();
            MethodInfo methodInfo = type.GetMethod(name, argumentTypes);
            if (methodInfo == null)
            {
                Debug.LogError("Method " + name + "not found in " + type.Name);
                return null;
            }

            return methodInfo.Invoke(o, arguments);
        }

        public static object GetFieldValue(this object o, string name)
        {
            var type = o.GetType();
            var fieldInfo = type.GetField(name);
            if (fieldInfo == null)
            {
                Debug.LogError("Field " + name + "not found in " + type.Name);
                return null;
            }

            return fieldInfo.GetValue(o);
        }

        public static void SetFieldValue(this object o, string name, object value)
        {
            var type = o.GetType();
            var fieldInfo = type.GetField(name);
            if (fieldInfo == null)
            {
                Debug.LogError("Field " + name + "not found in " + type.Name);
                return;
            }

            fieldInfo.SetValue(o, value);
        }
    }
}