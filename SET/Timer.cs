using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Popups;
namespace NextAgeClock
{
    public class Timer : Grid
    {
        public Timer(double radius)
        {
            // base.Height = base.ActualWidth;
            NumericalValuesSet(radius);
            FigureSet();
            GeometrySet();
            MotionTimerSet();
            MinutesSet();
            PathSet();
        }
        public void Start()
        {
            MotionTimer.Start();
        }
        public void Stop()
        {
            MotionTimer.Stop();
        }
        //variables
        PathFigure Figure;
        int count;
        double angle, radius;
        Path path;
        PathGeometry geometry;
        DispatcherTimer MotionTimer;
        TextBlock Minutes;
        void NumericalValuesSet(double radius)
        {
            this.radius = radius;
            count = 0;
            angle = 0;
        }
        void FigureSet()
        {
            Figure = new PathFigure();
            Figure.StartPoint = new Point(radius, 0);
            Figure.Segments.Add(new ArcSegment { Point = new Point(radius + radius * Math.Sin(angle), radius - radius * Math.Cos(angle)), Size = new Size(radius, radius), SweepDirection = SweepDirection.Clockwise, IsLargeArc = false });
        }
        void GeometrySet()
        {
            geometry = new PathGeometry();
            geometry.Figures.Add(Figure);
        }
        void PathSet()
        {
            path = new Path();
            path.Stroke = new SolidColorBrush(Colors.BlueViolet);
            path.StrokeThickness = 10;
            path.Data = geometry;
            base.Children.Add(path);
        }
        void MotionTimerSet()
        {
            MotionTimer = new DispatcherTimer();
            MotionTimer.Interval = new TimeSpan(5000000);
            MotionTimer.Tick += MotionTimer_Tick;
        }

        void MotionTimer_Tick(object sender, object e)
        {
            Figure.Segments.Remove(Figure.Segments[0]);
            angle += Math.PI * 3 / 180;
            if (angle <= Math.PI)
                Figure.Segments.Add(new ArcSegment { Point = new Point(radius + radius * Math.Sin(angle), radius - radius * Math.Cos(angle)), Size = new Size(radius, radius), SweepDirection = SweepDirection.Clockwise, IsLargeArc = false });
            else
                Figure.Segments.Add(new ArcSegment { Point = new Point(radius + radius * Math.Sin(angle), radius - radius * Math.Cos(angle)), Size = new Size(radius, radius), SweepDirection = SweepDirection.Clockwise, IsLargeArc = true });
            count++;
            if (count % 120 == 0)
            {
                angle = 0;
                int x = Int32.Parse(Minutes.Text) + 1;
                Minutes.Text = x.ToString();
            }

        }
        void MinutesSet()
        {
            Minutes = new TextBlock();
            Minutes.Text = "0";
            Minutes.Foreground = new SolidColorBrush(Colors.BlueViolet);
            Minutes.FontSize = 72;
            Minutes.FontFamily = new FontFamily("MV Buli");
            Minutes.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            Minutes.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Minutes.Margin = new Thickness(this.radius / 2);
            base.Children.Add(Minutes);
        }
    }
}
