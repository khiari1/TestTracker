using Microsoft.VisualStudio.Services.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq.Extensions
{
    public static class Queryable
    {
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> query, Query filter)
        {
            query = query.Where(filter.Keyword);

            return query.Where(filter.Operator, filter.Propertyfilters);

        }

        public static IQueryable<TSource> Where<TSource>(
            this IQueryable<TSource> source,
            string? keyword)
        {
            if (string.IsNullOrEmpty(keyword)) { return source; }

            // T is a compile-time placeholder for the element type of the query.
            Type elementType = typeof(TSource);

            // Get all the string properties on this specific type.
            PropertyInfo[] stringProperties =
                elementType.GetProperties()
                    .Where(x => x.PropertyType == typeof(string))
                    .ToArray();

            if (!stringProperties.Any()) { return source; }

            // Get the right overload of String.Contains
            MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;

            // Create a parameter for the expression tree:
            // the 'x' in 'x => x.PropertyName.Contains("term")'
            // The type of this parameter is the query's element type
            ParameterExpression prm = Expression.Parameter(elementType);

            // Map each property to an expression tree node
            IEnumerable<Expression> expressions = stringProperties
                .Select(prp =>
                    // For each property, we have to construct an expression tree node like x.PropertyName.Contains("term")
                    Expression.Call(                  // .Contains(...) 
                        Expression.Property(          // .PropertyName
                            prm,           // x 
                            prp
                        ),
                        containsMethod,
                        Expression.Constant(keyword)     // "term" 
                    )
                );

            // Combine all the resultant expression nodes using ||
            Expression body = expressions
                .Aggregate(
                    (prev, current) => Expression.Or(prev, current)
                );

            // Wrap the expression body in a compile-time-typed lambda expression
            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(body, prm);

            // Because the lambda is compile-time-typed (albeit with a generic parameter), we can use it with the Where method
            return source.Where(lambda);
        }

        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> query,
            LogicalOperator logicalOperator,
            IEnumerable<PropertyFilter> filters)
        {
            if (!(filters != null && filters.Any()))
                return query;

            Expression<Func<TSource, bool>>? predicate = null;

            var expressions = new List<Expression>();

            ParameterExpression sourceExpression = Expression.Parameter(typeof(TSource));

            foreach (var filter in filters)
            {
                if (filter is null
                    || string.IsNullOrWhiteSpace(filter.PropertyName))
                    continue;

                //Get proprety
                var prop = typeof(TSource)
                    .GetProperty(filter.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (prop != null)
                {
                    filter.PropertyName = prop.Name;

                    Type propertyType = prop.PropertyType;

                    bool isNullableProperty = propertyType.IsOfType(typeof(Nullable<>));

                    MethodInfo containsMethod = null!;

                    Expression expression = null!;

                    object? value = null!;

                    if (propertyType is not null && propertyType.IsEnum)
                    {
                        value = Enum.Parse(propertyType, filter.PropertyValue);
                    }
                    else
                    {
                        value = ConvertHelper.ChangeType(filter.PropertyValue, propertyType);
                    }

                    var memberExpression = Expression.Property(sourceExpression, prop);
                    if (filter.Operator == Operator.Contains)
                    {
                        containsMethod = propertyType.GetMethod("Contains", new[] { propertyType })!;

                        expression = Expression.Call(memberExpression, containsMethod, Expression.Constant(value, propertyType));
                    }
                    else if (filter.Operator == Operator.Equal)
                    {
                        expression = Expression.Equal(memberExpression, Expression.Constant(value, propertyType));
                    }
                    else if (filter.Operator == Operator.NotEqual)
                    {
                        expression = Expression.NotEqual(memberExpression, Expression.Constant(value, propertyType));
                    }
                    else if (filter.Operator == Operator.GreaterThan)
                    {
                        expression = Expression.GreaterThan(memberExpression, Expression.Constant(value, propertyType));
                    }
                    else if (filter.Operator == Operator.GreaterThanOrEqual)
                    {
                        expression = Expression.GreaterThanOrEqual(memberExpression, Expression.Constant(value, propertyType));
                    }
                    else if (filter.Operator == Operator.LessThan)
                    {
                        expression = Expression.LessThan(memberExpression, Expression.Constant(value, propertyType));
                    }
                    else if (filter.Operator == Operator.LessThanOrEqual)
                    {
                        expression = Expression.LessThanOrEqual(memberExpression, Expression.Constant(value,propertyType));
                    }

                    if (expression is not null)
                        expressions.Add(expression);
                }

            }

            if (!expressions.Any())
                return query;

            Expression body = expressions
                                .Aggregate(
                                    (prev, current) => logicalOperator == LogicalOperator.And ? Expression.And(prev, current) : Expression.Or(prev, current)
                                );

            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(body, sourceExpression);

            return query.Where(lambda);
        }
    }
}
