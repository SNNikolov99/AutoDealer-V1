using AutoDealer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealer.src.Decorators
{
    public abstract class CarDecorator : VehicleDecorator
    {
        protected Car Car => (Car)_vehicle;
        protected CarDecorator(Car car): base(car) { }
        
        public override abstract void AttachPart();
        
    }
}
