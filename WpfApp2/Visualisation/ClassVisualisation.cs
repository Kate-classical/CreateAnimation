using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp2.UserSprites;
using WpfApp2.Sprites;
using System.Windows.Input;
using System.Windows;

namespace WpfApp2.Visualisation
{
    public class ClassVisualisation
    {
        private Canvas shape;
        private List<UserControl1> ListUsers = new List<UserControl1>();
        double xxx;
        double yyy;
        int napr = 2;
        int i = 1;
        int nnn = 0; //счетчик n

        int IndexCirce = 0; //с какого начинать бесконечный цикл
        int IndexCirecleEnd = 0; //каким заканчивается бесконечный цикл
        int speed = 1;


        SqareVM sqare { get; set; }

        public ClassVisualisation(Canvas shape)
        {
            if (shape != null)
            {
                this.shape = shape;
                ListUsers = InforOfSprites.ListUserControl;
                xxx = Canvas.GetLeft(shape);
                yyy = Canvas.GetTop(shape);
                InforOfSprites.visualis = true;
                CompositionTarget.Rendering += RenderFrame;
            }
            else
            {
                MessageBox.Show("Выбереите элемент");
            }
        }
                
        private void RenderFrame(object sender, EventArgs e)
        {           
            if (InforOfSprites.visualis && i != ListUsers.Count())
            {                
                sqare = (SqareVM)ListUsers[i].DataContext as SqareVM;
                
                Circle();
                Conditions();
                Motions();

                if (sqare.Znatch != 0)
                {
                    if (nnn == sqare.Znatch * 10)
                    {
                        nnn = 0;
                        i++;
                    }
                }
                if(sqare.Text == "Идти вперед")
                {
                    speed = 1;
                }
               

            }
            else
            {
                CompositionTarget.Rendering -= RenderFrame;
            }
          
        }

        private void Motions()
        {           

            if (sqare.Text == "Идти вперед")
            {
                SetDirection(napr);
            }
            if (sqare.Text == "Влево на")
            {
                SetDirection(0);
                nnn++;
            }
            if (sqare.Text == "Вперёд на")
            {
                SetDirection(1);
                nnn++;
            }
            if (sqare.Text == "Вправо на")
            {
                SetDirection(2);
                nnn++;
            }
            if (sqare.Text == "Назад на")
            {
                SetDirection(3);
                nnn++;
            }
        }
        private void Conditions()
        {
            if (sqare.Text == "Если нажата вверх")
            {
                if (Keyboard.IsKeyDown(Key.Up))
                {
                    napr = 1;
                }
            }
            if (sqare.Text == "Если нажата вниз")
            {
                if (Keyboard.IsKeyDown(Key.Down))
                {
                    napr = 3;
                }
            }
            if (sqare.Text == "Если нажата влево")
            {
                if (Keyboard.IsKeyDown(Key.Left))
                {
                    napr = 0;
                }
            }
            if (sqare.Text == "Если нажата вправо")
            {
                if (Keyboard.IsKeyDown(Key.Right))
                {
                    napr = 2;
                }
            }
        }
        private void Circle()
        {
            if (sqare.Text == "Бесконечно")
            {               
                IndexCirce = i;
                IndexCirecleEnd = LisrCircle(i).Last();
                CompositionTarget.Rendering -= RenderFrame;
                CompositionTarget.Rendering += RenderEndlesse;
            }
            if (sqare.Text == "Повторять раз")
            {
                IndexCirce = i;
            
                IndexCirecleEnd = LisrCircle(i).Last() - 1;

                int n = ListUsers.Count - (IndexCirecleEnd - IndexCirce) - 1;
                ListUsers.Remove(ListUsers[IndexCirce]);

                int nom = 0;
                decimal bbb = IndexCirecleEnd / 2;
                for (int q = IndexCirce; q <= Math.Floor(bbb); q++)
                {
                    UserControl1 first = ListUsers[q ];
                    UserControl1 last = ListUsers[IndexCirecleEnd - nom];

                    ListUsers[q] = last;
                    ListUsers[IndexCirecleEnd - nom] = first;
                    nom++;
                }

               
                List<UserControl1> newList = new List<UserControl1>();
                while (sqare.Znatch != 1)
                {
                    while (IndexCirce != IndexCirecleEnd + 1)
                    {
                        newList.Add(ListUsers[IndexCirce]);
                        IndexCirce++;
                    }
                    sqare.Znatch--;
                }

                for(int w = 0; w < newList.Count; w++)
                {
                    ListUsers.Insert(IndexCirce + w, newList[w]);
                }
                

            
                
            }
        }
       

        private void RenderEndlesse(object sender, EventArgs e)  //бесконечный цикл
        {           
            if (InforOfSprites.visualis)
            {
                if (i == IndexCirce)
                { i = IndexCirecleEnd; }

                if (sqare.Znatch != 0)
                {
                    if (nnn == sqare.Znatch * 10)
                    {
                        nnn = 0;
                        i--;
                    }                   
                }
                else
                {
                    i--;
                }

                

                sqare = (SqareVM)ListUsers[i].DataContext as SqareVM;

                if (sqare.Text == "Идти вперед")
                {
                    speed = 3;
                }
                Motions();
                Conditions();
                Circle();            
               
             }
            else
            {
                CompositionTarget.Rendering -= RenderEndlesse;
            }
        }

        private List<int> LisrCircle(int n)  //список индексов входящих в цикл
        {
            double yy = Canvas.GetTop(ListUsers[n]) + ListUsers[n].ActualHeight;
            List<int> users = new List<int>();
            n++;

            for(; n < ListUsers.Count(); n++)
            {
                if (Canvas.GetTop(ListUsers[n]) < yy)
                    users.Add(n);
            }
                       

            return users;
        }

        private void SetDirection(int i) //изменение направления по индексу
        {
           
            if(i == 0)
            {
                Canvas.SetLeft(shape, Canvas.GetLeft(shape) - speed);             
            }
            if(i == 1)
            {            
                Canvas.SetTop(shape, Canvas.GetTop(shape) - speed);
            }
            if(i == 2)
            {
                Canvas.SetLeft(shape, Canvas.GetLeft(shape) + speed);
            }
            if(i == 3)
            {
                Canvas.SetTop(shape, Canvas.GetTop(shape) + speed);
            }
        }












        private void KeyLeft()
        {
            if (Keyboard.IsKeyDown(Key.Left))
            {
                Canvas.SetLeft(shape, Canvas.GetLeft(shape) - 3);
                Canvas.SetTop(shape, Canvas.GetTop(shape));
                i++;
            }
        }

        private void KeyDown()
        {
            if (Keyboard.IsKeyDown(Key.Down))
            {
                Canvas.SetLeft(shape, Canvas.GetLeft(shape));
                Canvas.SetTop(shape, Canvas.GetTop(shape) + 3);
            }
        }

        private void KeyUp()
        {
            if (Keyboard.IsKeyDown(Key.Up))
            {
                napr = 1;
               // Canvas.SetLeft(shape, Canvas.GetLeft(shape));
               // Canvas.SetTop(shape, Canvas.GetTop(shape) - 3);
            }
        }

        private void KeyRight(ref int i)
        {
            if (Keyboard.IsKeyDown(Key.Right))
            {
                Canvas.SetLeft(shape, Canvas.GetLeft(shape) + 3);
                Canvas.SetTop(shape, Canvas.GetTop(shape));                
            }           
        }

    }
}
