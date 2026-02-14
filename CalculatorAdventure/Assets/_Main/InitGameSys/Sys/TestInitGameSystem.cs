using System;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Sys;
using _Main.CalculatorSys.View;
using _Main.PlayerSys;
using _Main.PlayerSys.Data;
using Sirenix.OdinInspector;
using ToolKit;

namespace _Main.InitGameSys
{
    public class TestInitGameSystem : SingletonMonoBehaviour<TestInitGameSystem>
    {
        public PlayerData TestDefaultData;
        
        protected override void Awake()
        {
            InitEnterGame();
        }

        private void InitEnterGame()
        {
            PlayerSystem.Initialize(TestDefaultData);
        }

        [Button]
        public void LoadMainGameInitData()
        {
            CalculatorGameSetting calculatorData = PlayerSystem.GetPlayerData().CalculatorGameSetting;
            CalculatorButtonManager.InitializeButtons(calculatorData.ButtonsData);
            CalculatorSystem.InitializeSystem(calculatorData.CalculatorSystemData);
        }
        
        [Button]
        public void LoadMainGameInitView()
        {
            CalculatorButtonViewControl.InitializeView(CalculatorButtonManager.GetAllButtonData());
        }

        protected override void OnDestroy()
        {
            PlayerSystem.ClearInstance();
            CalculatorButtonManager.ClearInstance();
            CalculatorSystem.ClearInstance();
            base.OnDestroy();
        }
    }
}