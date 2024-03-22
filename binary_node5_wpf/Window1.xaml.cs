using System.Windows;
using System.Windows.Controls;

namespace binary_node5
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Build a test tree.
            //         A
            //        / \
            //       /   \
            //      /     \
            //     B       C
            //    / \     / \
            //   D   E   F   G
            //      / \     /
            //     H   I   J
            //            / \
            //           K   L
            var node_a = new BinaryNode<string>("A");
            var node_b = new BinaryNode<string>("B");
            var node_c = new BinaryNode<string>("C");
            var node_d = new BinaryNode<string>("D");
            var node_e = new BinaryNode<string>("E");
            var node_f = new BinaryNode<string>("F");
            var node_g = new BinaryNode<string>("G");
            var node_h = new BinaryNode<string>("H");
            var node_i = new BinaryNode<string>("I");
            var node_j = new BinaryNode<string>("J");
            var node_k = new BinaryNode<string>("K");
            var node_l = new BinaryNode<string>("L");

            node_a.AddLeft(node_b);
            node_a.AddRight(node_c);
            node_b.AddLeft(node_d);
            node_b.AddRight(node_e);
            node_c.AddLeft(node_f);
            node_c.AddRight(node_g);
            node_e.AddLeft(node_h);
            node_e.AddRight(node_i);
            node_g.AddLeft(node_j);
            node_j.AddLeft(node_k);
            node_j.AddRight(node_l);

            // Arrange and draw the tree.
            var margin = 10;
            node_a.ArrangeAndDrawSubtree(mainCanvas, margin, margin);
            // Width = node_a.SubtreeBounds.Width + margin * 2 + 
            //         SystemParameters.BorderWidth * 2 +
            //         SystemParameters.Wid * 2;
            // Height = node_a.SubtreeBounds.Height + margin * 2 + 
            //          SystemParameters.WindowCaptionHeight + 
            //          SystemParameters.ResizeFrameHorizontalBorderHeight;

            // Width = node_a.SubtreeBounds.Width + margin * 2;
            // Height = node_a.SubtreeBounds.Height + margin * 2;
            // SizeToContent = SizeToContent.WidthAndHeight;
            // mainCanvas.Margin = new Thickness(margin);

        }
    }
}
