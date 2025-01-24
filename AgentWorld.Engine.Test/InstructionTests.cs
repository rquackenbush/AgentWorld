using System;

namespace AgentWorld.Engine.Test;

using AgentWorld.Engine.Instructions;
using AgentWorld.Engine.Model;
using AgentWorld.Engine.Runtime;
using Xunit;

public class InstructionTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(20)]
    public void ClearMemoryTest(int programCounter)
    {
        var instruction = new SetMemoryInstruction();

        var instructionModel = new InstructionModel
        {
            Type = instruction.Type,
            Data = 0
        };

        var context = new InstructionExecutionContext(new bool[1], programCounter, instructionModel);

        var result = instruction.Execute(context);

        Assert.True(context.Memory[0]);

        Assert.Equal(programCounter + 1, result.ProgramCounter);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(20)]
    public void ClearOutputTest(int programCounter)
    {
        var instruction = new ClearMemoryInstruction();

        var instructionModel = new InstructionModel
        {
            Type = instruction.Type,
            Data = 0
        };

        var context = new InstructionExecutionContext(new bool[] { true }, programCounter, instructionModel);

        var result = instruction.Execute(context);

        Assert.False(context.Memory[0]);

        Assert.Equal(programCounter + 1, result.ProgramCounter);
    }

    [Theory]
    [InlineData(0, 40)]
    [InlineData(2, 10)]
    [InlineData(4, 2)]
    [InlineData(8, 0)]
    public void JumpTest(int programCounterInitial, int address)
    {
        var instruction = new JumpInstruction();

        var instructionModel = new InstructionModel()
        {
            Type = instruction.Type,
            Data = address,
        };

        var context = new InstructionExecutionContext(new bool[0], programCounterInitial, instructionModel);

        var result = instruction.Execute(context);

        Assert.Equal(address, result.ProgramCounter);
    }

    [Theory]
    [InlineData(0, false, 1)]
    [InlineData(0, true, 2)]
    [InlineData(10, false, 11)]
    [InlineData(10, true, 12)]
    public void ConditionalSkipTest(int programCounterInitial, bool inputValue, int expectedProgramCounter)
    {
        var instruction = new ConditionalSkipInstruction();

        var instructionModel = new InstructionModel()
        {
            Type = instruction.Type,
            Data = 0,
        };

        var context = new InstructionExecutionContext(new bool[] { inputValue }, programCounterInitial, instructionModel);

        var result = instruction.Execute(context);

        Assert.Equal(expectedProgramCounter, result.ProgramCounter);
    }
}
