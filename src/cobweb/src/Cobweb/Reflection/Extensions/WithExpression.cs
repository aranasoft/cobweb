using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Cobweb.Reflection.Extensions {
    public static class WithExpression {
        /// <summary>
        ///     Gather KeyValuePairs of a method's arguments and the agument value expressions used when executing the method.
        /// </summary>
        /// <typeparam name="TDelegate">The type of expression</typeparam>
        /// <param name="expression">The expression calling the method</param>
        /// <returns>Dictionary with method arguments as Key and argument value expression as Value.</returns>
        public static Dictionary<ParameterInfo, Expression> GetMethodArguments<TDelegate>(
            this Expression<TDelegate> expression) {
            var methodCall = ((MethodCallExpression) expression.Body);

            return methodCall.Arguments.Select((arg, argIndex) => {
                                 return new KeyValuePair<ParameterInfo, Expression>(
                                     methodCall.Method.GetParameters()
                                         [argIndex],
                                     arg);
                             })
                             .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        ///     Gather KeyValuePairs of a method's arguments and the evaluated agument value used when executing the method.
        /// </summary>
        /// <typeparam name="TDelegate">The type of expression</typeparam>
        /// <param name="expression">The expression calling the method</param>
        /// <returns>Dictionary with method arguments as Key and the evaluated argument value as Value.</returns>
        public static Dictionary<ParameterInfo, object> GetMethodArgumentValues<TDelegate>(
            this Expression<TDelegate> expression) {
            var args = GetMethodArguments(expression);

            return args.Select(arg => {
                           var argInfo = arg.Key;
                           var argInput = arg.Value;

                           return new KeyValuePair<ParameterInfo, object>(argInfo, GetExpressionValue(argInput));
                       })
                       .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static object GetExpressionValue(Expression argInput) {
            if (argInput.NodeType == ExpressionType.Convert && argInput is UnaryExpression) {
                return GetExpressionValue(((UnaryExpression) argInput).Operand);
            }

            switch (argInput.NodeType) {
                case ExpressionType.Constant:
                    return ((ConstantExpression) argInput).Value;
                default:
                    return Expression.Lambda(argInput).Compile().DynamicInvoke();
            }
        }
    }
}
