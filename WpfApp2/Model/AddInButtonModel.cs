using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp2.Interface;
using WpfApp2.Sprites;
using WpfApp2.Visualisation;

namespace WpfApp2.Model
{
    class AddInButtonModel : IModel
    {
        Point pS { get; set; }
        Point pE { get; set; }
        Canvas rectangle { get; set; }

        Canvas rec { get; set; } //объединение объектов
     //   List<Shape> shapes { get; set; }

        public AddInButtonModel()
        {
            List<double> listX = new List<double>();
            List<double> listY = new List<double>();

            for (int i = 0; i < Repositories.ListShapes.Count(); i++)
            {
                var sh = Repositories.ListShapes[i];

                listX.Add(Canvas.GetLeft(sh));
                listY.Add(Canvas.GetTop(sh));
                listX.Add(Canvas.GetLeft(sh) + sh.ActualWidth);
                listY.Add(Canvas.GetTop(sh) + sh.ActualHeight);

                Cache.NowModel.CurrentWindow.pictureBox.Children.Remove(Repositories.ListBorder[0]);
                Repositories.ListBorder.Remove(Repositories.ListBorder[0]);
            }

            listY.Sort();
            listX.Sort();

            try
            {

                pS = new Point(listX.First(), listY.First()); //начальные координаты
                pE = new Point(listX.Last(), listY.Last());  //конечные координаты

                rec = new Canvas();
                rec.Height = pE.Y - pS.Y;
                rec.Width = pE.X - pS.X;


                CreatButtonAll();

                Cache.NowModel.CurrentWindow.pictureBox.Children.Add(rec);
                Canvas.SetLeft(rec, 0);
                Canvas.SetTop(rec, 0);
            }
            catch
            {
                MessageBox.Show("Создайте элемент");

            }

            
        }


        public void CreatButtonAll()
        {
            rectangle = new Canvas();

            WindowName passwordWindow = new WindowName();

            if (passwordWindow.ShowDialog() == true)
            {
                try
                {
                    rectangle.Name = passwordWindow.Password;
                }
                catch
                {
                    MessageBox.Show("Создайте объект с другим именем");
                   
                }
            }
            else
            {
                MessageBox.Show("Авторизация не пройдена");
            }

           /* Elements elements = new Elements();

            elements.Squares1.Add(new SqareVM()
            {
                Position = new Point(30, elements.Squares1.Last().Position.Y + 40),
                Color = Brushes.LimeGreen,
                Id = 3,
                Text = rectangle.Name,
                Width = 100
            });*/

            GetRectangle();
           
            CreateButton();

            Cache.NowModel.CurrentWindow.Arrange(new Rect(0, 0, Cache.NowModel.CurrentWindow.Width, Cache.NowModel.CurrentWindow.Height));

            Repositories.ListShapes = new List<Shape>();
        }
       
      

      

        private void GetRectangle( )   //добавляет всё в datacontext
        {
            for (int i = 0; i < Repositories.ListShapes.Count(); i++)
            {
                var element = XamlClone(Repositories.ListShapes[i]);
                double chast = 0;

                if (pE.X - pS.X > pE.Y - pS.Y)
                {
                    chast = (pE.X - pS.X) / 78;
                }
                else
                {
                    chast = (pE.Y - pS.Y) / 78;
                }

              
                double nowW = element.Width / chast;
                double nowY = element.Height / chast;
                element.Width = nowW;
                element.Height = nowY;
                                
                rectangle.Children.Add(element);
                rectangle.Background = new SolidColorBrush(Colors.Black);

                Canvas.SetLeft(element, (Canvas.GetLeft(Repositories.ListShapes[i]) - pS.X ) /chast - 39);
                Canvas.SetTop(element, (Canvas.GetTop(Repositories.ListShapes[i]) - pS.Y ) / chast - 39);

                Cache.NowModel.CurrentWindow.pictureBox.Children.Remove(Repositories.ListShapes[i]);
                rec.Children.Add(Repositories.ListShapes[i]);
            }
        
        }



        public T XamlClone<T>(T source)
        {
            string savedObject = System.Windows.Markup.XamlWriter.Save(source);
            StringReader stringReader = new StringReader(savedObject);
            System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(stringReader);
            T target = (T)System.Windows.Markup.XamlReader.Load(xmlReader);
            return target;
        }

        public Button CreateButton() //создание кнопки
        {
            List<Sprites.SqareVM> spr = new List<Sprites.SqareVM>();           
            int i = 0;
            Button button = new Button();
            Cache.NowModel.CurrentWindow.repImages.Children.Add(button);

            button.Content = rectangle;
         
            button.Background = Brushes.White;
            button.TabIndex = Cache.IndexButton;
            button.Width = Cache.NowModel.CurrentWindow.repImages.Width;
            button.Height = Cache.NowModel.CurrentWindow.repImages.Width;
            button.BorderBrush = Brushes.LightBlue;

            button.Click += (s, e) =>
            {
                if (i == 0)
                {
                    InforOfSprites.ReakElement = rec;
                    button.Background = Brushes.DodgerBlue;
                    Cache.NowElement = button.TabIndex;
                    i++;
                }
                else
                {
                    button.Background = Brushes.White;
                    //   for(int j = 0; j < Repositories.ListSprites[Cache.NowElement].Count; j++)
                    {
                       // Cache.NowModel.CurrentWindow.Compilar.Children.RemoveAt(0);
                    }
                    InforOfSprites.ReakElement = null;

                    i--;
                }
            };
            button.MouseRightButtonDown += (s, e) =>
            {
                MessageBox.Show(rectangle.Name);
            };

            Cache.IndexButton++;
            return button;
        }
    }
}
