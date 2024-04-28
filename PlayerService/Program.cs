using PlayerService.PlayerService.Model;

namespace PlaygroundSharp
{
    public enum Command
    {
        Exit,
        Get,
        GetAll,
        Add,
        Delete,
        Create,
        Help,
        Clear
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Show commands type '{Command.Help}'");

            IPlayerService service = new PlayerService();
            PrintCommands();

            while (true)
            {
                Console.Write("\nType command: ");

                string? input = Console.ReadLine()!.ToLower();

                if (string.Equals(input, Command.Help.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    PrintCommands();
                }

                if (string.Equals(input, Command.Exit.ToString(), StringComparison.OrdinalIgnoreCase))
                    Environment.Exit(0);

                if (string.Equals(input, Command.Clear.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                }

                if (string.Equals(input, Command.Get.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    var response = service.GetPlayers();

                    if (response.Payload.Count == 0)
                    {
                        CommandHandler(response,
                        () => Console.WriteLine($"{response.Message}"),
                        () => Console.WriteLine($"{response.Status} {Environment.NewLine}{response.Message}"));
                    }
                    else
                    {
                        CommandHandler(response,
                        () => response.Payload.ToList().ForEach(p => Console.WriteLine(p)),
                        () => Console.WriteLine($"{response.Status} {Environment.NewLine}{response.Message}"));
                    }

                    continue;
                }

                if (input!.StartsWith(Command.Create.ToString().ToLower()))
                {
                    string nick = input[7..];
                    var response = service.CreatePlayer(nick);

                    CommandHandler(response,
                        () => Console.WriteLine(response.Message),
                        () => Console.WriteLine($"{response.Status} {Environment.NewLine}{response.Message}"));
                }

                if (input.StartsWith(Command.Delete.ToString().ToLower()))
                {
                    var result = InputHandler(Command.Delete, input);
                    if (result is not null)
                    {
                        var response = service.DeletePlayer((int)result[0]);

                        CommandHandler(response,
                            () => Console.WriteLine(response.Message),
                            () => Console.WriteLine($"{response.Status} {Environment.NewLine}{response.Message}"));
                    }
                }

                if (input.StartsWith(Command.Get.ToString().ToLower()))
                {
                    var result = InputHandler(Command.Get, input);
                    if (result is not null)
                    {
                        var response = service.GetPlayerId((int)result[0]);

                        CommandHandler(response,
                            () => Console.WriteLine(response.Payload),
                            () => Console.WriteLine($"{response.Status} {Environment.NewLine}{response.Message}"));
                    }
                }

                if (input.StartsWith(Command.Add.ToString().ToLower()))
                {
                    var result = InputHandler(Command.Add, input);
                    if (result is not null)
                    {
                        var response = service.AddPointsToPlayer((int)result[0], (int)result[1]);

                        CommandHandler(response,
                            () => Console.WriteLine(response.Message),
                            () => Console.WriteLine($"{response.Status} {Environment.NewLine}{response.Message}"));
                    }
                }

                if (!Enum.GetNames(typeof(Command)).Select(i => i.ToLower()).ToList().Any(i => input!.Contains(i)))
                {
                    Console.WriteLine("Incorrect input! For show commands type \"help\"");
                }
            }
        }

        private static object[] InputHandler(Command command, string? input)
        {
            int id = 0;
            int points = 0;

            return command switch
            {
                Command.Delete => TryParseInput(input!, 7, out id) ? new object[] { id } : null!,
                Command.Get => TryParseInput(input!, 3, out id) ? new object[] { id } : null!,
                Command.Add => TryParseInputForAdd(input!, 4, out id, out points) ? new object[] { id, points } : null!,
                _ => null!
            } ;

            bool TryParseInput(string input, int index, out int id)
            {
                if (!int.TryParse(input![index..], out id))
                {
                    Console.WriteLine("Incorrect input! Type int value!");
                    return false;
                }

                return true;
            }

            bool TryParseInputForAdd(string input, int index, out int id, out int points)
            {
                id = 0;
                points = 0;
                string parameters = input![index..];
                string[] arrayParams = parameters.Split(',');

                if (arrayParams.Length != 2 || !int.TryParse(arrayParams[0], out id) || !int.TryParse(arrayParams[1], out points))
                {
                    Console.WriteLine("Incorrect input! Type int value for id and points!");
                    return false;
                }

                return true;
            }
        }

        private static void CommandHandler<T>(Response<T> response, Action success, Action error)
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
            Console.WriteLine($"""
                    -for quit type "{Command.Exit}".
                    -get all players type "{Command.Get}".
                    -delete player type "{Command.Delete} 'idPlayer'".
                    -get player type "{Command.Get} 'idPlayer'".
                    -add points to player type "{Command.Add} 'idPlayer', 'points'".
                    -create player type "{Command.Create} 'idPlayer', 'points'".
                    -{Command.Clear} for clean console.
                    """);
        }
    }
}