using System.Windows;

namespace NetworkClasses;

public class Node(Network network, Point center, string text)
{
    public int Index { get; set; } = -1;
    public Network Network { get; set; } = network;
    public Point Center { get; set; } = center;
    public string Text { get; set; } = text;
    private readonly List<Link> Links = [];

    public override string ToString() => $"[{Text}]";
    public void AddLink(Link link) => Links.Add(link);
}
