using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Classes
{
    public interface IBuilder
    {
        public IBuilder setModel(string model);
        public IBuilder setColor(string color);
        public IBuilder setEngine(string engine);
        public IBuilder setPackage(string package);
    }
}
