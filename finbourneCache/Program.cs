using cacheLibrary;

public class Program
{
    public static void Main()
    {
        // Create a cache with a maximum size of 1000 entries
        finbourneCache<string> cache = new finbourneCache<string>(maxCacheSize: 1000);

        //declare a random number generator for key access and remval
        Random rnd = new Random();

        // Add data
        for(int i = 0; i < 1000; i++)
        {
            cache.Add($"key{i}", $"value{i}", out var avalue);
            if (avalue != null)
            {
                Console.WriteLine($"Maximum cache size exceeded least used item : {avalue} has been removed");
            }
        }

        // Complete 100000 data retreivals from the cache to build the access order
        for (int i = 0; i < 100000; i++)
        {
            int k = rnd.Next(0, 1000);    
            if (cache.TryGetValue("key"+k.ToString(), out var value))
            {
               // Console.WriteLine($"Value for key{k}: {value}"); //remove comments slashes to see get values in console
            }
        }

        for (int i = 100; i < 110; i++)
        {
            cache.Add($"key{i}", $"value{i}", out var avalue);
            if (avalue != null)
            {
                Console.WriteLine($"Maximum cache size exceeded least used item : {avalue} has been removed");
            }
        }
        Console.WriteLine($"cache size after additions : {cache.GetCachceCount}");

        // Removing random data from cache
        for (int i = 0; i < 100; i++)
        {
            int k = rnd.Next(0, 1000);
            cache.Remove("key" + k);
        }
        Console.WriteLine($"cache size after removals : {cache.GetCachceCount}");

        // Clearing the cache
        cache.Clear();
        Console.WriteLine($"cache size after clear : {cache.GetCachceCount}");
    }
}
