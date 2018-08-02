namespace AgentWorld.Engine.Instructions
{
    using AgentWorld.Engine.Model;
    using AgentWorld.Engine.Runtime;

    public class ConditionalSkipInstruction : InstructionBase
    {
        public override InstructionType Type => InstructionType.ConditionalSkip;

        protected override InstructionExecutionResult ExecuteCore(InstructionExecutionContext context)
        {
            int programCounter = context.ProgramCounter + 1;

            if (context.Instruction.Data < context.Memory.Length)
            {
                if (context.Memory[context.Instruction.Data])
                    programCounter++;
            }

            return new InstructionExecutionResult(programCounter);
        }

        public override InstructionModel Create(InstructionCreationContext context)
        {
            return new InstructionModel()
            {
                Type = Type,
                Data = context.Random.Next(0, context.MemorySize)
            };
        }
    }
}