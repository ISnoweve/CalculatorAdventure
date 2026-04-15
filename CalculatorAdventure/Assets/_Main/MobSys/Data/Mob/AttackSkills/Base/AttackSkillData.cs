using _Main.MobSys.Data.Mob.AttackSkills.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.MobSys.Data.Mob.AttackSkills.Base
{
    public abstract class AttackSkillData : ScriptableObject
    {
        [Title("Basic Info")] [TextArea] [SerializeField]
        private string description;

        [Title("Type Info")] public AttackSkillType attackSkillType;

        public int countDownRound;

        public string Description => description;
        public abstract void Execute();
    }
}