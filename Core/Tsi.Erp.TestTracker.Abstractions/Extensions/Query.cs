
namespace System.Linq.Extensions;
public class PropertyFilter
{
    public string PropertyName { get; set; }
    public Operator Operator { get; set; }
    public string PropertyValue { get; set; }
}

public class Query
{
    public string Keyword { get; set; }
    public LogicalOperator Operator { get; set; }
    public IEnumerable<PropertyFilter> Propertyfilters { get; set; }
}

public enum Operator
{
    Equal,
    NotEqual,
    Contains,
    GreaterThan,
    GreaterThanOrEqual,
    LessThanOrEqual,
    LessThan
}

public enum LogicalOperator
{
    And,
    Or,
}

