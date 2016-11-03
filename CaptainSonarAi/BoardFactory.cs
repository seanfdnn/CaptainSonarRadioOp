using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonarAi
{
    public class BoardFactory
    {
        private static Dictionary<string, string[]> mapDefinitions = new Dictionary<string, string[]>()
            {
                {"Alpha",new string[]{ "C2 ", "C3",
                "G2", "I3", "I4", "M2", "M3", "N2",
                "B7", "B8", "D7", "D8", "D9", "G7", "G8",
                "H9", "I7", "L9", "M9", "N9",
                "A13", "C12", "C11", "C14", "D15", "G14", "H12",
                "I14", "L12", "M13", "N14"}
                },
                {"Bravo",new string[]{"C2", "C3",
                    "I3", "I4", "M2", "N2", "D8", "D9", "L9", "M9", "N9",
                    "C12", "D11", "H12", "I14", "L12" }
                },
            {"Charlie", new string[] {"F3", "F4", "L4", "M3", "D8", "I8", "I9", "I10", "H10",
                "D13", "E14"} },
            };

        public static IEnumerable<string> GetAvaliableMaps()
        {
            return mapDefinitions.Keys;
        }

        public static GameBoard CreateBoard(string key)
        {
            return new GameBoard(mapDefinitions[key].Select(p => new Position(p)).ToList());
        }

        

    }
}
