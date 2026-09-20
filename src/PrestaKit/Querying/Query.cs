using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Serialization;

namespace PrestaKit.Querying
{
    /// <summary>
    /// A fluent, expression-based query for filtering, sorting, and paging a PrestaShop
    /// resource. Build one with the <c>Where</c>, <see cref="OrderBy"/>, and
    /// <see cref="Page"/> methods, then pass it to a list or enumerate call.
    /// </summary>
    /// <typeparam name="T">The resource entity type being queried.</typeparam>
    public sealed class Query<T>
    {
        internal record Filter(string Field, string Value);

        private readonly List<Filter> _filters = new List<Filter>();

        /// <summary>The sort field (its PrestaShop name) and direction, if ordering was set.</summary>
        public (string Property, bool Desc)? Order { get; private set; }

        /// <summary>The number of resources to take (page size), if paging was set.</summary>
        public int? Take { get; private set; }

        /// <summary>The number of resources to skip (offset), if paging was set.</summary>
        public int? Skip { get; private set; }

        /// <summary>Filters to resources whose field exactly equals the given value.</summary>
        /// <param name="field">An expression selecting the field, e.g. <c>p =&gt; p.Active</c>.</param>
        /// <param name="value">The value to match.</param>
        /// <returns>The same query, for chaining.</returns>
        public Query<T> WhereEquals<TKey>(Expression<Func<T, TKey>> field, string value)
        {
            _filters.Add(new Filter(ExtractMember(field.Body), Uri.EscapeDataString(value)));
            return this;
        }

        /// <summary>Filters to resources whose field begins with the given prefix.</summary>
        /// <param name="field">An expression selecting the field.</param>
        /// <param name="prefix">The prefix to match.</param>
        /// <returns>The same query, for chaining.</returns>
        public Query<T> WhereBeginsWith<TKey>(Expression<Func<T, TKey>> field, string prefix)
        {
            _filters.Add(new Filter(ExtractMember(field.Body), $"[{Uri.EscapeDataString(prefix)}]%"));
            return this;
        }

        /// <summary>Filters to resources whose field falls within the inclusive range.</summary>
        /// <param name="field">An expression selecting the field.</param>
        /// <param name="from">The lower bound.</param>
        /// <param name="to">The upper bound.</param>
        /// <returns>The same query, for chaining.</returns>
        public Query<T> WhereBetween<TKey>(Expression<Func<T, TKey>> field, string from, string to)
        {
            _filters.Add(new Filter(ExtractMember(field.Body), $"[{Uri.EscapeDataString(from)},{Uri.EscapeDataString(to)}]"));
            return this;
        }

        /// <summary>Orders the results by the given field.</summary>
        /// <param name="key">An expression selecting the field to sort by.</param>
        /// <param name="desc"><see langword="true"/> for descending order; otherwise ascending.</param>
        /// <returns>The same query, for chaining.</returns>
        public Query<T> OrderBy<TKey>(Expression<Func<T, TKey>> key, bool desc = false)
        {
            Order = (ExtractMember(key.Body), desc);
            return this;
        }

        /// <summary>Pages the results.</summary>
        /// <param name="skip">The number of resources to skip (offset).</param>
        /// <param name="take">The number of resources to take (page size).</param>
        /// <returns>The same query, for chaining.</returns>
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
