namespace CaptainSonarAi
{
    public abstract class GameEvent
    {
        public Direction Movement { get; set; } = Direction.NONE;
        
    }

    public class MoveEvent : GameEvent
    {
        public MoveEvent(Direction direction)
        {
            Movement = direction;
        }
        public override string ToString()
        {
            return Movement.ToString();
        }
    }


    public class EnemyTorpedo : GameEvent
    {
        public Position TargetPosition { get; set; }
        public EnemyTorpedo (Position targetPosition)
        {
            TargetPosition = targetPosition;
        }
        public override string ToString()
        {
            return "EN TORP DET @ " + TargetPosition;
        }
    }

    public class FriendlyTorpedo : GameEvent
    {
        public Position TargetPosition { get; set; }
        public AttackResult Result { get; set; }

        public override string ToString()
        {
            return "FR TORP/MINE " + Result.ToString()+ " @ " + TargetPosition;
        }
    }

    public class SilentRunning : GameEvent
    {
        public override string ToString()
        {
            return "SILENT RUNNING";

        }
    }

    public class SonarEvent :GameEvent
    {
        public int? Sector { get; set; }
        public int? Column { get; set; }
        public int? Row { get; set;  }

        public SonarEvent(int? sector, char? column, int? oneIndexedRow)
        {
            Sector = sector;
            Row = oneIndexedRow - 1;
            Column = (column == null) ? (int?)null : Position.GetColFromString(column ?? 'Z');
        }

        public override string ToString()
        {
            var sectorString = (Sector == null) ? "*" : Sector.ToString();
            var colString = (Column == null) ? "*" : Position.GetStringFromCol((int)Column);
            var rowString = (Row == null) ? "*" : Row.ToString();
            return $"SONAR {sectorString}{colString}{rowString}" ;
        }
    }

    public class Drone :GameEvent
    {
        public int Sector { get; set; }
        public bool Response { get; set; }
        public override string ToString()
        {
            return "DRONE Sector " + Sector.ToString() + " " + Response.ToString();
        }
    }

    public enum AttackResult
    {
        MISS,
        INDIRECT_HIT,
        DIRECT_HIT
    }
}