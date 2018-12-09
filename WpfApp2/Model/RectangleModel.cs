using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using WpfApp2.Interface;

namespace WpfApp2.Model
{
    public class RectangleModel : IModel
    {
        private Rectangle rectangle;
       
        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (e.LeftButton is MouseButtonState.Pressed)
            {
                Point startPoint = Mouse.GetPosition(this.CurrentWindow.pictureBox);


                if (Cache.Ok)
                {
                    rectangle = new Rectangle();
                    Cache.StartCoordinates = startPoint;
                    rectangle.Width = 0;
                    rectangle.Height = 0;
                    rectangle.Fill = Cache.ColorFill;
                    rectangle.Stroke = Cache.ColorStroke;
                    rectangle.StrokeThickness = Cache.Thickess;
                    Point shapeMousePosition = Mouse.GetPosition(rectangle);
                    Canvas.SetLeft(rectangle, startPoint.X);
                    Canvas.SetTop(rectangle, startPoint.Y);
                
                    this.CurrentWindow.pictureBox.Children.Add(rectangle);
                    Cache.Ok = false;
                    Cache.LastShape = rectangle;

                }
                else
                {
                    //  if (What.Helpers.GetElement(this.CurrentWindow) != null)
                    {
                        IncreaseSize(startPoint);
                    }
                }
            }
        }
               

            
        
    }
}
