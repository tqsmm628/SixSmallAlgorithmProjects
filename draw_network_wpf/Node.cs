using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace draw_network;

public class Node(Network network, Point center, string text)
{
    public int Index { get; set; } = -1;
    //public Network Network { get; set; } = network;
    public Point Center { get; set; } = center;
    public string Text { get; set; } = text;
    public readonly List<Link> Links = [];
    public int TotalCost { get; set; }
    public bool IsInPath { get; set; }
    public Link? ShortestPathLink { get; set; }
    public bool Visited { get; set; }

    private const double 
        LARGE_RADIUS = 10,
        SMALL_RADIUS = 3;

    public override string ToString() => $"[{Text}]";
    public void AddLink(Link link) => Links.Add(link);

    public Ellipse? MyEllipse { get; set; }
    public Label? MyLabel { get; set; }

    public void Draw(Canvas canvas, bool drawLabels)
    {
        var radius = drawLabels ? LARGE_RADIUS : SMALL_RADIUS;
        var bound = new Rect(Center.X - radius, Center.Y - radius, radius * 2, radius * 2);
        MyEllipse = canvas.DrawEllipse(bound, 
            Brushes.White,
            Brushes.Black, 1);
        MyEllipse.Tag = this;
        MyEllipse.MouseDown += network.ellipse_MouseDown;

        if (drawLabels)
        {
            MyLabel = canvas.DrawString(Text,
                radius * 2, radius * 2,
                Center, 0, 10, Brushes.Blue);
            MyLabel.Tag = this;
            MyLabel.MouseDown += network.label_MouseDown;
        }
    }

    private bool isStartNode;
    public bool IsStartNode
    {
        get => isStartNode;
        set
        {
            isStartNode = value;
            SetNodeAppearance();
        }
    }

    private bool isEndNode;
    public bool IsEndNode
    {
        get => isEndNode;
        set
        {
            isEndNode = value;
            SetNodeAppearance();
        }
    }

    private void SetNodeAppearance()
    {
        if (MyEllipse is null) return;

        if (IsStartNode)
        {
            MyEllipse.Fill = Brushes.Pink;
            MyEllipse.Stroke = Brushes.Red;
            MyEllipse.StrokeThickness = 2;
        }
        else if (IsEndNode)
        {
            MyEllipse.Fill = Brushes.LightGreen;
            MyEllipse.Stroke = Brushes.Green;
            MyEllipse.StrokeThickness = 2;
        }
        else
        {
            MyEllipse.Fill = Brushes.White;
            MyEllipse.Stroke = Brushes.Black;
            MyEllipse.StrokeThickness = 1;
        }
    }
}
