using _Main.MobSys.Data.AttackSkills.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills.Base
{
    public abstract class AttackSkillData : ScriptableObject
    {
        [Title("Basic Info")] 
        [SerializeField] private string name;
        [TextArea][SerializeField] private string description;
        
        [Title("AttackSkill Info")] 
        public AttackSkillType attackSkillType;
        public int countDownRound;
        public abstract void Execute();
    }
}