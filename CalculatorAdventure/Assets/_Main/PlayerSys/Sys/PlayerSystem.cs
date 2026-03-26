using System;
using _Main.PlayerSys.Data;
using _Main.ToolKit.SingletonFeature;
using UnityEngine;

namespace _Main.PlayerSys.Sys
{
    [Serializable]
    public class PlayerSystem : Singleton<PlayerSystem>
    {
        [SerializeField] private PlayerData _playerData;

        public static PlayerData GetPlayerData()
        {
            return Instance._playerData;
        }

        public static void Initialize(PlayerData data)
        {
            Instance._playerData = data;
        }
    }
}