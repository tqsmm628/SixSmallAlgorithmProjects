using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace draw_network;

public class Link(Network network, Node fromNode, Node toNode, int cost)
{
    public Network Network { get; set; } = network;
    public Node FromNode { get; set; } = fromNode;
    public Node ToNode { get; set; } = toNode;
    public int Cost { get; set; } = cost;

    public override string ToString() => $"{FromNode} --> {ToNode} ({Cost})";

    public Line? MyLine { get; set; }

    private bool isInTree;
    public bool IsInTree { 
        get => isInTree;
        set
        {
            isInTree = value;
            SetLinkAppearance();
        }
    }

    private bool isInPath;
    public bool IsInPath
    {
        get => isInPath;
        set
        {
            isInPath = value;
            SetLinkAppearance();
        }
    }


    public void Draw(Canvas canvas)
    {
        MyLine = canvas.DrawLine(
            FromNode.Center, ToNode.Center, 
            Brushes.Black, 1);
    }

    public void DrawLabel(Canvas canvas)
    {
        var dx = ToNode.Center.X - FromNode.Center.X;
        var dy = ToNode.Center.Y - FromNode.Center.Y;
        var angle = Math.Atan2(dy, dx) * 180 / Math.PI;
        const double radius = 10d;
        var labelCenter = new Point(
            FromNode.Center.X * .67 + ToNode.Center.X * .33,
            FromNode.Center.Y * .67 + ToNode.Center.Y * .33);
        var labelBound = new Rect(
            labelCenter.X - radius, labelCenter.Y - radius,
            radius * 2, radius * 2);
        canvas.DrawEllipse(labelBound, Brushes.White, default, default);
        canvas.DrawString(Cost.ToString(),
            radius * 2, radius * 2, 
            labelCenter, angle, 11, Brushes.Black);
    }

    private void SetLinkAppearance()
    {
        if (MyLine is null) return;

        if (IsInPath)
        {
            MyLine.Stroke = Brushes.Red;
            MyLine.StrokeThickness = 6;
        }
        else if (IsInTree)
        {
            MyLine.Stroke = Brushes.Lime;
            MyLine.StrokeThickness = 6;
        }
        else
        {
            MyLine.Stroke = Brushes.Black;
            MyLine.StrokeThickness = 1;
        }
    }
}
