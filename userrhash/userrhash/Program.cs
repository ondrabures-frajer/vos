namespace userrhash
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Aplikace startuje přihlašovacím formulářem
            Application.Run(new LoginForm());
        }
    }
}
