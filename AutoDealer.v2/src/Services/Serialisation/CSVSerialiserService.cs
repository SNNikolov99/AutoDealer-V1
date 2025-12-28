using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDealerV2.src.Classes;

namespace AutoDealerV2.src.Services.Serialisation
{
    public class CSVSerialiserService: ISerializer<List<Vehicle>>
    {
        public List<Vehicle> Load(string filename)
        {

            List<Vehicle> vehicles = new List<Vehicle>();

            if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
            {
                throw new ArgumentException("File path is invalid or file is emptry");
            }

            string[] lines = File.ReadAllLines(filename);
            int row = 0;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                {
                    throw new ArgumentException("line number " + row.ToString() + " is empty");
                }

                string[] parts = line.Split(',');
                if (parts.Length != 9)
                {
                    throw new ArgumentException($"There is an emptry field in this row {row}");
                }
                var Id = int.Parse(parts[0]);
                var type = parts[1].Trim();
                var brand = parts[2].Trim();
                var model = parts[3].Trim();
                var price = decimal.Parse(parts[4]);
                var colour = parts[5].Trim();
                var hp = int.Parse(parts[6]);
                var fuel = parts[7].Trim();
                var description = parts[8];


                Vehicle vehicle = new Vehicle(Id,brand,model,type,price,colour,hp,fuel,description);


                if (vehicle != null)
                {
                    vehicles.Add(vehicle);
                    row++;
                }
            }
            return vehicles;
        }

      

        public void Save(List<Vehicle> data,string pathName)
        {
            var lines = data.Select(v => v.ToString());

            File.WriteAllLines(pathName, lines);

        }
    }
}
