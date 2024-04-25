using PlayerService.PlayerService.Model;

namespace PlaygroundSharp
{
    public enum Command
    { 
        Exit,
        Get,
        Add,
        Delete,
        Create,
        Help
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Show commands type \"help\"");

            IPlayerService service = new PlayerService();
            PrintCommands();

            while (true)
            {
                Console.Write("\nType command: ");

                string? input = Console.ReadLine();

                if(!Enum.GetNames(typeof(Command)).Select(i => i.ToLower()).ToList().Any(i => input!.Contains(i)))
                {
                    Console.WriteLine("Incorrect input! For show commands type \"help\"");
                }

                if (string.Equals(input, "help", StringComparison.OrdinalIgnoreCase))
                {
                    PrintCommands();
                }

                if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                    Environment.Exit(0);

                if (string.Equals(input, "clear", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                }

                if (string.Equals(input, "get", StringComparison.OrdinalIgnoreCase))
                {
                    var response = service.GetPlayers();

                    CommandHandler(response, 
                        () => response.Payload.ToList().ForEach(p => Console.WriteLine(p)), 
                        () => Console.WriteLine($"{response.Status} {Environment.NewLine}{response.Message}"));
                    continue;
                }

                if (input!.StartsWith("create"))
                {
                    string parameters = input[7..];
                    string[] arrayParams = parameters.Split(',');
                    var response = service.CreatePlayer(new Player(int.Parse(arrayParams[0]), int.Parse(arrayParams[1])));

                    CommandHandler(response, 
                        () => Console.WriteLine(response.Message), 
                        () => Console.WriteLine($"{response.Status} {Environment.NewLine}{response.Message}"));
                }

                if (input.StartsWith("delete"))
                {
                    int id;
                    var isParsed = int.TryParse(input[7..], out id);

                    if (!isParsed)
                    {
                        Console.WriteLine("Incorrect input! Type int value!");
                    }
                    else
                    {
                        var response = service.DeletePlayer(id);

                        CommandHandler(response, 
                            () => Console.WriteLine(response.Message), 
                            () => Console.WriteLine($"{response.Status} {Environment.NewLine}{response.Message}"));
                    }
                }

                if (input.StartsWith("get"))
                {
                    string param = input[4..];
                    var response = service.GetPlayerId(int.Parse(param));

                    CommandHandler(response, 
                        () => Console.WriteLine(response.Payload), 
                        () => Console.WriteLine($"{response.Status} {Environment.NewLine}{response.Message}"));
                }

                if (input.StartsWith("add"))
                {
                    string parameters = input[4..];
                    string[] arrayParams = parameters.Split(',');
                    var response = service.AddPointsToPlayer(int.Parse(arrayParams[0]), int.Parse(arrayParams[1]));

                    CommandHandler(response,
                        () => Console.WriteLine(response.Message), 
                        () => Console.WriteLine($"{response.Status} {Environment.NewLine}{response.Message}"));
                }
            }
        }

        private static void CommandHandler<T>(Response<T> response,  Action success, Action error)
        {
            if (response.Status == Statuses.OK)
            {
                success?.Invoke();
            }
            else
            {
                error?.Invoke();
            }
        }

        private static void PrintCommands()
        {
            Console.WriteLine("""
                    -for quit type "exit".
                    -get all players type "get".
                    -delete player type "delete {idPlayer}".
                    -get player type "get {idPlayer}".
                    -add points to player type "add {idPlayer}, {points}" .
                    -create player type "create {idPlayer}, {points}".
                    -clear for clean console.
                    """);
        }
    }
}