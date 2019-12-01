namespace War3Lib.Mmd
{
    // https://wiki.wc3stats.com/Help:Special_Keys
    public static class SpecialKey
    {
        /// <summary>
        /// Used to determine which team a player should be grouped into.
        /// This is useful because replay files do not save team changes that happen during game, only the default ones defined in the map (lobby teams).
        /// The exact value of this key does not matter as long as everyone on the same team has the same value.
        /// If this key is not set, the default lobby teams will be used to group players.
        /// </summary>
        public const string Team = "team";

        /// <summary>
        /// Used to sort teams from 1st place to last place.
        /// If two teams have the same placement they are considered to be tied.
        /// Placement has a higher sorting precedence than score but a lower sorting precedence than a win flag (see FlagPlayer).
        /// </summary>
        public const string Placement = "placement";

        /// <summary>
        /// Used as an alternative to sort teams from 1st place to last place.
        /// The score used when sorting is the average score of all players on the team.
        /// A win flag and placement value take precedence over score.
        /// </summary>
        public const string Score = "score";

        /// <summary>
        /// The mode can be used to change which player profile a game should use for processing.
        /// This is useful when you want to separate ratings based on hero, race or faction.
        /// For example, in M.Z.I. there are two teams: "Undead" and "Human".
        /// A shared rating would be less appropriate than a separate undead and human rating.
        /// This is accomplished by setting the mode of all players on the undead team to "Undead" and the mode of all players on the human team to "Human".
        /// </summary>
        public const string Mode = "mode";

        /// <summary>
        /// Used to override the default lobby colour.
        /// This is useful when the in-game colour changes or is different from the lobby colour.
        /// </summary>
        public const string Colour = "colour";

        /// <summary>
        /// Rating delta coefficient. For example if the ELO system determines a player should get +10 points and they have a bonus set at 1.2,
        /// their actual rating change will be +12 for the game. These are player specific values, it does not propagate to the rest of the team.
        /// Be careful with this, replays with bugged values sent cannot be edited so they will have to be voided.
        /// </summary>
        public const string Bonus = "bonus";

        /// <summary>
        /// Can be used to manually void a player. Voided players will not be counted in statistics or rating calculations.
        /// Set to 1 or true to enable.
        /// </summary>
        public const string IsVoid = "isVoid";
    }
}