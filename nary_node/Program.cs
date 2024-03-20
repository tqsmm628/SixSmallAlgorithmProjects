using nary_node;

var root = new NaryNode<string>("Root");
var a = new NaryNode<string>("A");
var b = new NaryNode<string>("B");
var c = new NaryNode<string>("C");
var d = new NaryNode<string>("D");
var e = new NaryNode<string>("E");
var f = new NaryNode<string>("F");
var g = new NaryNode<string>("G");
var h = new NaryNode<string>("H");
var i = new NaryNode<string>("I");

root.AddChild(a);
root.AddChild(b);
root.AddChild(c);

a.AddChild(d);
a.AddChild(e);
d.AddChild(g);
c.AddChild(f);
f.AddChild(h);
f.AddChild(i);

// Find some values.
FindValue(root, "Root");
FindValue(root, "E");
FindValue(root, "F");
FindValue(root, "Q");

// Find F in the C subtree.
FindValue(c, "F");

return;

void FindValue<T>(NaryNode<T> node, T targetValue)
{
    Console.WriteLine(
        FindNode(node) is null 
            ? $"Value {targetValue} not found" 
            : $"Found {targetValue}"
    );
    return;

    NaryNode<T>? FindNode(NaryNode<T>? current)
    {
        if (current is null) return null;
        if (Equals(current.Value, targetValue)) return current;
        return current.Children
            .Select(FindNode)
            .FirstOrDefault(found => found is not null);
    }
}
