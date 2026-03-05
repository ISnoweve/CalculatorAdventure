using System;
using System.Collections.Generic;
using System.Linq;
using _Main.InitGameSys.Sys.Enum;
using TMPro;
using UnityEngine;

namespace _Main.InitGameSys.View.UI_DropDown
{
    public class DropDownView : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private List<String> dropdownOptions;
        
        [SerializeField] private GameObject labelMainGame;
        [SerializeField] private GameObject labelTestMobBattle;
        [SerializeField] private GameObject labelTestQuestionSpot;
        [SerializeField] private GameObject labelTestMap;
        [SerializeField] private GameObject labelTestStoreSpot;

        #region Life Cycle

        private void Awake()
        {
            CloseAllLabel();
            InitialDropdownOption();
            Subscribe();
            OnDropdownChanged(0);
        }

        private void InitialDropdownOption()
        {
            dropdownOptions = new List<string>();

            List<string> names = Enum.GetNames(typeof(InitGameType)).ToList();

            dropdownOptions.Clear();
            dropdownOptions.AddRange(names);

            dropdown.ClearOptions();
            dropdown.AddOptions(dropdownOptions);
        }

        private void Subscribe()
        {
            dropdown.onValueChanged.AddListener(OnDropdownChanged);
        }

        private void OnDestroy()
        {
            dropdown.onValueChanged.RemoveListener(OnDropdownChanged);
        }

        #endregion

        #region Behaviour

        private void OnDropdownChanged(int index)
        {
            string selectedOption = dropdownOptions[index];
            Enum.TryParse(selectedOption, out InitGameType selectedType);

            CloseAllLabel();
            
            switch (selectedType)
            {
                case InitGameType.MainGame:
                    labelMainGame.SetActive(true);
                    break;
                case InitGameType.TestMobBattle:
                    labelTestMobBattle.SetActive(true);
                    break;
                case InitGameType.TestQuestionSpot:
                    labelTestQuestionSpot.SetActive(true);
                    break;
                case InitGameType.TestMap:
                    labelTestMap.SetActive(true);
                    break;
                case InitGameType.TestStoreSpot:
                    labelTestStoreSpot.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CloseAllLabel()
        {
            labelMainGame.SetActive(false);
            labelTestMobBattle.SetActive(false);
            labelTestQuestionSpot.SetActive(false);
            labelTestMap.SetActive(false);
            labelTestStoreSpot.SetActive(false);
        }

        #endregion
        
    }
}