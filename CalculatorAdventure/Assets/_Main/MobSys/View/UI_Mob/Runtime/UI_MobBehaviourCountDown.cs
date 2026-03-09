using _Main.MobSys.Data.AttackSkills.Base;
using _Main.MobSys.Data.AttackSkills.Enum;
using TMPro;
using UnityEngine;

namespace _Main.MobSys.View.UI_Mob.Runtime
{
    public class UI_MobBehaviourCountDown : MonoBehaviour
    {
        [SerializeField] private AttackSkillType currentAttackSkillType; 
        [SerializeField] private int currentAttackSkillCountDown;
        [SerializeField] private TMP_Text countDownText;

        #region Life Cycle

        public void Initialize(AttackSkillData data)
        {
            UpdateAttackSkillTypeSprite(data.attackSkillType);
            UpdateAttackSkillCountDown(data.countDownRound);
        }

        #endregion

        #region Behaviour
        
                // 之後背面會有貼圖
        private void UpdateAttackSkillTypeSprite(AttackSkillType type)
        {
            currentAttackSkillType = type;
        }
        
        private void UpdateAttackSkillCountDown(int countDown)
        {
            //if (currentAttackSkillCountDown <= 0) return;
            currentAttackSkillCountDown = countDown;
            UpdateCountDownText();
        }
        
        private void UpdateCountDownText()
        {
            countDownText.text = currentAttackSkillCountDown.ToString();
        }

        public void UpdateNewCountDown(int countDown)
        {
            UpdateAttackSkillCountDown(countDown);
        }

        #endregion
    }
}