using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp2
{
    public class ViewModelTchicness
    {
        public ViewModelTchicness(ComboBoxItem tchicness)
        {
            Cache.Thickess = tchicness.TabIndex*2;
        }
    }
}
