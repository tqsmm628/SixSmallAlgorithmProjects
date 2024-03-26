using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace draw_network;

public class Node(Network network, Point center, string text)
{
    public int Index { get; set; } = -1;
    public Network Network { get; set; } = network;
    public Point Center { get; set; } = center;
    public string Text { get; set; } = text;
    private readonly List<Link> Links = [];

    public override string ToString() => $"[{Text}]";
    public void AddLink(Link link) => Links.Add(link);

    public void Draw(Canvas canvas)
    {
        const double radius = 10d;
        var bound = new Rect(Center.X - radius, Center.Y - radius, radius * 2, radius * 2);
        canvas.DrawEllipse(bound, 
            Brushes.White,
            Brushes.Black, 1);
        canvas.DrawString(Text, 
            radius * 2, radius * 2, 
            Center, 0, 12, Brushes.Blue);
    }
}
