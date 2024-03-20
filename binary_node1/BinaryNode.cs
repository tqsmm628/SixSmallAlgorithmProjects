using System.Text;

namespace binary_node1;

public class BinaryNode<T>(T value)
{
    public T Value { get; set; } = value;
    public BinaryNode<T>? LeftChild { get; private set; }
    public BinaryNode<T>? RightChild { get; private set; }
    
    public void AddLeft(BinaryNode<T> leftChild) => LeftChild = leftChild;
    public void AddRight(BinaryNode<T> rightChild) => RightChild = rightChild;

    public override string ToString() => ToString("");

    private string ToString(string spaces)
    {
        var self = spaces + Value + ":";
        if (LeftChild == null && RightChild == null) return self;

        var result = new List<string> { self };
        var nextSpaces = spaces + "  ";
        result.Add(LeftChild is null ? $"{nextSpaces}None" : LeftChild.ToString(nextSpaces));
        result.Add(RightChild is null ? $"{nextSpaces}None" : RightChild.ToString(nextSpaces));
        return string.Join(Environment.NewLine, result);
    }
}
