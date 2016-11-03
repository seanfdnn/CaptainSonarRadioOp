using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonarAi
{
    public class EventHistory
    {
        private readonly ObservableCollection<GameEvent> _events;

        public ObservableCollection<GameEvent> Events { get { return _events; } }

        public EventHistory()
        {
            _events = new ObservableCollection<GameEvent>();
        }

        public void RemoveLastEvent()
        {
            if (_events.Count > 0)
                _events.Remove(_events.Last());
        }

        public void Sonar(int? sector, char? column, int? row)
        {
            _events.Add(new SonarEvent(sector,column,row));
        }

        public void Move(Direction direction)
        {
            _events.Add(new MoveEvent(direction));
        }

        internal void Drone(int sector, bool isPresent)
        {
            _events.Add(new Drone { Sector = sector, Response = isPresent });
        }

        internal void SilentRunning()
        {
            _events.Add(new SilentRunning());
        }
        
        internal void EnemyTorpedo(char col, int row)
        {
            _events.Add(new EnemyTorpedo(new Position(Position.GetColFromString(col), row - 1)));
        }

        internal void FriendlyMineOrTorpedoDetonated(char nullableCol, int nullableRow, AttackResult result)
        {
            _events.Add(new FriendlyTorpedo { TargetPosition = new Position(Position.GetColFromString(nullableCol), nullableRow - 1) , Result = result });

        }
    }

    public enum Direction
    {
        EAST,
        NORTH,
        SOUTH,
        WEST,
        NONE
    }
}
