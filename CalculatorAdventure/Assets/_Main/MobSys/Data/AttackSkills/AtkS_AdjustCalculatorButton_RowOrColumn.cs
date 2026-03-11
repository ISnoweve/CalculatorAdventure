using System.Collections.Generic;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.CalculatorSys.Sys.Button;
using _Main.MobSys.Data.AttackSkills.Base;
using _Main.MobSys.Data.AttackSkills.Enum;
using _Main.SnoweveToolKit.UtilityFeature;
using Sirenix.OdinInspector;
using UnityEngine;


namespace _Main.MobSys.Data.AttackSkills
{
    [CreateAssetMenu(fileName = "AdjustCalculatorButton_RowOrColumn",
        menuName = "SoSetting/Mob/Skills/AdjustCalculatorButton_RowOrColumn", order = 1)]
    public class AtkS_AdjustCalculatorButton_RowOrColumn : AttackSkillData
    {
        public RowOrColumn rowOrColumn;
        public bool isMultiLine;
        [ShowIf("isMultiLine", false)] [InfoBox("Suggest not more than 3")]
        public int rowOrColumnCount;
        public int randomValueLimitMin;
        public int randomValueLimitMax;

        public override void Execute()
        {
            var calculatorButtonsNotClick = CalculatorButtonManager.GetAllActivateNumberButton();
            
            if(rowOrColumn == RowOrColumn.Row)
            {
                List<int> rowList = GetRowIndexes();
                ModifyButtonsInList(calculatorButtonsNotClick, rowList);
            }
            else if(rowOrColumn == RowOrColumn.Column)
            {
                List<int> columnList = GetColumnIndexes();
                ModifyButtonsInList(calculatorButtonsNotClick, columnList);
            }
        }
        
        private void ModifyButtonsInList(List<CalculatorButton> buttons, List<int> list)
        {
            List<CalculatorButton> buttonsInList = new List<CalculatorButton>();
            
            foreach (var button in buttons)
            {
                if (list.Contains(button.Index))
                {
                    buttonsInList.Add(button);
                }
            }
            
            var randomValue = Random.Range(randomValueLimitMin, randomValueLimitMax);

            ButtonSystem.ModifyButtonValueByAttackSkill(buttonsInList, randomValue);
        }

        [Button]
        private List<int> GetRowIndexes()
        {
            List<int> indexesList = new List<int> { 0,5,10,15,20 };
            List<int> rowIndexList = new List<int>();
            if (isMultiLine)
            {
                var indexes = GetIndexes(indexesList);
                foreach (var variable in indexes)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        rowIndexList.Add(variable+i);
                    }
                }
            }
            else
            {
                int randomIndex = GetRandomIndex(5);
                int startIndex = indexesList[randomIndex];
                for (int i = 0; i < 5; i++)
                {
                    rowIndexList.Add(startIndex+i);
                }
            }
            return rowIndexList;
        }
        
        [Button]
        private List<int> GetColumnIndexes()
        {
            List<int> indexesList = new List<int> { 0, 1, 2, 3, 4 };
            List<int> columnIndexList = new List<int>();
            if (isMultiLine)
            {
                var indexes = GetIndexes(indexesList);
                foreach (var variable in indexes)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        columnIndexList.Add(variable+i*5);
                    }
                }
            }
            else
            {
                int randomIndex = GetRandomIndex(5);
                for (int i = 0; i < 5; i++)
                {
                    columnIndexList.Add(randomIndex+i*5);
                }
            }
            return columnIndexList;
        }
        
        private List<int> GetIndexes(List<int> indexList)
        {
            indexList.ShuffleList();
            int startIndex = GetRandomIndex(3);
            List<int> resultList = indexList.GetRange(startIndex, rowOrColumnCount);
            return resultList;
        }
        
        private int GetRandomIndex(int index)
        {
            int randomIndex = Random.Range(0, index);
            return randomIndex;
        }
    }
}