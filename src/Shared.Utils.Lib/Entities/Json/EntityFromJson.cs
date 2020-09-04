using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace Shared.Utils.Lib.Entities.Json
{
    public class EntityFromJson<TEntity>
    {
        private readonly string _input;

        public EntityFromJson(string input)
        {
            _input = input ?? "";
        }

        public static implicit operator TEntity(EntityFromJson<TEntity> obj)
        {
            return obj.GetValue();
        }

        public TEntity GetValue()
        {
            return Create(_input);
        }

        private static TEntity Create(string json)
        {
            using (var memoryStream = new MemoryStream())
            {
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                memoryStream.Write(jsonBytes, 0, jsonBytes.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (var jsonReader = JsonReaderWriterFactory.CreateJsonReader(
                    memoryStream,
                    Encoding.UTF8,
                    XmlDictionaryReaderQuotas.Max,
                    null))
                {
                    var serializer = new DataContractJsonSerializer(typeof(TEntity));
                    TEntity entity = (TEntity)serializer.ReadObject(jsonReader);
                    return entity;
                }
            }
        }
    }
}