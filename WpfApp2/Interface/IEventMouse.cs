using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp2.Interface
{
    public class IEventMouse
    {
        public MainWindow CurrentWindow { get; set; }
       

        public virtual void AddHandler()
        {            
            Mouse.AddMouseDownHandler(this.CurrentWindow.pictureBox, MouseDownHandler);
            Mouse.AddMouseUpHandler(this.CurrentWindow.pictureBox, MouseUpHandler);
            Mouse.AddMouseMoveHandler(this.CurrentWindow.pictureBox, MouseMoveHandler);
        }

        public virtual void RemoveHandler()
        {
            Mouse.RemoveMouseDownHandler(this.CurrentWindow.pictureBox, MouseDownHandler);
            Mouse.RemoveMouseUpHandler(this.CurrentWindow.pictureBox, MouseUpHandler);
            Mouse.RemoveMouseMoveHandler(this.CurrentWindow.pictureBox, MouseMoveHandler);
        }

        public virtual void MouseMoveHandler(object sender, MouseEventArgs e)
        {
           // MessageBox.Show("MouseMove");
        }

        public virtual void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            Cache.StartCoordinates = null;
        }

        public virtual void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {           
            Cache.Ok = true;            
            Cache.StartCoordinates = e.GetPosition(this.CurrentWindow.pictureBox);
        }

    }
}
