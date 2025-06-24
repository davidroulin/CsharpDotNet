// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

namespace Soft
{
    class SoftLauncher
    {
        static string dataPath = "C:\\temp\\data";

        static void Main(string[] args)
        {
            Console.WriteLine("Demarrage...");
            Console.WriteLine("");

            foreach (var file in Directory.GetFiles(dataPath))
            {
                Console.WriteLine($"Ouverture du fichier {file}");
                foreach (var line in File.ReadAllLines(file))
                {
                    //Console.WriteLine($"Found line {line}");
                    Console.Write(".");
                }
                Console.WriteLine(" EOF");
            }

            // Garde la console a l'ecran une fois tout fini. Appuyer sur une touche pour fermer.
            Console.WriteLine("");
            Console.WriteLine("Fini. Appuyer sur une touche pour quitter.");
            Console.ReadKey();
        }
    }
}