using System;
using System.Collections.Generic;
using System.Linq;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.CalculatorSys.Sys.Calculator;
using _Main.PlayerSys.Data;
using _Main.PlayerSys.Sys;
using _Main.SnoweveToolKit.ToolKit;
using _Main.StateSys.GameStateMachine.Sys;
using Sirenix.OdinInspector;

namespace _Main.InitGameSys.Sys
{
    public class TestInitGameSystem : SingletonMonoBehaviour<TestInitGameSystem>
    {
        public PlayerSystem playerSystem;
        public CalculatorButtonManager calculatorButtonManager;
        public CalculatorSystem calculatorSystem;
        public ButtonSystem buttonSystem;
        public GameStateMachine gameStateMachine;
        
        public PlayerData DefaultData;
        
        #region Life cycle

        private void Start()
        {
            InitialSystem();
            InitialPlayerSystem();
            LoadMainGameInitData();
        }

        private void InitialSystem()
        {
            playerSystem = PlayerSystem.Instance;
            calculatorButtonManager = CalculatorButtonManager.Instance;
            calculatorSystem = CalculatorSystem.Instance;
            buttonSystem = ButtonSystem.Instance;
            gameStateMachine = GameStateMachine.Instance;
        }
        
        private void InitialPlayerSystem()
        {
            PlayerSystem.Initialize(DefaultData);
        }

        private void LoadMainGameInitData()
        {
            var calculatorData = PlayerSystem.GetPlayerData().CalculatorGameSetting;
            CalculatorButtonManager.InitializeButtons(calculatorData.ButtonsData);
            CalculatorSystem.InitializeSystem(calculatorData.CalculatorSystemData);
        }

        protected override void OnDestroy()
        {
            PlayerSystem.ClearInstance();
            CalculatorButtonManager.ClearInstance();
            CalculatorSystem.ClearInstance();
            ButtonSystem.ClearInstance();
            base.OnDestroy();
        }

        #endregion

        #region Behaviour

        private void NewGame()
        {
            
        }

        private void LoadGame()
        {
            
        }

        #endregion
    }
}