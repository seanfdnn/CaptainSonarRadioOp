using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonarAi
{
    public class RangeCalculator
    {

        public static IEnumerable<Position> CalculatePointsAdjacent(Position origin, GameBoard board)
        {
            var result = new List<Position>()
            {
                new Position(1,1) + origin,
                new Position(1,0) + origin,
                new Position(1,-1) + origin,
                new Position(0,1) + origin,
                new Position(0,-1) + origin,
                new Position(-1,1) + origin,
                new Position(-1,0) + origin,
                new Position(-1,-1) + origin
            };
            return result;
        }
        public static IEnumerable<Position> CalculatePointsInRange(Position origin, int distance, GameBoard board)
        {
            var result = new List<Position>();
            Position[] ordinals = { Position.North, Position.South, Position.East, Position.West };
            foreach (var vector in ordinals)
            {
                var plottedPosition = origin + vector;
                if (board.IsValidPosition(plottedPosition))
                {
                    result.Add(plottedPosition);
                    if (distance > 1) result.AddRange(CalculatePointsInRange(plottedPosition, distance - 1, board));
                }
            }
            return result.Distinct();
        }

        public static IEnumerable<Position> CalculateSilentRunning(Position origin, GameBoard board)
        {
            var result = new List<Position>();
            Position[] ordinals = { Position.North, Position.South, Position.East, Position.West };
            foreach (var vector in ordinals)
            {
                var cursor = origin;
                for (int i = 0; i < 3; i++)
                {
                    cursor = cursor + vector;
                    if (board.IsValidPosition(cursor)) result.Add(cursor);
                    // also need to check if intersects the previous path of the submarine
                    else break;
                }
            }
            return result;
        }
    }
}
