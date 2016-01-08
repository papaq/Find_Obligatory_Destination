using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ObligatoryDestinationAppL4
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph
    {
        private readonly PointCollection[] _vectOfPoints = new PointCollection[5];
        private readonly Polyline[] _polylines = new Polyline[5];
        private int[] _depthes = new int[5] { 10, 15, 20, 25, 30 };

        private readonly string[] _nameOfpoly = new string[5]
        {
            "poly10",
            "poly15",
            "poly20",
            "poly25",
            "poly30",
        };

        private readonly SolidColorBrush[] _brushes = new SolidColorBrush[6]
        {
            Brushes.Aqua,
            Brushes.BlueViolet,
            Brushes.Chartreuse,
            Brushes.DarkOrange,
            Brushes.DeepPink,
            Brushes.LavenderBlush,
        };

        public Graph()
        {
            InitializeComponent();
            BuildBothAxes();
            InitPointColl();
            InitPolyline();
            FillPointColls();
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

        private void FillPointColls()
        {
            for (var i = 0; i < _vectOfPoints.Length; i++)
            {
                var matr = new Matrix(_depthes[i]);
                for (var j = 1; j < 101; j++)
                {
                    _vectOfPoints[i].Add(new Point(j, matr.FindOblig()));
                }
                _vectOfPoints[i] = ConvertPointColl(_vectOfPoints[i]);
            }
        }

        private void ShowLine(int num)
        {
            if (CanGraph.Children.Contains(_polylines[num]))
                CanGraph.Children.Remove(_polylines[num]);
            else
                CanGraph.Children.Add(_polylines[num]);
        }

        private PointCollection ConvertPointColl(PointCollection pointCollection)
        {
            var maxY = pointCollection[0].Y;
            maxY = pointCollection.Select(point => point.Y).Concat(new[] { maxY }).Max();
            var maxX = pointCollection[0].X;
            maxX = pointCollection.Select(point => point.X).Concat(new[] { maxX }).Max();

            var scaleX = CanGraph.Width / maxX;
            var scaleY = CanGraph.Height / maxY;

            for (var i = 0; i < pointCollection.Count; i++)
                pointCollection[i] = new Point(pointCollection[i].X * scaleX, CanGraph.Height - pointCollection[i].Y * scaleY);

            return pointCollection;
        }

        private void BuildBothAxes()
        {
            const double margin = 10;
            const double xmin = margin;
            var ymax = CanGraph.Height - margin;
            const double step = 10;

            // Make the X axis.
            var xaxisGeom = new GeometryGroup();
            xaxisGeom.Children.Add(new LineGeometry(
                        new Point(0, ymax), new Point(CanGraph.Width, ymax)));
            for (var x = xmin + step; x <= CanGraph.Width - step; x += step)
            {
                xaxisGeom.Children.Add(new LineGeometry(
                    new Point(x, ymax - margin / 2),
                    new Point(x, ymax + margin / 2)));
            }

            var xaxisPath = new Path
            {
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Data = xaxisGeom
            };

            CanGraph.Children.Add(xaxisPath);

            // Make the Y ayis.
            var yaxisGeom = new GeometryGroup();
            yaxisGeom.Children.Add(new LineGeometry(
                            new Point(xmin, 0), new Point(xmin, CanGraph.Height)));
            for (var y = step; y <= CanGraph.Height - step; y += step)
            {
                yaxisGeom.Children.Add(new LineGeometry(
                    new Point(xmin - margin / 2, y),
                    new Point(xmin + margin / 2, y)));
            }

            var yaxisPath = new Path
            {
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Data = yaxisGeom
            };

            CanGraph.Children.Add(yaxisPath);
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
