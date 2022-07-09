using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace lab4_multiparadigma.Resources
{
    public class RadialPanel : Panel
    {
        //Referencia http://jobijoy.blogspot.com/2008/04/simple-radial-panel-for-wpf-and.html?m=1
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement element in Children)

            {


                element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            }

            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)

        {

            if (Children.Count == 0)
            {
                return finalSize;
            }


            double _angle = 0;
            double _incrementalAngularSpace = (360.0 / Children.Count) * (Math.PI / 180);
            double radiusX = finalSize.Width / 2;
            double radiusY = finalSize.Height / 2;

            foreach (UIElement element in Children)

            {
                Point childPoint = new Point(Math.Cos(_angle) * radiusX/2, -Math.Sin(_angle) * radiusY/2);
                Point actualChildPoint = new Point(finalSize.Width / 2 + childPoint.X - element.DesiredSize.Width / 2, finalSize.Height / 2 + childPoint.Y - element.DesiredSize.Height / 2);
                element.Arrange(new Rect(actualChildPoint.X, actualChildPoint.Y, element.DesiredSize.Width, element.DesiredSize.Height));
                _angle += _incrementalAngularSpace;
            }
            return finalSize;
        }
    }
}
