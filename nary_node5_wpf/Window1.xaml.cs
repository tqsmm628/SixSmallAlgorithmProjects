using System.Windows;

namespace nary_node5
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Build a test tree.
            // A
            //         |
            //     +---+---+
            // B   C   D
            //     |       |
            //    +-+      +
            // E F      G
            //    |        |
            //    +      +-+-+
            // H      I J K
            var node_a = new NaryNode<string>("A");
            var node_b = new NaryNode<string>("B");
            var node_c = new NaryNode<string>("C");
            var node_d = new NaryNode<string>("D");
            var node_e = new NaryNode<string>("E");
            var node_f = new NaryNode<string>("F");
            var node_g = new NaryNode<string>("G");
            var node_h = new NaryNode<string>("H");
            var node_i = new NaryNode<string>("I");
            var node_j = new NaryNode<string>("J");
            var node_k = new NaryNode<string>("K");

            node_a.AddChild(node_b);
            node_a.AddChild(node_c);
            node_a.AddChild(node_d);
            node_b.AddChild(node_e);
            node_b.AddChild(node_f);
            node_d.AddChild(node_g);
            node_e.AddChild(node_h);
            node_g.AddChild(node_i);
            node_g.AddChild(node_j);
            node_g.AddChild(node_k);

            // Draw the tree.
            node_a.ArrangeAndDrawSubtree(mainCanvas, 10, 10);
        }
    }
}
