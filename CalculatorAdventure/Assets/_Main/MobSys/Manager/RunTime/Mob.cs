using System;
using System.Collections.Generic;
using System.Linq;
using _Main.CalculatorSys.Enum;
using _Main.MobSys.Data;
using _Main.MobSys.Data.Mob;
using _Main.MobSys.Data.Mob.AttackSkills.Base;
using _Main.MobSys.Enum;
using _Main.UtilityFeature;
using Sirenix.OdinInspector;
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
        [SerializeField] private List<AttackSkillData> attackSkills;
        [SerializeField,ReadOnly] private List<AttackSkillData> attackSkillsInGame;
        [SerializeField] private MobType mobType;
        [SerializeField] private int attackSkillCountDown;
        [SerializeField] private int currentQuestionNumber;
        [SerializeField] private AttackSkillData nextAttackSkill;
        [SerializeField] private AttackSkillData previousAttackSkill;


        public Mob(MobData data)
        {
            id = data.Id;
            name = data.Name;
            description = data.Description;
            prefab = data.Prefab;
            originalQuestionNumber = data.OriginalQuestionNumber;
            currentQuestionNumber = originalQuestionNumber;
            attackSkills = data.AttackSkills.ToList();
            attackSkillsInGame = attackSkills;
            mobType = data.MobType;
            RandomNextAttackSkill();
        }

        public string Name => name;
        public string Description => description;
        public GameObject Prefab => prefab;
        public GameObject MobPrefab => prefab;
        public int CurrentQuestionNumber => currentQuestionNumber;
        public int AttackSkillCountDown => attackSkillCountDown;
        public MobType MobType => mobType;
        public AttackSkillData NextAttackSkill => nextAttackSkill;

        public void ModifyQuestionNumber(int changeValue, CalculatorOperator calculatorOperator)
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
                    if (currentQuestionNumber / changeValue != 0) currentQuestionNumber /= changeValue;
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
            if (attackSkills == null || attackSkills.Count <= 0) return;
            attackSkillsInGame.ShuffleList();
            foreach (AttackSkillData attackSkillData in attackSkillsInGame)
            {
                if (previousAttackSkill == null)
                {
                    previousAttackSkill = attackSkillData;
                    nextAttackSkill = attackSkillData; 
                    break;
                }
                
                if(previousAttackSkill.GetEntityId()==attackSkillData.GetEntityId())continue;
                previousAttackSkill = attackSkillData;
                nextAttackSkill = attackSkillData;
                break;
            }
            attackSkillCountDown = nextAttackSkill.countDownRound;
        }
    }
}