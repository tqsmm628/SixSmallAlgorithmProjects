namespace binary_node1;

public class BinaryNode<T>(T value)
{
    public T Value { get; } = value;
    private BinaryNode<T>? _leftChild, _rightChild;
    
    public void AddLeft(BinaryNode<T> child) => _leftChild = child;
    public void AddRight(BinaryNode<T> child) => _rightChild = child;

    public override string ToString() => ToString("");
    private string ToString(string spaces)
    {
        var self = spaces + Value + ":";
        if (_leftChild == null && _rightChild == null) return self;

        var result = new List<string> { self };
        var nextSpaces = spaces + "  ";
        result.Add(_leftChild is null ? $"{nextSpaces}None" : _leftChild.ToString(nextSpaces));
        result.Add(_rightChild is null ? $"{nextSpaces}None" : _rightChild.ToString(nextSpaces));
        return string.Join(Environment.NewLine, result);
    }
    
    public BinaryNode<T>? FindNode(T targetValue)
    {
        if (Equals(Value, targetValue)) return this;
        if (_leftChild?.FindNode(targetValue) is {} leftResult)
            return leftResult;
        if (_rightChild?.FindNode(targetValue) is {} rightResult)
            return rightResult;
        
        return null;
    }
}
