using System.Linq.Expressions;

namespace Doctorly.Api.Extensions;

public static class EndpointExtensions
{
    public static Expression<Func<T, bool>> AppendWhere<T, TValue>(this Expression<Func<T, bool>> original, string propName,
        TValue value)
    {
        ParameterExpression argParam = Expression.Parameter(typeof(T), "e");
        Expression nameProperty = Expression.Property(argParam, propName);

        var val1 = Expression.Constant(propName);

        Expression e1 = Expression.Equal(nameProperty, val1);
        var andExp = Expression.AndAlso(original, e1);

        return Expression.Lambda<Func<T, bool>>(andExp, argParam);
    }
}