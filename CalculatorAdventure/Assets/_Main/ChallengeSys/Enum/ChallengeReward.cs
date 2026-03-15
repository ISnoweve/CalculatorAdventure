using System;

namespace _Main.ChallengeSys.Enum
{
    [Flags]
    public enum ChallengeReward : byte
    {
        Multiply = 1 << 0,
        Divide = 1 << 1
    }
}