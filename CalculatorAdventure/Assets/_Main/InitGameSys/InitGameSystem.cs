using System;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Sys;
using _Main.ResourceSys;
using UnityEngine;

namespace _Main.InitGameSys
{
    [Serializable, DefaultExecutionOrder(-99900)]
    public static class InitGameSystem
    {
        private static bool _isInitGameSetting = false;
        public static bool IsInitGameSetting => _isInitGameSetting;
        private static bool _isLoadedSaveData = false;
        public static bool IsLoadedSaveData => _isLoadedSaveData;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void InitEnterGame()
        {
            _isInitGameSetting = false;
            InitFirstGameSetting();
            _isInitGameSetting = true;
        }

        private static void InitFirstGameSetting()
        {
            CalculatorButtonManager.Instance.InitializeButtons();
        }
        
        private static void OnLoadedInitDataSuccess()
        {
            
        }
        
        private static void OnLoadedInitDataFail()
        {
            
        }
    }
}