namespace nary_node;

public class NaryNode<T>(T value)
{
    public T Value { get; set; } = value;
    private readonly List<NaryNode<T>> _children = [];

    public void AddChild(NaryNode<T> child)
    {
        _children.Add(child);
    }

    public override string ToString() => ToString("");

    private string ToString(string spaces)
    {
        var result = new List<string>{ $"{spaces}{Value}:" };
        result.AddRange(_children.Select(child => child.ToString(spaces + "  ")));
        return string.Join(Environment.NewLine, result);
    }

    public NaryNode<T>? FindNode(T targetValue)
    {
        if (Equals(Value, targetValue)) return this;
        return _children
            .Select(child => child.FindNode(targetValue))
            .FirstOrDefault(found => found is not null);
    }

    public IEnumerable<NaryNode<T>> TraversePreorder() => Traverse("Preorder");
    public IEnumerable<NaryNode<T>> TraversePostorder() => Traverse("Postorder");

    private IEnumerable<NaryNode<T>> Traverse(string order)
    {
        if (order == "Preorder") yield return this;

        foreach (var node in _children.SelectMany(child => child.Traverse(order)))
            yield return node;

        if (order == "Postorder") yield return this;
    }

    public IEnumerable<NaryNode<T>> TraverseBreadthFirst()
    {
        var queue = new Queue<NaryNode<T>>();
        queue.Enqueue(this);
        while (queue.TryDequeue(out var item))
        {
            yield return item;
            foreach (var child in item._children)
            {
                queue.Enqueue(child);
            }
        }
    }
}
