using System;

namespace ObligatoryDestinationAppL4
{
    internal class Matrix
    {
        private readonly int _n;
        //private readonly int _detail;
        private readonly int[,] _matrix;
        Random _rnd = new Random();

        public Matrix(int n)
        {
            _n = n;
            //_detail = detail;
            _matrix = new int[n, n];
        }

        private void FillOnePercent()
        {
            var lastElem = _n * _n;
            var dots = lastElem / 100;
            while (dots > 0)
            {
                var elem = _rnd.Next(0, lastElem);
                if (_matrix[elem % _n, elem / _n] == 0)
                {
                    _matrix[elem % _n, elem / _n] = 1;
                    dots--;
                }
            }
        }

        public int FindOblig()
        {
            FillOnePercent();
            //FillMatrix();
            var obligs = 0;

            for (var i = 0; i < _n; i++)
            {
                var wannie = OnlyOneRow(i);

                // Theo 2
                if (wannie < -1 && CheckTheo2InRow(i))
                    obligs++;

                // Theo 1
                else
                    if (wannie > -1 && (OnlyOneCol(wannie) == i || CheckTheo2InCol(wannie, i)))
                    obligs++;

            }

            return obligs;
        }

        private int OnlyOneRow(int row)
        {
            var oneIsHere = -1;
            var numOfOnes = -1;

            for (var i = 0; i < _n; i++)
                if (_matrix[row, i] == 1)
                {
                    if (oneIsHere != -1)
                        numOfOnes--;
                    oneIsHere = i;
                }

            return numOfOnes < -1 ? numOfOnes : oneIsHere;
        }

        private int OnlyOneCol(int col)
        {
            var oneIsHere = -1;
            var numOfOnes = -1;

            for (var i = 0; i < _n; i++)
                if (_matrix[i, col] == 1)
                {
                    if (oneIsHere != -1)
                        numOfOnes--;
                    oneIsHere = i;
                }

            return numOfOnes < -1 ? numOfOnes : oneIsHere;
        }

        private bool CheckTheo2InRow(int row)
        {
            for (var i = 0; i < _n; i++)
                if (_matrix[row, i] == 1 && OnlyOneCol(i) < -1)
                    return false;
            return true;
        }

        private bool CheckTheo2InCol(int col, int row)
        {
            for (var i = 0; i < _n; i++)
            {
                if (_matrix[col, i] == 1 && (i < row || OnlyOneRow(row) < 0))
                    return false;
            }

            return true;
        }
    }
}
