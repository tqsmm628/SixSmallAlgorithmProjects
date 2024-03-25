using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace nary_node5
{
    internal class NaryNode<T>
    {
        internal T Value { get; set; }
        internal List<NaryNode<T>> Children = new List<NaryNode<T>>();

        internal NaryNode(T value) => Value = value;

        internal void AddChild(NaryNode<T> child)
        {
            Children.Add(child);
        }

        public override string ToString() => ToString("");

        public string ToString(string spaces)
        {
            var result = string.Format("{0}{1}:\n", spaces, Value);

            foreach (var child in Children)
                result += child.ToString(spaces + "  ");
            return result;
        }

        internal NaryNode<T> FindNode(T target)
        {
            if (Value.Equals(target)) return this;

            return Children
                .Select(child => child.FindNode(target))
                .FirstOrDefault(result => result != null);
        }

        internal List<NaryNode<T>> TraversePreorder()
        {
            var result = new List<NaryNode<T>>();
            result.Add(this);
            foreach (var child in Children)
            {
                result.AddRange(child.TraversePreorder());
            }

            return result;
        }

        internal List<NaryNode<T>> TraversePostorder()
        {
            var result = new List<NaryNode<T>>();
            foreach (var child in Children)
            {
                result.AddRange(child.TraversePreorder());
            }
            result.Add(this);
            return result;
        }

        internal List<NaryNode<T>> TraverseBreadthFirst()
        {
            var result = new List<NaryNode<T>>();
            var queue = new Queue<NaryNode<T>>();

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                result.Add(node);

                foreach (var child in node.Children)
                    queue.Enqueue(child);
            }

            return result;
        }
        
        private bool IsLeaf => Children.Count == 0;
        private bool IsTwig => Children.All(o => o.IsLeaf);

        private const double
            //NODE_RADIUS = 18,
            FONT_SIZE = 10,
            X_SPACING = 10,
            Y_SPACING = 60,
            BOX_HALF_WIDTH = 80 / 2,
            BOX_HALF_HEIGHT = 40 / 2,
            TWIG_OFFSET = 24;

        public Point Center { get; private set; }
        public Rect SubtreeBounds { get; private set; }

        public void ArrangeAndDrawSubtree(Canvas canvas, double xmin, double ymin)
        {
            ArrangeSubtree(xmin, ymin);
            DrawSubtreeLinks(canvas);
            DrawSubtreeNodes(canvas);
        }

        private void ArrangeSubtree(double xmin, double ymin)
        {
            var boundPoint = new Point(xmin, ymin);
            if (Children.Count == 0)
            {
                Center = new Point(xmin + BOX_HALF_WIDTH, ymin + BOX_HALF_HEIGHT);
                SubtreeBounds = new Rect(boundPoint, new Size(BOX_HALF_WIDTH * 2, BOX_HALF_HEIGHT * 2));
                return;
            }

            var firstChild = Children.First();
            var lastChild = Children.Last();
            
            if (IsTwig)
            {
                var y_offset = ymin;
                foreach (var child in Children)
                {
                    child.ArrangeSubtree(xmin + TWIG_OFFSET, y_offset + Y_SPACING);
                    y_offset += Y_SPACING;
                }
                
                Center = new Point(xmin + BOX_HALF_WIDTH, ymin + BOX_HALF_HEIGHT);
                SubtreeBounds = new Rect(boundPoint,
                    new Size(
                        TWIG_OFFSET + BOX_HALF_WIDTH * 2,
                        lastChild.SubtreeBounds.Bottom - ymin
                    )
                );
                return;
            }

            var x_offset = xmin;
            foreach (var child in Children)
            {
                child.ArrangeSubtree(x_offset, ymin + Y_SPACING);
                x_offset = child.SubtreeBounds.Right + X_SPACING;
            }

            Center = new Point(
                (firstChild.Center.X + lastChild.Center.X) / 2,
                ymin + BOX_HALF_HEIGHT
            );
            SubtreeBounds = new Rect(boundPoint,
                new Size(
                    lastChild.SubtreeBounds.Right - firstChild.SubtreeBounds.Left,
                    Math.Max(firstChild.SubtreeBounds.Height, lastChild.SubtreeBounds.Height) + Y_SPACING
                )
            );
        }

        private void DrawSubtreeLinks(Canvas canvas)
        {
            if (Children.Count == 0) return;
            
            var firstChild = Children.First();
            var lastChild = Children.Last();
            
            if (IsTwig)
            {
                var x = Center.X - BOX_HALF_WIDTH + TWIG_OFFSET / 2;
                DrawLine(Center, new Point(x, Center.Y));
                DrawLine(new Point(x, Center.Y), new Point(x, lastChild.Center.Y));
                foreach (var child in Children) 
                    DrawLine(new Point(x, child.Center.Y), child.Center);
                return;
            }
            
            DrawLine(Center, Center.Move(y: Y_SPACING / 2));
            if (Children.Count > 1)
            {
                DrawLine(
                    firstChild.Center.Move(y: -Y_SPACING / 2), 
                    lastChild.Center.Move(y: -Y_SPACING / 2));
            }
            foreach (var child in Children)
            {
                DrawLine(child.Center, child.Center.Move(y: -Y_SPACING / 2));
                child.DrawSubtreeLinks(canvas);
            }

            return;

            void DrawLine(Point p1, Point p2)
            {
                canvas.DrawLine(p1, p2, Brushes.Green, 1);
            }
        }

        private void DrawSubtreeNodes(Canvas canvas)
        {
            var backgroundBrush = IsLeaf ? Brushes.White : Brushes.Pink;
            canvas.DrawRectangle(
                new Rect(Center.X - BOX_HALF_WIDTH, Center.Y - BOX_HALF_HEIGHT, BOX_HALF_WIDTH * 2, BOX_HALF_HEIGHT * 2),
                backgroundBrush,
                Brushes.Black,
                1);

            var labelText = Value.ToString();
            if (labelText.Length > 10)
                labelText = string.Join(Environment.NewLine, Value.ToString().Split(' '));
            var label = canvas.DrawLabel(GetLabelBound(), labelText, Brushes.Transparent, Brushes.Red, HorizontalAlignment.Center, VerticalAlignment.Center, FONT_SIZE, 0);
            label.FontWeight = FontWeights.Bold;
            
            // draw boundary
            //canvas.DrawRectangle(SubtreeBounds, Brushes.Transparent, Brushes.Blue, 1);
            
            foreach (var child in Children) child.DrawSubtreeNodes(canvas);
        }
        
        private Rect GetLabelBound()
        {
            return new Rect(
                Center.X - BOX_HALF_WIDTH, 
                Center.Y - BOX_HALF_HEIGHT, 
                BOX_HALF_WIDTH * 2, 
                BOX_HALF_HEIGHT * 2);
        }
    }
}
