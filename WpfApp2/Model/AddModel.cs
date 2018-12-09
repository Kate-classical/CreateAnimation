using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp2.Interface;
using WpfApp2.Visualisation;

namespace WpfApp2.Model
{
    public class AddModel : IModel
    {
        private Rectangle rectangle;    
        private Shape _shape;

        public override void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            HitTestResult Result = VisualTreeHelper.HitTest(Cache.NowModel.CurrentWindow.pictureBox, e.GetPosition(Cache.NowModel.CurrentWindow.pictureBox));

            if (Result.VisualHit is Ellipse)
            {
                _shape = (Ellipse)Result.VisualHit;               
            }

            if (Result.VisualHit is Rectangle)
            {
                _shape = (Rectangle)Result.VisualHit;              
            }

            if (Result.VisualHit is Line)
            {
                _shape = (Line)Result.VisualHit;             
            }

            if (_shape != null)
            {
                Point startPoint = Mouse.GetPosition(this.CurrentWindow.pictureBox);
                rectangle = new Rectangle();
                Cache.StartCoordinates = startPoint;
                rectangle.Width = _shape.ActualWidth;
                rectangle.Height = _shape.ActualHeight;
                rectangle.Stroke = new SolidColorBrush(Colors.Black);
                rectangle.StrokeThickness = 1;
                           
                {
                    rectangle.Margin = new Thickness(Canvas.GetLeft(_shape), Canvas.GetTop(_shape), 0, 0);
                }
          
                this.CurrentWindow.pictureBox.Children.Add(rectangle);
                Cache.Ok = false;
                Cache.LastShape = rectangle;

                Repositories.ListBorder.Add(rectangle);
                Repositories.ListShapes.Add(_shape);
            }

        }
       
    }
}
