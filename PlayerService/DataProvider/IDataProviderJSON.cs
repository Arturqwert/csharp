using PlayerService.PlayerService.Model;
using System.Collections.ObjectModel;

namespace PlayerService.PlayerService.DataProvider
{
    internal interface IDataProviderJSON
    {
        Collection<Player> Load();

        void Save(Collection<Player> players);
    }
}
