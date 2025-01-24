namespace AgentWorld.Engine.Model;

using System;

public class AgentModel
{
    public Guid Id { get; set; }

    public InstructionModel[] Instructions { get; set; }

    public int MemorySize { get; set; }

    public int Generation { get; set; }
}