using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaptainSonarAi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EventHistory _history;
        private OptionTree _tree;
       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                _history.Move(Direction.WEST);
            else if (e.Key == Key.Up)
                _history.Move(Direction.NORTH);
            else if (e.Key == Key.Right)
                _history.Move(Direction.EAST);
            else if (e.Key == Key.Down)
                _history.Move(Direction.SOUTH);
            else if (e.Key == Key.Back)
                _history.RemoveLastEvent();
            else if (e.Key == Key.S)
                AddSonar();
            else if (e.Key == Key.D)
                AddDrone();
            else if (e.Key == Key.G)
                _history.SilentRunning();
            else if (e.Key == Key.T)
                AddFriendlyTorpedoOrMine();
            else if (e.Key == Key.I)
                AddEnemyTorpedo();


            var pruningResult = _tree.Prune(_history.Events);
            gameBoard.Update(pruningResult);
        }

        private void AddEnemyTorpedo()
        {
            var dlg = new AddEnemyTorpedoDialog();
            dlg.Owner = this;
            if (dlg.ShowDialog() ?? false)
            {
                char? nullableCol = (dlg.txtCol.Text.Length == 0) ? (char?)null : dlg.txtCol.Text[0];
                int? nullableRow = ParseNullableInt(dlg.txtRow.Text);
                _history.EnemyTorpedo((char)nullableCol, (int)nullableRow);
            }
        }

        private void AddFriendlyTorpedoOrMine()
        {
            var dlg = new AddFriendlyMineTriggeredDialog();
            dlg.Owner = this;
            if (dlg.ShowDialog() ?? false)
            {
                char? nullableCol = (dlg.txtCol.Text.Length == 0) ? (char?)null : dlg.txtCol.Text[0];
                int? nullableRow = ParseNullableInt(dlg.txtRow.Text);
                AttackResult result = AttackResult.MISS;
                switch (dlg.cmbOutcome.Text)
                {
                    case "Miss":
                        result = AttackResult.MISS;
                        break;
                    case "Indirect Hit":
                        result = AttackResult.INDIRECT_HIT;
                        break;
                    case "Direct Hit":
                        result = AttackResult.DIRECT_HIT;
                        break;
                }
                _history.FriendlyMineOrTorpedoDetonated((char)nullableCol, (int)nullableRow, result);
            }

        }

        private void AddSonar()
        {
            AddSonarDialog dlg = new AddSonarDialog();
            dlg.Owner = this;
            if (dlg.ShowDialog()?? false)
            {
                int? nullableSect = ParseNullableInt(dlg.txtSector.Text);
                char? nullableCol = (dlg.txtCol.Text.Length == 0) ? (char?)null : dlg.txtCol.Text[0];
                int? nullableRow = ParseNullableInt(dlg.txtRow.Text);
                _history.Sonar(nullableSect, nullableCol, nullableRow);
            }
        }

        private void AddDrone()
        {
            AddDroneDialog dlg = new AddDroneDialog();
            dlg.Owner = this;
            if (dlg.ShowDialog() ?? false)
            {
                int? nullableSect = ParseNullableInt(dlg.txtSector.Text);
                bool? isPresent = (dlg.chkPresent.IsChecked);
                if (nullableSect != null && isPresent != null)
                    _history.Drone((int)nullableSect, (bool)isPresent);
            }

        }

        private int? ParseNullableInt(string text)
        {
            int value;
            bool canParseValue = int.TryParse(text, out value);
            return canParseValue ? value : (int?)null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dlg = new SelectMapDialog();
            dlg.ShowDialog();
            var board = BoardFactory.CreateBoard((string)dlg.cmbMap.SelectedValue);
            gameBoard.GameBoard = board;
            _history = new EventHistory();
            traceMap.EventHistory = _history;
            History.ItemsSource = _history.Events;
            _tree = new OptionTree(board);
        }
    }
}
