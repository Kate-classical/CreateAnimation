using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Sprites;
using WpfApp2.Visualisation;

namespace WpfApp2.UserSprites
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        Vector relativeMousePos; // смещение мыши от левого верхнего угла квадрата
        Canvas container;        // канвас-контейнер


        Point StartPoint { get; set; }
        Canvas parent { get; set; }
        UserControl StartUserControl { get; set; }


        public UserControl1()
        {
            InitializeComponent();
            SetBinding(RequestMoveCommandProperty, new Binding("RequestMove"));
        }

        public ICommand RequestMoveCommand
        {
            get { return (ICommand)GetValue(RequestMoveCommandProperty); }
            set { SetValue(RequestMoveCommandProperty, value); }
        }

        public Shape DraggedImageContainer
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
            try
            {
                MouseMove -= OnDragMove;
                LostMouseCapture -= OnLostCapture;
                var dragImageContainer = DraggedImageContainer;
                var position = e.GetPosition(Cache.NowModel.CurrentWindow.Compilar) - relativeMousePos; //позиция после перемещения


                var position_ = e.GetPosition(Cache.NowModel.CurrentWindow) - relativeMousePos;

                if (position_.X > Cache.NowModel.CurrentWindow.aaaa.ActualWidth &&
                    Cache.NowModel.CurrentWindow.Compilar.ActualWidth + Cache.NowModel.CurrentWindow.aaaa.ActualWidth - 80 > position_.X)
                {
                    AddSprites.Create create = new AddSprites.Create(this, position);

                }
                UpdateDraggedSquarePosition(null);
            }
            catch
            {
                MessageBox.Show("Не создан елемент");
                UpdateDraggedSquarePosition(null);
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
            var dragImageContainer = DraggedImageContainer;
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

        private void textCount_LostFocus(object sender, RoutedEventArgs e)
        {
            var element = this;
            SqareVM square = element.DataContext as SqareVM;
            if (proverka(textCount.Text) && textCount.Text != null && textCount.Text != "")
            {
                int n = Convert.ToInt32(textCount.Text);
                square.Znatch = n;             
            }
            else
            {
                MessageBox.Show("Введено неверное значение");
            }             
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
