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
    class Event : VM
    {
        public ObservableCollection<SqareVM> Squares1 { get; } =
            new ObservableCollection<SqareVM>()
            {                           
            new SqareVM() { Position = new Point( 30,  20),
                Color = Brushes.HotPink, Id=1, Text="Повернуть вверх",Width=SizeText("Повернуть вверх") - 31},
             new SqareVM() { Position = new Point( 30,  60),
                Color = Brushes.HotPink, Id=1, Text="Повернуть вниз",Width=SizeText("Повернуть вниз") - 31},
              new SqareVM() { Position = new Point( 30,  100),
                Color = Brushes.HotPink, Id=1, Text="Повернуть влево",Width=SizeText("Повернуть влево") - 31},
               new SqareVM() { Position = new Point( 30,  140),
                Color = Brushes.HotPink, Id=1, Text="Повернуть вправо",Width=SizeText("Повернуть вправо") - 31}

            };



        static int SizeText(string scriptText)
        {
            return scriptText.Length * 10 + 20;
        }
    }
}
