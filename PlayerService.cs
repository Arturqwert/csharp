using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace PlaygroundSharp
{
    internal class PlayerService : IPlayerService
    {
        string path = "C:\\Users\\artur\\source\\repos\\курс\\PlaygroundSharp\\players.json";

        public int AddPointsToPlayer(int playerId, int points)
        {
            var list = LoadPlayers().ToList();
            var player = list.Select(p => p).Where(p => p.Id == playerId).FirstOrDefault()!;
            player.Points += points;
            WrireToFile(list);

            return playerId;
        }

        public int CreatePlayer(Player player)
        {
            Collection<Player>? list = LoadPlayers();
            list!.Add(player);

            WrireToFile(list.ToList());

            return player.Id;
        }

        public Player DeletePlayer(int playerId)
        {
            var list = LoadPlayers().ToList();
            Player player = list.Select(p => p).Where(p => p.Id == playerId).FirstOrDefault()!;
            list.Remove(player);
            WrireToFile(list);

            return player;
        }

        public Player GetPlayerId(int playerId)
        {
            var list = LoadPlayers().ToList();
            return list.Select(p => p).Where(p => p.Id == playerId).FirstOrDefault()!;
        }

        public Collection<Player> GetPlayers()
        {
            Collection<Player>? list = new();
            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Exists)
            {
                list = LoadPlayers();
            }

            return list!;
        }

        private Collection<Player> LoadPlayers()
        {
            Collection<Player>? list = new();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                try
                {
                    list = JsonSerializer.Deserialize<Collection<Player>>(fs);
                }
                catch { }
            }

            return list!;
        }
    
        private void WrireToFile(List<Player> list)
        {
            using (FileStream fs = new FileStream(path, FileMode.Truncate))
            {
                JsonSerializer.Serialize(fs, list);
            }
        }
    }
}
