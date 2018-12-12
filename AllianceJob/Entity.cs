using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AllianceResSample
{
    /// <summary>
    /// Base class defining basic-root methods and properties.
    /// </summary>
    /// <typeparam name="T">Type to be implemented.</typeparam>
    [Serializable]
    public class Entity<T> : IEquatable<Entity<T>> 
    {
        private Guid _Id;

        /// <summary>
        /// Constructor.
        /// </summary>
        internal Entity() { }

        /// <summary>
        /// Constructor.  
        /// </summary>
        /// <param name="item">Item to be copied in.</param>
        public Entity(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("obj");
            }

            CopyValues(item);
        }

        /// <summary>
        /// This method saves the current item to the persisted collection.
        /// </summary>
        public void Save()
        {
            Dictionary<Guid, object> list = LoadListFromFile();
           
            if (this.IdAsGuid == Guid.Empty)
            {
                this._Id = Guid.NewGuid();
                list.Add(this.IdAsGuid, this);
            }
            else
            {
                object obj = FindInList(this.IdAsGuid);

                if (obj != null)
                {
                    list.Remove(this.IdAsGuid);
                    list.Add(this.IdAsGuid, this);
                }
            }

            SaveInteral(list);
        }

        /// <summary>
        /// This method removes the current item from the persisted collection and then saves the collection to the file system.
        /// </summary>
        public void Delete()
        {
            Dictionary<Guid, object> list = LoadListFromFile();

            if (this.IdAsGuid == Guid.Empty)
            {
                throw new InvalidOperationException("Can not delete object that has not been set.");
            }

            list.Remove(this.IdAsGuid);
            SaveInteral(list);

            NullAllValues();
        }

        /// <summary>
        /// Finds a given item, based on the given id (as a string), in the saved collection.
        /// </summary>
        /// <param name="id">id to search for.</param>
        /// <returns></returns>
        public static T Find(string id)
        {
            if (id == null || id == string.Empty)
            {
                return default(T);
            }

            Guid localid;

            if (Guid.TryParse(id, out localid)){
               return Find(localid);
            }
            else
            {
                throw new ArgumentException("id is not a valid GUID value.");
            }                       
        }

        /// <summary>
        /// Finds a given item, based on the given id (as a GUID), in the saved collection.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T Find(Guid id)
        {
            T obj = FindInList(id);

            if (obj != null)
            {
                return obj;
            }
            else
            {
                return default(T);
            }
        }
        
        private void SaveInteral(Dictionary<Guid, object> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            using (FileStream fstream = new FileStream(Constants.FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, list);
                    stream.Position = 0;
                    stream.WriteTo(fstream);
                }
            }
        }

        private void NullAllValues()
        {
            this._Id = Guid.Empty;
        }

        private static Dictionary<Guid, object> LoadListFromFile()
        {
            if (File.Exists(Constants.FilePath))
            {
                using (FileStream reader = new FileStream(Constants.FilePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (Dictionary<Guid, Object>)formatter.Deserialize(reader);
                }                                     
            }
            else
            {
                return new Dictionary<Guid, object>();
            }
        }

        private static T FindInList(Guid id)
        {
            Dictionary<Guid, object> list = LoadListFromFile();
            object val;

            if(list.TryGetValue(id, out val))
            {
                return (T)val;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Overridable method used to populate all properties for the current item using the given item. 
        /// </summary>
        /// <param name="item">item to </param>
        internal virtual void CopyValues(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("obj");
            }

            Entity<T> localentity = item as Entity<T>;

            if (localentity == null)
            {
                throw new InvalidCastException("obj must be of type Entity.");
            }

            _Id = new Guid(localentity.Id);
        }

        /// <summary>
        /// This method evaluates the given <see cref="Entity{T}"/> against the current <see cref="Entity{T}"/>.
        /// </summary>
        /// <param name="other">Item to evaluate the current <see cref="Entity{T}"/> against.</param>
        /// <returns>boolean</returns>
        public bool Equals(Entity<T> other)
        {
            if (other == null) { return false; }

            return (object.ReferenceEquals(this.IdAsGuid, other.IdAsGuid)
                || this.IdAsGuid != null && this.IdAsGuid.Equals(other.IdAsGuid));
        }

        /// <summary>
        /// Current item's unique id in string format.
        /// </summary>
        /// <remarks>
        /// NOTE:  This property was added in order to satisfy the given test cases.  I usually perfer to use guid ids when possible
        /// and then test for GUID.Empty. Since Guid values cannot be null and the given test cases (which are not to be modified) 
        /// evaluate against null, I created this additional property.  
        /// </remarks>
        public string Id
        {
            get
            {
                if (this.IdAsGuid == Guid.Empty)
                {
                    return string.Empty;
                }
                else
                {
                    return this.IdAsGuid.ToString();
                }
            }
        }

        /// <summary>
        /// The current <see cref="Entity{T}"/>'s unique id.
        /// </summary>
        /// <remarks>
        /// See <see cref="Id"/> remarks for addition information.
        /// </remarks>
        internal Guid IdAsGuid
        {
            get
            {
                return _Id;
            }
        }
    }
}
