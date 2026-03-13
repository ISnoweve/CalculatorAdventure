using System;
using TMPro;
using UnityEngine;

namespace _Main.MobBattleSys.View.UI_Mob.Runtime
{
    public class UI_MobGetDefeated : MonoBehaviour
    {
        [SerializeField] private GameObject defeatedPanel;

        private void Awake()
        {
            defeatedPanel.SetActive(false);
        }

        public void ShowDefeatedPanel()
        {
            defeatedPanel.SetActive(true);
        }
    }
}