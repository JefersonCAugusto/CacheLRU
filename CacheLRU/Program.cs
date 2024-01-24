namespace CacheLRU
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cacheControl = new CacheControl();

            while (true)
            {
                Console.WriteLine("1- Adicionar no cache");
                Console.WriteLine("2- Usa cache");
                string value = Console.ReadLine();

                if (int.Parse(value) == 1)
                {

                    Console.WriteLine("key: ");
                    int cachekey = int.Parse(Console.ReadLine());

                    Console.WriteLine("value: ");
                    string cachevalue = Console.ReadLine();
                    cacheControl.SetKey(cachekey, cachevalue);
                }
                else
                {
                    Console.WriteLine("key: ");
                    int cachekey = int.Parse(Console.ReadLine());
                    Console.WriteLine(cacheControl.GetValue(cachekey));
                }

                Console.WriteLine( "Clear");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public class Data
        {
            public Data(string? value)
            {
                Value = value;
                Used = DateTime.UtcNow;
            }

            public DateTime Used { get; set; }
            public string? Value { get; set; }

            public void UpdatesDatetime()
            {
                Used = DateTime.UtcNow;
            }

        }

        public class CacheControl
        {
            private const int CACHE_LIMIT = 5;
            public Dictionary<int, Data> Cache { get; set; } = new Dictionary<int, Data>();

            public void SetKey(int key, string value)
            {
                if (Cache.Count() <= CACHE_LIMIT)
                    Cache.Add(key, new Data(value));

                else
                {
                    var oldestValueCache = Cache.OrderBy(x => x.Value.Used).First();

                    Cache.Remove(oldestValueCache.Key);

                    Cache.Add(key, new Data(value));
                }
            }
            public string GetValue(int key)
            {
                if (!Cache.TryGetValue(key, out var value))
                    return "-1";

                value.UpdatesDatetime();
                Cache[key] = value;
                return value.Value!;


            }
        }
    }
}