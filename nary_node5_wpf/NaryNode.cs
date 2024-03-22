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

        private const double
            NODE_RADIUS = 18,
            FONT_SIZE = 16,
            X_SPACING = 30,
            Y_SPACING = 60;

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
                Center = new Point(xmin + NODE_RADIUS, ymin + NODE_RADIUS);
                SubtreeBounds = new Rect(boundPoint, new Size(NODE_RADIUS * 2, NODE_RADIUS * 2));
                return;
            }
            
            var x_offset = xmin;
            foreach (var child in Children)
            {
                child.ArrangeSubtree(x_offset, ymin + Y_SPACING);
                x_offset = child.SubtreeBounds.Right + X_SPACING;
            }

            var LeftChild = Children.First();
            var RightChild = Children.Last();
            
            Center = new Point(
                (LeftChild.Center.X + RightChild.Center.X) / 2,
                ymin + NODE_RADIUS
            );
            SubtreeBounds = new Rect(boundPoint,
                new Size(
                    RightChild.SubtreeBounds.Right - LeftChild.SubtreeBounds.Left,
                    Math.Max(LeftChild.SubtreeBounds.Height, RightChild.SubtreeBounds.Height) + Y_SPACING
                )
            );
        }

        private void DrawSubtreeLinks(Canvas canvas)
        {
            if (Children.Count == 0) return;
            
            canvas.DrawLine(Center, Center.Move(y: Y_SPACING / 2), Brushes.Black, 1);
            if (Children.Count > 1)
            {
                canvas.DrawLine(
                    Children.First().Center.Move(y: -Y_SPACING / 2), 
                    Children.Last().Center.Move(y: -Y_SPACING / 2), 
                    Brushes.Black, 1);
            }
            foreach (var child in Children)
            {
                canvas.DrawLine(child.Center, child.Center.Move(y: -Y_SPACING / 2), Brushes.Black, 1);
                child.DrawSubtreeLinks(canvas);
            }
        }

        private void DrawSubtreeNodes(Canvas canvas)
        {
            canvas.DrawEllipse(
                new Rect(Center.X - NODE_RADIUS, Center.Y - NODE_RADIUS, NODE_RADIUS * 2, NODE_RADIUS * 2),
                Brushes.White,
                Brushes.Green,
                2);
            var label = canvas.DrawLabel(GetLabelBound(), Value, Brushes.Transparent, Brushes.Red, HorizontalAlignment.Center, VerticalAlignment.Center, FONT_SIZE, 0);
            label.FontWeight = FontWeights.Bold;
            
            // draw boundary
            canvas.DrawRectangle(SubtreeBounds, Brushes.Transparent, Brushes.Red, 1);
            
            foreach (var child in Children) child.DrawSubtreeNodes(canvas);
        }
        
        private Rect GetLabelBound()
        {
            var label = new Label();
            
            var formattedText = new FormattedText(
                Value?.ToString(),
                CultureInfo.CurrentCulture,
                label.FlowDirection,
                new Typeface(label.FontFamily, label.FontStyle, label.FontWeight, label.FontStretch),
                FONT_SIZE,
                Brushes.Black,
                VisualTreeHelper.GetDpi(new DrawingVisual()).PixelsPerDip
            );
            return new Rect(
                Center.X - formattedText.Width / 2, 
                Center.Y - formattedText.Height / 2, 
                formattedText.Width, 
                formattedText.Height);
        }
    }
}
