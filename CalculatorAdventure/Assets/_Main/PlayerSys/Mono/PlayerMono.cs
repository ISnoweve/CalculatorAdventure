using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Sys;
using _Main.CalculatorSys.Sys.Calculator;
using _Main.PlayerSys.Sys;
using ToolKit;

namespace _Main.PlayerSys.Mono
{
    // 主要用來檢測所有 singleton 內容物
    public class PlayerMono : SingletonMonoBehaviour<PlayerMono>
    {
        public PlayerSystem playerSystem;
        public CalculatorButtonManager calculatorButtonManager;
        public CalculatorSystem calculatorSystem;
        
        protected override void Awake()
        {
            base.Awake();
            GetInstance();
        }

        private void GetInstance()
        {
            playerSystem = PlayerSystem.Instance;
            calculatorButtonManager = CalculatorButtonManager.Instance;
            calculatorSystem = CalculatorSystem.Instance;
        }
    }
}