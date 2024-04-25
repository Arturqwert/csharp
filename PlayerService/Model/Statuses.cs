using System.ComponentModel;

namespace PlayerService.PlayerService.Model
{
    internal enum Statuses
    {
        [Description("Success")]
        OK = 200,

        [Description("Not found")]
        BAD_REQUEST = 400
    }
}
