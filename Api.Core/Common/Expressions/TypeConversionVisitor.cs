using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Api.Core.Common.Expressions
{
    internal class TypeConversionVisitor : ExpressionVisitor
    {
        private readonly Dictionary<Expression, Expression> _parameterMap;

        public TypeConversionVisitor(
            Dictionary<Expression, Expression> parameterMap)
        {
            _parameterMap = parameterMap;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            // re-map the parameter
            Expression found;
            if (!_parameterMap.TryGetValue(node, out found))
                found = base.VisitParameter(node);
            return found;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            // re-perform any member-binding
            var expr = Visit(node.Expression);
            if (expr.Type != node.Type)
            {
                var newMember = expr.Type.GetMember(node.Member.Name)
                    .Single();
                return Expression.MakeMemberAccess(expr, newMember);
            }

            return base.VisitMember(node);
        }
    }
}