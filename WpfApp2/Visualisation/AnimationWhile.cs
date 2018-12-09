using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WpfApp2.Sprites;
using WpfApp2.UserSprites;
using WpfApp2.Visualisation;

namespace WpfApp2.Visualisation
{
    public class AnimationWhile
    {
        private UserControl1 sprites { get; set; }
        private Point posNow { get; set; }

        public AnimationWhile(UserControl1 sprites, Point posNow)
        {
            this.sprites = sprites;
            this.posNow = posNow;

            Create();
        }

        private void Create()
        {
            if(Repositories.ListUserControl != null)
            {
                List<int> indexAllWhile = GetAllWhile();
                Level(indexAllWhile);
            }
            else
            {
                CreateNewSprites();
                ClonUserControl(posNow);
                AddInfRepositories("1.",posNow);
            }
        }
        
        private void Level(List<int> allIndex)
        {
            switch(allIndex.Count())
            {
                case 0: {
                        ClonUserControl(Repositories.PointEnd.Last());                      
                        AddInfRepositories(Repositories.StartIndex.ToString() + ".", Repositories.PointEnd.Last());
                    } break;
                case 1: {                            
                        if(Repositories.ListUserControl[allIndex[0]].Increase.ActualHeight == 30)
                        {
                            if (sprites.ActualHeight > 40)
                            {
                                int u = GetRealIndexSprites(Repositories.PointStart[allIndex[0]]);
                                ClonUserControl(new Point(Repositories.PointEnd[allIndex[0]].X + 20, Repositories.PointEnd[allIndex[0]].Y - sprites.ActualHeight + 30));
                                Repositories.ListUserControl[allIndex[0]].Increase.Height = sprites.ActualHeight;
                                CreateAnimationWhile(u,sprites.ActualHeight - 30,allIndex[0]);

                                CreateOffset(allIndex[0]);

                            }
                            else
                            {
                                Point newPoint = new Point(Repositories.PointEnd[allIndex[0]].X, Repositories.PointEnd[allIndex[0]].Y - sprites.ActualHeight );
                                ClonUserControl(new Point(newPoint.X + 20, newPoint.Y - 20 ));
                                Repositories.PointEnd[allIndex[0]] = new Point(newPoint.X, newPoint.Y + 11 + 20);
                                Repositories.ListUserControl[allIndex[0]].Increase.Height = sprites.ActualHeight;
                            }
                        }
                        else
                        {
                            int u = GetRealIndexSprites(Repositories.PointStart[allIndex[0]]);
                             ClonUserControl(new Point(Repositories.PointEnd[allIndex[0]].X + 20, Repositories.PointEnd[allIndex[0]].Y - 20 ));
                            Repositories.ListUserControl[allIndex[0]].Increase.Height = sprites.ActualHeight;
                            CreateAnimationWhile(u,sprites.ActualHeight,allIndex[0]);                         
                            CreateOffset(allIndex[0]);
                          
                        }

                    }
                    break;
                case 2: MessageBox.Show("2");break;
                 
               
            }            
        }
         
        private void CreateOffset(int index)
        {
            for (int i = 0; i < Repositories.ListUserControl.Count(); i++)
            {
                if (i == Repositories.ListUserControl.Count)
                { }
                else
                {
                    SqareVM sqare = Repositories.ListUserControl[i].DataContext as SqareVM;
                    if (sqare.Position.Y + Repositories.ListUserControl[i].ActualHeight >= Repositories.PointEnd[index].Y )
                    {
                        CreateOffset(sprites.ActualHeight - 30, i);
                    }
                }
              
            }
        }

        private int GetRealIndexSprites(Point pointEnd)
        {
            for(int i = 0; i < Repositories.ListUserControl.Count(); i++)
            {
                SqareVM sqare = Repositories.ListUserControl[i].DataContext as SqareVM;
                if (sqare.Position.Y == pointEnd.Y)
                {
                    return i;
                }
            }
            return 666;
        }

