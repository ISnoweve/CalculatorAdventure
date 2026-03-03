using System;
using _Main.CalculatorSys.Data.Enum;
using _Main.MobSys.Data;
using _Main.MobSys.Data.AttackSkills;
using UnityEngine;
using Random = System.Random;

namespace _Main.MobSys.Manager.RunTime
{
    [Serializable]
    public class Mob
    {
        [SerializeField] private byte id;
        [SerializeField] private int originalQuestionNumber;
        [SerializeField] private int currentQuestionNumber;
        [SerializeField] private AttackSkillData[] attackSkills;
        [SerializeField] private AttackSkillData nextAttackSkill;
        [SerializeField] private int attackSkillCountDown;
        public int CurrentQuestionNumber => currentQuestionNumber;
        public int AttackSkillCountDown => attackSkillCountDown;
        
        public Mob(MobData data)
        {
            
        }
        
        public void ModifyQuestionNumber(int changeValue,CalculatorOperator calculatorOperator)
        {
            switch (calculatorOperator)
            {
                case CalculatorOperator.Add:
                    currentQuestionNumber += changeValue;
                    break;
                case CalculatorOperator.Subtract:
                    currentQuestionNumber += changeValue;
                    break;
                case CalculatorOperator.Multiply:
                    currentQuestionNumber *= changeValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(calculatorOperator), calculatorOperator, null);
            }
        }
        
        public void DecreaseAttackSkillCountDown()
        {
            attackSkillCountDown--;
        }
        
        public void ExecuteNextAttackSkill()
        {
            if (nextAttackSkill == null) return;
            nextAttackSkill.Execute();
        }
        
        public void RandomNextAttackSkill()
        {
            if (attackSkills == null || attackSkills.Length <= 0) return;
            Random random = new Random();
            int randomIndex = random.Next(0, attackSkills.Length);
            nextAttackSkill = attackSkills[randomIndex];
            attackSkillCountDown = nextAttackSkill.attackSkillBase.countDownRound;
        }
    }
}