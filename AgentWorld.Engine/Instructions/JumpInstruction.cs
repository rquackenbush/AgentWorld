namespace AgentWorld.Engine.Instructions;

using AgentWorld.Engine.Model;
using AgentWorld.Engine.Runtime;

public class JumpInstruction : InstructionBase
{
    public override InstructionType Type => InstructionType.Jump;

    protected override InstructionExecutionResult ExecuteCore(InstructionExecutionContext context)
    {
        //return new InstructionExecutionResult(context.Instruction.Data);
        return new InstructionExecutionResult(context.ProgramCounter + 1);
    }

    public override InstructionModel Create(InstructionCreationContext context)
    {
        return new InstructionModel()
        {
            Type = Type,
            Data = context.Random.Next(0, context.NumberOfInstructions)
        };
    }
}