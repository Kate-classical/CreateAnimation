using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2.What
{
    public static class Helpers
    {
        public static UIElement GetElementFromMouseOver<T>(T item) where T : Panel
        {
            var elem = item.Children.OfType<UIElement>().Where(e => e.Visibility == Visibility.Visible && e.IsMouseOver);
            if (elem != null)
            {
                return elem.FirstOrDefault();
            }
            return null;
        }

        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
           
                return FindVisualParent<T>(parentObject);
            
        }
    }
}
