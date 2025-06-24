partial class Program
{

    private static Program _singleton;

    public static Program Instance
    {
        get
        {
            _singleton ??= new Program();
            return _singleton;
        }
    }

    static void Main(string[] args)
    {
        
        using var cancelSource = new CancellationTokenSource();
        Console.CancelKeyPress += (object? s, ConsoleCancelEventArgs e) =>
        {
            e.Cancel = true;
            cancelSource.Cancel();
        };

        Console.WriteLine("coucou");

        //Instance.MethodeBloquante(4000);

        //Instance.ExoLinq1();
        //Instance.ExoLinq2();
        //Instance.ExoLinq3();
        //Instance.ExoLinq4();
        //Instance.ExoLinq5();
        Instance.ExoLinq6();

        Console.WriteLine("");
        Console.WriteLine("Fini. Appuyer sur une touche pour quitter.");
        Console.ReadKey();

    }
}