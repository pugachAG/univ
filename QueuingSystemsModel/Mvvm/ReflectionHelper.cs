using System;
using System.Linq.Expressions;
using System.Reflection;

namespace QueuingSystemsModel
{
    /// <summary>
    /// Reflection helper functions.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// extracts property name from lambda expression
        /// </summary>
        /// <param name="propertyExpression">lambda expression</param>
        /// <returns>property name</returns>
        public static string ExtractPropertyName2(LambdaExpression propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Not member access", "propertyExpression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("Expression is not property", "propertyExpression");
            }

            var getMethod = property.GetGetMethod(true);
            if (getMethod.IsStatic)
            {
                throw new ArgumentException("static expression", "propertyExpression");
            }

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Extracts the property name from a property expression.
        /// </summary>
        /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
        /// <param name="propertyExpression">The property expression (e.g. p => p.PropertyName)</param>
        /// <returns>The name of the property.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="propertyExpression"/> is null.</exception>
        /// <exception cref="ArgumentException"/>
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return ExtractPropertyName2((LambdaExpression)propertyExpression);
        }
    }
}
