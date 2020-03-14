using static War3Api.Common;

namespace War3Lib.Event.Player
{
    public class PlayerEventArgs : System.EventArgs
    {
        private readonly player _triggerPlayer;

        public PlayerEventArgs(player triggerPlayer)
        {
            _triggerPlayer = triggerPlayer;
        }

        public player TriggerPlayer => _triggerPlayer;
    }
}