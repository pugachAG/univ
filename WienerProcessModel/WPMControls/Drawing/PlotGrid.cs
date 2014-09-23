using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPMControls.Drawing
{
    enum TextAssignmentLocation
    {
        Left,
        Bottom,
        BottomLeft
    }

    class PlotGrid
    {
        /// <summary>
        /// for floating point numbers issues
        /// </summary>
        private const double Epsilon = 0.00001;
        private const double PenThickness = 2;
        private const int PenChacheSize = 100;

        private const double MaximumFontSize = 20;
        private const string FontTestText = "123456.99";
        private const double DefaultFontSize = 10;
        /// <summary>
        /// DefaultFontSize * FontCoefficient -> drawing with size 1x1;
        /// </summary>
        private static readonly double FontHeightCoefficient;
        private static readonly double FontWidhtCoefficient;
        private const string TypefaceName = "Verdana";
        private static readonly Brush DefaultTextBrush = Brushes.Black;

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

        /// <summary>
        /// Number of points on the plot
        /// </summary>
        public int PointsCount { get; set; }

        private Brush[] penBrushes = new Brush[5] { Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Orange, Brushes.Brown };
        private int penLastBrushIndex = 0;
        private Queue<Tuple<IFunction, int>> penChache = new Queue<Tuple<IFunction, int>>();

        static PlotGrid()
        {
            FormattedText ftext = new FormattedText(
                FontTestText,
                CultureInfo.InstalledUICulture,
                FlowDirection.LeftToRight,
                new Typeface(TypefaceName),
                DefaultFontSize,
                DefaultTextBrush);
            FontWidhtCoefficient = 1.0 / ftext.Width;
            FontHeightCoefficient = 1.0 / ftext.Height;
        }

        public PlotGrid()
        {
            this.GridBackround = Brushes.Transparent;
            this.PointsCount = 500;
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

            DoDrawGrid(context);

            if (Functions != null)
                foreach (var function in Functions)
                    if (function is IFunction)
                        DrawSingleFunctionPlot(context, (IFunction)function);

            context.Close();
            return result;
        }

        public Point ConvertToGridCoordinates(Point p)
        {
            double multerX = (MaxX - MinX) / GridWidth;
            double multerY = (MaxY - MinY) / GridHeight;
            double x = (p.X - GridMarginLeft) * multerX + MinX;
            double y = (ParentHeight - p.Y - GridMarginBottom) * multerY + MinY;
            return new Point(x, y);
        }

        private void DoDrawGrid(DrawingContext context)
        {
            context.DrawRectangle(GridBackround, null, new Rect(GridMarginLeft, GridMarginTop, GridWidth, GridHeight));

            DrawText(context, string.Format("({0},{1})", MinX, MinY), MinX, MinY, TextAssignmentLocation.BottomLeft);
        }

        private double GetFontSize()
        {
            double fontSize = DefaultFontSize * Math.Min(FontWidhtCoefficient * GridMarginLeft, FontHeightCoefficient * GridMarginBottom);
            return Math.Min(fontSize, MaximumFontSize);
        }

        private void DrawText(DrawingContext context, string text, double x, double y, TextAssignmentLocation location)
        {
            Point p = ConvertForDrawing(x, y);
            double fontSize = GetFontSize();
            if (fontSize > 0)
            {
                FormattedText ftext = new FormattedText(
                    text,
                    CultureInfo.InstalledUICulture,
                    FlowDirection.LeftToRight,
                    new Typeface(TypefaceName),
                    fontSize,
                    DefaultTextBrush);

                Point origin = new Point();
                switch (location)
                {
                    case TextAssignmentLocation.BottomLeft:
                        origin = new Point(p.X - ftext.Width, p.Y);
                        break;
                }
                
                context.DrawText(ftext, origin);
            }
        }

        private void DrawSingleFunctionPlot(DrawingContext context, IFunction function)
        {
            double dx = (MaxX - MinX) / (PointsCount - 1);
            if(dx < UpperBound(0))
                return;

            Point? previousPoint = null;
            Pen pen = GetPen(function);

            for (double x = MinX; x < UpperBound(MaxX); x += dx)
            {
                double y = function.GetValue(x);
                Point currentPoint = ConvertForDrawing(x, y);
                if (previousPoint.HasValue && !IsLineOut(previousPoint.Value, currentPoint))
                {
                    context.DrawLine(pen, previousPoint.Value, currentPoint);
                }
                previousPoint = currentPoint;
            }
        }

        private bool IsLineOut(Point point0, Point point1)
        {
            double drawingMinY = ConvertForDrawing(0, MaxY).Y;
            double drawingMaxY = ConvertForDrawing(0, MinY).Y;
            bool isUpper = point0.Y > LowerBound(drawingMaxY) && point1.Y > LowerBound(drawingMaxY);
            bool isLower = point0.Y < UpperBound(drawingMinY) && point1.Y < UpperBound(drawingMinY);
            return isLower || isUpper;
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

            Func<double, double, double, double> fixValue = (mn, mx, val) => Math.Max(Math.Min(mx, val), mn);

            double xFixed = fixValue(MinX, MaxX, x);
            double yFixed = fixValue(MinY, MaxY, y);

            return new Point(multerX * (xFixed - MinX) + GridMarginLeft, ParentHeight - (multerY * (yFixed - MinY) + GridMarginBottom));
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
