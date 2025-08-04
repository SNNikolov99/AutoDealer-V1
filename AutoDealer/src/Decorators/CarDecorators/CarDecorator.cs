using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDealer.Models;

namespace AutoDealer.src.Decorators
{
    public abstract class CarDecorator : VehicleDecorator
    {
      
        protected CarDecorator(Car car): base(car) { }
        
        public override abstract void AttachPart();
        
    }
}
