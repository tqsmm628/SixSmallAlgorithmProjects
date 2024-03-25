namespace NetworkClasses;

public class Link(Network network, Node fromNode, Node toNode, int cost)
{
    public Network Network { get; set; } = network;
    public Node FromNode { get; set; } = fromNode;
    public Node ToNode { get; set; } = toNode;
    public int Cost { get; set; } = cost;

    public override string ToString() => $"{FromNode} --> {ToNode} ({Cost})";
}