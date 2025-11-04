using System.Threading.Tasks;

using Orleans;

namespace OrleansWorkbench.Application.Interfaces;

public interface IRobotGrain : IGrainWithStringKey
{
    Task AddInstruction(string instruction);
    Task<string> GetNextInstructionAsync();
    Task<int> GetInstructionCountAsync();
}