        private void CreateOffset(double offset, int index)
        {
            TranslateTransform trans = new TranslateTransform();
            Repositories.ListUserControl[index].RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(offset, TimeSpan.FromSeconds(1));
            trans.BeginAnimation(TranslateTransform.YProperty, anim1);

            Point newPoint = new Point(Repositories.PointEnd[index].X, Repositories.PointEnd[index].Y + offset);
            Repositories.PointEnd[index] = newPoint;
        }

        private void CreateAnimationWhile(int index, double yyy, int iPoint)
        {            
            DoubleAnimation scriptAnimation = new DoubleAnimation();
            scriptAnimation.From = Repositories.ListUserControl[index].Increase.ActualHeight;
            scriptAnimation.To = yyy + Repositories.ListUserControl[index].Increase.ActualHeight;
            scriptAnimation.Duration = TimeSpan.FromSeconds(1);
            Repositories.ListUserControl[index].Increase.BeginAnimation(StackPanel.HeightProperty, scriptAnimation);

            Point newPoint = new Point(Repositories.PointEnd[iPoint].X, Repositories.PointEnd[iPoint].Y + yyy);
            Repositories.PointEnd[iPoint] = newPoint;
        }

        private List<int> GetAllWhile()  //получаю список индексов всех возможных вхождений
        {
            List<int> allSpritesIndex = new List<int>();
            for(int i = 0; i < Repositories.PointStart.Count(); i++)
            {
                if(Repositories.PointStart[i].Y < posNow.Y && Repositories.PointEnd[i].Y > posNow.Y)
                {
                    allSpritesIndex.Add(i);
                }
            }
            return allSpritesIndex;
        }

        private string[] GetListLevel(string strLevel)
        {
            string[] str = strLevel.Split('.');
            return str;
        }

        private void AddInfRepositories(string level, Point start)   //добавление в репозиторий , добавление usercontrol в клонировании
        {
            SetListLevel(level);
            SetStartPoint(start);
            SetEndPoint();
            
            Repositories.StartIndex++;
        }

        private void ClonUserControl(Point point)  //клонирование
        {
            SqareVM square = sprites.DataContext as SqareVM;
            SqareVM sq = (SqareVM)square.Clone();           
            UserControl1 userControl_ = RetUser(sq,point);
            Repositories.ListUserControl.Add(userControl_);
            Cache.NowModel.CurrentWindow.Compilar.Children.Add(userControl_);           
            Canvas.SetLeft(userControl_, point.X);
            Canvas.SetTop(userControl_, point.Y);                     
        }

        public UserControl1 RetUser(SqareVM square, Point point)  // установление данных
        {
            UserControl1 userControl = new UserControl1();
            userControl.DataContext = new SqareVM()
            {
                Position = point,
                PointWhike = square.PointWhike,
                Color = square.Color,
                Id = square.Id,
                Text = square.Text,
                Width = SizeText(square.Text),
                HeightFor = square.HeightFor,
                HeighDown = square.HeighDown
            };
            int SizeText(string scriptText)
            {
                return scriptText.Length * 10 + 20;
            }
            return userControl;
        }

        private void CreateNewSprites()  // создание новых списков в репозитории
        { 
            Repositories.ListLevel = new List<string>();
            Repositories.PointStart = new List<Point>();
            Repositories.PointEnd = new List<Point>();
            Repositories.ListUserControl = new List<UserControl1>();
            Repositories.StartPOint = posNow;

            Repositories.StartIndex = 1;           
        }
               
        private void SetListLevel(string level) //добавление уровня
        {
            Repositories.ListLevel.Add(level);
        }
                
        private void SetStartPoint(Point point) //точные координаты, так как PointEnd заполняется программно
        {
            Repositories.PointStart.Add(point);
        }

        private void SetEndPoint()
        {
            var ppp = new Point(Repositories.PointStart.Last().X , Repositories.PointStart.Last().Y + sprites.ActualHeight);
            Repositories.PointEnd.Add(ppp);
        }


    }
}
