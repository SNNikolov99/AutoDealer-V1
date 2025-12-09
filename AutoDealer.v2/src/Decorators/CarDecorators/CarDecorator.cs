using AutoDealerV2.src.Classes;
using AutoDealerV2.src.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Decorators.CarDecorators
{
    public abstract class CarDecorator : VehicleDecorator
    {
        protected Car Car => (Car)_vehicle;
        protected CarDecorator(Car car): base(car) { }
        
        public override abstract void AttachPart();
        
    }
}
