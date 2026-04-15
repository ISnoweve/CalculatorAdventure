using _Main.CalculatorSys.Enum;
using _Main.MobSys.Data.Mob.AttackSkills.Base;
using _Main.MobSys.Data.Mob.AttackSkills.Event;
using _Main.MobSys.Manager;
using MessagePipe;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace _Main.MobSys.Data.Mob.AttackSkills
{
    [CreateAssetMenu(fileName = "Recover_Multiply", menuName = "SoSetting/Mob/Skills/Recover_Multiply", order = 4)]
    public class AtkS_Recover_Multiply : AttackSkillData
    {
        [Title("AtkSkill Info")] public int randomLimitMin;

        public int randomLimitMax;

        public override void Execute()
        {
            var random = new Random();
            var randomValue = random.Next(randomLimitMin, randomLimitMax);

            MobManager.CurrentsMob.ModifyQuestionNumber(randomValue, CalculatorOperator.Multiply);
            var currentQuestionNumber = MobManager.CurrentsMob.CurrentQuestionNumber;

            var eventData = new Event_AtkS_Recover_Multiply(currentQuestionNumber, randomValue);
            GlobalMessagePipe.GetPublisher<Event_AtkS_Recover_Multiply>().Publish(eventData);
        }
    }
}