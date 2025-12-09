using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDealerV2.src.Services;
using FluentAssertions;
using Microsoft.Win32;
using AutoDealerV2.src.Classes;



namespace AutoDealerV2.Tests.ModelsTests
{
    public class AutoRegistryTests
    {
        //List<Vehicle> vehicles = new List<Vehicle>();
       
        public AutoRegistryService registry = new AutoRegistryService();

        
        
        [Fact]
        public void AutoRegistry_AddVehicle_Success()
        {
            //Arange
            Vehicle vehicle = new Car("VW","Golf 7", 2018,Convert.ToDecimal(35000),"Mettalic grey",150,"Gasoline");

            //Act
            registry.AddVehicle(vehicle);

            //Assert
            registry.Vehicles.Should().NotBeNull();
            registry.Vehicles.Should().Contain(vehicle);
            //registry.Vehicles[].Should().NotBeNull();
        }

        [Fact]
        public void AutoRegistry_AddVehicle_ThrowArgNullException()
        {
            Vehicle? vehicle = null;

            Action action = () => registry.AddVehicle(vehicle);

            action.Should().Throw<ArgumentNullException>();


        }

        [Fact]
        public void AutoRegistry_AddVehicle_ThrowArgException()
        {
            Vehicle vehicle = new Car("", "", 2018, Convert.ToDecimal(35000), "Mettalic grey", 150, "Gasoline");

            Action action = () => registry.AddVehicle(vehicle);

            action.Should().Throw<ArgumentException>().WithMessage("One or more vehicle fields are missing or invalid");
        }

        [Theory]
        [InlineData("Brand",false)]
        [InlineData("Brand", true)]
        public void AutoRegistry_SortVehiclesByProperty_ReturnsSortedList(string property , bool isDescending)
        {
            //Arange
            Vehicle vehicle1 = new Car("VW", "Golf 7", 2018, Convert.ToDecimal(35000), "Mettalic grey", 150, "Gasoline");
            Vehicle vehicle2 = new Motorbike("Ducati", "Stradale", 2010, Convert.ToDecimal(25000), "red", 170, "Gasoline");
            Vehicle vehicle3 = new MiniBus("VW", "Caddy", 2011, Convert.ToDecimal(12500), "white", 130, "Diesel");
            Vehicle vehicle4 = new Car("BMW", "330xd", 2018, Convert.ToDecimal(35000), "Pearl black", 272, "Diesel");

            registry.AddVehicle(vehicle1);
            registry.AddVehicle(vehicle2);
            registry.AddVehicle(vehicle3);
            registry.AddVehicle(vehicle4);

            //Act
            List<Vehicle> res = registry.SortVehiclesByProperty(property, isDescending);

            //Assert
            var expected = isDescending
           ? new[] { "vw", "vw", "ducati", "bmw" }
           : new[] { "bmw", "ducati", "vw", "vw" };

            res.Select(x => x.Brand)
                  .Should().ContainInOrder(expected);
            res.Count.Should().Be(4);

        }
        [Theory]
        [InlineData ("",true)]
        [InlineData("Big Bertha", true)]
        public void AutoRegistry_SortVehiclesByProperty_ThrowsArgException(string property, bool isDescending)
        {
            Vehicle vehicle1 = new Car("VW", "Golf 7", 2018, Convert.ToDecimal(35000), "Mettalic grey", 150, "Gasoline");
            Vehicle vehicle2 = new Motorbike("Ducati", "Stradale", 2010, Convert.ToDecimal(25000), "red", 170, "Gasoline");
            Vehicle vehicle3 = new MiniBus("VW", "Caddy", 2011, Convert.ToDecimal(12500), "white", 130, "Diesel");
            Vehicle vehicle4 = new Car("BMW", "330xd", 2018, Convert.ToDecimal(35000), "Pearl black", 272, "Diesel");

            registry.AddVehicle(vehicle1);
            registry.AddVehicle(vehicle2);
            registry.AddVehicle(vehicle3);
            registry.AddVehicle(vehicle4);

            //Act
            Action act = () => registry.SortVehiclesByProperty(property, isDescending);

            //Assert
            if (string.IsNullOrEmpty(property))
            {
                act.Should().Throw<ArgumentException>().WithMessage("Property name cannot be null or empty*");
            }
            else
            {
                act.Should().Throw<ArgumentException>().WithMessage($"Property '{property}' does not exist*");
            }

        }
        [Theory]
        [InlineData (1)]
        [InlineData (3)]
        public void AutoRegistry_RemoveVehicleByID_VehicleRemoved(int id)
        {
            Vehicle vehicle1 = new Car("VW", "Golf 7", 2018, Convert.ToDecimal(35000), "Mettalic grey", 150, "Gasoline");
            Vehicle vehicle2 = new Motorbike("Ducati", "Stradale", 2010, Convert.ToDecimal(25000), "red", 170, "Gasoline");
            Vehicle vehicle3 = new MiniBus("VW", "Caddy", 2011, Convert.ToDecimal(12500), "white", 130, "Diesel");
            Vehicle vehicle4 = new Car("BMW", "330xd", 2018, Convert.ToDecimal(35000), "Pearl black", 272, "Diesel");

            registry.AddVehicle(vehicle1);
            registry.AddVehicle(vehicle2);
            registry.AddVehicle(vehicle3);
            registry.AddVehicle(vehicle4);
            Vehicle toBeDeleted = registry.Vehicles[id];

            registry.RemoveVehicleByID(id);

            registry.Vehicles.Count.Should().Be(3);
            registry.Vehicles.Should().NotContain(v => v.Id == id);


        }

    }
}

