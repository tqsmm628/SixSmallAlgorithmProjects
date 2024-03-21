using binary_node1;

var root = new BinaryNode<string>("Root");
var a = new BinaryNode<string>("A");
var b = new BinaryNode<string>("B");
var c = new BinaryNode<string>("C");
var d = new BinaryNode<string>("D");
var e = new BinaryNode<string>("E");
var f = new BinaryNode<string>("F");

root.AddLeft(a);
root.AddRight(b);
a.AddLeft(c);
a.AddRight(d);
b.AddRight(e);
e.AddLeft(f);

// Find some values.
FindValue(root, "Root");
FindValue(root, "E");
FindValue(root, "F");
FindValue(root, "Q");

// Find F in the B subtree.
FindValue(b, "F");

return;

void FindValue<T>(BinaryNode<T> node, T targetValue)
{
    Console.WriteLine(
        node.FindNode(targetValue) is null 
            ? $"Value {targetValue} not found" 
            : $"Found {targetValue}"
    );
}
