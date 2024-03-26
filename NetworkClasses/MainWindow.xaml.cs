using System.Windows;
using Point = System.Windows.Point;

namespace NetworkClasses;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
    }

    private void ValidateNetwork(Network network, string filePath)
    {
        var serialized1 = network.Serialization();
        network.SaveIntoFile(filePath);
        network.ReadFromFile(filePath);
        var serialized2 = network.Serialization();

        netTextBox.Text = serialized1;
        statusLabel.Content = serialized1 == serialized2 
            ? "OK" 
            : "Serializations do not match";
    }

    private void network1_Click(object sender, RoutedEventArgs e)
    {
        var network = new Network();
        var nodeA = new Node(network, new Point(20, 20), "A");
        var nodeB = new Node(network, new Point(120, 20), "B");
        var link1 = new Link(network, nodeA, nodeB, 10);
        network.AddNode(nodeA);
        network.AddNode(nodeB);
        network.AddLink(link1);

        ValidateNetwork(network, "network.txt");
    }

    private void network2_Click(object sender, RoutedEventArgs e)
    {
        var network = new Network();
        var nodeA = new Node(network, new Point(20, 20), "A");
        var nodeB = new Node(network, new Point(120, 20), "B");
        var nodeC = new Node(network, new Point(20, 120), "C");
        var nodeD = new Node(network, new Point(120, 120), "D");
        var link1 = new Link(network, nodeA, nodeB, 10);
        var link2 = new Link(network, nodeB, nodeD, 15);
        var link3 = new Link(network, nodeA, nodeC, 20);
        var link4 = new Link(network, nodeC, nodeD, 25);
        network.AddNode(nodeA);
        network.AddNode(nodeB);
        network.AddNode(nodeC);
        network.AddNode(nodeD);
        network.AddLink(link1);
        network.AddLink(link2);
        network.AddLink(link3);
        network.AddLink(link4);

        ValidateNetwork(network, "network.txt");
    }

    private void network3_Click(object sender, RoutedEventArgs e)
    {
        var network = new Network();
        var nodeA = new Node(network, new Point(20, 20), "A");
        var nodeB = new Node(network, new Point(120, 20), "B");
        var nodeC = new Node(network, new Point(20, 120), "C");
        var nodeD = new Node(network, new Point(120, 120), "D");
        var link1 = new Link(network, nodeA, nodeB, 10);
        var link2 = new Link(network, nodeB, nodeD, 15);
        var link3 = new Link(network, nodeA, nodeC, 20);
        var link4 = new Link(network, nodeC, nodeD, 25);
        var link5 = new Link(network, nodeB, nodeA, 11);
        var link6 = new Link(network, nodeD, nodeB, 16);
        var link7 = new Link(network, nodeC, nodeA, 21);
        var link8 = new Link(network, nodeD, nodeC, 26);
        network.AddNode(nodeA);
        network.AddNode(nodeB);
        network.AddNode(nodeC);
        network.AddNode(nodeD);
        network.AddLink(link1);
        network.AddLink(link2);
        network.AddLink(link3);
        network.AddLink(link4);
        network.AddLink(link5);
        network.AddLink(link6);
        network.AddLink(link7);
        network.AddLink(link8);

        ValidateNetwork(network, "network.txt");
    }
}
