namespace AgentWorld.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using AgentWorld.Engine.Runtime;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Threading;

    public class WorldViewModel : ViewModelBase
    {
        private ObservableCollection<AgentViewModel> _agents;
       
        public WorldViewModel(IEnumerable<Agent> agents, Size size, Rect foodLocation)
        {
            Random random = new Random();

            _agents = new ObservableCollection<AgentViewModel>(agents.Select(a =>
            {
                //var agentX = random.NextDouble() * size.Width;
                //var agentY = random.NextDouble() * size.Height;

                var agentX = 100;
                var agentY = 100;

                var agentLocation = new Point(agentX, agentY);

                return new AgentViewModel(this, a, agentLocation);
            }));

            Size = size;
            FoodLocation = foodLocation;
        }

        /// <summary>
        /// The bounds of the world
        /// </summary>
        public Size Size { get; }

        public Rect FoodLocation { get; }

        public ObservableCollection<AgentViewModel> Agents
        {
            get => _agents;
            set
            {
                _agents = value;
                RaisePropertyChanged();
            }
        }

        public void Cycle()
        {
            //Parallel.ForEach(Agents, a =>
            //{
            //    if (!a.IsDead)
            //    {
            //        a.Cycle();
            //    }
            //});

            foreach (var agent in Agents.ToArray())
            {
                if (!agent.IsDead)
                {
                    agent.Cycle();
                }
            }
        }
    }
}