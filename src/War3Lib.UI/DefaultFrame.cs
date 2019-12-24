using System.Collections.Generic;

using static War3Api.Common;

namespace War3Lib.UI
{
    public static class DefaultFrame
    {
        private const int ArraySize = 12;

        private static readonly framehandle _game;
        private static readonly framehandle _world;
        private static readonly framehandle _heroBar;
        private static readonly framehandle[] _heroButton = new framehandle[ArraySize];
        private static readonly framehandle[] _heroHPBar = new framehandle[ArraySize];
        private static readonly framehandle[] _heroMPBar = new framehandle[ArraySize];
        private static readonly framehandle[] _heroIndicator = new framehandle[ArraySize];
        private static readonly framehandle[] _itemButton = new framehandle[ArraySize];
        private static readonly framehandle[] _commandButton = new framehandle[ArraySize];
        private static readonly framehandle[] _systemButton = new framehandle[ArraySize];
        private static readonly framehandle _portrait;
        private static readonly framehandle _minimap;
        private static readonly framehandle[] _minimapButton = new framehandle[ArraySize];
        private static readonly framehandle _tooltip;
        private static readonly framehandle _uberTooltip;
        private static readonly framehandle _chatMsg;
        private static readonly framehandle _unitMsg;
        private static readonly framehandle _topMsg;

        private static readonly framehandle _console;
        private static readonly framehandle _goldText;
        private static readonly framehandle _lumberText;
        private static readonly framehandle _foodText;
        private static readonly framehandle _unitNameText;
        private static readonly framehandle _resourceBar;
        private static readonly framehandle _upperButtonBar;

        static DefaultFrame()
        {
            _game = BlzGetOriginFrame(ORIGIN_FRAME_GAME_UI, 0);
            _world = BlzGetOriginFrame(ORIGIN_FRAME_WORLD_FRAME, 0);
            _heroBar = BlzGetOriginFrame(ORIGIN_FRAME_HERO_BAR, 0);
            _portrait = BlzGetOriginFrame(ORIGIN_FRAME_PORTRAIT, 0);
            _minimap = BlzGetOriginFrame(ORIGIN_FRAME_MINIMAP, 0);
            _tooltip = BlzGetOriginFrame(ORIGIN_FRAME_TOOLTIP, 0);
            _uberTooltip = BlzGetOriginFrame(ORIGIN_FRAME_UBERTOOLTIP, 0);
            _chatMsg = BlzGetOriginFrame(ORIGIN_FRAME_CHAT_MSG, 0);
            _unitMsg = BlzGetOriginFrame(ORIGIN_FRAME_UNIT_MSG, 0);
            _topMsg = BlzGetOriginFrame(ORIGIN_FRAME_TOP_MSG, 0);

            for (var i = 0; i < ArraySize; i++)
            {
                _heroButton[i] = BlzGetOriginFrame(ORIGIN_FRAME_HERO_BUTTON, i);
                _heroHPBar[i] = BlzGetOriginFrame(ORIGIN_FRAME_HERO_HP_BAR, i);
                _heroMPBar[i] = BlzGetOriginFrame(ORIGIN_FRAME_HERO_MANA_BAR, i);
                _heroIndicator[i] = BlzGetOriginFrame(ORIGIN_FRAME_HERO_BUTTON_INDICATOR, i);
                _itemButton[i] = BlzGetOriginFrame(ORIGIN_FRAME_ITEM_BUTTON, i);
                _commandButton[i] = BlzGetOriginFrame(ORIGIN_FRAME_COMMAND_BUTTON, i);
                _systemButton[i] = BlzGetOriginFrame(ORIGIN_FRAME_SYSTEM_BUTTON, i);
                _minimapButton[i] = BlzGetOriginFrame(ORIGIN_FRAME_MINIMAP_BUTTON, i);
            }

            _console = BlzGetFrameByName("ConsoleUI", 0);
            _goldText = BlzGetFrameByName("ResourceBarGoldText", 0);
            _lumberText = BlzGetFrameByName("ResourceBarLumberText", 0);
            _foodText = BlzGetFrameByName("ResourceBarSupplyText", 0);
            _resourceBar = BlzGetFrameByName("ResourceBarFrame", 0);
            _unitNameText = BlzGetFrameByName("SimpleNameValue", 0);
            _upperButtonBar = BlzGetFrameByName("UpperButtonBarFrame", 0);
        }

        public static framehandle Game => _game;

        public static framehandle World => _world;

        public static framehandle HeroBar => _heroBar;

        public static framehandle Portrait => _portrait;

        public static framehandle Minimap => _minimap;

        public static framehandle Tooltip => _tooltip;

        public static framehandle UberTooltip => _uberTooltip;

        public static framehandle ChatMsg => _chatMsg;

        public static framehandle UnitMsg => _unitMsg;

        public static framehandle TopMsg => _topMsg;

        public static framehandle Console => _console;

        public static framehandle GoldText => _goldText;

        public static framehandle LumberText => _lumberText;

        public static framehandle FoodText => _foodText;

        public static framehandle UnitNameText => _unitNameText;

        public static framehandle ResourceBar => _resourceBar;

        public static framehandle UpperButtonBar => _upperButtonBar;

        public static IEnumerable<framehandle> GetCommandButtons()
        {
            foreach (var button in _commandButton)
            {
                yield return button;
            }
        }

        // todo: enum method for other 7 array-framehandles
    }
}