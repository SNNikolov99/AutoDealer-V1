using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDealer.Models;
using FluentAssertions;
using Microsoft.Win32;

namespace AutoDealer.Tests.ModelsTests
{
    public class AutoRegistryTests
    {
        //List<Vehicle> vehicles = new List<Vehicle>();
       
        //public AutoRegistry registry = new AutoRegistry();


        // 1) Declare once, for all tests
        private readonly Vehicle v1;
        private readonly Vehicle v2;
        private readonly Vehicle v3;
        private readonly Vehicle v4;
        private readonly List<Vehicle> allVehicles;
        private AutoRegistry registry;

        public AutoRegistryTests()
        {
            // 2) Initialize your sample vehicles
            v1 = new Car("VW", "Golf 7", 2018, 35000m, "Metallic grey", 150, "Gasoline");
            v2 = new Motorbike("Ducati", "Stradale", 2010, 25000m, "red", 170, "Gasoline");
            v3 = new MiniBus("VW", "Caddy", 2011, 12500m, "white", 130, "Diesel");
            v4 = new Car("BMW", "330xd", 2018, 35000m, "Pearl black", 272, "Diesel");

            allVehicles = new List<Vehicle> { v1, v2, v3, v4 };

            // 3) Fresh registry for each test (xUnit recreates the test class per [Fact]/[Theory])
            registry = new AutoRegistry();
        }

        // 4) Helper to seed the registry with the four vehicles
        private void SeedRegistry()
        {
            registry = new AutoRegistry();      // ensure a clean slate
            foreach (var v in allVehicles)
                registry.AddVehicle(v);
        }




        [Fact]
        public void AutoRegistry_AddVehicle_Success()
        {
            //Arange
            SeedRegistry();
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
            SeedRegistry();

            //Act
            List<Vehicle> res = registry.SortVehiclesByProperty(property, isDescending);

            //Assert
            var expected = isDescending
           ? new[] { "VW", "VW", "Ducati", "BMW" }
           : new[] { "BMW", "Ducati", "VW", "VW" };

            res.Select(x => x.Brand)
                  .Should().ContainInOrder(expected);
            res.Count.Should().Be(4);

        }
        [Theory]
        [InlineData ("",true)]
        [InlineData("Big Bertha", true)]
        public void AutoRegistry_SortVehiclesByProperty_ThrowsArgException(string property, bool isDescending)
        {
            SeedRegistry();

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
            SeedRegistry();
            Vehicle toBeDeleted = registry.Vehicles[id];

            registry.RemoveVehicleByID(id);

            registry.Vehicles.Count.Should().Be(3);
            registry.Vehicles.Should().NotContain(v => v.Id == id);


        }


        [Fact]
        public void AutoRegistry_RemoveVehicleByID_ThrowsArgException()
        {
            //Arrange
            SeedRegistry();

            //Act
            Action act = () => registry.RemoveVehicleByID(5);

            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("No vehicle found with ID 5*");
        }

        [Theory]
        [InlineData("Year",2015,FilterOperator.GreaterThan)]
        public void AutoRegistry_FilterByProperty_ReturnsFilteredList(string property, int value, FilterOperator op)
        {
            //Arrange
            SeedRegistry();
            //Act
            List<Vehicle> res = registry.FilterByProperty(property, value.ToString(), op);
            //Assert
            res.Count.Should().Be(2);
            res.Should().Contain(v => v.Year > value);
        }

    }
}

