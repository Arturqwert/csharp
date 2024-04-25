using PlayerService.PlayerService.DataProvider;
using PlayerService.PlayerService.Model;
using System.Collections.ObjectModel;

namespace PlaygroundSharp
{
    internal class PlayerService : IPlayerService
    {
        private IDataProviderJSON provider;

        public PlayerService()
        {
            provider = new DataProviderJSON();
        }

        public Response<int> AddPointsToPlayer(int playerId, int points)
        {
            var list = provider.Load().ToList();
            var player = list.Select(p => p).Where(p => p.Id == playerId).FirstOrDefault()!;

            if (player is null)
            {
                return new Response<int>(Statuses.BAD_REQUEST, $"Player with id {playerId} doesn't exist!", playerId);
            }

            player.Points += points;
            provider.Save(new Collection<Player>(list));

            return new Response<int>(Statuses.OK, $"{points} points has been added to player with id {playerId}.", playerId);
        }

        public Response<int> CreatePlayer(Player player)
        {
            Collection<Player>? list = provider.Load();
            list!.Add(player);
            provider.Save(new Collection<Player>(list));

            return new Response<int>(Statuses.OK, $"Player '{player}' has been created!", player.Id);
        }

        public Response<Player> DeletePlayer(int playerId)
        {
            var list = provider.Load().ToList();
            Player player = list.Select(p => p).Where(p => p.Id == playerId).FirstOrDefault()!;

            if (player is null)
            {
                return new Response<Player>(Statuses.BAD_REQUEST, $"Player with id {playerId} doesn't exist!", player!);
            }

            list.Remove(player);
            provider.Save(new Collection<Player>(list));

            return new Response<Player>(Statuses.OK, $"Player with id {playerId} has been deleted!", player);
        }

        public Response<Player> GetPlayerId(int playerId)
        {
            var list = provider.Load().ToList();
            var res = list.Select(p => p).Where(p => p.Id == playerId).FirstOrDefault()!;

            if(res is null)
            {
                return new Response<Player>(Statuses.BAD_REQUEST, $"Player with id {playerId} doesn't exist!", res!);
            }

            return new Response<Player>(Statuses.OK, $"Player with id {playerId}.", res);
        }

        public Response<Collection<Player>> GetPlayers()
        {
            Collection<Player>? list = new();
            list = provider.Load();
        
            return new Response<Collection<Player>>(Statuses.OK, "Players collection.", list!);
        }
    }
}
