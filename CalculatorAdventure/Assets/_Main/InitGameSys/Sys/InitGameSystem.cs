using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Sys.Button;
using _Main.CalculatorSys.Sys.Calculator;
using _Main.ChallengeSys.Data;
using _Main.ChallengeSys.Sys;
using _Main.GameSceneSys.Sys;
using _Main.MobBattleSys.MobBattleState.Sys;
using _Main.MobBattleSys.MobReward.Data.MobReward;
using _Main.MobBattleSys.MobReward.Sys.MobRewardSys;
using _Main.MobSys.Manager;
using _Main.MobSys.Sys.MobSys;
using _Main.MobSys.Sys.SelectSys;
using _Main.MoneySys.Sys;
using _Main.PlayerSys.Data;
using _Main.PlayerSys.Sys;
using _Main.StateSys.GameStateMachineSys.Sys;
using _Main.ToolKit.SingletonFeature;
using _Main.UniqueItemSys.Data;
using _Main.UniqueItemSys.Manager;
using _Main.UniqueItemSys.Sys;
using UnityEngine;

namespace _Main.InitGameSys.Sys
{
    public class InitGameSystem : SingletonMonoBehaviour<InitGameSystem>
    {
        public PlayerSystem playerSystem;
        public GameSceneSystem gameSceneSystem;
        public GameStateMachine gameStateMachine;
        public CalculatorButtonManager calculatorButtonManager;
        public CalculatorSystem calculatorSystem;
        public ButtonSystem buttonSystem;
        public MobManager mobManager;
        public MobSystem mobSystem;
        public SelectMobDataSystem selectMobDataSystem;
        public MobBattleState mobBattleState;
        public ChallengeSystem challengeSystem;
        public UniqueItemManager uniqueItemManager;
        public UniqueItemSystem uniqueItemSystem;
        public MobBattleRewardSystem mobBattleRewardSystem;
        public MoneySystem moneySystem;

        public ChallengeDataSoList challengeDataSoList;
        public UniqueItemDataSoList uniqueItemDataSoList;
        public MobRewardDataSoList mobRewardDataSoList;
        public PlayerData DefaultData;
        [SerializeField] private CalculatorGameSettingSoList calculatorGameSettingSoList;
        protected override bool IsDontDestroyOnLoad => true;

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

            if (Instance != this) return;

            InitialSystem();
            LoadSystem();
        }

        private void InitialSystem()
        {
            playerSystem = PlayerSystem.Instance;
            gameSceneSystem = GameSceneSystem.Instance;
            gameStateMachine = GameStateMachine.Instance;
            calculatorButtonManager = CalculatorButtonManager.Instance;
            calculatorSystem = CalculatorSystem.Instance;
            buttonSystem = ButtonSystem.Instance;
            mobManager = MobManager.Instance;
            mobSystem = MobSystem.Instance;
            selectMobDataSystem = SelectMobDataSystem.Instance;
            mobBattleState = MobBattleState.Instance;
            challengeSystem = ChallengeSystem.Instance;
            uniqueItemManager = UniqueItemManager.Instance;
            uniqueItemSystem = UniqueItemSystem.Instance;
            mobBattleRewardSystem = MobBattleRewardSystem.Instance;
            moneySystem = MoneySystem.Instance;
        }

        private void LoadSystem()
        {
            challengeSystem.SetChallengeSoList(challengeDataSoList);
            uniqueItemSystem.SetUniqueItemDataSoList(uniqueItemDataSoList);
            mobBattleRewardSystem.SetMobRewardDataSoList(mobRewardDataSoList);
            UniqueItemSystem.SpawnAllUniqueItemInSoList();
            var calculatorGameSetting = calculatorGameSettingSoList.CalculatorGameSettings[0];
            CalculatorButtonManager.InitializeButtons(calculatorGameSetting.ButtonsData);
            CalculatorSystem.InitializeSystem(calculatorGameSetting.CalculatorSystemData);
        }

        private bool TryGetPlayerSaveData()
        {
            return false;
        }

        protected override void OnDestroy()
        {
            if (Instance == this) ClearSystemsInstance();
            base.OnDestroy();
        }

        private void ClearSystemsInstance()
        {
            PlayerSystem.ClearInstance();
            GameSceneSystem.ClearInstance();
            GameStateMachine.ClearInstance();
            CalculatorButtonManager.ClearInstance();
            CalculatorSystem.ClearInstance();
            ButtonSystem.ClearInstance();
            MobManager.ClearInstance();
            MobSystem.ClearInstance();
            SelectMobDataSystem.ClearInstance();
            MobBattleState.ClearInstance();
            ChallengeSystem.ClearInstance();
            UniqueItemManager.ClearInstance();
            UniqueItemSystem.ClearInstance();
            MobBattleRewardSystem.ClearInstance();
            MoneySystem.ClearInstance();
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
        //     PlayerSystem.Initialize(DefaultData);
        //     var calculatorData = PlayerSystem.GetPlayerData().CalculatorGameSetting;
        //     CalculatorButtonManager.InitializeButtons(calculatorData.ButtonsData);
        //     CalculatorSystem.InitializeSystem(calculatorData.CalculatorSystemData);
        // }
    }
}