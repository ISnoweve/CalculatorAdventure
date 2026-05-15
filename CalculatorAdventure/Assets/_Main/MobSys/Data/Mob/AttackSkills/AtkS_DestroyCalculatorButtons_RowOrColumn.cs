using System.Collections.Generic;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.HealthSys.Sys;
using _Main.MobSys.Data.Mob.AttackSkills.Base;
using _Main.MobSys.Data.Mob.AttackSkills.Enum;
using _Main.UtilityFeature;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main.MobSys.Data.Mob.AttackSkills
{
    [CreateAssetMenu(fileName = "DestroyButtons_RowOrColumn",
        menuName = "SoSetting/Mob/Skills/DestroyButtons_RowOrColumn", order = 2)]
    public class AtkS_DestroyCalculatorButtons_RowOrColumn : AttackSkillData
    {
        [Title("AtkSkill Info")] public RowOrColumn rowOrColumn;

        public bool isMultiLine;

        [ShowIf("isMultiLine", false)] [InfoBox("Suggest not more than 3")]
        public int destroyLineCount;

        public override void Execute()
        {
            var calculatorButtonsNotClick = CalculatorButtonManager.GetAllNumberButton();

            if (rowOrColumn == RowOrColumn.Row)
            {
                var rowList = GetRowIndexes();
                DestroyButtonsInList(calculatorButtonsNotClick, rowList);
            }
            else if (rowOrColumn == RowOrColumn.Column)
            {
                var columnList = GetColumnIndexes();
                DestroyButtonsInList(calculatorButtonsNotClick, columnList);
            }
        }

        private void DestroyButtonsInList(List<CalculatorButton> buttons, List<int> list)
        {
            var buttonsInList = new List<CalculatorButton>();

            foreach (var button in buttons)
                if (list.Contains(button.Index))
                    buttonsInList.Add(button);

            HealthSystem.Instance.ModifyPlayerHealthByMobAttack(buttonsInList);
            ButtonSystem.CloseNumberButtonClickableByAttackSkill(buttonsInList);
        }

        [Button]
        private List<int> GetRowIndexes()
        {
            var indexesList = new List<int> { 0, 6, 11, 16, 21 };
            var rowIndexList = new List<int>();
            if (isMultiLine)
            {
                var indexes = GetIndexes(indexesList);
                foreach (var variable in indexes)
                    for (var i = 0; i < 5; i++)
                        rowIndexList.Add(variable + i);
            }
            else
            {
                var randomIndex = GetRandomIndex(5);
                var startIndex = indexesList[randomIndex];
                for (var i = 0; i < 5; i++) rowIndexList.Add(startIndex + i);
            }

            return rowIndexList;
        }

        [Button]
        private List<int> GetColumnIndexes()
        {
            var indexesList = new List<int> { 0, 1, 2, 3, 4 };
            var columnIndexList = new List<int>();
            if (isMultiLine)
            {
                var indexes = GetIndexes(indexesList);
                foreach (var variable in indexes)
                    for (var i = 0; i < 5; i++)
                        columnIndexList.Add(variable + i * 5);
            }
            else
            {
                var randomIndex = GetRandomIndex(5);
                for (var i = 0; i < 5; i++) columnIndexList.Add(randomIndex + i * 5);
            }

            return columnIndexList;
        }

        private List<int> GetIndexes(List<int> indexList)
        {
            indexList.ShuffleList();
            var startIndex = GetRandomIndex(3);
            var resultList = indexList.GetRange(startIndex, destroyLineCount);
            return resultList;
        }

        private int GetRandomIndex(int index)
        {
            var randomIndex = Random.Range(0, index);
            return randomIndex;
        }
    }
}