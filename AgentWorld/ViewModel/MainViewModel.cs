namespace AgentWorld.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using AgentWorld.Engine.Model;
    using AgentWorld.Engine.Runtime;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;

    public class MainViewModel : ViewModelBase
    {
        private int _numberOfAgents = 100000;
        private int _memorySize = 8;
        private int _numberOfCycles = 10000;
        private int _numberOfInstructions = 30;
        private int _worldWidth = 1000;
        private int _worldHeight = 1000;
        private WorldViewModel _world;
        private int _iterationNumber;

        private readonly InstructionFactory _instructionFactory = new InstructionFactory();

        public MainViewModel()
        {
            RunCommand = new RelayCommand(Run);
        }

        public ICommand RunCommand { get; }

        private void Run()
        {
            var random = new Random();

            var agentModels = new List<AgentModel>(NumberOfAgents);

            var agentModelFactory = new AgentModelFactory(_instructionFactory, MemorySize, NumberOfInstructions);

            //create the agents
            for (int x = 0; x < NumberOfAgents; x++)
            {
                //Create the agent model
                var agentModel = agentModelFactory.Create();

                agentModels.Add(agentModel);
            }

            Task.Run(() =>
            {
                AgentModel[] toRun = agentModels.ToArray();

                for (int generationIndex = 0; generationIndex < 10 && toRun.Length > 0; generationIndex++)
                {
                    Console.WriteLine($"Generation {generationIndex} starting....");

                    FoodX = (int)(random.NextDouble() * 800) + 100;
                    FoodY = (int)(random.NextDouble() * 800) + 100;

                    Console.WriteLine($"  Food is at ({FoodX}, {FoodY})");

                    toRun = RunAgents(toRun);

                    string message = $"  Generation {generationIndex} complete - {toRun.Length} survived!";

                    Console.WriteLine(message);
                }

                MessageBox.Show("Done!");
            });
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
                RaisePropertyChanged();
            }
        }

        public int NumberOfAgents
        {
            get => _numberOfAgents;
            set
            {
                _numberOfAgents = value;
                RaisePropertyChanged();
            }
        }

        public int MemorySize
        {
            get => _memorySize;
            set
            {
                _memorySize = value;
                RaisePropertyChanged();
            }
        }

        public int NumberOfInstructions
        {
            get => _numberOfInstructions;
            set
            {
                _numberOfInstructions = value;
                RaisePropertyChanged();
            }
        }

        public int NumberOfCycles
        {
            get => _numberOfCycles;
            set
            {
                _numberOfCycles = value;
                RaisePropertyChanged();
            }
        }

        public int WorldWidth
        {
            get => _worldWidth;
            set
            {
                _worldWidth = value; 
                RaisePropertyChanged();
            }
        }

        public int WorldHeight
        {
            get => _worldHeight;
            set
            {
                _worldHeight = value; 
                RaisePropertyChanged();
            }
        }

        public int IterationNumber
        {
            get => _iterationNumber;
            set
            {
                _iterationNumber = value;
                RaisePropertyChanged();
            }
        }

        public int FoodX { get; set; } = 500;

        public int FoodY { get; set; } = 400;

        public int FoodSize { get; set; } = 50;
    }
}