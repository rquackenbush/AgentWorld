namespace AgentWorld.Engine.Runtime;

using AgentWorld.Engine.Model;

public class InstructionExecutionContext
{
    public InstructionExecutionContext(bool[] memory, int programCounter, InstructionModel instruction)
    {
        Memory = memory;
        ProgramCounter = programCounter;
        Instruction = instruction;
    }

    public bool[] Memory { get; }

    public int ProgramCounter { get; }

    public InstructionModel Instruction { get; }
}