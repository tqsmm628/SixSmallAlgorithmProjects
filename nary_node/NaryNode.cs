namespace nary_node;

public class NaryNode<T>(T value)
{
    public T Value { get; set; } = value;
    public List<NaryNode<T>> Children { get; set; } = new();

    public void AddChild(NaryNode<T> child)
    {
        Children.Add(child);
    }

    public override string ToString()
    {
        var childValues = Children.Select(c => c.Value);
        return $"{Value}: {string.Join(" ", childValues)}";
    }
}
