using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundSharp
{
    internal interface IPlayerService
    {
        Player GetPlayerId(int playerId);
        Collection<Player> GetPlayers();
        int CreatePlayer (Player player);
        Player DeletePlayer (int playerId);
        int AddPointsToPlayer(int playerId, int points);
    }
}
