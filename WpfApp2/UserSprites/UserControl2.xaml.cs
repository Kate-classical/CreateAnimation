using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Sprites;

namespace WpfApp2.UserSprites
{
    /// <summary>
    /// Логика взаимодействия для UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        Vector relativeMousePos; // смещение мыши от левого верхнего угла квадрата
        Canvas container;        // канвас-контейнер


        Point StartPoint { get; set; }
        Canvas parent { get; set; }
        UserControl StartUserControl { get; set; }

        public UserControl2()
        {
            InitializeComponent();
            SetBinding(RequestMoveCommandProperty, new Binding("RequestMove"));
        }

        public ICommand RequestMoveCommand
        {
            get { return (ICommand)GetValue(RequestMoveCommandProperty); }
            set { SetValue(RequestMoveCommandProperty, value); }
        }

        public Shape DraggedImageContainer2
        {
            get { return (Shape)GetValue(DraggedImageContainerProperty); }
            set { SetValue(DraggedImageContainerProperty, value); }
        }

        public string TextScriptCommand
        {
            get { return (string)GetValue(Text); }
            set { SetValue(Text, value); }
        }

        public static readonly DependencyProperty DraggedImageContainerProperty =
            DependencyProperty.Register(
                "DraggedImageContainer", typeof(Shape), typeof(UserControl1));

        public static readonly DependencyProperty RequestMoveCommandProperty =
            DependencyProperty.Register("RequestMoveCommand", typeof(ICommand),
                                        typeof(UserControl1));

        public static readonly DependencyProperty Text =
            DependencyProperty.Register("TextScriptCommand", typeof(string), typeof(UserControl1));

        // по нажатию на левую клавишу начинаем следить за мышью
        void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            StartUserControl = this;
            SqareVM square = this.DataContext as SqareVM;
            StartPoint = square.Position;

            relativeMousePos = e.GetPosition(this) - new Point();
            MouseMove += OnDragMove;
            LostMouseCapture += OnLostCapture;
            Mouse.Capture(this);
        }

        // клавиша отпущена - завершаем процесс
        void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            FinishDrag(sender, e);
            Mouse.Capture(null);
        }

        // потеряли фокус (например, юзер переключился в другое окно) - завершаем тоже
        void OnLostCapture(object sender, MouseEventArgs e)
        {
            FinishDrag(sender, e);
        }

        void OnDragMove(object sender, MouseEventArgs e)
        {
            UpdateDraggedSquarePosition(e);
        }

        void FinishDrag(object sender, MouseEventArgs e)
        {
            MouseMove -= OnDragMove;
            LostMouseCapture -= OnLostCapture;
            var dragImageContainer = DraggedImageContainer2;
            var position = e.GetPosition(Cache.NowModel.CurrentWindow.Compilar) - relativeMousePos; //позиция после перемещения

            var position_ = e.GetPosition(Cache.NowModel.CurrentWindow) - relativeMousePos;

            if (position_.X > Cache.NowModel.CurrentWindow.aaaa.ActualWidth &&
                Cache.NowModel.CurrentWindow.Compilar.ActualWidth + Cache.NowModel.CurrentWindow.aaaa.ActualWidth - 80 > position_.X)
            {
                ClonUserControl(position);
            }


            UpdateDraggedSquarePosition(null);
        }

        private void ClonUserControl(Point point)
        {
            UserControl1 userControl = new UserControl1();

            SqareVM square = this.DataContext as SqareVM;

            userControl.DataContext = new SqareVM()
            {
                // Position = new Point(30, 70),
                PointWhike = square.PointWhike,
                Color = square.Color,
                Id = square.Id,
                Text = square.Text,
                Width = SizeText(square.Text)
            };

            Cache.NowModel.CurrentWindow.Compilar.Children.Add(userControl);

            if (point.Y > 160 && point.Y < 210)
            {
                Canvas.SetLeft(userControl, 60);
                Canvas.SetTop(userControl, 160);
            }
            else
            {
                Canvas.SetLeft(userControl, point.X);
                Canvas.SetTop(userControl, point.Y);
            }

            int SizeText(string scriptText)
            {
                return scriptText.Length * 10 + 20;
            }

        }


        void Attraction(Tuple<double, double> tuple)
        {
            int x = 0;
            var point = new Point(tuple.Item1 + x, tuple.Item2);
            RequestMoveCommand?.Execute(point);
        }

        // требуем у VM обновить позицию через команду
        void UpdatePosition(MouseEventArgs e)
        {
            var point = e.GetPosition(Cache.NowModel.CurrentWindow.Compilar);

            // не забываем проверку на null
            RequestMoveCommand?.Execute(point - relativeMousePos);
        }



        // это вспомогательная функция, ей место в общей библиотеке
        static private T FindParent<T>(FrameworkElement from) where T : FrameworkElement
        {
            FrameworkElement current = from;
            T t;
            do
            {
                t = current as T;
                current = (FrameworkElement)VisualTreeHelper.GetParent(current);
            }
            while (t == null && current != null);
            return t;
        }

        static private SqareVM FindInfoControl<T>(FrameworkElement from) where T : FrameworkElement
        {
            FrameworkElement current = from;
            var qwer = current.DataContext as SqareVM;

            return qwer;
        }


        void UpdateDraggedSquarePosition(MouseEventArgs e)
        {
            var dragImageContainer = DraggedImageContainer2;
            if (dragImageContainer == null)
                return;
            var needVisible = e != null;
            var wasVisible = dragImageContainer.Visibility == Visibility.Visible;
            // включаем/выключаем видимость перемещаемой картинки
            dragImageContainer.Visibility = needVisible ? Visibility.Visible : Visibility.Collapsed;
            if (!needVisible) // если мы выключились, нам больше нечего делать
                return;

            if (!wasVisible) // а если мы были выключены и включились,
            {                // нам надо привязать изображение себя
                dragImageContainer.Fill = new VisualBrush(this); //перерисоввывывает
                dragImageContainer.SetBinding( // а также ширину/высоту
                    Shape.WidthProperty,
                    new Binding(nameof(ActualWidth)) { Source = this });
                dragImageContainer.SetBinding(
                    Shape.HeightProperty,
                    new Binding(nameof(ActualHeight)) { Source = this });
                // Binding нужен потому, что наш размер может по идее измениться
            }
            // перемещаем картинку на нужную позицию
            parent = FindParent<Canvas>(dragImageContainer);
            var l = parent.Children;
            var position = e.GetPosition(parent) - relativeMousePos;
            Canvas.SetLeft(dragImageContainer, position.X);
            Canvas.SetTop(dragImageContainer, position.Y);
        }

       

        private bool proverka(string str)
        {
            foreach (var s in str)
            {
                if (!Char.IsDigit(s))
                    return false;
            }
            return true;
        }
    }
}
