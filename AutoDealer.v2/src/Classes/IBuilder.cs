using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Classes
{
    public interface IBuilder
    {
        public void setModel(string model);
        public void setColor(string color);
        public void setEngine();
        public void setPackage();
    }
}
