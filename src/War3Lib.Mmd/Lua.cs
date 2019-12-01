namespace War3Lib.Mmd
{
    internal class LuaMethods
    {
        /// @CSharpLua.Template = "print({*0})"
        public static extern void Print(params object[] args);
    }

    internal class Blizzard
    {
        /// @CSharpLua.Template = "bj_MAX_PLAYER_SLOTS"
        public static readonly int MaxPlayerSlots;
    }
}