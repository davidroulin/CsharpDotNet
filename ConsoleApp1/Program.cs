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
        Console.WriteLine("coucou");
        Instance.MethodeBloquante(4000);
    }
}