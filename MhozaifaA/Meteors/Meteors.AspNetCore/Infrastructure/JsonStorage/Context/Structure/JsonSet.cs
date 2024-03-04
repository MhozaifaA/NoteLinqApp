
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Infrastructure.JsonStorage
{
    public class JsonSet<T> : IEnumerable<T>, IDisposable where T : JsonStorageBase
    {
        public static JsonSet<T> Configere() => new();

        public bool HasChanged { get; set; }
        private bool IsLoad { get; set; } = false;
        private Collection<T> Collection = new Collection<T>();
        private void ActiveHasChanged() => HasChanged = true;
        private string CollectionPath = Path.Combine(Directory.GetCurrentDirectory(), typeof(T).Name + "_collection.json");

        public T this[int index]
        {
            get
            {
                return Collection[index];
            }

            set
            {
                if (index < Collection.Count)
                    Collection[index] = value;
                ActiveHasChanged();
            }
        }

        public T? Find(T item)
        {
            return Collection.Where(x => item.Equals(x)).FirstOrDefault();
        }


        public void Add(T item)
        {
            item.AutoIdentity();
            Collection.Add(item);
            ActiveHasChanged();
        }

        public bool Update(T item)
        {
           return Update(item.Id, item);
        }

        public bool Update(string id,T item)
        {
            var data = Collection.Where(x => x.Id == id).FirstOrDefault();
            if (data is not null)
            {
                Remove(data);
                Add(item);
                ActiveHasChanged();
                return true;
            }
            return false;
        }

        public bool Remove(T item)
        {
            if (Collection.Remove(item))
            {
                ActiveHasChanged();
                return true;
            }
            return false;
        }

        public T? Remove(string id)
        {
            var item = Collection.Where(x => x.Id == id).FirstOrDefault();
            if (item is not null)
            {
                if (Collection.Remove(item))
                {
                    ActiveHasChanged();
                    return item;
                }
            }
            return null;
        }

        public void Fill()
        {
            IsLoad = true;
            Collection.Clear();
            string data = File.ReadAllText(CollectionPath);
            if(data is not null)
                foreach(var item in System.Text.Json.JsonSerializer.Deserialize<Collection<T>>(data)??Collection)
                    Collection.Add(item);
        }


        public async Task FillAsync()
        {
            IsLoad = true;
            Collection.Clear();
            using var stream =  File.OpenRead(CollectionPath);
            var data = await System.Text.Json.JsonSerializer.DeserializeAsync<Collection<T>>(stream);
            await stream.DisposeAsync();
            if (data is not null)
                foreach (var item in data)
                    Collection.Add(item);
        }

        public void Load()
        {
            if(!File.Exists(CollectionPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(CollectionPath));
                File.WriteAllText(CollectionPath,$"[{Environment.NewLine}]");
            }
            if (!IsLoad)
                Fill();
        }

        public async ValueTask LoadAsync()
        {
            if (!File.Exists(CollectionPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(CollectionPath));
                await File.WriteAllTextAsync(CollectionPath, $"[{Environment.NewLine}]");
            }
            if (!IsLoad)
              await FillAsync();
        }

        public void SaveChange()
        {
            if (HasChanged)
            {
                File.WriteAllText(CollectionPath
                    , System.Text.Json.JsonSerializer.Serialize(Collection));
            }
            HasChanged = false;
        }

        public async ValueTask SaveChangeAsync()
        {
            if (HasChanged)
            {
                using var stream = File.Create(CollectionPath);
                await System.Text.Json.JsonSerializer.SerializeAsync(stream, Collection);
                await stream.DisposeAsync();
            }
            HasChanged = false;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        ~JsonSet()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (Collection is not null)
                Collection.Clear();
            Collection = null;
            CollectionPath = null;

        }
    }
}
