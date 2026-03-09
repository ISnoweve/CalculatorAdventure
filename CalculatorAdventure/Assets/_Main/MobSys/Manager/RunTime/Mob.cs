using System;
using _Main.CalculatorSys.Enum;
using _Main.MobSys.Data;
using _Main.MobSys.Data.AttackSkills;
using _Main.MobSys.Data.AttackSkills.Base;
using _Main.MobSys.Enum;
using UnityEngine;
using Random = System.Random;

namespace _Main.MobSys.Manager.RunTime
{
    [Serializable]
    public class Mob
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int originalQuestionNumber;
        [SerializeField] private AttackSkillData[] attackSkills;
        [SerializeField] private MobType mobType;
        [SerializeField] private int attackSkillCountDown;
        [SerializeField] private int currentQuestionNumber;
        [SerializeField] private AttackSkillData nextAttackSkill;
        public string Name => name;
        public string Description => description;
        public GameObject Prefab => prefab;
        public GameObject MobPrefab => prefab;
        public int CurrentQuestionNumber => currentQuestionNumber;
        public int AttackSkillCountDown => attackSkillCountDown;
        public MobType MobType => mobType;
        public AttackSkillData NextAttackSkill => nextAttackSkill;
        
        
        public Mob(MobData data)
        {
            id = data.Id;
            name = data.Name;
            description = data.Description;
            prefab = data.Prefab;
            originalQuestionNumber = data.OriginalQuestionNumber;
            currentQuestionNumber = originalQuestionNumber;
            attackSkills = data.AttackSkills;
            mobType = data.MobType;
            RandomNextAttackSkill();
        }
        
        public void ModifyQuestionNumber(int changeValue,CalculatorOperator calculatorOperator)
        {
            switch (calculatorOperator)
            {
                case CalculatorOperator.Add:
                    currentQuestionNumber += changeValue;
                    break;
                case CalculatorOperator.Subtract:
                    currentQuestionNumber -= changeValue;
                    break;
                case CalculatorOperator.Multiply:
                    currentQuestionNumber *= changeValue;
                    break;
                case CalculatorOperator.Divide:
                    currentQuestionNumber /= changeValue;
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
            attackSkillCountDown = nextAttackSkill.countDownRound;
        }
    }
}