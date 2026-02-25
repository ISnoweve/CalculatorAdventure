using System;
using _Main.StateSys.GameStateMachine.Root.Enum;

namespace _Main.StateSys.GameStateMachine.Root
{
    [Serializable]
    public class AbstractStateRoot
    {
        public RootLoadType rootLoadType;
        
        public virtual void EnterState(){}

        public virtual void ExitState(){}
    }
}