using System;
using System.Windows;

namespace sorted_binary_node1
{
    public partial class Window1 : Window
    {
        private SortedBinaryNode<int> _root = new SortedBinaryNode<int>(-1);
        
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RunTests();
            Draw();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ValueTextBox.Text, out var newValue) ||
                newValue < 0)
            {
                MessageBox.Show("Please enter a non-negative integer.");
                return;
            }

            try
            {
                _root.AddNode(newValue);
                Draw();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ValueTextBox.Text, out var targetValue) ||
                targetValue < 0)
            {
                MessageBox.Show("Please enter a non-negative integer.");
                return;
            }

            try
            {
                var foundNode = _root.FindNode(targetValue);
                if (foundNode == null)
                    MessageBox.Show("Value not found.");
                else
                    MessageBox.Show($"Value found: {foundNode.Value}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ValueTextBox.Text, out var targetValue) ||
                targetValue < 0)
            {
                MessageBox.Show("Please enter a non-negative integer.");
                return;
            }
            
            try 
            {
                _root.RemoveNode(targetValue);
                Draw();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            _root = new SortedBinaryNode<int>(-1);
            RunTests();
            Draw();
        }

        private void RunTests()
        {
            var values = new [] { 60, 35, 76, 21, 42, 71, 89, 17, 24, 74, 11, 23, 72, 75 };
            foreach (var value in values)
            {
                _root.AddNode(value);
            }
        }

        private void Draw()
        {
            ValueTextBox.Text = "";
            mainCanvas.Children.Clear();
            _root.ArrangeAndDrawSubtree(mainCanvas, 10, 10);
        }
    }
}
