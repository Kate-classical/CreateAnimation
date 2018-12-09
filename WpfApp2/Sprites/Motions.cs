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
    class Motions : VM
    {
        public ObservableCollection<SqareVM> Squares1 { get; } =
            new ObservableCollection<SqareVM>()
            {
                new SqareVM() { Position = new Point( 30,  30),
                Color = Brushes.Blue , Id=1,Text="Вперёд на",Width=SizeText("Вперед на"), PointWhike = 21 },

                new SqareVM() { Position = new Point( 30,  70),PointWhike = 21,
                Color = Brushes.Blue, Id=1, Text="Назад на",Width=SizeText("Назад на")},

                 new SqareVM() { Position = new Point( 30,  110),PointWhike = 21,
                Color = Brushes.Blue, Id=1, Text="Влево на",Width=SizeText("Влево на")},

                  new SqareVM() { Position = new Point( 30,  150),PointWhike = 21,
                Color = Brushes.Blue, Id=1, Text="Вправо на",Width=SizeText("Вправо на")},
                  
            new SqareVM() { Position = new Point( 30,  230),
                Color = Brushes.Blue, Id=1, Text="Идти вперед",Width=SizeText("Идти вперед") - 21}

            };

        

        static int SizeText(string scriptText)
        {
            return scriptText.Length * 10 + 20;
        }
    }
}
