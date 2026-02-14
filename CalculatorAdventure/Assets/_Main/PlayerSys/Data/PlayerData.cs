using System;
using _Main.CalculatorSys.Data;
using UnityEngine;

namespace _Main.PlayerSys.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "SoSetting/Player/PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private CalculatorGameSetting calculatorGameSetting;
        public CalculatorGameSetting CalculatorGameSetting => calculatorGameSetting;
    }
}