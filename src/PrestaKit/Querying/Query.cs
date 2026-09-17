using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Serialization;

namespace PrestaKit.Querying
{ 
    public sealed class Query<T>
    {
        internal record Filter(string Field, string Value);

        private readonly List<Filter> _filters = new List<Filter>();

        public (string Property, bool Desc)? Order { get; private set; }
        public int? Take { get; private set; }
        public int? Skip { get; private set; }

        public Query<T> WhereEquals<TKey>(Expression<Func<T, TKey>> field, string value)
        {
            _filters.Add(new Filter(ExtractMember(field.Body), Uri.EscapeDataString(value)));
            return this;
        }

        public Query<T> WhereBeginsWith<TKey>(Expression<Func<T, TKey>> field, string prefix)
        {
            _filters.Add(new Filter(ExtractMember(field.Body), $"[{Uri.EscapeDataString(prefix)}]%"));
            return this;
        }

        public Query<T> WhereBetween<TKey>(Expression<Func<T, TKey>> field, string from, string to)
        {
            _filters.Add(new Filter(ExtractMember(field.Body), $"[{Uri.EscapeDataString(from)},{Uri.EscapeDataString(to)}]"));
            return this;
        }

        public Query<T> OrderBy<TKey>(Expression<Func<T, TKey>> key, bool desc = false)
        {
            Order = (ExtractMember(key.Body), desc);
            return this;
        }

        public Query<T> Page(int skip, int take) 
        {
            Skip = skip; 
            Take = take; 
            
            return this; 
        }

        internal string ToQueryString(DisplayMode display)
        {
            var parts = new List<string>();

            foreach (var filter in _filters)
            {
                parts.Add($"filter[{filter.Field}]={filter.Value}");
            }   
            if (Order.HasValue)
            {
                parts.Add($"sort=[{Order.Value.Property}_{(Order.Value.Desc ? "DESC" : "ASC")}]");
            }
            if (Skip.HasValue && Take.HasValue)
            {
                parts.Add($"limit={Skip.Value},{Take.Value}");
            }
                
            parts.Add(display switch
            {
                DisplayMode.Full => "display=full",
                DisplayMode.IdsOnly => "",
                _ => "display=full"
            });

            return string.Join("&", parts.Where(p => p.Length > 0));
        }

        private static string ExtractMember(Expression body)
        {
            if (body is UnaryExpression { NodeType: ExpressionType.Convert } unary)
            {
                body = unary.Operand;
            }
            if (body is not MemberExpression { Expression: ParameterExpression, Member: var member })
            {
                throw new NotSupportedException($"Expected a single property, but got: {body}.");
            }
                
            return member.GetCustomAttribute<XmlElementAttribute>()?.ElementName
                ?? throw new InvalidOperationException(
                    $"Property '{member.Name}' on {typeof(T).Name} is missing [XmlElement]; " +
                    $"cannot map it to a PrestaShop field name.");
        }
    }
}
