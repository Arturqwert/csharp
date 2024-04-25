namespace PlaygroundSharp
{
    public enum Command
    { 
        Exit,
        GetAll,
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

            while (true)
            {
                Console.Write("Type command: ");

                string? input = Console.ReadLine()!.ToLower();

                if (!Enum.GetNames(typeof(Command)).Select(c => c.ToLower()).ToList().Contains(input!))
                {
                    Console.WriteLine("Incorrect input! For show commands type \"help\"");
                }

                if (input == "help")
                {
                    Console.WriteLine("""
                    -for quit type "exit"
                    -get all players type "get"
                    -delete player type "delete {idPlayer}"
                    -get player type "get {idPlayer}"
                    -add points to player type "add {idPlayer}, {points}" 
                    -create player type "create {idPlayer}, {points}"
                    """);
                }

                if (input == "get all")
                {
                    List<Player> players = service.GetPlayers().ToList();
                    if (players.Count == 0)
                        Console.WriteLine("Players list is empty or not exists.");
                    else
                        players.ForEach(p => Console.WriteLine(p));

                    continue;
                }

                if (input!.Contains("create"))
                {
                    string parameters = input[7..];
                    string[] arrayParams = parameters.Split(',');
                    service.CreatePlayer(new Player(int.Parse(arrayParams[0]), int.Parse(arrayParams[1])));

                    Console.WriteLine($"Player with id {arrayParams[0]} has been created!");
                }

                if (input.Contains("delete"))
                {
                    int id;
                    var isParsed = int.TryParse(input[7..], out id);

                    if (!isParsed)
                    {
                        Console.WriteLine("Incorrect input! Type int value!");
                    }
                    else
                    {
                        var player = service.GetPlayerId(id);
                        if (player != null)
                        {
                            var res = service.DeletePlayer(id);
                            Console.WriteLine($"Player with id {res.Id} has been deleted!");
                        }
                        else
                            Console.WriteLine($"Player with id {id} does not exist!");
                    }
                }

                if (input.Contains("get"))
                {
                    string param = input[4..];
                    var res = service.GetPlayerId(int.Parse(param));

                    Console.WriteLine(res);
                }

                if (input.Contains("add"))
                {
                    string parameters = input[4..];
                    string[] arrayParams = parameters.Split(',');
                    var res = service.AddPointsToPlayer(int.Parse(arrayParams[0]), int.Parse(arrayParams[1]));

                    Console.WriteLine($"To player with id {res} added {arrayParams[1]} points!");
                }

                if (input == "exit")
                    Environment.Exit(0);
            }
        }
    }
}