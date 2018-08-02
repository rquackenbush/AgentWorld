namespace AgentWorld.Engine.Instructions
{
    using System;
    using AgentWorld.Engine.Model;
    using AgentWorld.Engine.Runtime;

    public abstract class InstructionBase
    {
        public abstract InstructionType Type { get; }

        public InstructionExecutionResult Execute(InstructionExecutionContext context)
        {
            if (context.Instruction.Type != Type)
                throw new InvalidOperationException($"This instruction is of type {Type}, while the presented instruction is of type {context.Instruction.Type}.");

            return ExecuteCore(context);
        }

        protected abstract InstructionExecutionResult ExecuteCore(InstructionExecutionContext context);

        public abstract InstructionModel Create(InstructionCreationContext context);
    }
}