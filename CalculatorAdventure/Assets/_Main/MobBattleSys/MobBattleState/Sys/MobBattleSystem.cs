using System;
using _Main.ToolKit.SingletonFeature;
using UnityEngine;

namespace _Main.MobBattleSys.MobBattleState.State
{
    [Serializable]
    public class MobBattleSystem : Singleton<MobBattleSystem>
    {
        [SerializeField] private int currentRound;
    }
}