using _Main.CalculatorSys.Enum;
using _Main.MobSys.Data.AttackSkills.Base;
using _Main.MobSys.Data.AttackSkills.Event;
using _Main.MobSys.Manager;
using MessagePipe;
using UnityEngine;
using Random = System.Random;

namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "Recover_AddOrSubtract", menuName = "SoSetting/Mob/Skills/Recover_AddOrSubtract",
        order = 4)]
    public class AtkS_Recover_AddOrSubtract : AttackSkillData
    {
        public int randomLimitMin;
        public int randomLimitMax;

        public override void Execute()
        {
            var random = new Random();
            var randomValue = random.Next(randomLimitMin, randomLimitMax);

            if (MobManager.CurrentsMob.CurrentQuestionNumber > 0)
                MobManager.CurrentsMob.ModifyQuestionNumber(randomValue, CalculatorOperator.Add);
            else
                MobManager.CurrentsMob.ModifyQuestionNumber(-randomValue, CalculatorOperator.Subtract);

            var currentQuestionNumber = MobManager.CurrentsMob.CurrentQuestionNumber;

            var eventData = new Event_AtkS_Recover_AddOrSubtract(currentQuestionNumber, randomValue);
            GlobalMessagePipe.GetPublisher<Event_AtkS_Recover_AddOrSubtract>().Publish(eventData);
        }
    }
}