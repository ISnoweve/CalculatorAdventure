using System;
using System.Runtime.CompilerServices;

namespace _Main.ResourceSys
{
    public static class ResourceConstPath
    {
        private const string GameSettingPath = "GameSetting/";
        private const string CalculatorBaseData = "CalculatorBaseData";
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetGameSettingDataPath(in string settingName) => 
            String.Concat(GameSettingPath, settingName);
        
        public static string GetCalculatorBaseDataPath() => GetGameSettingDataPath(CalculatorBaseData);
    }
}