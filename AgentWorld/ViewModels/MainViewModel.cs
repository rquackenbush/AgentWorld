using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using AgentWorld.Engine.Model;
using AgentWorld.Engine.Runtime;
using Avalonia;
using ReactiveUI;

namespace AgentWorld.ViewModels;

public class MainViewModel : ViewModelBase
{
    private int _numberOfAgents = 100;
    private int _memorySize = 8;
    private int _numberOfCycles = 10000;
    private int _numberOfInstructions = 50;
    private int _worldWidth = 1000;
    private int _worldHeight = 1000;
    private WorldViewModel? _world;
    private int _iterationNumber;

    private readonly InstructionFactory _instructionFactory = new InstructionFactory();

    public MainViewModel()
    {
        RunCommand =  ReactiveCommand.Create(() => Run());
    }

    public ICommand RunCommand { get; }

    private void Run()
    {
        var random = new Random();

        var agentModelFactory = new AgentModelFactory(_instructionFactory, MemorySize, NumberOfInstructions);

        Task.Run(() =>
        {
            AgentModel[] survived = new AgentModel[] { };

            for (int generationIndex = 0; generationIndex < 1; generationIndex++)
            {
                AgentModel[] toRun = GenerateAgents(agentModelFactory, survived);

                Console.WriteLine($"Generation {generationIndex} starting....");

                FoodX = (int)(random.NextDouble() * 800) + 100;
                FoodY = (int)(random.NextDouble() * 800) + 100;

                Console.WriteLine($"  Food is at ({FoodX}, {FoodY})");

                survived = RunAgents(toRun);

                var maxGeneration = -1;

                if (survived.Length > 0)
                    maxGeneration = survived.Max(a => a.Generation);

                string message = $"  Generation {generationIndex} complete - {survived.Length} survived with max generation {maxGeneration}!";

                Console.WriteLine(message);
            }

            Console.WriteLine("Survivors:");

            foreach (var agent in survived.OrderByDescending(a => a.Generation))
            {
                Console.WriteLine($"  Agent {agent.Id} - generation {agent.Generation}");
            }
        });
    }

    private AgentModel[] GenerateAgents(AgentModelFactory agentFactory, AgentModel[] survivors)
    {
        var agentModels = new List<AgentModel>(NumberOfAgents);

        foreach (var survivor in survivors)
        {
            survivor.Generation++;
        }

        agentModels.AddRange(survivors);

        //create the agents
        for (int x = 0; x < NumberOfAgents - survivors.Length; x++)
        {
            //Create the agent model
            var agentModel = agentFactory.Create();

            agentModels.Add(agentModel);
        }

        return agentModels.ToArray();
    }

    private AgentModel[] RunAgents(AgentModel[] agentModels)
    {
        var agents = new List<Agent>(NumberOfAgents);

        foreach (var agentModel in agentModels)
        {
            //Create the agent
            var agent = new Agent(agentModel, _instructionFactory);

            agents.Add(agent);
        }

        var worldSize = new Size(WorldWidth, WorldHeight);

        var foodLocation = new Rect(new Point(FoodX, FoodY), new Size(FoodSize, FoodSize));

        //Create the world viewmodel
        var world = new WorldViewModel(agents, worldSize, foodLocation);

        this.World = world;

        for (int i = 0; i < NumberOfCycles; i++)
        {
            IterationNumber = i;

            world.Cycle();
        }

        var lived = world.Agents
            .Where(a => !a.IsDead)
            .Select(a => a.GetModel())
            .ToArray();
        
        return lived;
    }

    public WorldViewModel World
    {
        get => _world;
        private set
        {
            _world = value; 
            OnPropertyChanged();
        }
    }

    public int NumberOfAgents
    {
        get => _numberOfAgents;
        set
        {
            _numberOfAgents = value;
            OnPropertyChanged();
        }
    }

    public int MemorySize
    {
        get => _memorySize;
        set
        {
            _memorySize = value;
            OnPropertyChanged();
        }
    }

    public int NumberOfInstructions
    {
        get => _numberOfInstructions;
        set
        {
            _numberOfInstructions = value;
            OnPropertyChanged();
        }
    }

    public int NumberOfCycles
    {
        get => _numberOfCycles;
        set
        {
            _numberOfCycles = value;
            OnPropertyChanged();
        }
    }

    public int WorldWidth
    {
        get => _worldWidth;
        set
        {
            _worldWidth = value;
            OnPropertyChanged();
        }
    }

    public int WorldHeight
    {
        get => _worldHeight;
        set
        {
            _worldHeight = value;
            OnPropertyChanged();
        }
    }

    public int IterationNumber
    {
        get => _iterationNumber;
        set
        {
            _iterationNumber = value;
            OnPropertyChanged();
        }
    }

    public int FoodX { get; set; } = 500;

    public int FoodY { get; set; } = 400;

    public int FoodSize { get; set; } = 50;
}