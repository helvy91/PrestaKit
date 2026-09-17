namespace PrestaKit.Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class ApiResourceAttribute(string name) : Attribute
    {
        public string Name { get; } = name;
    }
}
