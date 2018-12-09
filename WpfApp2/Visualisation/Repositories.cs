using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace WpfApp2.Visualisation
{
    public static class Repositories
    {
        //   public static List<List<Sprites.SqareVM>> ListSprites { get; set; }
        //   public static Point Position;  
        //   public static double ActualHeightLast { get; set; }
        // public static List<double> Y1;
        //  public static List<double> Y2;

        public static int StartIndex;   //первый элемент - 1, следующий - 2 просто целочисленное число
        public static Point StartPOint; // начальная позиция скрипта


        public static List<string> ListLevel; //список номеров 1, 1.1, 2, 2.1, 2.2  не используется
        public static List<Point> PointStart; // список координат  не используется
        public static List<Point> PointEnd; // список координат       не используется
        public static List<UserSprites.UserControl1> ListUserControl;   // список usercontrol не используется

        public static List<Shape> ListShapes;  //список на добавление на кнопку
        public static List<Rectangle> ListBorder; //список границ 
    }
}
