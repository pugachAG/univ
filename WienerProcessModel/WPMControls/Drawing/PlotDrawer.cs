using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPMControls.Drawing
{
    public class PlotDrawer : VisualHost
    {
        #region Dependency Properties

        public double MinimumX
        {
            get { return (double)GetValue(MinimumXProperty); }
            set { SetValue(MinimumXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinimumX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimumXProperty =
            DependencyProperty.Register("MinimumX", typeof(double), typeof(PlotDrawer), new PropertyMetadata(default(double), GridSizePropertiesChanged));

        public double MaximumX
        {
            get { return (double)GetValue(MaximumXProperty); }
            set { SetValue(MaximumXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaximumX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumXProperty =
            DependencyProperty.Register("MaximumX", typeof(double), typeof(PlotDrawer), new PropertyMetadata(default(double), GridSizePropertiesChanged));

        public double MinimumY
        {
            get { return (double)GetValue(MinimumYProperty); }
            set { SetValue(MinimumYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinimumY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimumYProperty =
            DependencyProperty.Register("MinimumY", typeof(double), typeof(PlotDrawer), new PropertyMetadata(default(double), GridSizePropertiesChanged));

        public double MaximumY
        {
            get { return (double)GetValue(MaximumYProperty); }
            set { SetValue(MaximumYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaximumY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumYProperty =
            DependencyProperty.Register("MaximumY", typeof(double), typeof(PlotDrawer), new PropertyMetadata(default(double), GridSizePropertiesChanged));

        public Brush GridBackround
        {
            get { return (Brush)GetValue(GridBackroundProperty); }
            set { SetValue(GridBackroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GridBackround.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridBackroundProperty =
            DependencyProperty.Register("GridBackround", typeof(Brush), typeof(PlotDrawer), new PropertyMetadata(null, GridSizePropertiesChanged));

        #endregion

        #region Members

        private PlotGrid gridDrawing = new PlotGrid();

        #endregion

        #region Constructors

        public PlotDrawer()
        {
        }

        #endregion

        #region Private Methods

        private void UpdateGridDrawingProperties()
        {
            gridDrawing.MinX = MinimumX;
            gridDrawing.MaxX = MaximumX;
            gridDrawing.MinY = MinimumY;
            gridDrawing.MaxY = MaximumY;
            gridDrawing.ParentWidth = Width;
            gridDrawing.ParentHeight = Height;
            gridDrawing.GridBackround = GridBackround;
        }

        #endregion

        #region Dependency properties event handlers

        private static void GridSizePropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PlotDrawer sender = (PlotDrawer)d;
            sender.UpdateGridDrawingProperties();
            DrawingVisual drawing = sender.gridDrawing.DrawGrid();
            sender.AddChild(drawing);
        }

        #endregion

    }
}
