using static War3Api.Common;

namespace War3Lib.Event.Player
{
    public class PlayerChatEventArgs : PlayerEventArgs
    {
        private readonly string _chatString;

        public PlayerChatEventArgs(player triggerPlayer, string chatString)
            : base(triggerPlayer)
        {
            _chatString = chatString;
        }

        public string ChatString => _chatString;
    }
}