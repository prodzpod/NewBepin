using System.Security.AccessControl;
using System.Security.Principal;

public class RemoveOldCecilConsole
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"Trying to delete {args.Length} files, try closing & rebooting the game if it doesn't finish.");
        foreach (string arg in args)
        {
            Console.WriteLine($"Waiting for {arg} to be available");
            while (true)
            {
                try { if (File.Exists(arg)) File.Delete(arg); break; }
                catch { Thread.Sleep(1000); }
            }
        }
        Console.WriteLine($"Setup complete!");
    }
}