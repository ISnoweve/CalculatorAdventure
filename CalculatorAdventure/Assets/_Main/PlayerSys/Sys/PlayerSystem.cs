using System;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Sys;
using _Main.PlayerSys.Data;
using BolingsUnityTools;
using UnityEngine;

namespace _Main.PlayerSys
{
    [Serializable]
    public class PlayerSystem : Singleton<PlayerSystem>
    {
        [SerializeField] private PlayerData _playerData;
        public static PlayerData GetPlayerData() => Instance._playerData;
        public static void Initialize(PlayerData data)
        {
            Instance._playerData = data;
        }
    }
}