using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Infrastructure.JsonStorage
{

    public interface IIndexJson<T>
    {
        public T Id { get; set; }
    }

    public interface Dateable
    {
        public DateTime? Date { get; set; }
    }


    public interface IJsonKey : IIndexJson<string> { }

    public class GeneratorJsonIndex
    {
        protected int Id { get; private set; }
        public GeneratorJsonIndex()
        {
             Id = new Random().Next(100000, 999999);
        }
    }

    public record JsonStorageBase : Dateable, IJsonKey
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        public JsonStorageBase()
        {
            //if (string.IsNullOrEmpty(Id) || Id == default(Guid).ToString())
            //    Id = Guid.NewGuid().ToString();
        }
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        public void AutoIdentity()
        {
            if (string.IsNullOrEmpty(Id) || Id == Guid.Empty.ToString())
                Id = Guid.NewGuid().ToString();

            if (!Date.HasValue)
                Date = DateTime.Now;
        }
    }

}
