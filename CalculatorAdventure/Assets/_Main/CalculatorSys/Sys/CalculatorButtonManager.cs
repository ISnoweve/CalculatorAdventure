using System;
using System.Collections.Generic;
using _Main.CalculatorSys.Data;
using _Main.CalculatorSys.Sys.EventData;
using _Main.CalculatorSys.Sys.Runtime;
using _Main.ResourceSys;
using BolingsUnityTools;
using MessagePipe;
using UnityEngine;

namespace _Main.CalculatorSys.Sys
{
    [Serializable]
    public sealed class CalculatorButtonManager : Singleton<CalculatorButtonManager>
    {
        [SerializeField] private List<CalculatorButton> _calculatorButtons;
        
        protected override void Initialize()
        {
            base.Initialize();
        }
        
        public void InitializeButtons()
        {
            string path = ResourceConstPath.GetCalculatorBaseDataPath();
            CalculatorButtonsData buttonsData = ResourceUnityManager.Load<CalculatorButtonsData>(path);
            
            _calculatorButtons = new List<CalculatorButton>();
            
            foreach (var buttonData in buttonsData.NumberButtons)
            {
                CalculatorButton calculatorButton = new CalculatorButton(buttonData);
                if(_calculatorButtons.Contains(calculatorButton))continue;
                _calculatorButtons.Add(calculatorButton);
            }
            
            ButtonsSpawn buttonsSpawn = new ButtonsSpawn(_calculatorButtons);
            GlobalMessagePipe.GetPublisher<ButtonsSpawn>().Publish(buttonsSpawn);
            
            ResourceUnityManager.Unload(path);
        }

        public List<CalculatorButton>  GetAllButtonData()
        {
            return _calculatorButtons;
        }

        public void GetButtonByIndex(byte index)
        {
            foreach (var VARIABLE in _calculatorButtons)
            {
                if (VARIABLE.Index == index)
                {
                    Debug.Log(VARIABLE.Index);
                }
            }
        }

        protected override void Release()
        {
            base.Release();
        }
    }
}