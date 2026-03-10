using _Main.MobSys.Data.AttackSkills.Base;
using _Main.MobSys.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.MobSys.Data
{
    [CreateAssetMenu(fileName = "MobData", menuName = "SoSetting/Mob/Mob", order = 2)]
    public class MobData : ScriptableObject
    {
        #region ID

        [Title("ID")] [SerializeField] private int id;

        public int Id => id;

        #endregion

        #region Basic Info

        [Title("Basic Info")] [SerializeField] private string name;

        [TextArea] [SerializeField] private string description;
        public string Name => name;
        public string Description => description;

        #endregion

        #region Instance View

        [Title("Instance Info")] [SerializeField]
        private GameObject prefab;

        public GameObject Prefab => prefab;

        #endregion

        #region MobBattleSetting

        [Title("Mob Battle Setting")] [SerializeField]
        private int originalQuestionNumber;

        [SerializeField] private AttackSkillData[] attackSkills;
        [SerializeField] private MobType mobType;
        public int OriginalQuestionNumber => originalQuestionNumber;
        public AttackSkillData[] AttackSkills => attackSkills;
        public MobType MobType => mobType;

        #endregion
    }
}