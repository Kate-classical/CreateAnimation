using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp2.Sprites
{
    class Circle : VM
    {
        public ObservableCollection<SqareVM> Squares1 { get; } =
           new ObservableCollection<SqareVM>()
           {
                new SqareVM() { Position = new Point( 20,  20),
                Color = Brushes.Orange , Id=2,Text="Повторять секунд",Width=SizeText("Повторять секунд" ) - 10, PointWhike = 21, HeightFor=30, HeighDown = 20 },

                new SqareVM() { Position = new Point( 20, 110),
                Color = Brushes.Orange, Id=2, Text="До столкновения с",Width=SizeText("До столкновения с") - 41 ,HeightFor=30, HeighDown = 20 },

            new SqareVM() { Position = new Point( 20,  200),
                Color = Brushes.Orange, Id=2, Text="Бесконечно",Width=SizeText("Бесконечно"), HeightFor=30,  HeighDown = 20   },

              new SqareVM() { Position = new Point( 20,  290),PointWhike = 21,
                Color = Brushes.Orange, Id=2, Text="Повторять раз",Width=SizeText("Повторять раз") - 10, HeightFor=30,  HeighDown = 20   }

            };

        static int SizeText(string scriptText)
        {
            return scriptText.Length * 10 + 20;
        }
    }
}
