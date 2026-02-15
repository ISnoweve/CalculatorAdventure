using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Manager.EventData;
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
        
        protected override void Initialize()
        {
            base.Initialize();
        }
        
        public static void InitializeButtons(CalculatorButtonsData buttonsData)
        {
            Instance.calculatorButtons = new List<CalculatorButton>();
            
            foreach (var buttonData in buttonsData.NumberButtons)
            {
                CalculatorButton calculatorButton = new CalculatorButton(buttonData);
                if(Instance.calculatorButtons.Contains(calculatorButton))continue;
                Instance.calculatorButtons.Add(calculatorButton);
            }
            
            ButtonsSpawn buttonsSpawn = new ButtonsSpawn(Instance.calculatorButtons);
            GlobalMessagePipe.GetPublisher<ButtonsSpawn>().Publish(buttonsSpawn);
        }

        public static List<CalculatorButton> GetAllButton()
        {
            return Instance.calculatorButtons;
        }

        public static CalculatorButton GetButtonByIndex(byte index)
        {
            foreach (var buttonByIndex in Instance.calculatorButtons)
            {
                if (buttonByIndex.Index == index)
                {
                    return buttonByIndex;
                }
            }

            return null;
        }
    }
}