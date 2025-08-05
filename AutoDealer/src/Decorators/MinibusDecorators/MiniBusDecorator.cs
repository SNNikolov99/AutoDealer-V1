using AutoDealer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealer.src.Decorators
{
    public abstract class MiniBusDecorator : VehicleDecorator
    {
        protected MiniBus MiniBus => (MiniBus)_vehicle;
        protected MiniBusDecorator(MiniBus miniBus) : base(miniBus) { }

        public abstract override void AttachPart();
    }
}
