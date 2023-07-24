using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace cacheLibrary
{
        public class finbourneCache<T>
    {
        private readonly ConcurrentDictionary<string, CacheItem> cache = new ConcurrentDictionary<string, CacheItem>();
        private readonly int maxCacheSize;
        private readonly LinkedList<string> accessOrderList = new LinkedList<string>();

        public finbourneCache(int maxCacheSize)
        {
            if (maxCacheSize <= 0)
            {
                throw new ArgumentException("maxCacheSize must be greater than 0.");
            }

            this.maxCacheSize = maxCacheSize;
        }

        public void Add(string key, T value, out T outvalue)
        {

            outvalue = default;
            var newCacheItem = new CacheItem { Value = value };
            cache.AddOrUpdate(key, newCacheItem, (_, existingItem) =>
            {
                accessOrderList.Remove(existingItem.AccessNode);
                return newCacheItem;
                
            });

            var newNode = new LinkedListNode<string>(key);
            newCacheItem.AccessNode = newNode;
            accessOrderList.AddLast(newNode);

            // Check if the cache size exceeds the maximum limit and evict least accessed elements if necessary, return cachitem if evicted to calling class for reporting
            while (cache.Count > maxCacheSize)
            {
                var leastAccessedKey = accessOrderList.First.Value;
                accessOrderList.RemoveFirst();
                if (cache.TryGetValue(leastAccessedKey, out var remvalue))
                {
                    outvalue = remvalue.Value;
                }
                cache.TryRemove(leastAccessedKey, out _);
            }
        }

        public bool TryGetValue(string key, out T value)
        {
            if (cache.TryGetValue(key, out var cacheItem))
            {
                MoveToRecentlyUsed(cacheItem);
                value = cacheItem.Value;
                return true;
            }

            value = default;
            return false;
        }

        public bool Remove(string key)
        {
            return cache.TryRemove(key, out _);
        }

        public void Clear()
        {
            cache.Clear();
            accessOrderList.Clear();
        }

        public int GetCachceCount
        {
            get
            {
                return cache.Count;
            }
        }

        private void MoveToRecentlyUsed(CacheItem cacheItem)
        {
            accessOrderList.Remove(cacheItem.AccessNode);
            var newNode = new LinkedListNode<string>(cacheItem.AccessNode.Value);
            cacheItem.AccessNode = newNode;
            accessOrderList.AddLast(newNode);
        }

        private class CacheItem
        {
            public T Value { get; set; }
            public LinkedListNode<string> AccessNode { get; set; }
        }
    }

    

}