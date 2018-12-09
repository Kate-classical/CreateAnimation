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
    public class CursorModel : IModel
    {
        private IModel Model;
        private Shape _shape;

        public override void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if (_shape  != null)
                {
                    Model.Transform(_shape);
                }
            }
        }

        public override void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
          //  Canvas _gr = (Canvas)sender;
            HitTestResult Result = VisualTreeHelper.HitTest(Cache.NowModel.CurrentWindow.pictureBox, e.GetPosition(Cache.NowModel.CurrentWindow.pictureBox));
                     
            if(Result.VisualHit is Ellipse )
            {
                _shape = (Ellipse)Result.VisualHit;
                SetModel<EllipsModel>();
            }

            if (Result.VisualHit is Rectangle)
            {
                _shape = (Rectangle)Result.VisualHit;
                SetModel<RectangleModel>();
            }

            if(Result.VisualHit is Line)
            {
                _shape = (Line)Result.VisualHit;
                SetModel<LineModel>();
            }

        }


        private void SetModel<T>() where T : IModel, new()
        {
            T model = new T();
            model.CurrentWindow = this.CurrentWindow;
            Model = model;
        }
    }
}
