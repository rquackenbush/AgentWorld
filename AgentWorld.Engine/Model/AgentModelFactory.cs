namespace AgentWorld.Engine.Model;

using System;
using System.Collections.Generic;
using AgentWorld.Engine.Instructions;
using AgentWorld.Engine.Runtime;

public class AgentModelFactory
{
    private readonly InstructionFactory _instructionFactory;
    private readonly int _memorySize;
    private readonly int _numberOfInstructions;
    private readonly Random _random;

    public AgentModelFactory(InstructionFactory instructionFactory, int memorySize, int numberOfInstructions, Random random = null)
    {
        _instructionFactory = instructionFactory ?? throw new ArgumentNullException(nameof(instructionFactory));
        _memorySize = memorySize;
        _numberOfInstructions = numberOfInstructions;
        _random = random ?? new Random();
    }

    public AgentModel Create()
    {
        List<InstructionModel> instructions = new List<InstructionModel>(_numberOfInstructions);

        for (int x = 0; x < _numberOfInstructions; x++)
        {
            instructions.Add(CreateInstruction());
        }

        return new AgentModel()
        {
            Id = Guid.NewGuid(),
            Instructions = instructions.ToArray(),
            MemorySize = _memorySize
        };
    }

    private InstructionModel CreateInstruction()
    {
        //Get an instruction type
        InstructionType instructionType = (InstructionType)_random.Next(0, (int) InstructionType.ConditionalSkip + 1);

        //Get the instruction implementation
        InstructionBase instruction = _instructionFactory[instructionType];

        //Create the context
        var context = new InstructionCreationContext(_random, _memorySize, _numberOfInstructions);

        //finally, create the damn instruction
        return instruction.Create(context);
    }
}