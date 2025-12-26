using AutoDealerV2.src.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoDealerV2.src.Services.Serialisation
{
    public class JSONSerialiserService: ISerializer<Task>
    {
       public T Load<T>(string pathName) 
        {
            T? list;
            string jsonString = File.ReadAllText(pathName);
            try
            {
                if (jsonString.Length!=0)
                {
                    list = JsonSerializer.Deserialize<T>(jsonString);
                }
                else
                {
                    throw new Exception("The given file is empty");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;
        }
       public void Save(List<Vehicle> vehicles, string pathName)
       {
            string JsonString = JsonSerializer.Serialize(vehicles,
                new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(pathName, JsonString);

       }
    }
}
