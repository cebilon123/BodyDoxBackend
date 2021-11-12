using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Api.Core.Common.Expressions;

namespace Api.Core.Common.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<TTo, bool>> Convert<TFrom, TTo>(
            this Expression<Func<TFrom, bool>> from)
        {
            return ConvertImpl<Func<TFrom, bool>, Func<TTo, bool>>(from);
        }

        private static Expression<TTo> ConvertImpl<TFrom, TTo>(Expression<TFrom> from)
            where TFrom : class
            where TTo : class
        {
            // figure out which types are different in the function-signature
            var fromTypes = from.Type.GetGenericArguments();
            var toTypes = typeof(TTo).GetGenericArguments();
            if (fromTypes.Length != toTypes.Length)
                throw new NotSupportedException(
                    "Incompatible lambda function-type signatures");
            var typeMap = new Dictionary<Type, Type>();
            for (var i = 0; i < fromTypes.Length; i++)
                if (fromTypes[i] != toTypes[i])
                    typeMap[fromTypes[i]] = toTypes[i];

            // re-map all parameters that involve different types
            var parameterMap = new Dictionary<Expression, Expression>();
            var newParams = new ParameterExpression[from.Parameters.Count];
            for (var i = 0; i < newParams.Length; i++)
            {
                Type newType;
                if (typeMap.TryGetValue(from.Parameters[i].Type, out newType))
                    parameterMap[@from.Parameters[i]] = newParams[i] =
                        Expression.Parameter(newType, @from.Parameters[i].Name);
                else
                    newParams[i] = @from.Parameters[i];
            }

            // rebuild the lambda
            var body = new TypeConversionVisitor(parameterMap).Visit(from.Body);
            return Expression.Lambda<TTo>(body, newParams);
        }
    }
}