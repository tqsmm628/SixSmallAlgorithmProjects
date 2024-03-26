//#nullable disable

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace draw_network
{
    public static class Extensions
    {

        #region Add Shapes to a Canvas

        // Add a Line to a Canvas.
        public static Line DrawLine(this Canvas canvas,
            Point point1, Point point2,
            Brush stroke, double stroke_thickness)
        {
            var line = new Line
            {
                X1 = point1.X,
                Y1 = point1.Y,
                X2 = point2.X,
                Y2 = point2.Y
            };
            line.SetShapeProperties(null, stroke, stroke_thickness);
            canvas.Children.Add(line);
            return line;
        }

        // Add a Rectangle to a Canvas.
        public static Rectangle DrawRectangle(this Canvas canvas,
            Rect bounds,
            Brush fill, Brush stroke, double stroke_thickness)
        {
            var rectangle = new Rectangle();
            rectangle.SetElementBounds(bounds);
            rectangle.SetShapeProperties(fill, stroke, stroke_thickness);
            canvas.Children.Add(rectangle);
            return rectangle;
        }

        // Add an Ellipse to a Canvas.
        public static Ellipse DrawEllipse(this Canvas canvas,
            Rect bounds,
            Brush fill, Brush stroke, double stroke_thickness)
        {
            var ellipse = new Ellipse();
            ellipse.SetElementBounds(bounds);
            ellipse.SetShapeProperties(fill, stroke, stroke_thickness);
            canvas.Children.Add(ellipse);
            return ellipse;
        }

        // Add a Label to a Canvas.
        public static Label DrawLabel(this Canvas canvas,
            Rect bounds, object content,
            Brush background, Brush foreground,
            HorizontalAlignment h_align,
            VerticalAlignment v_align,
            double font_size, double padding)
        {
            var label = new Label
            {
                Content = content
            };
            label.SetElementBounds(bounds);
            label.Foreground = foreground;
            label.Background = background;
            label.HorizontalContentAlignment = h_align;
            label.VerticalContentAlignment = v_align;
            label.FontSize = font_size;
            label.Padding = new Thickness(padding);
            canvas.Children.Add(label);
            return label;
        }

        public static Label DrawString(this Canvas canvas,
            string text, double width, double height,
            Point center, double angle,
            double fontSize, Brush foreground)
        {
            var label = new Label
            {
                Content = text,
                Width = width,
                Height = height,
                Padding = new Thickness(0),
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                FontSize = fontSize,
                Foreground = foreground
            };

            // Center the label at the origin.
            Canvas.SetLeft(label, -width / 2);
            Canvas.SetTop(label, -height / 2);

            // Transform to rotate and positon
            // the label at the center point.
            var trans = new TransformGroup();
            trans.Children.Add(new RotateTransform(angle));
            trans.Children.Add(new TranslateTransform(center.X, center.Y));
            label.RenderTransform = trans;
            label.RenderTransformOrigin = new Point(0.5, 0.5);

            canvas.Children.Add(label);            
            return label;
        }

        #endregion Add Shapes to a Canvas

        #region Set Shape Properties

        // Set an element's Canvas.Left, Canvas.Top, Width, and Height properties.
        public static void SetElementBounds(this FrameworkElement element,
            Rect bounds)
        {
            Canvas.SetLeft(element, bounds.Left);
            Canvas.SetTop(element, bounds.Top);
            element.Width = bounds.Width;
            element.Height = bounds.Height;
        }

        // Set fill and outline drawing properties.
        public static void SetShapeProperties(this Shape shape,
            Brush fill, Brush stroke, double stroke_thickness)
        {
            shape.Fill = fill;
            shape.Stroke = stroke;
            shape.StrokeThickness = stroke_thickness;
        }

        #endregion Set Shape Properties
    }
}
