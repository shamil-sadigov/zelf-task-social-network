using System;

namespace Application
{
    public class EntityNotFoundException<TKey>:ApplicationException
    {
        public TKey Key { get; }

        public EntityNotFoundException(TKey key, string entityName)
        {
            Key = key;
            Message = $"'{entityName}' entity was not found by '{key}' key";
        }

        public override string Message { get; }
    }
}