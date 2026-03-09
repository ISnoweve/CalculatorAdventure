using _Main.CalculatorSys.Enum;
using _Main.MobSys.Data.AttackSkills.Base;
using _Main.MobSys.Data.AttackSkills.Event;
using _Main.MobSys.Manager;
using MessagePipe;
using UnityEngine;
using Random = System.Random;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "Recover_Multiply", menuName = "SoSetting/Mob/Skills/Recover_Multiply", order = 4)]
    public class AtkS_Recover_Multiply : AttackSkillData
    {
        public int randomLimitMin;
        public int randomLimitMax;
        public override void Execute()
        {
            Random random = new Random();
            int randomValue = random.Next(randomLimitMin, randomLimitMax);
            
            
            MobManager.CurrentsMob.ModifyQuestionNumber(randomValue,CalculatorOperator.Multiply);
            int currentQuestionNumber = MobManager.CurrentsMob.CurrentQuestionNumber;
            
            Event_AtkS_Recover_Multiply eventData = new Event_AtkS_Recover_Multiply(currentQuestionNumber,randomValue);
            GlobalMessagePipe.GetPublisher<Event_AtkS_Recover_Multiply>().Publish(eventData);
        }
    }
}