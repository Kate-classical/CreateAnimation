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
    class Control : VM
    {
        public ObservableCollection<SqareVM> Squares1 { get; } =
          new ObservableCollection<SqareVM>()
          {
                new SqareVM() { Position = new Point( 30,  30),
                Color = Brushes.Brown , Id=3,Text="Когда запущен",Width=120},

                 new SqareVM() { Position = new Point( 30,  70),
                Color = Brushes.Brown , Id=3,Text="Остановить",Width= 100 }

                
           };

        static int SizeText(string scriptText)
        {
            return scriptText.Length * 10 + 20;
        }
    }
}
