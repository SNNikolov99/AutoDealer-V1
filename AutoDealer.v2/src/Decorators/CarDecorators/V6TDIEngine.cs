using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDealerV2.src.Classes;

namespace AutoDealerV2.src.Decorators.CarDecorators
{
    public class V6TDIEngine : CarDecorator
    {
       
        public V6TDIEngine(Car car): base(car) { }
        public override void AttachPart()
        {
            _vehicle.Price += 6500;
            _vehicle.FuelType = "Diesel";
            _vehicle.HorsePower = 210;
        }

    }
}
