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

namespace WpfApp2.Model
{
    public class LineModel : IModel
    {
        Line line;

        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {            
            if (e.LeftButton is MouseButtonState.Pressed)
            {
                Point startPoint = Mouse.GetPosition(this.CurrentWindow.pictureBox);

                if (Cache.Ok)
                {
                    line = new Line();

                    
                    line.X1 = Cache.StartCoordinates.Value.X;
                    line.Y1 = Cache.StartCoordinates.Value.Y;
                    line.SnapsToDevicePixels = true;

                    line.StrokeThickness = Cache.Thickess;
                    line.Stroke = Cache.ColorStroke;
                                        
                    this.CurrentWindow.pictureBox.Children.Add(line);


                    Cache.Ok = false;
                    Cache.LastShape = line;
                }
                else
                {
                    line.X2 = startPoint.X ;
                    line.Y2 = startPoint.Y;

                    if (line.X1 > line.X2)
                    {
                        Canvas.SetLeft(line, line.X2);
                    }
                    else
                    {
                        Canvas.SetLeft(line, line.X1);
                    }

                    if (line.Y1 > line.Y2)
                    {
                        Canvas.SetTop(line, Cache.StartCoordinates.Value.Y - line.Y2);
                    }
                    else
                    {
                        Canvas.SetTop(line, Cache.StartCoordinates.Value.Y);
                    }

                }

            }   
        }
        public override void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {            

           
            Cache.StartCoordinates = null;

        }

        
    }
}
