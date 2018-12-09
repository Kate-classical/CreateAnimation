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
using WpfApp2.Interface;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

       

       

        private void thickessBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModelTchicness viewModel = new ViewModelTchicness((ComboBoxItem)thickessBox.SelectedItem);
           
        }

        private void ColorStroke_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var q = ColorStroke.SelectedItem.ToString();
            string[] str = q.Split(' ');
            SolidColorBrush redBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(str[1]);
            Cache.ColorStroke = redBrush;          
        }

        private void Squares_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Squares_Drop(object sender, DragEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sprites.Motions mainVM = new Sprites.Motions();
           
            SquaresPanel.DataContext = mainVM;            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Sprites.Circle mainVM = new Sprites.Circle();
            SquaresPanel.DataContext = mainVM;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Sprites.Control mainVM = new Sprites.Control();
            SquaresPanel.DataContext = mainVM;
        }

        private void ColorFill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var q = ColorFill.SelectedItem.ToString();
            string[] str = q.Split(' ');
            SolidColorBrush redBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(str[1]);
            Cache.ColorFill = redBrush;
        }

        private void Ellipse_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Right)
            {
                //  Canvas.SetLeft(Cache.NowModel.CurrentWindow.el, Canvas.GetLeft(Cache.NowModel.CurrentWindow.el) + 10);
                // Canvas.SetTop(Cache.NowModel.CurrentWindow.el, Canvas.GetTop(Cache.NowModel.CurrentWindow.el));

          //      var rotateTransform = el.RenderTransform as RotateTransform;
           //     var transform = new RotateTransform(15 + (rotateTransform?.Angle ?? 0), el.ActualWidth / 2, el.ActualHeight / 2);
           //     el.RenderTransform = transform;
           
            }
        }

      
              

        private void ItemLine_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            Cache.NowRadButton = pressed;
            Cache.NowRadButton.Background = Brushes.Violet;
            ViewModelShape viewModel = new ViewModelShape(pressed.Name);   
        }

        private void Compilar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FocusControl.Focus();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Visualisation.ClassVisualisation vs = new Visualisation.ClassVisualisation(Visualisation.InforOfSprites.ReakElement);
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Sprites.Event even = new Sprites.Event();
            SquaresPanel.DataContext = even;
        }


        Sprites.Elements elements = new Sprites.Elements();


        private void Button_Click_5(object sender, RoutedEventArgs e)
        {           
            SquaresPanel.DataContext = elements;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Sprites.Conditions conditions = new Sprites.Conditions();
            SquaresPanel.DataContext = conditions;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Visualisation.InforOfSprites.visualis = false;
        }

        private void RemoveListSprites_Click(object sender, RoutedEventArgs e)
        {
            AddSprites.Create create = new AddSprites.Create();
        }
    }
}
