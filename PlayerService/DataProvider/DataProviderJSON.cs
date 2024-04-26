using PlayerService.PlayerService.Model;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace PlayerService.PlayerService.DataProvider
{
    internal class DataProviderJSON : IDataProviderJSON
    {
        string path = "C:\\Users\\artur\\source\\repos\\курс\\PlaygroundSharp\\PlayerService\\Storage\\players.json";

        public Collection<Player> Load()
        {
            Collection<Player>? list = new();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                try
                {
                    list = JsonSerializer.Deserialize<Collection<Player>>(fs);
                }
                catch  { }
            }

            return list!;
        }

        public void Save(Collection<Player> players)
        {
            using (FileStream fs = new FileStream(path, FileMode.Truncate))
            {
                JsonSerializer.Serialize(fs, players);
            }
        }
    }
}
