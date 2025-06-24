
namespace Soft
{
    class SoftLauncher
    {
        static string dataPath = "C:\\temp\\data";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Demarrage...");
            Console.WriteLine("");

            //IEnumerable<Func<Task<string[]>>> parrallelTasks = LogReader.LoadFolder(dataPath);

            //foreach (var task in parrallelTasks)
            //{
            //    string[] lines = await task();
            //    Console.WriteLine($"Found {lines.Length} lines");
            //}

            var data = await IPInfo.GetGeoDataAsync("94.104.28.29");

            // Garde la console a l'ecran une fois tout fini. Appuyer sur une touche pour fermer.
            Console.WriteLine("");
            Console.WriteLine("Fini. Appuyer sur une touche pour quitter.");
            Console.ReadKey();
        }
    }
}