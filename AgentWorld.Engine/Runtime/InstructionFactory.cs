namespace AgentWorld.Engine.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AgentWorld.Engine.Instructions;
    using AgentWorld.Engine.Model;

    public class InstructionFactory
    {
        private readonly IDictionary<InstructionType, InstructionBase> _instructions;

        public InstructionFactory()
        {
            var instructions = new InstructionBase[]
            {
                new ClearMemoryInstruction(), 
                new ConditionalSkipInstruction(), 
                new JumpInstruction(), 
                new SetMemoryInstruction(), 
            };

            _instructions = instructions.ToDictionary(i => i.Type);
        }

        public InstructionBase this[InstructionType type]
        {
            get
            {
                if (!_instructions.TryGetValue(type, out var instruction))
                    throw new Exception($"Unable to find instruction for type {type}.");

                return instruction;
            }
        }
    }
}