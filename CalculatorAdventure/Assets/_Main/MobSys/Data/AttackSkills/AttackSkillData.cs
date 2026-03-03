using _Main.MobSys.Data.AttackSkills.Base;
using UnityEngine;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "MobAttackSkillData", menuName = "SoSetting/Mob/MobData", order = 0)]
    public class AttackSkillData : ScriptableObject
    {
        public AttackSkillBase attackSkillBase;
        public void Execute()
        {
            attackSkillBase.Execute();
        }
    }
}