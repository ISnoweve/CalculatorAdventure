using _Main.CalculatorSys.Data.Enum;
using _Main.CalculatorSys.Sys;
using _Main.CalculatorSys.Sys.Calculator;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class CalculatorCalculateTest
    {
        [Test]
        public void Calculate_Example_Add()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
                { 
                    CalculatorOperator.Add, 
                    CalculatorOperator.Add, 
                    CalculatorOperator.Add, 
                    CalculatorOperator.Add, 
                };
            int[] numbers =
                {
                    2, 
                    3, 
                    4, 
                    5
                };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(14, result, "2+3+4+5 should equal 14");
        }
        
        [Test]
        public void Calculate_Example_Subtract()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Subtract, 
                CalculatorOperator.Subtract, 
                CalculatorOperator.Subtract, 
            };
            int[] numbers =
            {
                9, 
                1, 
                2, 
                3
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(3, result, "9-1-2-3 should equal 3");
        }
        
        [Test]
        public void Calculate_Example_Add_Subtract()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Subtract, 
                CalculatorOperator.Add, 
                CalculatorOperator.Subtract, 
            };
            int[] numbers =
            {
                3, 
                1, 
                2, 
                6
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(-2, result, "3-1+2-6 should equal -2");
        }
        
        [Test]
        public void Calculate_Example_Multiply_One()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Multiply,
                CalculatorOperator.Multiply,
            };
            int[] numbers =
            {
                7, 
                8, 
                6, 
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(3);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(336, result, "7+8*6 should equal 336");
        }
        
        [Test]
        public void Calculate_Example_Multiply_Two()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Multiply,
                CalculatorOperator.Multiply,
                CalculatorOperator.Multiply,
            };
            int[] numbers =
            {
                7, 
                8, 
                6,
                2
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(672, result, "7+8*6*2 should equal 672");
        }
        
        [Test]
        public void Calculate_Example_Multiply_Three()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Multiply,
                CalculatorOperator.Multiply,
                CalculatorOperator.Multiply,
                CalculatorOperator.Multiply,
            };
            int[] numbers =
            {
                7, 
                8, 
                6,
                2,
                2
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(5);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(1344, result, "7+8*6*2*2 should equal 1344");
        }
        
        [Test]
        public void Calculate_Example_Multiply_Add_One()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add,
                CalculatorOperator.Add,
                CalculatorOperator.Multiply,
            };
            int[] numbers =
            {
                7, 
                8, 
                6, 
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(3);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(55, result, "7+8+6 should equal 55");
        }
        
        [Test]
        public void Calculate_Example_Multiply_Add_Two()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add,
                CalculatorOperator.Add,
                CalculatorOperator.Multiply,
            };
            int[] numbers =
            {
                8, 
                4, 
                6
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(3);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(32, result, "8+4*6 should equal 32");
        }
        
        [Test]
        public void Calculate_Example_Multiply_Subtract_One()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add,
                CalculatorOperator.Subtract,
                CalculatorOperator.Multiply,
            };
            int[] numbers =
            {
                8, 
                4, 
                6
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(3);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(-16, result, "8-4*6 should equal -16");
        }
        
        [Test]
        public void Calculate_Example_Multiply_Subtract_Two()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add,
                CalculatorOperator.Multiply,
                CalculatorOperator.Subtract,
            };
            int[] numbers =
            {
                8, 
                4, 
                6
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(3);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(26, result, "8*4-6 should equal 26");
        }
        
        [Test]
        public void Calculate_Example_Divide_One()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Divide,
                CalculatorOperator.Divide,
                CalculatorOperator.Divide,
            };
            int[] numbers =
            {
                127,
                11,
                5,
                2
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(1, result, "127÷11÷5÷2 should equal 1");
        }
        
        [Test]
        public void Calculate_Example_Divide_Two()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Divide
            };
            int[] numbers =
            {
                2,
                2
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(2);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(1, result, "2/2 should equal 1");
        }
        
        [Test]
        public void Calculate_Example_Divide_Add_One()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Divide,
                CalculatorOperator.Add,
            };
            int[] numbers =
            {
                7,
                8,
                1
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(3);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(1, result, "7/8+1 should equal 1");
        }

        [Test]
        public void Calculate_Example_Divide_Add_Two()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Add,
                CalculatorOperator.Divide,
            };
            int[] numbers =
            {
                7,
                8,
                2
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(3);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(11, result, "7+8/2 should equal 11");
        }
        
        [Test]
        public void Calculate_Example_Divide_Subtract_One()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Subtract,
                CalculatorOperator.Divide
            };
            int[] numbers =
            {
                80,
                40,
                8
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(3);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(75, result, "80-40/8 should equal 75");
        }
        
        [Test]
        public void Calculate_Example_Divide_Subtract_Two()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Divide,
                CalculatorOperator.Subtract 
            };
            int[] numbers =
            {
                2,
                2,
                3
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(3);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(-2, result, "2/2-3 should equal -2");
        }
        
        [Test]
        public void Calculate_Example_Divide_Subtract_Three()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Divide,
                CalculatorOperator.Subtract,
                CalculatorOperator.Divide
            };
            int[] numbers =
            {
                99,
                11,
                33,
                3
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(-2, result, "99/11-33/3 should equal -2");
        }
        
        [Test]
        public void Calculate_Example_Divide_Add_Subtract_One()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add,
                CalculatorOperator.Divide,
                CalculatorOperator.Add,
                CalculatorOperator.Divide,
                CalculatorOperator.Add,
            };
            int[] numbers =
            {
                42,
                3,
                74,
                4,
                2
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(5);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(34, result, "45/3+74/4+2 should equal 34");
        }
        
        [Test]
        public void Calculate_Example_Divide_Subtract_Multiply_One()
        {
            CalculatorSystem calculatorSystem = new CalculatorSystem();
            
            CalculatorOperator[] operators = 
            { 
                CalculatorOperator.Add, 
                CalculatorOperator.Subtract,
                CalculatorOperator.Divide,
                CalculatorOperator.Multiply
            };
            int[] numbers =
            {
                630,
                7,
                3,
                7
            };
            calculatorSystem.SetCurrentCalculatorOperationAndValueCount(4);
            calculatorSystem.SetCurrentOperators(operators);
            calculatorSystem.SetNumbersInBox(numbers);
            
            int result = calculatorSystem.SetEqualTest();
            Assert.AreEqual(616, result, "630-7÷3x7 should equal 616");
        }
    }
}