using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp2.UserSprites;
using WpfApp2.Visualisation;
using WpfApp2.Sprites;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfApp2.AddSprites
{
    public class Create
    {
        private UserControl1 userControl { set; get; }
        private Point posNow { get; set; }

        public Create(UserControl1 userControl, Point posNow)
        {
            this.userControl = userControl;
            this.posNow = posNow;

            CreateVoid();
        }
        public Create()
        {
            while(Cache.NowModel.CurrentWindow.Compilar.Children.Count != 0)
            {
                Cache.NowModel.CurrentWindow.Compilar.Children.RemoveAt(0);
                InforOfSprites.ListUserControl.RemoveAt(0);
            }
            InforOfSprites.ListUserControl = null;
        }

        private void CreateVoid()
        {
            SqareVM sqaren = (SqareVM)userControl.DataContext as SqareVM;
            if ( InforOfSprites.ListUserControl != null && sqaren.Text != "Когда запущен")
            {
                

                List<int> ListIndex = CheckPosition();
                if (ListIndex.Count() == 0)
                {                  
                    posNow = new Point(InforOfSprites.StartX, InforOfSprites.StartY);
                    InforOfSprites.StartY += userControl.ActualHeight - userControl.Increase.ActualHeight;
                    ClonUserControl();
                    userControl.Increase.Height = 0;
                    InforOfSprites.ListUserControl.Insert(InforOfSprites.ListUserControl.Count(), userControl);
                }
                else
                {
                    ListIndex.Sort();
                    
                    int n = ListIndex.Last();

                    SqareVM sqare = (SqareVM)InforOfSprites.ListUserControl[n].DataContext as SqareVM;

                    if (userControl.ActualHeight > 50 )
                    {                        
                        posNow = new Point(sqare.Position.X + 20, sqare.Position.Y + InforOfSprites.ListUserControl[n].ActualHeight - InforOfSprites.ListUserControl[n].BOTTON.ActualHeight);
                        InforOfSprites.StartY += userControl.ActualHeight;

                        TransforElNow(ListIndex);
                        ListIndex = GetListElBottom(n);
                        TransformElBotton(ListIndex);

                        ClonUserControl();
                        userControl.Increase.Height = 0;
                        InforOfSprites.ListUserControl.Insert( ++n, userControl);                            
                    }
                    else
                    {
                        posNow = new Point(sqare.Position.X + 20, sqare.Position.Y + InforOfSprites.ListUserControl[n].ActualHeight - InforOfSprites.ListUserControl[n].BOTTON.ActualHeight);
                        InforOfSprites.StartY += userControl.ActualHeight;

                        TransforElNow(ListIndex);
                        ListIndex = GetListElBottom(n);
                        TransformElBotton(ListIndex);

                        ClonUserControl();
                        InforOfSprites.ListUserControl.Insert(++n, userControl);
                    }
                }
            }
            else
            {               
                if (sqaren.Text == "Когда запущен" &&  InforOfSprites.ListUserControl == null)
                {
                    InforOfSprites.ListUserControl = new List<UserControl1>();
                    InforOfSprites.StartX = posNow.X;
                    InforOfSprites.StartY = posNow.Y + userControl.ActualHeight;
                    ClonUserControl();
                    InforOfSprites.ListUserControl.Insert(InforOfSprites.ListUserControl.Count(), userControl);
                }
            }
            
        }  
        
        private List<int> GetListElBottom(int n) //список элементов, которые надо сдвинуть
        {
            double realYstart = Canvas.GetTop(InforOfSprites.ListUserControl[n]);
            double realYend = Canvas.GetTop(InforOfSprites.ListUserControl[n]) + InforOfSprites.ListUserControl[n].ActualHeight;
           List<int> ListIndex = new List<int>();

            for(int i = 0; i < InforOfSprites.ListUserControl.Count(); i++)
            {            
                double elY = Canvas.GetTop(InforOfSprites.ListUserControl[i]);
        
                if(realYstart <= elY && realYend <= elY)
                {
                    ListIndex.Add(i);
                }
            }
            return ListIndex;
        }

        private void TransforElNow(List<int> ListIndex) // изменение настоящего элемента 
        {
            for (int i = 0; i < ListIndex.Count(); i++)
            {
                DoubleAnimation scriptAnimation = new DoubleAnimation();
                scriptAnimation.From = InforOfSprites.ListUserControl[ListIndex[i]].Increase.ActualHeight;
                scriptAnimation.To = InforOfSprites.ListUserControl[ListIndex[i]].Increase.ActualHeight + userControl.ActualHeight - userControl.Increase.ActualHeight;
                scriptAnimation.Duration = TimeSpan.FromSeconds(1);
                InforOfSprites.ListUserControl[ListIndex[i]].Increase.BeginAnimation(StackPanel.HeightProperty, scriptAnimation);

                InforOfSprites.ListUserControl[ListIndex[i]].Increase.Height += userControl.ActualHeight - userControl.Increase.ActualHeight;
            }
        }

        private void TransformElBotton(List<int> ListIndex)  // перемещение объектов в низ
        {
            for (int i = 0; i < ListIndex.Count(); i++)
            {             
                SqareVM sqare = (SqareVM)InforOfSprites.ListUserControl[ListIndex[i]].DataContext as SqareVM;             
                double www = Canvas.GetTop(InforOfSprites.ListUserControl[ListIndex[i]]) + userControl.ActualHeight - userControl.Increase.ActualHeight;
                Canvas.SetTop(InforOfSprites.ListUserControl[ListIndex[i]], www);           
            }       
        }

        private List<double> GetPosition(List<int> ListIndex)  //возвращает позицию добавления элемента
        {
            List<double> ListGetleft = new List<double>();
            List<double> ListGetTop = new List<double>();

            for (int i = 0; i < ListIndex.Count(); i++)
            {
                ListGetleft.Add(Canvas.GetLeft(InforOfSprites.ListUserControl[ListIndex[i]]));
                ListGetTop.Add(Canvas.GetTop(InforOfSprites.ListUserControl[ListIndex[i]]));
            }

            ListGetleft.Sort();
            ListGetTop.Sort();

            List<double> returnList = new List<double>();
            returnList.Add(ListGetleft.Last());
            returnList.Add(ListGetTop.Last());

            return returnList;
        }

        private List<int> CheckPosition()  //возвращает список индексо элементов из списка, если в этом месте уже есть элемент
        {
            List<int> ListIndex = new List<int>();

            for(int i = 0; i < InforOfSprites.ListUserControl.Count(); i++)
            {
                SqareVM sqare = InforOfSprites.ListUserControl[i].DataContext as SqareVM;
                if(posNow.Y> sqare.Position.Y && InforOfSprites.ListUserControl[i].ActualHeight + sqare.Position.Y > posNow.Y)
                {
                    if (InforOfSprites.ListUserControl[i].ActualHeight > 40)
                    {
                        ListIndex.Add(i);
                    }
                }
            }
    
            return ListIndex;
        }

        private void ClonUserControl()  //клонирование 
        {
            SqareVM square = userControl.DataContext as SqareVM;
            SqareVM sq = (SqareVM)square.Clone();
            UserControl1 userControl_ = RetUser();           
            Cache.NowModel.CurrentWindow.Compilar.Children.Add(userControl_);       
        //    InforOfSprites.ListUserControl.Insert(n,userControl_);
           
            Canvas.SetLeft(userControl_, posNow.X);
            Canvas.SetTop(userControl_, posNow.Y);

            userControl = userControl_;
          
        }

        private UserControl1 RetUser()  // установление данных
        {
            SqareVM square = userControl.DataContext as SqareVM;
            UserControl1 userControlnew = new UserControl1();
            userControlnew.DataContext = new SqareVM()
            {
                Position = posNow,
                PointWhike = square.PointWhike,
                Color = square.Color,
                Id = square.Id,
                Text = square.Text,
                Width = square.Width,
                HeightFor = square.HeightFor,
                HeighDown = square.HeighDown
            };
           
            return userControlnew;
        }

       
    }
}
