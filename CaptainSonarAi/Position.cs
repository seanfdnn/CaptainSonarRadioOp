using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonarAi
{


    public struct Position
    {
        public int Row { get; private set; }
        public int Col { get; private set; }

        public static readonly Position East = new Position(1, 0);
        public static readonly Position West = new Position(-1, 0);
        public static readonly Position North = new Position(0, -1);
        public static readonly Position South = new Position(0, 1);
        public static readonly Position NoChange = new Position(0, 0);

        public int Sector
        {
            get
            {
                int macroRow = Row / 5;
                int macroCol = Col / 5;
                return (macroCol + 1) + (macroRow * 3);
            }
        }

        public Position(string coordinates)
        {
            char colChar = coordinates[0];
            int zeroIndexCol = GetColFromString(colChar) ; //index == 0
            int zeroIndexedRow = int.Parse(coordinates.Substring(1)) - 1;
            Col = zeroIndexCol;
            Row = zeroIndexedRow;
        }

        public static int GetColFromString(char alphaIndex)
        {
            return char.ToUpper(alphaIndex) - 64 - 1;
        }
        public static string GetStringFromCol(int col)
        {
            return ((char)(col + 64 + 1)).ToString();
        }

        public Position(int col, int row)
        {
            Col = col;
            Row = row;
        }

        public override bool Equals(object obj)
        {
            var a = (Position)obj;
            var b = this;
            return (a.Col == b.Col) && (a.Row == b.Row);
        }

        public static Position operator *(Position a, int b)
        {
            return new Position(a.Col * b, a.Row * b);
        }

        public static Position operator +(Position a, Position b)
        {
            return (new Position(a.Col + b.Col, a.Row + b.Row));
        }

        public static Position operator -(Position a, Position b)
        {
            return (new Position(a.Col - b.Col, a.Row - b.Row));
        }

        public Position GoEast()
        {
            return this + East;
        }

        public Position GoWest()
        {
            return this + West;
        }

        public Position GoNorth()
        {
            return this + North;
        }

        public Position GoSouth()
        {
            return this + South;
        }

        public override string ToString()
        {
            return GetStringFromCol(Col) + (Row+1).ToString();
        }

        public override int GetHashCode()
        {
            return Row * 100 + Col;
        }
    }
}
