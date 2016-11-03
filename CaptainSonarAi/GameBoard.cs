using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonarAi
{

    public class GameBoard
    {
        public IList<Position> OutOfBoundsPositions { get; private set; }

        public IEnumerable<Position> GetValidPositions()
        {
            List<Position> validPositions = new List<Position>();
            for (int i = 0; i < 15; i ++)
            {
                for (int j = 0; j < 15; j++)
                {
                    var pos = new Position(i, j);
                    if (IsValidPosition(pos)) validPositions.Add(pos);
                }
            }
            return validPositions;
        }

        public bool IsValidPosition(Position pos)
        {
            if (pos.Col > 14 || pos.Col < 0) return false;
            if (pos.Row > 14 || pos.Row < 0) return false;
            if (OutOfBoundsPositions.Contains(pos)) return false;
            return true;
        }

        public GameBoard(IList<Position> outOfBoundsPosition)
        {
            OutOfBoundsPositions = outOfBoundsPosition;
        }
    }
}
