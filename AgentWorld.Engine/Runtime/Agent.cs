namespace AgentWorld.Engine.Runtime
{
    using System;
    using AgentWorld.Engine.Model;

    public class Agent
    {
        private readonly AgentModel _model;
        private readonly InstructionFactory _instructionFactory;
        private readonly bool[] _memory;
        private int _programCounter;

        public Agent(AgentModel model, InstructionFactory instructionFactory)
        {
            _model = model;
            _instructionFactory = instructionFactory;
            _memory = new bool[model.MemorySize];
        }

        public void Cycle()
        {
            //Get the current instruction model
            var instructionModel = _model.Instructions[_programCounter];

            //Create an execution context for the instruction
            var context = new InstructionExecutionContext(_memory, _programCounter, instructionModel);

            //Get the instruction implementation
            var instruction = _instructionFactory[instructionModel.Type];

            //Execute the instruction
            var result = instruction.Execute(context);

            if (result.ProgramCounter < 0)
            {
                _programCounter = 0;
            }
            else if (result.ProgramCounter >= _model.Instructions.Length)
            {
                _programCounter = 0;
            }
            else
            {
                _programCounter = result.ProgramCounter;
            }
    }

        public bool[] Memory => _memory;
    }
}