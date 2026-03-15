using System.Collections.Generic;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Sys.Calculator;
using _Main.MobBattleSys.Sys.SelectSys;
using _Main.MobSys.Data;
using _Main.MobSys.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.InitGameSys.View.UI_MobSelect
{
    public class MobBattleConfirmButton : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown mobDropdown;
        [SerializeField] private TMP_Dropdown calculatorSettingDropdown;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button playButton;
        [SerializeField] private MobDataSoList mobDataSoList;
        [SerializeField] private CalculatorGameSettingSoList calculatorGameSettingSoList;


        private void ConfirmMobBattleSetting()
        {
            SetCalculator();
            SetMobBattle();
            confirmButton.interactable = false;
            playButton.interactable = true;
        }

        private void SetMobBattle()
        {
            var index = mobDropdown.value;
            var data = mobDataSoList.Mobs[index];
            MobManager.SetMobDataSoList(mobDataSoList);
            SelectMobDataSystem.SelectMobData(data.Id);
        }

        private void SetCalculator()
        {
            var index = calculatorSettingDropdown.value;
            var calculatorGameSetting = calculatorGameSettingSoList.CalculatorGameSettings[index];
            CalculatorButtonManager.InitializeButtons(calculatorGameSetting.ButtonsData);
            CalculatorSystem.InitializeSystem(calculatorGameSetting.CalculatorSystemData);
        }

        #region Life Cycle

        private void Awake()
        {
            ClosePlayButton(0);
            InitializeMobDropdown();
            InitializeCalculatorSettingDropdown();
            Subscribe();
        }

        private void InitializeMobDropdown()
        {
            mobDropdown.ClearOptions();
            var options = new List<string>();
            foreach (var mobData in mobDataSoList.Mobs) options.Add(mobData.Id.ToString());
            mobDropdown.AddOptions(options);
        }

        private void InitializeCalculatorSettingDropdown()
        {
            calculatorSettingDropdown.ClearOptions();
            var options = new List<string>();
            for (var i = 0; i < calculatorGameSettingSoList.CalculatorGameSettings.Length; i++)
                options.Add((i + 2).ToString());
            calculatorSettingDropdown.AddOptions(options);
        }

        private void Subscribe()
        {
            confirmButton.onClick.AddListener(ConfirmMobBattleSetting);
            mobDropdown.onValueChanged.AddListener(ClosePlayButton);
            calculatorSettingDropdown.onValueChanged.AddListener(ClosePlayButton);
        }

        private void OnDestroy()
        {
            confirmButton.onClick.RemoveListener(ConfirmMobBattleSetting);
            mobDropdown.onValueChanged.RemoveListener(ClosePlayButton);
            calculatorSettingDropdown.onValueChanged.RemoveListener(ClosePlayButton);
        }

        private void ClosePlayButton(int index)
        {
            confirmButton.interactable = true;
            playButton.interactable = false;
        }

        #endregion
    }
}