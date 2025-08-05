using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDealer.Models;

namespace AutoDealer.src.Decorators.CarDecorators
{
    public class HybridTraction : CarDecorator
    {

        public HybridTraction(Car car) : base(car)
        {
        }

        public override void AttachPart()
        {
            _vehicle.Price += 5000;
            _vehicle.FuelType += "+ Hybrid";
            _vehicle.HorsePower += 50;
        }
    }
}
