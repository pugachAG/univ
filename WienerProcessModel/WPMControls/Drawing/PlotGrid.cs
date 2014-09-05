using System;
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

            context.Close();
            return result;
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
