using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp2.Sprites
{
    class Conditions : VM
    {
        public ObservableCollection<SqareVM> Squares1 { get; } =
            new ObservableCollection<SqareVM>()
            {
            new SqareVM() { Position = new Point( 30,  20),HeightFor=30, HeighDown = 20,
                Color = Brushes.BlueViolet, Id=5, Text="Если нажата вверх",Width=SizeText("Если нажата вверх") - 41, },

             new SqareVM() { Position = new Point( 30,  110),HeightFor=30, HeighDown = 20,
                Color = Brushes.BlueViolet, Id=5, Text="Если нажата вниз",Width=SizeText("Если нажата вниз") - 41},

              new SqareVM() { Position = new Point( 30,  200),HeightFor=30, HeighDown = 20,
                Color = Brushes.BlueViolet, Id=5, Text="Если нажата влево",Width=SizeText("Если нажата влево") - 41},

               new SqareVM() { Position = new Point( 30,  290),HeightFor=30, HeighDown = 20,
                Color = Brushes.BlueViolet, Id=5, Text="Если нажата вправо",Width=SizeText("Если нажата вправо") - 41},

                new SqareVM() { Position = new Point( 30,  380),HeightFor=30, HeighDown = 20,
                Color = Brushes.BlueViolet, Id=5, Text="Иначе",Width=SizeText("Иначе") }

            };



        static int SizeText(string scriptText)
        {
            return scriptText.Length * 10 + 20;
        }
    }
}
