using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp2.Sprites
{
    public class SqareVM : VM , ICloneable
    {
        public SqareVM()
        {
            RequestMove = new SimpleCommand<Point>(MoveTo);
        }

        public ICommand RequestMove { get; }

        void MoveTo(Point newPosition)
        {
            Position = newPosition;
        }



        public object Clone()
        {
            return new SqareVM {
                Position = this.Position,
                Color = this.Color,
                Width = this.Width,
                HeighDown = this.HeighDown,
                HeightFor = this.HeightFor,
                Text = this.Text,
                PointWhike = this.PointWhike,
                Id = this.Id };
        }

        int znatch;
        public int Znatch
        {
            get { return znatch; }
            set { if (znatch != value) { znatch = value; NotifyPropertyChanged(); } }
        }

        Point position;
        public Point Position
        {
            get { return position; }
            set { if (position != value) { position = value; NotifyPropertyChanged(); } }
        }

        Brush color;
        public Brush Color
        {
            get { return color; }
            set { if (color != value) { color = value; NotifyPropertyChanged(); } }
        }

        int width;
        public int Width
        {
            get { return width; }
            set { if (width != value) { width = value; NotifyPropertyChanged(); } }
        }

        int heightDown;
        public int HeighDown
        {
            get { return heightDown; }
            set { if (heightDown != value) { heightDown = value; NotifyPropertyChanged(); } }
        }

        int heightFor;
        public int HeightFor
        {
            get { return heightFor; }
            set { if (heightFor != value) { heightFor = value; NotifyPropertyChanged(); } }
        }

        String text;
        public string Text
        {
            get { return text; }
            set { if (text != value) { text = value; NotifyPropertyChanged(); } }
        }

        int id;
        public int Id
        {
            get { return id; }
            set { if (id != value) { id = value; NotifyPropertyChanged(); } }
        }

        double pointWhile;
        public double PointWhike
        {
            get { return pointWhile; }
            set { if (pointWhile != value) { pointWhile = value; NotifyPropertyChanged(); } }
        }
    }
}
