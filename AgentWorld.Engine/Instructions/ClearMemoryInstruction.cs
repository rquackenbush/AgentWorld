namespace AgentWorld.Engine.Instructions
{
    using AgentWorld.Engine.Model;
    using AgentWorld.Engine.Runtime;

    /// <summary>
    /// Clears a memory location
    /// </summary>
    public class ClearMemoryInstruction : InstructionBase
    {
        public override InstructionType Type => InstructionType.ClearMemory;

        protected override InstructionExecutionResult ExecuteCore(InstructionExecutionContext context)
        {
            if (context.Instruction.Data < context.Memory.Length)
            {
                context.Memory[context.Instruction.Data] = false;
            }

            return new InstructionExecutionResult(context.ProgramCounter + 1);
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