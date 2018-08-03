namespace AgentWorld.ViewModel
{
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Shapes;
    using AgentWorld.Engine.Model;
    using AgentWorld.Engine.Runtime;
    using GalaSoft.MvvmLight;

    public class AgentViewModel : ViewModelBase
    {
        private readonly WorldViewModel _world;
        private readonly Agent _agent;

        private int _foodLevel = 2000;
        private Point _location;
        private bool _isDead;

        private static class MemoryLocations
        {
            public const int IsFoodUpInput = 0;
            public const int IsFoodDownInput = 1;
            public const int IsFoodLeftInput = 2;
            public const int IsFoodRightInput = 3;

            public const int MoveUpOutput = 4;
            public const int MoveDownOutput = 5;
            public const int MoveLeftOutput = 6;
            public const int MoveRightOutput = 7;
        }

        public AgentViewModel(WorldViewModel world, Agent agent, Point location)
        {
            Location = location;
            _world = world;
            _agent = agent;
        }

        /// <summary>
        /// Returns true if deleted, false otherwise.
        /// </summary>
        /// <returns></returns>
        public void Cycle()
        {
            //Put the bloody food input sensors in place
            bool isFoodUp = Location.Y < _world.FoodLocation.Bottom;
            bool isFoodDown = Location.Y > _world.FoodLocation.Top;
            bool isFoodLeft = Location.X > _world.FoodLocation.Right;
            bool isFoodRight = Location.X < _world.FoodLocation.Left;

            _agent.Memory[MemoryLocations.IsFoodUpInput] = isFoodUp;
            _agent.Memory[MemoryLocations.IsFoodDownInput] = isFoodDown;
            _agent.Memory[MemoryLocations.IsFoodLeftInput] = isFoodLeft;
            _agent.Memory[MemoryLocations.IsFoodRightInput] = isFoodRight;

            _agent.Cycle();

            double vectorX = 0;
            double vectorY = 0;

            if (_agent.Memory[MemoryLocations.MoveUpOutput])
            {
                vectorY++;
            }

            if (_agent.Memory[MemoryLocations.MoveDownOutput])
            {
                vectorY--;
            }

            if (_agent.Memory[MemoryLocations.MoveLeftOutput])
            {
                vectorX--;
            }

            if (_agent.Memory[MemoryLocations.MoveRightOutput])
            {
                vectorX++;
            }

            if (Move(new Vector(vectorX, vectorY)))
            {
                IsDead = true;
            }

            if (_world.FoodLocation.Contains(Location))
            {
                _foodLevel++;
            }
            else
            {
                _foodLevel--;
            }

            if (_foodLevel < 0)
            {
                IsDead = true;
            }
        }

        private bool Move(Vector vector)
        {
            var newLocation = Location + vector;

            var x = newLocation.X;
            var y = newLocation.Y;

            bool died = false;

            if (x < 0)
            {
                x = 0;
                died = true;
            }
            else if (x > _world.Size.Width)
            {
                x = _world.Size.Width;
                died = true;
            }

            if (y < 0)
            {
                y = 0;
                died = true;
            }
            else if (y > _world.Size.Height)
            {
                y = _world.Size.Height;
                died = true;
            }

            //Set the new location
            Location  = new Point(x, y);

            return died;
        }

        public bool IsDead
        {
            get => _isDead;
            private set
            {
                _isDead = value; 
                RaisePropertyChanged();
            }
        }

        public Point Location
        {
            get => _location;
            set
            {
                _location = value; 
                RaisePropertyChanged();
            }
        }

        public AgentModel GetModel()
        {
            return _agent.GetModel();
        }
    }
}