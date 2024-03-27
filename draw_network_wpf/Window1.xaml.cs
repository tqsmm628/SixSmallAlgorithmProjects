using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace draw_network
{
    public partial class Window1 : Window
    {
        private Network MyNetwork = new();
        
        public Window1()
        {
            InitializeComponent();
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog
                {
                    DefaultExt = ".net",
                    Filter = "Network Files|*.net|All Files|*.*"
                };

                if (dialog.ShowDialog() == true)
                {
                    MyNetwork.ReadFromFile(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MyNetwork = new Network();
            }

            DrawNetwork();
        }

        private void DrawNetwork()
        {
            mainCanvas.Children.Clear();
            MyNetwork.Draw(mainCanvas);
        }

        private void ExitCommand_Executed(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double width = 600;
            double height = 400;
            Width = width + 70;
            Height = height + 130;
            MyNetwork.ReadFromFile("grid(10x15).net");
            MyNetwork.Draw(mainCanvas);
        }

        private Network BuildGridNetwork(
            string filename,
            double width, double height,
            int numRows, int numCols)
        {
            var unitWidth = width / (numCols - 1);
            var unitHeight = height / (numRows - 1);
            var random = new Random();

            var network = new Network();
            for (var row = 0; row < numRows; row++)
            {
                for (var col = 0; col < numCols; col++)
                {
                    var text = (row * numCols + col + 1).ToString();
                    var center = new Point(col * unitWidth, row * unitHeight);
                    var node = new Node(network, center, text);
                    network.AddNode(node);

                    if (row > 0)
                    {
                        var upNode = network.Nodes[(row - 1) * numCols + col];
                        var link1 = new Link(network, upNode, node, (int)(unitHeight * NextFactor()));
                        var link2 = new Link(network, node, upNode, (int)(unitHeight * NextFactor()));
                        network.AddLink(link1);
                        network.AddLink(link2);
                    }
                    
                    if (col > 0)
                    {
                        var leftNode = network.Nodes[row * numCols + (col - 1)];
                        var link3 = new Link(network, leftNode, node, (int)(unitWidth * NextFactor()));
                        var link4 = new Link(network, node, leftNode, (int)(unitWidth * NextFactor()));
                        network.AddLink(link3);
                        network.AddLink(link4);
                    }
                }
            }
            network.SaveIntoFile(filename);
            return network;

            double NextFactor() => random.NextDouble() * 0.2 + 1;
        }

        private void Make_Test_Networks_OnClicked(object sender, RoutedEventArgs e)
        {
            BuildGridNetwork("grid(6x10).net", 600, 400, 6, 10);
            BuildGridNetwork("grid(10x15).net", 600, 400, 10, 15);
        }
    }
}
