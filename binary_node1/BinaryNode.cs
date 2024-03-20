namespace binary_node1;

public class BinaryNode<T>(T value)
{
    public T Value { get; set; } = value;
    public BinaryNode<T>? LeftChild { get; private set; }
    public BinaryNode<T>? RightChild { get; private set; }
    
    public void AddLeft(BinaryNode<T> leftChild) => LeftChild = leftChild;
    public void AddRight(BinaryNode<T> rightChild) => RightChild = rightChild;

    public override string ToString()
    {
        var leftValue = LeftChild?.Value?.ToString() ?? "null";
        var rightValue = RightChild?.Value?.ToString() ?? "null";
        return $"{Value}: {leftValue} {rightValue}";
    }
}
