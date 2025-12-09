using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoDealerV2.src.Services.Serialisation;
using AutoDealerV2.src.Classes;


namespace AutoDealerV2.src.Services
{
    internal class SerialisationService
    {
        private string pathname;
        private ISerializer serializer;

        public SerialisationService(string pathname)
        {
            this.pathname = pathname;

            string ext = Path.GetExtension(pathname).ToLower();

            switch (ext)
            {
                case ".json":
                   serializer =  new JSONSerialiserService();
                    break;
                case ".csv":
                   serializer =  new CSVSerialiserService();
                    break;
                default: throw new NotSupportedException("Unsupported file extension");
            }

        }

        public void Save(List<Vehicle> vehicles)
        {
            serializer.Save(vehicles,pathname);
        }

        public List<Vehicle> Load()
        {
            return serializer.Load(pathname);
        }

    }
}
