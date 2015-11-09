using System;
using System.Linq.Expressions;
using System.Reflection;

// ReSharper disable CheckNamespace

namespace Agile
    // ReSharper restore CheckNamespace
{
    public static class ExpressionExtensions
    {


        private static MemberExpression ToMemberExpression(this LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
                return memberExpression;

            var unaryExpression = expression.Body as UnaryExpression;
            if (unaryExpression != null)
                memberExpression = unaryExpression.Operand as MemberExpression;

            return memberExpression;
        }

        public static MemberInfo ToMember<TSource, TProperty>(this Expression<Func<TSource, TProperty>> expression)
        {
   
            MemberExpression member = expression.Body as MemberExpression;
            if (member == null)
            {
                UnaryExpression body =  expression.Body as UnaryExpression;
                if (body == null)
                    return ToMethod(expression);

                member = body.Operand as MemberExpression;
            }

            if (member == null)
                return null;

            return   member.Member;

        }

        public static PropertyInfo ToProperty<TSource, TProperty>(this Expression<Func<TSource, TProperty>> expression)
        {
            MemberExpression member = expression.Body as MemberExpression;
            if (member == null)
            {
                UnaryExpression body = expression.Body as UnaryExpression;
                if (body == null)
                    return null;
                member = body.Operand as MemberExpression;
            }

            if (member == null)
                return null;

            return member.Member as PropertyInfo;
        }

        public static FieldInfo ToField<TSource, TField>(this Expression<Func<TSource, TField>> expression)
        {
            MemberExpression member = expression.Body as MemberExpression;
            if (member == null)
            {
                UnaryExpression body = expression.Body as UnaryExpression;
                if (body == null)
                    return null;
                member = body.Operand as MemberExpression;
            }

            if (member == null)
                return null;

            return member.Member as FieldInfo;
        }

        public static MethodInfo ToMethod<TSource, TField>(this Expression<Func<TSource, TField>> expression)
        {
            MethodCallExpression member = expression.Body as MethodCallExpression;
            if (member == null)
                return null;

            return member.Method;
        }



    }
}