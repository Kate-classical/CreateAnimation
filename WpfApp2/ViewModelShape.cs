using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using WpfApp2.Interface;
using WpfApp2.Model;

namespace WpfApp2
{
    public class ViewModelShape
    {
        private IModel model;

        public ViewModelShape(string ModelModel)
        {
            this.model = ModelTypeTo(ModelModel);
           
            Application application = Application.Current;
            MainWindow window = (MainWindow)application.MainWindow;

            Cache.NowModel = model;
            Cache.IndexButton = 0;

            if (Cache.NowModel != Cache.LastMode && Cache.LastMode != null)
            {
                Cache.LastMode.RemoveHandler();
                Cache.LastRadioButton.Background = Brushes.AliceBlue;
            }        
         
            model.CurrentWindow = window;
            
            model.AddHandler();
            Cache.LastMode = model;
            Cache.LastRadioButton = Cache.NowRadButton;

            Visualisation.Repositories.ListBorder = new List<System.Windows.Shapes.Rectangle>();
            Visualisation.Repositories.ListShapes = new List<System.Windows.Shapes.Shape>();
        }

       

        private IModel ModelTypeTo(string model)
        {
            IModel model2 = new IModel();
           
            switch (model)
            {
                case "ItemLine":
                    {
                        LineModel line = new LineModel();
                        return line;
                    }
                case "ItemEllipse":
                    {
                        EllipsModel ellipsModel = new EllipsModel();
                        return ellipsModel;
                    }
                case "ItemCursor":
                    {
                        CursorModel cursor = new CursorModel();
                        return cursor;
                    }
                case "ItemRectangle":
                    {
                        RectangleModel rectangle = new RectangleModel();
                        return rectangle;
                    }
                case "ItemAdd":
                    {
                        AddModel add = new AddModel();
                        return add;
                    }
                case "ItemAddInButton":
                    {
                        AddInButtonModel addInButton = new AddInButtonModel();
                        return addInButton;
                    }
                case "RemoveListSprites":
                    {
                        AddSprites.Create create = new AddSprites.Create();
                    }break;
            }
            
            return model2;
        }
    }
}
