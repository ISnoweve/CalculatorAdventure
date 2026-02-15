using _Main.CalculatorSys.Data.Enum;
using _Main.CalculatorSys.Sys;
using _Main.CalculatorSys.Sys.Calculator;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class CalculatorDeleteTest
    {
        [Test]
        public void Calculator_Example_DeleteNumber_One()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            int[] numbers =
            {
                2, 
                3, 
                4, 
                5
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetNumbersInBox(numbers);
            
            calculatorSystem.SetDeleteNumber();

            int target = 0;
            for (int i = 0; i < calculatorSystem.NumbersInBox.Length; i++)
            {
                if (calculatorSystem.NumbersInBox[i] == 0)
                {
                    target++;
                }
            }
            
            Assert.AreEqual(1, target);
        }
        
        [Test]
        public void Calculator_Example_DeleteNumber_Two()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            int[] numbers =
            {
                2, 
                3, 
                4, 
                5
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetNumbersInBox(numbers);
            
            calculatorSystem.SetDeleteNumber();
            calculatorSystem.SetDeleteNumber();

            int target = 0;
            for (int i = 0; i < calculatorSystem.NumbersInBox.Length; i++)
            {
                if (calculatorSystem.NumbersInBox[i] == 0)
                {
                    target++;
                }
            }
            
            Assert.AreEqual(2, target);
        }
        
        [Test]
        public void Calculator_Example_DeleteNumber_Three()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            int[] numbers =
            {
                2, 
                3, 
                4, 
                5
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetNumbersInBox(numbers);
            
            calculatorSystem.SetDeleteNumber();
            calculatorSystem.SetDeleteNumber();
            calculatorSystem.SetDeleteNumber();

            int target = 0;
            for (int i = 0; i < calculatorSystem.NumbersInBox.Length; i++)
            {
                if (calculatorSystem.NumbersInBox[i] == 0)
                {
                    target++;
                }
            }
            
            Assert.AreEqual(3, target);
        }
        
        [Test]
        public void Calculator_Example_DeleteOperator_One()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Add, 
                CalculatorOperator.Add, 
                CalculatorOperator.Add, 
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetCurrentOperators(operators);
            
            calculatorSystem.SetDeleteOperator();
            
            int target = 0;
            for (int i = 0; i < calculatorSystem.CurrentOperators.Length; i++)
            {
                if (calculatorSystem.CurrentOperators[i] == CalculatorOperator.None)
                {
                    target++;
                }
            }
            
            Assert.AreEqual(1, target);
        }
        
        [Test]
        public void Calculator_Example_DeleteOperator_Two()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Add, 
                CalculatorOperator.Add, 
                CalculatorOperator.Add, 
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetCurrentOperators(operators);
            
            calculatorSystem.SetDeleteOperator();
            calculatorSystem.SetDeleteOperator();
            
            int target = 0;
            for (int i = 0; i < calculatorSystem.CurrentOperators.Length; i++)
            {
                if (calculatorSystem.CurrentOperators[i] == CalculatorOperator.None)
                {
                    target++;
                }
            }
            
            Assert.AreEqual(2, target);
        }
        
        [Test]
        public void Calculator_Example_DeleteOperator_Three()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Add, 
                CalculatorOperator.Add, 
                CalculatorOperator.Add, 
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetCurrentOperators(operators);
            
            calculatorSystem.SetDeleteOperator();
            calculatorSystem.SetDeleteOperator();
            calculatorSystem.SetDeleteOperator();
            
            int target = 0;
            for (int i = 0; i < calculatorSystem.CurrentOperators.Length; i++)
            {
                if (calculatorSystem.CurrentOperators[i] == CalculatorOperator.None)
                {
                    target++;
                }
            }
            
            Assert.AreEqual(3, target);
        }
    }
}