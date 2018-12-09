using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp2.Interface;

namespace WpfApp2
{
    public static class Cache
    {
        public static Point? StartCoordinates { get; set; }   //начальные координаты при клике
        public static bool Ok { get; set; }  //да - когда сохранить, нет - когда не сохранять, признак:клика-> да
        public static Shape LastShape { get; set; } // рисование по этим данным

        public static IModel LastMode { get; set; }  //для сравнения и 
        public static IModel NowModel { get; set; }  //удаления событий мыши

       public static RadioButton NowRadButton { get; set; }
        public static RadioButton LastRadioButton { get; set; }

        public static int Thickess { get; set; }  // размерность линии

        public static int IndexButton { get; set; }

        public static SolidColorBrush ColorFill { get; set; }
        public static SolidColorBrush ColorStroke { get; set; }
        
              
       public static int NowElement { get; set; }
    }
}
