namespace AgentWorld.Engine.Runtime;

using System;

public class InstructionCreationContext
{
    public InstructionCreationContext(Random random, int memorySize, int numberOfInstructions)
    {
        Random = random;
        MemorySize = memorySize;
        NumberOfInstructions = numberOfInstructions;
    }

    public Random Random { get; }

    public int MemorySize { get; }

    public int NumberOfInstructions { get; }
}