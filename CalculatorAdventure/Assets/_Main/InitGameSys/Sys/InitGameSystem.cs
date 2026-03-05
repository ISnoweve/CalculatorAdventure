using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Sys.Button;
using _Main.CalculatorSys.Sys.Calculator;
using _Main.MobSys.Sys;
using _Main.PlayerSys.Data;
using _Main.PlayerSys.Sys;
using _Main.SnoweveToolKit.ToolKit;
using _Main.StateSys.GameStateMachine.Sys;

namespace _Main.InitGameSys.Sys
{
    public class InitGameSystem : SingletonMonoBehaviour<InitGameSystem>
    {
        protected override bool IsDontDestroyOnLoad => true;

        public PlayerSystem playerSystem;
        public GameStateMachine gameStateMachine;
        public CalculatorButtonManager calculatorButtonManager;
        public CalculatorSystem calculatorSystem;
        public ButtonSystem buttonSystem;
        public MobSystem mobSystem;
        public SelectMobDataSystem selectMobDataSystem;
        
        public PlayerData DefaultData;
        
        /*
         * <未來設計>
         * 未來應該會透過 addressable 的方式去讀取資料，或者用 resources 的方式。
         *
         *  預設玩家第一次遊玩，不會有任何的東西，讀取的資料會是遊戲的各類初始計算機設定，讓玩家開始遊戲選擇。
         *
         *  存檔的時候透過 PlayerSystem 進行存檔。
         */
        
        #region Life cycle

        protected override void Awake()
        {
            base.Awake();
            InitialSystem();
            LoadPlayerSystem();
        }

        private void InitialSystem()
        {
            playerSystem = PlayerSystem.Instance;
            gameStateMachine = GameStateMachine.Instance;
            calculatorButtonManager = CalculatorButtonManager.Instance;
            calculatorSystem = CalculatorSystem.Instance;
            buttonSystem = ButtonSystem.Instance;
            mobSystem = MobSystem.Instance;
            selectMobDataSystem = SelectMobDataSystem.Instance;
        }
        
        private void LoadPlayerSystem()
        {
            PlayerSystem.Initialize(DefaultData);
            //if(TryGetPlayerSaveData());
        }

        private bool TryGetPlayerSaveData()
        {
            return false;
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
        
        // private void LoadMainGameInitData()
        // {
        //     var calculatorData = PlayerSystem.GetPlayerData().CalculatorGameSetting;
        //     CalculatorButtonManager.InitializeButtons(calculatorData.ButtonsData);
        //     CalculatorSystem.InitializeSystem(calculatorData.CalculatorSystemData);
        // }
    }
}