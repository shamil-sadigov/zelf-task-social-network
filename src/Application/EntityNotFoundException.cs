#region

using System;

#endregion

namespace Application
{
    public class EntityNotFoundException<TKey> : ApplicationException
    {
        public EntityNotFoundException(TKey key, string entityName)
        {
            Key = key;
            Message = $"'{entityName}' entity was not found by '{key}' key";
        }

        public TKey Key { get; }

        public override string Message { get; }
    }
}