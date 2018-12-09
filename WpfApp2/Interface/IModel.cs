using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using WpfApp2.Model;

namespace WpfApp2.Interface
{
    public class IModel : IEventMouse 
    {
        public IModel()
        {
            
        }
        public virtual void Transform(Shape shape)
        {
            Point currentPosition = Mouse.GetPosition(Cache.NowModel.CurrentWindow.pictureBox);
            Point shapeMousePosition = Mouse.GetPosition(shape);
            shape.Margin = new Thickness(0, 0, 0, 0);

            double calculateX = 0;
            double calculateY = 0;

            if (shape.ActualWidth > shapeMousePosition.X)
            {
                calculateX = currentPosition.X - shapeMousePosition.X;
            }

            if (shape.ActualHeight > shapeMousePosition.Y)
            {
                calculateY = currentPosition.Y - shapeMousePosition.Y;
            }
            //                 Текущая позиция мыши   Актуальная ширина                          Позиция мыши при захвате фигуры
            Canvas.SetLeft(shape, currentPosition.X - shape.ActualWidth);
            Canvas.SetTop(shape, currentPosition.Y - shape.ActualHeight);

        }

        public void IncreaseSize(Point startPoint)
        {
            double currentHeight = startPoint.Y - Cache.StartCoordinates.Value.Y;
            double currentWidth = startPoint.X - Cache.StartCoordinates.Value.X;

            Tuple<double, double> calculateHeightAndWidth = new Tuple<double, double>
                (
                         (currentHeight < 0) ? ((Cache.LastShape.ActualHeight + currentHeight) < 50 ? 0 : currentHeight) : currentHeight,
                         (currentWidth < 0) ? ((Cache.LastShape.ActualWidth + currentWidth) < 50 ? 0 : currentWidth) : currentWidth
                );

            Cache.LastShape.Height += calculateHeightAndWidth.Item1;
            Cache.LastShape.Width += calculateHeightAndWidth.Item2;

            Cache.StartCoordinates = startPoint;
        }

        
    }
}
