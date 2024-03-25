using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace sorted_binary_node1
{
    internal class SortedBinaryNode<T> where T: IComparable<T>
    {
        internal T Value { get; set; }
        internal SortedBinaryNode<T> LeftChild, RightChild;

        private const double NODE_RADIUS = 10;  // Radius of a node’s circle
        private const double X_SPACING = 20;    // Horizontal distance between neighboring subtrees
        private const double Y_SPACING = 20;    // Horizontal distance between parent and child subtree
        internal Point Center { get; private set; }
        internal Rect SubtreeBounds { get; private set; }

        internal SortedBinaryNode(T value)
        {
            Value = value;
            LeftChild = null;
            RightChild = null;
        }

        internal void AddLeft(SortedBinaryNode<T> child)
        {
            LeftChild = child;
        }

        internal void AddRight(SortedBinaryNode<T> child)
        {
            RightChild = child;
        }

        // Return an indented string representation of the node and its children.
        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string spaces)
        {
            var result = $"{spaces}{Value}:\n";

            if ((LeftChild != null) || (RightChild != null))
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

        internal SortedBinaryNode<T> FindNode(T value)
        {
            if (Value.Equals(value)) return this;

            var targetChild = value.CompareTo(Value) < 0 ? LeftChild : RightChild;
            return targetChild?.FindNode(value);
        }

        internal void AddNode(T value)
        {
            if (Equals(value, Value))
                throw new ArgumentException("Duplicate value added to the tree.");
            
            ref var child = ref value.CompareTo(Value) < 0 ? ref LeftChild : ref RightChild;
            if (child == null)
                child = new SortedBinaryNode<T>(value);
            else
                child.AddNode(value);
        }
        
        internal void RemoveNode(T value)
        {
            var nodeAndParent = FindNodeAndParent(this, value);
            if (nodeAndParent == null)
                throw new ArgumentException("Value not found in the tree.");
            
            var (node, parent) = nodeAndParent.Value;
            RemoveNode(node, parent);
        }

        private static void RemoveNode(SortedBinaryNode<T> node, SortedBinaryNode<T> parent)
        {
            if (node.LeftChild == null && node.RightChild == null)
            {
                if (parent.LeftChild == node)
                    parent.LeftChild = null;
                else
                    parent.RightChild = null;

                return;
            }
            
            if (node.LeftChild == null || node.RightChild == null)
            {
                var child = node.LeftChild ?? node.RightChild;
                if (parent.LeftChild == node)
                    parent.LeftChild = child;
                else
                    parent.RightChild = child;

                return;
            }

            var leftmostParent = node;
            var leftmost = node.RightChild;
            while (leftmost.LeftChild != null)
            {
                leftmostParent = leftmost;
                leftmost = leftmost.LeftChild;
            }
                
            node.Value = leftmost.Value;
            RemoveNode(leftmost, leftmostParent);
        }

        private (SortedBinaryNode<T> Node, SortedBinaryNode<T> Parent)? 
            FindNodeAndParent(SortedBinaryNode<T> parent, T value)
        {
            if (Equals(value, Value)) return (this, parent);
            return value.CompareTo(Value) < 0 
                ? LeftChild?.FindNodeAndParent(this, value) 
                : RightChild?.FindNodeAndParent(this, value);
        }

        internal List<SortedBinaryNode<T>> TraversePreorder()
        {
            var result = new List<SortedBinaryNode<T>>();

            result.Add(this);

            if (LeftChild != null) result.AddRange(LeftChild.TraversePreorder());
            if (RightChild != null) result.AddRange(RightChild.TraversePreorder());
            return result;
        }

        internal List<SortedBinaryNode<T>> TraverseInorder()
        {
            var result = new List<SortedBinaryNode<T>>();

            // Add the left subtree.
            if (LeftChild != null) result.AddRange(LeftChild.TraverseInorder());

            // Add this node.
            result.Add(this);

            // Add the right subtree.
            if (RightChild != null) result.AddRange(RightChild.TraverseInorder());
            return result;
        }

        internal List<SortedBinaryNode<T>> TraversePostorder()
        {
            var result = new List<SortedBinaryNode<T>>();

            // Add the child subtrees.
            if (LeftChild != null) result.AddRange(LeftChild.TraversePostorder());
            if (RightChild != null) result.AddRange(RightChild.TraversePostorder());

            // Add this node.
            result.Add(this);
            return result;
        }

        internal List<SortedBinaryNode<T>> TraverseBreadthFirst()
        {
            var result = new List<SortedBinaryNode<T>>();
            var queue = new Queue<SortedBinaryNode<T>>();

            // Start with the top node in the queue.
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                // Remove the top node from the queue and
                // add it to the result list.
                var node = queue.Dequeue();
                result.Add(node);

                // Add the node's children to the queue.
                if (node.LeftChild != null) queue.Enqueue(node.LeftChild);
                if (node.RightChild != null) queue.Enqueue(node.RightChild);
            }

            return result;
        }

        // Position the node's subtree.
        private void ArrangeSubtree(double xmin, double ymin)
        {
            // Calculate cy, the Y coordinate for this node.
            // This doesn't depend on the children.
            var cy = ymin + NODE_RADIUS;

            // If the node has no children, just place it here and return.
            if ((LeftChild == null) && (RightChild == null))
            {
                var cx = xmin + NODE_RADIUS;
                Center = new Point(cx, cy);
                SubtreeBounds = new Rect(xmin, ymin, 2 * NODE_RADIUS, 2 * NODE_RADIUS);
                return;
            }

            // Set child_xmin and child_ymin to the
            // start position for child subtrees.
            var childXmin = xmin;
            var childYmin = ymin + 2 * NODE_RADIUS + Y_SPACING;

            // Position the child subtrees.
            if (LeftChild != null)
            {
                // Arrange the left child subtree and update
                // child_xmin to allow room for its subtree.
                LeftChild.ArrangeSubtree(childXmin, childYmin);
                childXmin = LeftChild.SubtreeBounds.Right;

                // If we also have a right child,
                // add space between their subtrees.
                if (RightChild != null) childXmin += X_SPACING;
            }

            // Arrange the right child subtree.
            RightChild?.ArrangeSubtree(childXmin, childYmin);

            // Arrange this node depending on the number of children.
            if ((LeftChild != null) && (RightChild != null))
            {
                // Two children. Center this node over the child nodes.
                // Use the subtree bounds to set our subtree bounds.
                var cx = (LeftChild.Center.X + RightChild.Center.X) / 2;
                Center = new Point(cx, cy);
                var xmax = RightChild.SubtreeBounds.Right;
                var ymax = Math.Max(LeftChild.SubtreeBounds.Bottom, RightChild.SubtreeBounds.Bottom);
                SubtreeBounds = new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
            }
            else if (LeftChild != null)
            {
                // We have only a left child.
                var cx = LeftChild.Center.X;
                Center = new Point(cx, cy);
                var xmax = LeftChild.SubtreeBounds.Right;
                var ymax = LeftChild.SubtreeBounds.Bottom;
                SubtreeBounds = new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
            }
            else
            {
                // We have only a right child.
                var cx = RightChild.Center.X;
                Center = new Point(cx, cy);
                var xmax = RightChild.SubtreeBounds.Right;
                var ymax = RightChild.SubtreeBounds.Bottom;
                SubtreeBounds = new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
            }
        }

        // Draw the subtree's links.
        private void DrawSubtreeLinks(Canvas canvas)
        {
            // Draw the subtree's links.
            if (LeftChild != null)
            {
                LeftChild.DrawSubtreeLinks(canvas);
                canvas.DrawLine(Center, LeftChild.Center, Brushes.Black, 1);
            }

            if (RightChild != null)
            {
                RightChild.DrawSubtreeLinks(canvas);
                canvas.DrawLine(Center, RightChild.Center, Brushes.Black, 1);
            }

            // Outline the subtree for debugging.
            //canvas.DrawRectangle(SubtreeBounds, null, Brushes.Red, 1);
        }

        private void DrawSubtreeNodes(Canvas canvas)
        {
            var rect = new Rect(
                Center.X - NODE_RADIUS,
                Center.Y - NODE_RADIUS,
                2 * NODE_RADIUS,
                2 * NODE_RADIUS);
            canvas.DrawEllipse(rect, Brushes.White, Brushes.Green, 1);

            canvas.DrawLabel(
                rect, Value, null, Brushes.Red,
                HorizontalAlignment.Center,
                VerticalAlignment.Center,
                12, 0);

            LeftChild?.DrawSubtreeNodes(canvas);
            RightChild?.DrawSubtreeNodes(canvas);
        }

        public void ArrangeAndDrawSubtree(Canvas canvas, double xmin, double ymin)
        {
            ArrangeSubtree(xmin, ymin);
            DrawSubtreeLinks(canvas);
            DrawSubtreeNodes(canvas);
        }
    }
}
