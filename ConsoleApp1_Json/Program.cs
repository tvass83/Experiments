using Newtonsoft.Json;

namespace ConsoleApp1_Json
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = new Data();

            string file = "data.json";

            var ser = new JsonSerializer
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };

            var s = SerializeData2(ser, data);

            //var data2 = DeserializeData(ser, file);
        }

        private static void SerializeData(JsonSerializer serializer, string file, Data data)
        {
            using var streamWriter = File.CreateText(file);
            using var jsonWriter = new JsonTextWriter(streamWriter);

            serializer.Serialize(jsonWriter, data);
        }

        private static string SerializeData2(JsonSerializer serializer, Data data)
        {
            using var stringWriter = new StringWriter();
            using var jsonWriter = new JsonTextWriter(stringWriter);

            serializer.Serialize(jsonWriter, data);

            return stringWriter.ToString();
        }

        private static Data DeserializeData(JsonSerializer serializer, string file)
        {
            using var streamReader = File.OpenText(file);
            using var jsonReader = new JsonTextReader(streamReader);

            return serializer.Deserialize<Data>(jsonReader);
        }
    }

    public class Data
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

        public Data2 Data2 { get; set; } = new();
    }

    public class Data2
    {
        public int MyProperty { get; set; }
    }
}
