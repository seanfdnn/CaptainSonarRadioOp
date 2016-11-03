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
    /// Interaction logic for GameBoardControl.xaml
    /// </summary>
    public partial class GameBoardControl : UserControl
    {
        private readonly Dictionary<Position, Shape> _dotLookup = new Dictionary<Position, Shape>();

        private GameBoard _gameBoard;
        public GameBoard GameBoard
        {
            get { return _gameBoard; }
            set {
                _gameBoard = value;
                RegenerateBoard();
            }
        }


        public GameBoardControl()
        {
            InitializeComponent();
            
            for (int i = 0; i < 16; i++)
                MasterGrid.RowDefinitions.Add(new RowDefinition());

            for (int j = 0; j < 16; j++)
                MasterGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 1; i < 16; i++)
            {
                var label = new Label();
                label.Content = i.ToString();
                MasterGrid.Children.Add(label);
                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
            }

            char c = 'A';
            for (int j = 1; j <16; j++)
            {
                var label = new Label();
                label.Content = c.ToString();
                MasterGrid.Children.Add(label);
                label.HorizontalAlignment = HorizontalAlignment.Center;
                c++;
                Grid.SetColumn(label, j);
                Grid.SetRow(label, 0);
            }


          
        }

        internal void Update(Dictionary<Position, bool> pruningResult)
        {
            ResetDots();
            foreach(var kv in pruningResult.Where(p => p.Value))
            {
                if (_dotLookup.ContainsKey(kv.Key))
                    {
                    var shape = _dotLookup[kv.Key];
                    shape.Fill = Brushes.Transparent;
                }
            }
        }

        private void ResetDots()
        {
            foreach (var dot in _dotLookup)
            {
                dot.Value.Fill = Brushes.Green;
            }
        }

        private void RegenerateBoard()
        {
            _dotLookup.Clear();
            for (int i = 1; i < 16; i++)
            {
                for (int j = 1; j < 16; j++)
                {
                    var pos = new Position(j - 1, i - 1);
                    if (_gameBoard.IsValidPosition(pos))
                    {
                        var marker = new Ellipse();
                        marker.Fill = Brushes.Green;
                        marker.Stroke = Brushes.White;
                        marker.StrokeThickness = 2;
                        marker.Width = 15;
                        marker.Height = 15;
                        MasterGrid.Children.Add(marker);
                        _dotLookup.Add(pos, marker);
                        Grid.SetRow(marker, i);
                        Grid.SetColumn(marker, j);
                    }
                }
            }
        }
    }

    public class BorderGrid : Grid
    {
        protected override void OnRender(DrawingContext dc)
        {
            double leftOffset = 0;
            double topOffset = 0;
            Pen pen = new Pen(Brushes.GhostWhite, 2);
            pen.Freeze();

            foreach (RowDefinition row in this.RowDefinitions)
            {
                dc.DrawLine(pen, new Point(0, topOffset), new Point(this.ActualWidth, topOffset));
                topOffset += row.ActualHeight;
            }
            // draw last line at the bottom
            dc.DrawLine(pen, new Point(0, topOffset), new Point(this.ActualWidth, topOffset));

            foreach (ColumnDefinition column in this.ColumnDefinitions)
            {
              dc.DrawLine(pen, new Point(leftOffset, 0), new Point(leftOffset, this.ActualHeight));
              leftOffset += column.ActualWidth;
            }
            // draw last line on the right
            dc.DrawLine(pen, new Point(leftOffset, 0), new Point(leftOffset, this.ActualHeight));

            base.OnRender(dc);
        }
    }
}
