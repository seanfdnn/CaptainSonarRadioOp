using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonarAi
{
    public struct BranchOutcome
    {
        public bool Pruned { get; set; }
        public Position LastPosition { get; set; }
    }

    public class OptionTree
    {
        private IList<Position> _initialConditions = new List<Position>();
        private readonly GameBoard _board;

        /// <summary>
        /// Constructions a new option tree from all initial conditions of a blank game board
        /// </summary>
        /// <param name="board">A blank game board</param>
        public OptionTree(GameBoard board)
        {
            _board = board;
            _initialConditions = board.GetValidPositions().ToList();
        }

        public Dictionary<Position, bool> Prune(IEnumerable<GameEvent> history)
        {
            return Prune(history, _initialConditions);
        }



        public Dictionary<Position, bool> Prune(IEnumerable<GameEvent> history, IEnumerable<Position> initialConditions)
        {
            var result = new Dictionary<Position, bool>();

            foreach (Position initialCondition in initialConditions)
            {
                var outcome = PruneBranch(initialCondition, history);
                result.Add(initialCondition, outcome);
            }
            return result;
        }

        private bool PruneBranch(Position hypothesizedPosition, IEnumerable<GameEvent> history)
        {
            var reversedEvents = history.Reverse();
            Position cursor = hypothesizedPosition;            
            foreach (GameEvent e in reversedEvents)
            {

                // Trim down by friendly mine/torpedo detonation
                if (e is FriendlyTorpedo)
                {
                    var torpedo = (FriendlyTorpedo)e;
                    bool onTarget = torpedo.TargetPosition.Equals(cursor);
                    var adjacentPositions = RangeCalculator.CalculatePointsAdjacent(torpedo.TargetPosition, _board);
                    bool adjacentToTarget = adjacentPositions.Contains(cursor);
                    bool onOrAdjacentToTarget = onTarget || adjacentToTarget;
                    switch(torpedo.Result)
                    {
                        case AttackResult.DIRECT_HIT:
                            if (!onTarget) return true;
                            break;
                        case AttackResult.INDIRECT_HIT:
                            if (!adjacentToTarget) return true;
                            break;
                        case AttackResult.MISS:
                            if (onOrAdjacentToTarget) return true;
                            break;
                    }
                }

                // Trim down by enemy torpedo detonation
                if (e is EnemyTorpedo)
                {
                    var torpedo = (EnemyTorpedo)e;
                    var inRangePositions = RangeCalculator.CalculatePointsInRange(torpedo.TargetPosition, 4, _board);
                    if (!(inRangePositions.Contains(cursor))) return true;
                }

                // Trim down by sonar events
                if (e is SonarEvent)
                {
                    var sonar = (SonarEvent)e;
                    int numberConditionsMatching = 0;
                    numberConditionsMatching += (cursor.Sector == sonar.Sector) ? 1 : 0;
                    numberConditionsMatching += (cursor.Col == sonar.Column) ? 1 : 0;
                    numberConditionsMatching += (cursor.Row == sonar.Row) ? 1 : 0;
                    if (numberConditionsMatching == 0 || numberConditionsMatching == 2)
                        return true;
                }

                if (e is Drone)
                {
                    var drone = (Drone)e;
                    if ((drone.Response) && (cursor.Sector != drone.Sector)) return true;
                    if ((!drone.Response) && (cursor.Sector == drone.Sector)) return true;
                }


                if (e is MoveEvent)
                {
                    var vector = Position.NoChange;
                    switch (e.Movement)
                    {
                        case Direction.NORTH:
                            vector = Position.North;
                            break;
                        case Direction.SOUTH:
                            vector = Position.South;
                            break;
                        case Direction.EAST:
                            vector = Position.East;
                            break;
                        case Direction.WEST:
                            vector = Position.West;
                            break;
                    }
                    cursor -= vector;
                    if (!_board.IsValidPosition(cursor))
                        return true;
                }

                if (e is SilentRunning)
                {
                    OptionTree recursiveTree = new OptionTree(_board);
                    var possiblePositions = RangeCalculator.CalculateSilentRunning(cursor, _board);
                    var prunedTree = recursiveTree.Prune(history.TakeWhile(p => p != e),possiblePositions);
                    if (prunedTree.Count(p => p.Value == false) == 0) return true; // if there are no viable branches, this can't work
                    // Can't continue to process the rest of the history
                    break; 

                }


            }

            return false ;
        }
    }
}
