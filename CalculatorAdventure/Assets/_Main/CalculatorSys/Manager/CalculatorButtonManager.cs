using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Enum;
using _Main.CalculatorSys.Manager.Event;
using _Main.CalculatorSys.Manager.Runtime;
using _Main.SnoweveToolKit.ToolKit;
using MessagePipe;
using UnityEngine;

namespace _Main.CalculatorSys.Manager
{
    [Serializable]
    public class CalculatorButtonManager : Singleton<CalculatorButtonManager>
    {
        [SerializeField] private List<CalculatorButton> calculatorButtons;
        [SerializeField] private List<CalculatorButton> numberButtons;
        [SerializeField] private CalculatorButton multiplyButton;
        [SerializeField] private CalculatorButton divideButton;

        public static void InitializeButtons(CalculatorButtonsData buttonsData)
        {
            Instance.calculatorButtons = new List<CalculatorButton>();
            Instance.numberButtons = new List<CalculatorButton>();

            foreach (var buttonData in buttonsData.NumberButtons)
            {
                var calculatorButton = new CalculatorButton(buttonData);
                if (Instance.calculatorButtons.Contains(calculatorButton)) continue;
                Instance.calculatorButtons.Add(calculatorButton);
                if (calculatorButton.CalculatorOperator == CalculatorOperator.Multiply)
                    Instance.multiplyButton = calculatorButton;
                if (calculatorButton.CalculatorOperator == CalculatorOperator.Divide)
                    Instance.divideButton = calculatorButton;
                if (calculatorButton.CalculatorButtonType == CalculatorButtonType.NumberActivate ||
                    calculatorButton.CalculatorButtonType == CalculatorButtonType.NumberNotActivate)
                    Instance.numberButtons.Add(calculatorButton);
            }
        }

        public static void CallRuntimeButtons()
        {
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

        public static List<CalculatorButton> GetAllActivateNumberButton()
        {
            var activateNumberButtons = new List<CalculatorButton>();
            foreach (var numberButton in Instance.numberButtons)
                if (numberButton.CalculatorButtonType == CalculatorButtonType.NumberActivate)
                    activateNumberButtons.Add(numberButton);

            return activateNumberButtons;
        }

        public static CalculatorButton GetButtonByIndex(byte index)
        {
            foreach (var buttonByIndex in Instance.calculatorButtons)
                if (buttonByIndex.Index == index)
                    return buttonByIndex;

            return null;
        }

        public static CalculatorButton GetMultiplyButton()
        {
            return Instance.multiplyButton;
        }

        public static CalculatorButton GetDivideButton()
        {
            return Instance.divideButton;
        }
    }
}