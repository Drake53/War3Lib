namespace War3Lib.Mmd
{
    internal static class EnumNameProvider
    {
        public static string GetGoalString(Goal goal)
        {
            return goal switch
            {
                Goal.None => "none",
                Goal.High => "high",
                Goal.Low => "low",
            };
        }

        public static string GetTypeString(Type type)
        {
            return type switch
            {
                Type.String => "string",
                Type.Float => "real",
                Type.Int => "int",
            };
        }

        public static string GetSuggestionString(Suggest suggestion)
        {
            return suggestion switch
            {
                Suggest.None => "none",
                Suggest.Track => "track",
                Suggest.Leaderboard => "leaderboard",
            };
        }

        public static string GetOperatorString(Operator @operator)
        {
            return @operator switch
            {
                Operator.Add => "+=",
                Operator.Subtract => "-=",
                Operator.Set => "=",
            };
        }

        public static string GetFlagString(Flag flag)
        {
            return flag switch
            {
                Flag.Drawer => "drawer",
                Flag.Loser => "loser",
                Flag.Winner => "winner",
                Flag.Leaver => "leaver",
                Flag.Practicing => "practicing",
            };
        }
    }
}