using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace draw_network;

public class Network
{
    public List<Node> Nodes { get; private set; } = [];
    public List<Link> Links { get; private set; } = [];
    private bool drawLabels => Nodes.Count < 100;
    

    public void Clear()
    {
        Nodes = [];
        Links = [];
    }

    public void AddNode(Node node)
    {
        node.Index = Nodes.Count;
        Nodes.Add(node);
    }

    public void AddLink(Link link)
    {
        Links.Add(link);
    }

    public string Serialization() => 
        string.Join(Environment.NewLine, Summarize());

    private IEnumerable<string> Summarize()
    {
        yield return $"{Nodes.Count} # Num nodes.";
        yield return $"{Links.Count} # Num links.";
        yield return "# Nodes";
        foreach (var node in Nodes)
            yield return $"{node.Center.X},{node.Center.Y},{node.Text}";
        yield return "# Links";
        foreach (var link in Links)
            yield return $"{link.FromNode.Index},{link.ToNode.Index},{link.Cost}";
    }

    public void SaveIntoFile(string filePath)
    {
        File.WriteAllText(filePath, Serialization());
    }

    public static string? ReadNextLine(StringReader reader)
    {
        while (true)
        {
            var line = reader.ReadLine();
            if (line is null) return null;
            
            var result = line.Split('#')[0].Trim();
            if (result.Length > 0) return result;
        }
    }

    public void Deserialize(string text)
    {
        Clear();
        using var reader = new StringReader(text);
        var numNodes = int.Parse(ReadNextLine(reader)!);
        var numLinks = int.Parse(ReadNextLine(reader)!);
        for (var i = 0; i < numNodes; i++)
        {
            var nodeData = ReadNextLine(reader)!.Split(',');
            var x = double.Parse(nodeData[0]);
            var y = double.Parse(nodeData[1]);
            var nodeText = nodeData[2];
            var node = new Node(this, new Point(x, y), nodeText);
            AddNode(node);
        }
        for (var i = 0; i < numLinks; i++)
        {
            var linkData = ReadNextLine(reader)!.Split(',');
            var fromNodeIndex = int.Parse(linkData[0]);
            var toNodeIndex = int.Parse(linkData[1]);
            var cost = int.Parse(linkData[2]);
            var sourceNode = Nodes[fromNodeIndex];
            var destNode = Nodes[toNodeIndex];
            var link = new Link(this, sourceNode, destNode, cost);
            sourceNode.AddLink(link);
            AddLink(link);
        }
    }

    public void ReadFromFile(string filePath)
    {
        var text = File.ReadAllText(filePath);
        Deserialize(text);
    }

    private Rect GetBounds()
    {
        if (Nodes.Count == 0) return new Rect(0, 0, 0, 0);
        var maxX = Nodes[0].Center.X;
        var maxY = Nodes[0].Center.Y;
        foreach (var node in Nodes.Skip(1))
        {
            maxX = Math.Max(maxX, node.Center.X);
            maxY = Math.Max(maxY, node.Center.Y);
        }
        return new Rect(0, 0, maxX, maxY);
    }

    public void Draw(Canvas canvas)
    {
        canvas.Children.Clear();

        var bound = GetBounds();
        canvas.Width = bound.Width;
        canvas.Height = bound.Height;
        foreach (var link in Links)
        {
            link.Draw(canvas);
        }

        if (drawLabels)
            foreach (var link in Links)
            {
                link.DrawLabel(canvas);
            }

        foreach (var node in Nodes)
        {
            node.Draw(canvas, drawLabels);
        }
    }

    public Node? StartNode { get; set; }
    public Node? EndNode { get; set; }

    public void ellipse_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var ellipse = (Ellipse)sender;
        var node = (Node)ellipse.Tag;
        node.NodeClicked(e);
    }

    public void label_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var label = (Label)sender;
        var node = (Node)label.Tag;
        node.NodeClicked(e);
    }
}
