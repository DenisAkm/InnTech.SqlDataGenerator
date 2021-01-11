using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InnTech.SqlDataGenerator
{
    public class EntityModel : IEnumerable<EntityProperty>, IDisposable
    {
        public string EntityName { get; }

        private List<EntityProperty> _properties;
        private bool disposedValue;

        public Guid Id
        {
            get
            {
                var stringId = _properties.First(x => x.Name == "Id").Value.Replace("'", "");
                return Guid.TryParse(stringId, out var guid) ? guid : Guid.Empty;
            }
        }
        public EntityModel(string entityName)
        {
            EntityName = entityName;
            _properties = new List<EntityProperty>();
        }

        public EntityProperty this[int index] => _properties[index];

        public void Add(EntityProperty columnInfo)
        {
            _properties.Add(columnInfo);
        }

        public IEnumerator<EntityProperty> GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public EntityModel Copy()
        {
            EntityModel em = new EntityModel(EntityName);

            foreach (var prop in this)
            {
                var entityProp = new EntityProperty()
                {
                    Name = prop.Name,
                    ReferenceEntity = prop.ReferenceEntity,
                    Type = prop.Type,
                    Value = prop.Value
                };
                em.Add(entityProp);
            }

            return em;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _properties = null;
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~EntityModel()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}