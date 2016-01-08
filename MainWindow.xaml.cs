using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ObligatoryDestinationAppL4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly PointCollection[] _vectOfPoints = new PointCollection[5];
        private readonly Polyline[] _polylines = new Polyline[5];
        private readonly int[] _depthes = new int[] { 10, 15, 20, 25, 30 };

        private readonly string[] _nameOfpoly = new string[]
        {
            "poly10",
            "poly15",
            "poly20",
            "poly25",
            "poly30",
        };

        private readonly SolidColorBrush[] _brushes = new SolidColorBrush[]
        {
            Brushes.ForestGreen,
            Brushes.BlueViolet,
            Brushes.Blue,
            Brushes.DarkOrange,
            Brushes.DeepPink,
            Brushes.LavenderBlush,
        };

        public MainWindow()
        {
            InitializeComponent();
            //BuildBothAxes();
            InitPointColl();
            InitPolyline();
            //FillPointColls();
        }

        private void InitPointColl()
        {
            for (var i = 0; i < _vectOfPoints.Length; i++)
                _vectOfPoints[i] = new PointCollection();
        }

        private void InitPolyline()
        {
            for (var i = 0; i < _polylines.Length; i++)
            {
                _polylines[i] = new Polyline()
                {
                    StrokeThickness = 1,
                    Stroke = _brushes[i],
                    Points = _vectOfPoints[i],
                    Name = _nameOfpoly[i],
                };
                CanGraph.RegisterName(_nameOfpoly[i], CanGraph);
            }
        }

        private void FillPointColls(int i)
        {
            var matr = new Matrix(_depthes[i]);
            for (var j = 1; j < 101; j++)
            {
                _vectOfPoints[i].Add(new Point(j, matr.FindOblig()));
            }
            _vectOfPoints[i] = ConvertPointColl(_vectOfPoints[i]);
        }

        private void ShowLine(int num)
        {
            if (CanGraph.Children.Contains(_polylines[num]))
            {
                CanGraph.Children.Remove(_polylines[num]);
                _vectOfPoints[num].Clear();
            }
            else
            {
                FillPointColls(num);
                CanGraph.Children.Add(_polylines[num]);
            }
        }

        private PointCollection ConvertPointColl(PointCollection pointCollection)
        {
            var maxY = pointCollection[0].Y;
            maxY = pointCollection.Select(point => point.Y).Concat(new[] { maxY }).Max();
            var maxX = pointCollection[0].X;
            maxX = pointCollection.Select(point => point.X).Concat(new[] { maxX }).Max();

            var scaleX = CanGraph.Width / maxX;
            var scaleY = (CanGraph.Height - 50) / maxY;

            for (var i = 0; i < pointCollection.Count; i++)
                pointCollection[i] = new Point(pointCollection[i].X * scaleX + 10, - 15 + CanGraph.Height - pointCollection[i].Y * scaleY);

            return pointCollection;
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            ShowLine(0);
            SetButonColor(0);
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            ShowLine(1);
            SetButonColor(1);
        }

        private void button20_Click(object sender, RoutedEventArgs e)
        {
            ShowLine(2);
            SetButonColor(2);
        }

        private void button25_Click(object sender, RoutedEventArgs e)
        {
            ShowLine(3);
            SetButonColor(3);
        }

        private void button30_Click(object sender, RoutedEventArgs e)
        {
            ShowLine(4);
            SetButonColor(4);
        }

        private void SetButonColor(int buttonColor)
        {
            switch (buttonColor)
            {
                case 0:
                    button10.Background = (button10.Background.ToString() != _brushes[5].ToString())
                        ? _brushes[5]
                        : _brushes[buttonColor];
                    break;
                case 1:
                    button15.Background = (button15.Background.ToString() != _brushes[5].ToString())
                        ? _brushes[5]
                        : _brushes[buttonColor];
                    break;
                case 2:
                    button20.Background = (button20.Background.ToString() != _brushes[5].ToString())
                        ? _brushes[5]
                        : _brushes[buttonColor];
                    break;
                case 3:
                    button25.Background = (button25.Background.ToString() != _brushes[5].ToString())
                        ? _brushes[5]
                        : _brushes[buttonColor];
                    break;
                default:
                    button30.Background = (button30.Background.ToString() != _brushes[5].ToString())
                        ? _brushes[5]
                        : _brushes[buttonColor];
                    break;

            }

        }
    }
}
