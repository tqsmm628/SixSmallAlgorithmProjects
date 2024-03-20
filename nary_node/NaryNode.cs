namespace nary_node;

public class NaryNode<T>(T value)
{
    public T Value { get; set; } = value;
    public List<NaryNode<T>> Children { get; set; } = new();

    public void AddChild(NaryNode<T> child)
    {
        Children.Add(child);
    }

    public override string ToString() => ToString("");

    private string ToString(string spaces)
    {
        var result = new List<string>{ $"{spaces}{Value}:" };
        result.AddRange(Children.Select(child => child.ToString(spaces + "  ")));
        return string.Join(Environment.NewLine, result);
    }
}
