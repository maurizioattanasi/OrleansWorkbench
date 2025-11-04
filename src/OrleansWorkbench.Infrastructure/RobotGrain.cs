using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Orleans;

using OrleansWorkbench.Application.Interfaces;

namespace OrleansWorkbench.Infrastructure;

public class RobotGrain(ILogger<RobotGrain> logger) : Grain, IRobotGrain
{
    private readonly Queue<string> _instructions = new Queue<string>();
    
    public Task AddInstruction(string instruction)
    {
        logger.LogInformation("Adding {@Instruction} instruction", instruction);
        
        _instructions.Enqueue(instruction);
        return Task.CompletedTask;
    }

    public Task<int> GetInstructionCountAsync()
    {
        logger.LogInformation("Returning instuction count: {@InstructionCount}", _instructions.Count);
        return Task.FromResult(_instructions.Count);
    }

    public Task<string> GetNextInstructionAsync()
    {
        if (_instructions.Count == 0)
        {
            logger.LogInformation("No instruction available");
            return Task.FromResult<string>(null!);
        }

        var instruction = _instructions.Dequeue();
        logger.LogInformation("Next instruction is {@NextInstructin}", instruction);
        return Task.FromResult(instruction);
    }
}