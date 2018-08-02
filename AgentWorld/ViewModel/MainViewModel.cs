namespace AgentWorld.ViewModel
{
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
        private int _numberOfAgents = 10000;
        private int _memorySize = 10;
        private int _numberOfCycles = 10000;
        private int _numberOfInstructions = 20;
        private int _worldWidth = 1000;
        private int _worldHeight = 1000;
        private WorldViewModel _world;
        private int _iterationNumber;

        public MainViewModel()
        {
            RunCommand = new RelayCommand(Run);
        }

        public ICommand RunCommand { get; }

        private void Run()
        {
            var agents = new List<Agent>(NumberOfAgents);

            var instructionFactory = new InstructionFactory();

            var agentModelFactory = new AgentModelFactory(instructionFactory, MemorySize, NumberOfInstructions);

            //create the agents
            for (int x = 0; x < NumberOfAgents; x++)
            {
                //Create the agent model
                var agentModel = agentModelFactory.Create();

                //Create the agent
                var agent = new Agent(agentModel, instructionFactory);

                agents.Add(agent);
            }

            var worldSize = new Size(WorldWidth, WorldHeight);

            var foodLocation = new Rect(new Point(FoodX, FoodY), new Size(FoodSize, FoodSize));

            //Create the world viewmodel
            World = new WorldViewModel(agents, worldSize, foodLocation);

            Task.Run(() =>
            {
                for (int i = 0; i < NumberOfCycles; i++)
                {
                    IterationNumber = i;

                    World.Cycle();
                }

                int lived = World.Agents.Count(a => !a.IsDead);

                MessageBox.Show($"Done - {lived} survived!");
            });
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

        public int FoodY { get; set; } = 500;

        public int FoodSize { get; set; } = 50;
    }
}