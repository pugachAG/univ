using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CalcMethLab
{

    public class PlotDrawer : FrameworkElement
    {
        public const int PointCount = 1000;
        public const double LineThickness = 1;
        public const double PointSize = 2;

        private VisualCollection children;
        private double minX;
        private double maxX;
        private double minY;
        private double maxY;
        

        public PlotDrawer()
        {
            children = new VisualCollection(this);
        }

        public void Draw(Func<double, double> f)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();

            Point? previous = null;
            double dx = (this.maxX - this.minX) / (PointCount - 1);
            for (double x = minX; x <= maxX; x += dx)
            {
                double y = f(x);
                Point current = this.convert(x, y);
                if (previous.HasValue)
                    context.DrawLine(new Pen(this.children.Count % 2 == 0 ? Brushes.Red : Brushes.Blue, LineThickness), previous.Value, current);
                previous = current;
            }
            context.Close();
            this.children.Add(visual);
        }

        public void Draw(Func<double, double> f, double minX, double maxX, double minY, double maxY)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
            this.Draw(f);
        }

        public void Draw(double x)
        {
            Point begin = this.convert(x, this.minY);
            Point end = this.convert(x, this.maxY);
            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            context.DrawLine(new Pen(Brushes.Black, LineThickness), begin, end);
            context.Close();
            this.children.Add(visual);
        }

        public void DrawDiscrete(Func<double, double> f, double a, double b, int m)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();

            double dx = (b - a) / (m - 1);
            for (double x = a; x < b + dx/2; x += dx)
            {
                double y = f(x);
                Point current = this.convert(x, y);
                context.DrawEllipse(Brushes.Black, new Pen(Brushes.Black, LineThickness), current, PointSize, PointSize);
            }
            context.Close();
            this.children.Add(visual);
        }

        private Point convert(double x, double y)
        {
            double kx = this.Width / (this.maxX - this.minX);
            double ky = this.Height / (this.maxY - this.minY);

            return new Point((x - this.minX) * kx, this.Height - (y - this.minY) * ky);
        }

        protected override int VisualChildrenCount
        {
            get { return children.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= children.Count)
                throw new ArgumentOutOfRangeException();

            return children[index];
        }
    }

}
