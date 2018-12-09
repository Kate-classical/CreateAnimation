using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp2.Interface;
using WpfApp2.Sprites;
using WpfApp2.UserSprites;
using WpfApp2.Visualisation;

namespace WpfApp2.Model
{
    public class EllipsModel : IModel
    {

        private static Ellipse ellipse;

        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {            
            if (e.LeftButton is MouseButtonState.Pressed)
            {                
                Point startPoint = Mouse.GetPosition(this.CurrentWindow.pictureBox);

                
                if (Cache.Ok)             
                {
                    ellipse = new Ellipse();
                    Cache.StartCoordinates = startPoint;                   
                    ellipse.Width = 0;
                    ellipse.Height = 0;
                    ellipse.Fill = Cache.ColorFill; //Cache.ColorFill; //(Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString(Cache.ColorFill.ToString());
                    ellipse.Stroke = Cache.ColorStroke;   //new SolidColorBrush(Cache.ColorStroke);
                    ellipse.StrokeThickness = Cache.Thickess;
                    Canvas.SetLeft(ellipse, startPoint.X);
                    Canvas.SetTop(ellipse, startPoint.Y);

                    this.CurrentWindow.pictureBox.Children.Add(ellipse);
                    Cache.Ok = false;
                    Cache.LastShape = ellipse;
                    
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

        

      

        

       

      

        public UserControl RetUser(SqareVM square)
        {
            UserControl1 userControl = new UserControl1();
            userControl.DataContext = new SqareVM()
            {
                Position = square.Position,
                PointWhike = square.PointWhike,
                Color = square.Color,
                Id = square.Id,
                Text = square.Text,
                Width = SizeText(square.Text),
                HeightFor = square.HeightFor,
                HeighDown = square.HeighDown
            };

            int SizeText(string scriptText)
            {
                return scriptText.Length * 10 + 20;
            }
            return userControl;
        }

        

        
        
    }
}
