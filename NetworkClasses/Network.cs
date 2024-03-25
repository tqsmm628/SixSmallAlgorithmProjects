using System.Drawing;
using System.IO;

namespace NetworkClasses;

public class Network
{
    public List<Node> Nodes = new();
    public List<Link> Links = new();

    public void Clear()
    {
        Nodes = new();
        Links = new();
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
            var x = int.Parse(nodeData[0]);
            var y = int.Parse(nodeData[1]);
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
            var link = new Link(this, Nodes[fromNodeIndex], Nodes[toNodeIndex], cost);
            AddLink(link);
        }
    }

    public void ReadFromFile(string filePath)
    {
        var text = File.ReadAllText(filePath);
        Deserialize(text);
    }
}
