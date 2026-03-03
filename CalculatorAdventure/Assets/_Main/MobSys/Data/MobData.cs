using _Main.MobSys.Data.AttackSkills;
using UnityEngine;

namespace _Main.MobSys.Data
{
    [CreateAssetMenu(fileName = "MobData", menuName = "SoSetting/Mob/Mob", order = 0)]
    public class MobData : ScriptableObject
    {
        private byte id;
        private int OriginalQuestionNumber;
        private int currentQuestionNumber;
        private AttackSkillData[] attackSkills;
        private AttackSkillData nextAttackSkill;
        private int attackSkillCountDown;
    }
}