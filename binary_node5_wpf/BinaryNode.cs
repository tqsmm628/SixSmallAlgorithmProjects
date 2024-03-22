using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace binary_node5
{
    internal class BinaryNode<T>
    {
        internal T Value { get; set; }
        internal BinaryNode<T>? LeftChild, RightChild;
        
        private IEnumerable<BinaryNode<T>> _children => new[] { LeftChild!, RightChild! }.Where(o => o is not null);

        internal BinaryNode(T value)
        {
            Value = value;
        }

        internal void AddLeft(BinaryNode<T> child)
        {
            LeftChild = child;
        }

        internal void AddRight(BinaryNode<T> child)
        {
            RightChild = child;
        }

        public override string ToString() => ToString("");

        public string ToString(string spaces)
        {
            var result = $"{spaces}{Value}:\n";

            if (LeftChild != null || RightChild != null)
            {
                if (LeftChild == null)
                    result += $"{spaces}  null\n";
                else
                    result += LeftChild.ToString(spaces + "  ");

                if (RightChild == null)
                    result += $"{spaces}  null\n";
                else
                    result += RightChild.ToString(spaces + "  ");
            }
            return result;
        }

        internal BinaryNode<T> FindNode(T target)
        {
            if (Value.Equals(target)) return this;

            BinaryNode<T> result = null;
            if (LeftChild != null)
                result = LeftChild.FindNode(target);
            if (result != null) return result;

            if (RightChild != null)
                result = RightChild.FindNode(target);
            if (result != null) return result;

            return null;
        }

        internal List<BinaryNode<T>> TraversePreorder()
        {
            var result = new List<BinaryNode<T>>();

            result.Add(this);

            if (LeftChild != null) result.AddRange(LeftChild.TraversePreorder());
            if (RightChild != null) result.AddRange(RightChild.TraversePreorder());
            return result;
        }

        internal List<BinaryNode<T>> TraverseInorder()
        {
            var result = new List<BinaryNode<T>>();

            if (LeftChild != null) result.AddRange(LeftChild.TraverseInorder());

            result.Add(this);

            if (RightChild != null) result.AddRange(RightChild.TraverseInorder());
            return result;
        }

        internal List<BinaryNode<T>> TraversePostorder()
        {
            var result = new List<BinaryNode<T>>();

            if (LeftChild != null) result.AddRange(LeftChild.TraversePostorder());
            if (RightChild != null) result.AddRange(RightChild.TraversePostorder());

            result.Add(this);
            return result;
        }

        internal List<BinaryNode<T>> TraverseBreadthFirst()
        {
            var result = new List<BinaryNode<T>>();
            var queue = new Queue<BinaryNode<T>>();

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                result.Add(node);

                if (node.LeftChild != null) queue.Enqueue(node.LeftChild);
                if (node.RightChild != null) queue.Enqueue(node.RightChild);
            }

            return result;
        }

        // New code goes here...
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
            if (LeftChild is null && RightChild is null)
            {
                Center = new Point(xmin + NODE_RADIUS, ymin + NODE_RADIUS);
                SubtreeBounds = new Rect(boundPoint, new Size(NODE_RADIUS * 2, NODE_RADIUS * 2));
                return;
            }

            if (LeftChild is null || RightChild is null)
            {
                var child = LeftChild ?? RightChild!;
                child.ArrangeSubtree(xmin, ymin + Y_SPACING);
                Center = new Point(child.Center.X, ymin + NODE_RADIUS);
                SubtreeBounds = new Rect(boundPoint,
                    new Size(
                        child.SubtreeBounds.Width,
                        child.SubtreeBounds.Height + Y_SPACING
                    )
                );
                return;
            }
            
            LeftChild.ArrangeSubtree(xmin, ymin + Y_SPACING);
            RightChild.ArrangeSubtree(LeftChild.SubtreeBounds.Right + X_SPACING, ymin + Y_SPACING);
            
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
            foreach (var child in _children)
            {
                canvas.DrawLine(Center, child.Center, Brushes.Black, 1);
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
            //canvas.DrawRectangle(SubtreeBounds, Brushes.Transparent, Brushes.Red, 1);
            
            foreach (var child in _children) child.DrawSubtreeNodes(canvas);
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
