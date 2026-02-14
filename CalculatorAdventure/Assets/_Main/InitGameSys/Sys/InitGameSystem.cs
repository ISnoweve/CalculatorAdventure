using System;
using System.Collections;
using _Main.PlayerSys;
using ToolKit;
using UnityEngine;

namespace _Main.InitGameSys
{
    [Serializable, DefaultExecutionOrder(-99900)]
    public class InitGameSystem : SingletonMonoBehaviour<InitGameSystem>
    {
        private void OnEnable()
        {
            InitEnterGame();
        }

        public static void InitEnterGame()
        {
            /*
             * <未來設計>
             * 未來應該會透過 addressable 的方式去讀取資料，或者用 resources 的方式。
             * 
             *  預設玩家第一次遊玩，不會有任何的東西，讀取的資料會是遊戲的各類初始計算機設定，讓玩家開始遊戲選擇。
             *
             *  存檔的時候透過 PlayerSystem 進行存檔。
             */
        }

        private static void InitFirstGameSetting()
        {
        }

        private static void OnLoadedInitDataSuccess()
        {

        }

        private static void OnLoadedInitDataFail()
        {

        }
    }
}