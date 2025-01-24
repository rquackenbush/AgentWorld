namespace AgentWorld.Engine.Runtime;

public class InstructionExecutionResult
{
    public InstructionExecutionResult(int programCounter)
    {
        ProgramCounter = programCounter;
    }

    public int ProgramCounter { get; }
}