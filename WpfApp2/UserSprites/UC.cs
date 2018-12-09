using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.UserSprites
{
    public class UC : ICloneable
    {
        public UserControl1 userControl { get; set; }

        public object Clone()
        {
            UserControl1 user = new UserControl1();
            return new UC { userControl = user};
        }
    }
}
