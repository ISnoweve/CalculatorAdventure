using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Sys.Button;
using _Main.CalculatorSys.Sys.Calculator;
using _Main.PlayerSys.Data;
using _Main.PlayerSys.Sys;
using ToolKit;

namespace _Main.InitGameSys.Sys
{
    public class TestInitGameSystem : SingletonMonoBehaviour<TestInitGameSystem>
    {
        public PlayerData TestDefaultData;

        private void Start()
        {
            InitEnterGame();
            LoadMainGameInitData();
        }

        protected override void OnDestroy()
        {
            PlayerSystem.ClearInstance();
            CalculatorButtonManager.ClearInstance();
            CalculatorSystem.ClearInstance();
            ButtonSystem.ClearInstance();
            base.OnDestroy();
        }

        private void InitEnterGame()
        {
            PlayerSystem.Initialize(TestDefaultData);
        }

        private void LoadMainGameInitData()
        {
            var calculatorData = PlayerSystem.GetPlayerData().CalculatorGameSetting;
            CalculatorButtonManager.InitializeButtons(calculatorData.ButtonsData);
            CalculatorSystem.InitializeSystem(calculatorData.CalculatorSystemData);
        }
    }
}