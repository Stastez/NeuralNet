namespace NN
{
    class InputNode : INode
    {
        private double input;
        private double weight;

        public InputNode(double pWeight)
        {
            weight = pWeight;
        }

        public void SetInput(double pInput)
        {
            input = pInput;
        }

        public double GetOutput()
        {
            return input * weight;
        }

        public double GetWeight()
        {
            return weight;
        }

        public void ModifyWeight(double pFactor)
        {
            weight *= pFactor;
        }
    }

    class Node : INode
    {
        private double input0;
        private double input1;
        private double input2;
        private double input3;

        private Operationen operation;
        private double weight;

        /*private INode nodeOben;
		private INode nodeUnten;*/

        public Node(double pWeight)
        {
            weight = pWeight;
        }

        public void SetInputs(double pInput0, double pInput1, double pInput2, double pInput3)
        {
            input0 = pInput0;
            input1 = pInput1;
            input2 = pInput2;
            input3 = pInput3;
        }

        public void SetOperation(Operationen pOperation)
        {
            operation = pOperation;
        }

        public double GetOutput()
        {
            if (operation == Operationen.ADDITION)
            {
                return (input0 + input1 + input2 + input3) * weight;
            }
            else if (operation == Operationen.MULTIPLIKATION)
            {
                return (input0 * input1 * input2 * input3) * weight;
            }
            else
            {
                return default;
            }
        }

        public double GetWeight()
        {
            return weight;
        }

        public void ModifyWeight(double pFactor)
        {
            weight *= pFactor;
        }
    }

    enum Operationen
    {
        ADDITION, MULTIPLIKATION
    }
}
