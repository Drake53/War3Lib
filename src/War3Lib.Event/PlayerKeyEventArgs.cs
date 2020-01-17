using static War3Api.Common;

namespace War3Lib.Event
{
    public class PlayerKeyEventArgs : PlayerEventArgs
    {
        private readonly oskeytype _key;
        private readonly int _metaKey;
        private readonly bool _keyDown;

        public PlayerKeyEventArgs(player triggerPlayer, oskeytype key, int metaKey, bool keyDown)
            : base(triggerPlayer)
        {
            _key = key;
            _metaKey = metaKey;
            _keyDown = keyDown;
        }

        public oskeytype Key => _key;

        public int MetaKey => _metaKey;

        public bool KeyDown => _keyDown;
    }
}