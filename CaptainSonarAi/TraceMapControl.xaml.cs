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
    /// Interaction logic for TraceMapControl.xaml
    /// </summary>
    public partial class TraceMapControl : UserControl
    {
        private EventHistory _eventHistory;
        public EventHistory EventHistory
        { get { return _eventHistory; }
          set { _eventHistory = value;
                _eventHistory.Events.CollectionChanged += Events_CollectionChanged;
            }
        }

        private void Events_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MapCanvas.Children.Clear();
            // Origin of 0,0
            var cursor = new Position(250,250);
            foreach (GameEvent ev in _eventHistory.Events)
            {
                // Clear the board if SilentRunning event
                if (ev is SilentRunning)
                    MapCanvas.Children.Clear();

                var vector = Position.NoChange;
                switch (ev.Movement)
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
                vector = vector * 30;
                var newCursor = cursor + vector;
                Line line = new Line();
                line.X1 = cursor.Col;
                line.X2 = newCursor.Col;
                line.Y1 = cursor.Row;
                line.Y2 = newCursor.Row;
                cursor = newCursor;
                line.Stroke = Brushes.Black;
                MapCanvas.Children.Add(line);
                
            }
            scrollViewer.ScrollToRightEnd();
            scrollViewer.ScrollToBottom();
        }

        public TraceMapControl()
        {
            InitializeComponent();
        }
    }
}
