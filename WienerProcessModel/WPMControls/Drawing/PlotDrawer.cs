using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        public IEnumerable Functions
        {
            get { return (IEnumerable)GetValue(FunctionsProperty); }
            set { SetValue(FunctionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Functions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FunctionsProperty =
            DependencyProperty.Register("Functions", typeof(IEnumerable), typeof(PlotDrawer), new PropertyMetadata(null, FunctionsPropertyChanged));
        
        public double MinimumX
        {
            get { return (double)GetValue(MinimumXProperty); }
            set 
            {
                SetValue(MinimumXProperty, value);
                UpdateGridDrawingProperties();
            }
        }

        // Using a DependencyProperty as the backing store for MinimumX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimumXProperty =
            DependencyProperty.Register("MinimumX", typeof(double), typeof(PlotDrawer), new PropertyMetadata(default(double), GridPropertiesChanged));

        public double MaximumX
        {
            get { return (double)GetValue(MaximumXProperty); }
            set 
            {
                SetValue(MaximumXProperty, value);
                UpdateGridDrawingProperties();
            }
        }

        // Using a DependencyProperty as the backing store for MaximumX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumXProperty =
            DependencyProperty.Register("MaximumX", typeof(double), typeof(PlotDrawer), new PropertyMetadata(default(double), GridPropertiesChanged));

        public double MinimumY
        {
            get { return (double)GetValue(MinimumYProperty); }
            set 
            {
                SetValue(MinimumYProperty, value);
                UpdateGridDrawingProperties();
            }
        }

        // Using a DependencyProperty as the backing store for MinimumY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimumYProperty =
            DependencyProperty.Register("MinimumY", typeof(double), typeof(PlotDrawer), new PropertyMetadata(default(double), GridPropertiesChanged));

        public double MaximumY
        {
            get { return (double)GetValue(MaximumYProperty); }
            set 
            { 
                SetValue(MaximumYProperty, value);
                UpdateGridDrawingProperties();
            }
        }

        // Using a DependencyProperty as the backing store for MaximumY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumYProperty =
            DependencyProperty.Register("MaximumY", typeof(double), typeof(PlotDrawer), new PropertyMetadata(default(double), GridPropertiesChanged));

        public Brush GridBackround
        {
            get { return (Brush)GetValue(GridBackroundProperty); }
            set 
            {
                SetValue(GridBackroundProperty, value);
                UpdateGridDrawingProperties();
            }
        }

        // Using a DependencyProperty as the backing store for GridBackround.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridBackroundProperty =
            DependencyProperty.Register("GridBackround", typeof(Brush), typeof(PlotDrawer), new PropertyMetadata(null, GridPropertiesChanged));

        #endregion

        #region Members

        private PlotGrid gridDrawing = new PlotGrid();

        #endregion

        #region Constructors

        public PlotDrawer()
        {
            this.Loaded += (o, e) => OnGridPropertyChanged();
            this.SizeChanged += (o, e) => OnGridPropertyChanged();
        }

        #endregion

        #region Private Methods

        private void UpdateGridDrawingProperties()
        {
            gridDrawing.MinX = MinimumX;
            gridDrawing.MaxX = MaximumX;
            gridDrawing.MinY = MinimumY;
            gridDrawing.MaxY = MaximumY;
            gridDrawing.ParentWidth = ActualWidth;
            gridDrawing.ParentHeight = ActualHeight;
            gridDrawing.GridBackround = GridBackround;
            gridDrawing.Functions = Functions;
        }

        private void OnGridPropertyChanged()
        {
            GridPropertiesChanged(this, new DependencyPropertyChangedEventArgs(MinimumXProperty, 0, 0));
        }

        private void OnFunctionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //just redraw all
            OnGridPropertyChanged();
        }

        #endregion

        #region Dependency properties event handlers

        private static void GridPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PlotDrawer sender = (PlotDrawer)d;
            sender.UpdateGridDrawingProperties();
            DrawingVisual drawing = sender.gridDrawing.DrawGrid();
            if (sender.ChildrenCount == 0)
                sender.AddChild(drawing);
            else
                sender.SetChild(drawing, 0);
        }

        private static void FunctionsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PlotDrawer sender = (PlotDrawer)d;
            if (e.OldValue is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)e.OldValue).CollectionChanged -= sender.OnFunctionsCollectionChanged;
            }
            if (e.NewValue is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)e.NewValue).CollectionChanged -= sender.OnFunctionsCollectionChanged;
            }
            GridPropertiesChanged(d, e);
        }

        #endregion

    }
}
