using System;
using _Main.CalculatorSys.Sys;
using UnityEngine;

namespace _Main.CalculatorSys
{
    public class Test : MonoBehaviour
    {
        public CalculatorButtonManager calculatorButtonManager;

        private void Awake()
        {
            calculatorButtonManager = CalculatorButtonManager.Instance;
        }
    }
}