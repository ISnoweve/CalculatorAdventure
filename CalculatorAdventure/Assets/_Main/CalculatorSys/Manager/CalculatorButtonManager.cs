using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Data.Enum;
using _Main.CalculatorSys.Manager.Event;
using _Main.CalculatorSys.Manager.Runtime;
using BolingsUnityTools;
using MessagePipe;
using UnityEngine;

namespace _Main.CalculatorSys.Manager
{
    [Serializable]
    public class CalculatorButtonManager : Singleton<CalculatorButtonManager>
    {
        [SerializeField] private List<CalculatorButton> calculatorButtons;
        [SerializeField] private List<CalculatorButton> numberButtons;

        public static void InitializeButtons(CalculatorButtonsData buttonsData)
        {
            Instance.calculatorButtons = new List<CalculatorButton>();
            Instance.numberButtons = new List<CalculatorButton>();

            foreach (var buttonData in buttonsData.NumberButtons)
            {
                var calculatorButton = new CalculatorButton(buttonData);
                if (Instance.calculatorButtons.Contains(calculatorButton)) continue;
                Instance.calculatorButtons.Add(calculatorButton);
                if (calculatorButton.CalculatorButtonType == CalculatorButtonType.NumberActivate ||
                    calculatorButton.CalculatorButtonType == CalculatorButtonType.NumberLock)
                    Instance.numberButtons.Add(calculatorButton);
            }

            var buttonsSpawn = new ButtonsSpawn(Instance.calculatorButtons);
            GlobalMessagePipe.GetPublisher<ButtonsSpawn>().Publish(buttonsSpawn);
        }

        public static List<CalculatorButton> GetAllButton()
        {
            return Instance.calculatorButtons;
        }

        public static List<CalculatorButton> GetAllNumberButton()
        {
            return Instance.numberButtons;
        }

        public static CalculatorButton GetButtonByIndex(byte index)
        {
            foreach (var buttonByIndex in Instance.calculatorButtons)
                if (buttonByIndex.Index == index)
                    return buttonByIndex;

            return null;
        }
    }
}