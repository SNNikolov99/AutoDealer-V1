using AutoDealerV2.src.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Decorators
{
    public abstract class VehicleDecorator : Vehicle
    {
        protected Vehicle _vehicle;

        public VehicleDecorator(Vehicle vehicle) : 
            base(vehicle.Brand,  vehicle.Model,vehicle.Type, vehicle.Year,vehicle.Price,vehicle.Colour, vehicle.HorsePower, vehicle.FuelType,vehicle.Description)
        {
            _vehicle = vehicle;
        }

        public abstract void AttachPackage();

        
        
    }
}
