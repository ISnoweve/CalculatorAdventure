using System.Collections.Generic;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Manager;
using _Main.CalculatorSys.Sys.Calculator;
using _Main.MobSys.Data;
using _Main.MobSys.Data.Mob;
using _Main.MobSys.Manager;
using _Main.MobSys.Sys.SelectSys;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.InitGameSys.View.UI_MobSelect
{
    public class MobBattleConfirmButton : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown mobDropdown;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button playButton;
        [SerializeField] private MobDataSoList mobDataSoList;
        [SerializeField] private CalculatorGameSettingSoList calculatorGameSettingSoList;


        private void ConfirmMobBattleSetting()
        {
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
            var options = new List<string>();
            for (var i = 0; i < calculatorGameSettingSoList.CalculatorGameSettings.Length; i++)
                options.Add((i + 2).ToString());
        }

        private void Subscribe()
        {
            confirmButton.onClick.AddListener(ConfirmMobBattleSetting);
            mobDropdown.onValueChanged.AddListener(ClosePlayButton);
        }

        private void OnDestroy()
        {
            confirmButton.onClick.RemoveListener(ConfirmMobBattleSetting);
            mobDropdown.onValueChanged.RemoveListener(ClosePlayButton);
        }

        private void ClosePlayButton(int index)
        {
            confirmButton.interactable = true;
            playButton.interactable = false;
        }

        #endregion
    }
}