using System.Text;

namespace Cache.LinkedList
{
    internal class Program
    {
        /*
        Cache LRU?
             O objetivo é manter em memória as páginas que foram acessadas recentemente e remover aquelas que não foram utilizadas por um período mais longo.
        */
        static void Main(string[] args)
        {
            var cacheControl = new CacheControl();


            while (true)
            {
                Console.WriteLine(cacheControl.ToString());
           
                Console.WriteLine("1- Adicionar no cache");
                Console.WriteLine("2- Usa cache");
                string value = Console.ReadLine();

                if (int.Parse(value) == 1)
                {

                    Console.Write("key: ");
                    int cachekey = int.Parse(Console.ReadLine());

                    Console.Write("value: ");
                    string cachevalue = Console.ReadLine();

                    cacheControl.Set(cachekey, cachevalue);
                }
                else
                {
                    Console.WriteLine("key: ");
                    int cachekey = int.Parse(Console.ReadLine());
                    Console.WriteLine(cacheControl.Get(cachekey));
                }

                Console.WriteLine();




                Console.WriteLine("Clear");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public class CacheControl
        {
            private readonly int _cacheSize = 2;
            public LinkedList<Dictionary<int, string>> Cache { get; set; } = new LinkedList<Dictionary<int, string>>();

            public void Set(int key, string value)
            {
                var newCache = new Dictionary<int, string>() { { key, value } };

                if (Cache.Any(x => x.ContainsKey(key)))
                    return;

                if (Cache.Count <= _cacheSize)
                {
                    Cache.AddFirst(newCache);
                }
                else
                {
                    Cache.RemoveLast();
                    Cache.AddFirst(newCache);
                }
            }

            public string Get(int key)
            {
                var cache = Cache.FirstOrDefault(x => x.ContainsKey(key));

                if (cache == null)
                    return "-1";

                Cache.Remove(cache);
                Cache.AddFirst(cache);

                return cache[key];
            }

            public override string ToString()
            {
                var text = new StringBuilder("Cache view:\n");

                foreach (var node in Cache)
                {
                    foreach (var dic in node)
                    {
                        text.AppendLine($"Key: {dic.Key}, Value: {dic.Value}");
                    }
                }
                text.AppendLine("\n---------------------------------");
                return text.ToString();
            }

        }
    }
}