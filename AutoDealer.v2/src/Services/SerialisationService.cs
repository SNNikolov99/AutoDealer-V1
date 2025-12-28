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
    internal class SerialisationService<T>
    {
        private string pathname;
        private ISerializer<T> serializer;

        public SerialisationService(string pathname)
        {
            this.pathname = pathname;

            if (!File.Exists(pathname))
            {
                File.Create(pathname).Close();
            }

            

            string ext = Path.GetExtension(pathname).ToLower();

            switch (ext)
            {
                case ".json":
                   serializer =  new JSONSerialiserService<T>();
                    break;
                case ".csv":
                    if (typeof(T) != typeof(List<Vehicle>))
                        throw new NotSupportedException(
                            "CSV serialization is supported only for List<Vehicle>"
                        );

                    // cast is safe because of the type check above
                    serializer = (ISerializer<T>)(object)new CSVSerialiserService();
                    break;
                default: throw new NotSupportedException("Unsupported file extension");
            }

        }

        public void Save(T data)
        {
            serializer.Save(data,pathname);
        }

        public T Load()
        {
            return serializer.Load(pathname);
        }

    }
}
