using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleCoin
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var table = SetupTableByArgs(args);

            await table.Create();
            table.Render();
            SetRefreshTimer(table);
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static MainTable SetupTableByArgs(string[] args)
        {
            if (args.Length == 1)
            {
                return new MainTable(args[0]);
            }
            if (args.Length >= 2)
            {
                return new MainTable(args[0], args[1].Split(','));
            }
            
            return new MainTable();
        }

        private static void SetRefreshTimer(MainTable table)
        {
            var startTimeSpan = TimeSpan.FromMinutes(1);
            var periodTimeSpan = TimeSpan.FromMinutes(1);
            var timer = new Timer((e) =>
            {
                _ = Task.Factory.StartNew(table.Refresh);
            }, null, startTimeSpan, periodTimeSpan);
        }
    }
}
