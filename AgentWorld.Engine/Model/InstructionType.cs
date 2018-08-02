namespace AgentWorld.Engine.Model
{
    public enum InstructionType
    {
        /// <summary>
        /// Set output[data]
        /// </summary>
        SetOutput, 

        /// <summary>
        /// Clear output[Data]
        /// </summary>
        ClearMemory,

        /// <summary>
        /// Jump to Data location in the program.
        /// </summary>
        Jump, 

        /// <summary>
        /// if Inputs[Data] == true, skip the next instruction
        /// </summary>
        ConditionalSkip, 
    }
}