using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPMControls.Drawing
{
    class PlotGrid
    {
        /// <summary>
        /// Number of points on the plot
        /// </summary>
        private const double PointsCount = 1000;
        /// <summary>
        /// for floating point numbers issues
        /// </summary>
        private const double Epsilon = 0.00001;
        private const double PenThickness = 2;
        private const int PenChacheSize = 100;

        private const double MarginMulterLeft = 0.1;
        private const double MarginMulterRight = 0.05;
        private const double MarginMulterTop = MarginMulterRight;
        private const double MarginMulterBottom = MarginMulterLeft;

        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }
        public double ParentWidth { get; set; }
        public double ParentHeight { get; set; }
        public Brush GridBackround { get; set; }
        public IEnumerable Functions { get; set; }

        private Brush[] penBrushes = new Brush[5] { Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Orange, Brushes.Brown };
        private int penLastBrushIndex = 0;
        private Queue<Tuple<IFunction, int>> penChache = new Queue<Tuple<IFunction, int>>();

        public PlotGrid()
        {
            this.GridBackround = Brushes.Transparent;
        }

        public PlotGrid(double parentWidth, double parentHeight, double minX, double maxX, double minY, double maxY, Brush gridBackround)
        {
            this.ParentWidth = parentWidth;
            this.ParentHeight = parentHeight;
            this.MinX = minX;
            this.MaxX = maxX;
            this.MinY = minY;
            this.MaxY = maxY;
            this.GridBackround = gridBackround;
        }

        public virtual DrawingVisual DrawGrid()
        {
            DrawingVisual result = new DrawingVisual();
            DrawingContext context = result.RenderOpen();

            context.DrawRectangle(GridBackround, null, new Rect(GridMarginLeft, GridMarginTop, GridWidth, GridHeight));

            if (Functions != null)
                foreach (var function in Functions)
                    if (function is IFunction)
                        DrawSingleFunctionPlot(context, (IFunction)function);

            context.Close();
            return result;
        }

        private void DrawSingleFunctionPlot(DrawingContext context, IFunction function)
        {
            double dx = (MaxX - MinX) / (PointsCount - 1);
            Point? previousPoint = null;
            Pen pen = GetPen(function);

            for (double x = MinX; x < UpperBound(MaxX); x += dx)
            {
                double y = function.GetValue(x);
                if (IsPointInside(x, y))
                {
                    Point currentPoint = ConvertForDrawing(x, y);
                    if (previousPoint.HasValue)
                    {
                        context.DrawLine(pen, previousPoint.Value, currentPoint);
                    }
                    previousPoint = currentPoint;
                }
                else
                    previousPoint = null;
            }
        }

        private Pen GetPen(IFunction func)
        {
            int index = 0;
            if (penChache.Select(t => t.Item1).Contains(func))
            {
                index = penChache.FirstOrDefault(t => t.Item1 == func).Item2;
            }
            else
            {
                penLastBrushIndex = (penLastBrushIndex + 1) % penBrushes.Length;
                index = penLastBrushIndex;

                penChache.Enqueue(new Tuple<IFunction, int>(func, index));
                if (penChache.Count > PenChacheSize)
                    penChache.Dequeue();
            }
            return new Pen(penBrushes[index], PenThickness);   
        }

        private bool IsPointInside(double x, double y)
        {
            return
                x > LowerBound(MinX) &&
                x < UpperBound(MaxX) &&
                y > LowerBound(MinY) &&
                y < UpperBound(MaxY);
        }

        private Point ConvertForDrawing(double x, double y)
        {
            double multerX = GridWidth / (MaxX - MinX);
            double multerY = GridHeight / (MaxY - MinY);

            return new Point(multerX * x + GridMarginLeft, ParentHeight - (multerY * y + GridMarginBottom));
        }

        private double UpperBound(double val)
        {
            return val + Epsilon;
        }

        private double LowerBound(double val)
        {
            return val - Epsilon;
        }

        public virtual double GridMarginLeft
        {
            get
            {
                return MarginMulterLeft * ParentWidth;
            }
        }

        public virtual double GridMarginTop
        {
            get
            {
                return MarginMulterTop * ParentHeight;
            }
        }

        public virtual double GridMarginRight
        {
            get
            {
                return MarginMulterRight * ParentWidth;
            }
        }

        public virtual double GridMarginBottom
        {
            get
            {
                return MarginMulterBottom * ParentHeight;
            }
        }

        public virtual double GridWidth
        {
            get
            {
                return ParentWidth - GridMarginLeft - GridMarginRight;
            }
        }

        public virtual double GridHeight
        {
            get
            {
                return ParentHeight - GridMarginTop - GridMarginBottom;
            }
        }

    }
}
