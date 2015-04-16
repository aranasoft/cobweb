using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Cobweb.Reflection.Extensions {
    public static class WithMember {
        /// <summary>
        ///     Indicates whether one or more instance of <typeparamref name="TAttribute" /> is defined on the
        ///     <paramref name="member" />.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type.</typeparam>
        /// <param name="member">The member to analyze.</param>
        /// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
        /// <returns>true if the attributeType is defined on this member; false otherwise.</returns>
        public static bool IsDefined<TAttribute>(this ICustomAttributeProvider member, bool inherit = true)
            where TAttribute : Attribute {
            return member.IsDefined(typeof (TAttribute), inherit);
        }

        /// <summary>
        ///     Returns an array of custom attributes defined on this member, identified by type, or an empty array if there are no
        ///     custom attributes of that type.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type.</typeparam>
        /// <param name="member">The member to analyze.</param>
        /// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
        /// <returns>An array of <typeparamref name="TAttribute" /> representing custom attributes, or an empty array.</returns>
        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this ICustomAttributeProvider member,
                                                                              bool inherit = true)
            where TAttribute : Attribute {
            return member.GetCustomAttributes(typeof (TAttribute), inherit).Cast<TAttribute>();
        }
    }

    public static class WithExpression {
        public static Dictionary<ParameterInfo, Expression> GetMethodArguments<TDelegate>(
            this Expression<TDelegate> expression) {
            var methodCall = ((MethodCallExpression) expression.Body);

            return methodCall.Arguments.Select((arg, argIndex) => {
                return new KeyValuePair<ParameterInfo, Expression>(
                    methodCall.Method.GetParameters()[argIndex],
                    arg);
            }).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public static Dictionary<ParameterInfo, object> GetMethodArgumentValues<TDelegate>(
            this Expression<TDelegate> expression) {
            var args = GetMethodArguments(expression);

            return args.Select(arg => {
                var argInfo = arg.Key;
                var argInput = arg.Value;

                return new KeyValuePair<ParameterInfo, object>(argInfo, GetExpressionValue(argInput));
            }).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static object GetExpressionValue(Expression argInput) {
            if (argInput.NodeType == ExpressionType.Convert && argInput is UnaryExpression) {
                return GetExpressionValue(((UnaryExpression) argInput).Operand);
            }

            switch (argInput.NodeType) {
                case ExpressionType.Constant:
                    return ((ConstantExpression) argInput).Value;

                case ExpressionType.New:
                case ExpressionType.MemberAccess:
                case ExpressionType.Convert:
                    return Expression.Lambda(argInput).Compile().DynamicInvoke();
            }
            return null;
        }
    }
}
