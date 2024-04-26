using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerService.PlayerService.Model;

namespace PlaygroundSharp
{
    internal interface IPlayerService
    {
        Response<Player> GetPlayerId(int playerId);
        Response<Collection<Player>> GetPlayers();
        Response<int> CreatePlayer (string nick);
        Response<Player> DeletePlayer (int playerId);
        Response<int> AddPointsToPlayer(int playerId, int points);
    }
}
