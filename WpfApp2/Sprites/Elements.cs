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
    class Elements : VM
    {
        public ObservableCollection<SqareVM> Squares1 { get; set; } =
         new ObservableCollection<SqareVM>()
         {
                new SqareVM() { Position = new Point( 30,  30),
                Color = Brushes.LimeGreen , Id=3,Text="Левая граница",Width=SizeText("Левая граница" ) - 31 },

                 new SqareVM() { Position = new Point( 30,  70),
                Color = Brushes.LimeGreen , Id=3,Text="Правая граница",Width=SizeText("Правая граница" ) - 31 },
                  new SqareVM() { Position = new Point( 30,  110),
                Color = Brushes.LimeGreen , Id=3,Text="Верхняя граница",Width=SizeText("Верхняя граница" )-31 },
                   new SqareVM() { Position = new Point( 30,  150),
                Color = Brushes.LimeGreen , Id=3,Text="Нижняя граница",Width=SizeText("Нижняя граница" ) -30 }

          };

        static int SizeText(string scriptText)
        {
            return scriptText.Length * 10 + 20;
        }
    }
}
