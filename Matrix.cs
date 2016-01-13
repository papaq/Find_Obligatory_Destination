using System;
using System.Collections.Generic;

namespace ObligatoryDestinationAppL4
{
    internal class Matrix
    {
        private readonly int _n;
        private readonly int[,] _matrix;
        private readonly int _lastElem;
        private readonly Random _rnd;
        

        public Matrix(int n, Random rnd)
        {
            _n = n;
            _rnd = rnd;
            _lastElem = _n * _n;
            _matrix = new int[n, n];
        }

        private void FillDetail(int detail)
        {
            var dots = _lastElem * detail / 100;
            while (dots > 0)
            {
                var elem = _rnd.Next(0, _lastElem);
                if (_matrix[elem%_n, elem/_n] != 0) continue;
                _matrix[elem % _n, elem / _n] = 1;
                dots--;
            }
        }

        public bool HasOblig(int detail)
        {
            FillDetail(detail);

            for (var i = 0; i < _n; i++)
            {
                var col = 0;
                var row = 0;
                for (var j = 0; j < _n; j++)
                {
                    if (_matrix[i, j] == 1)
                        row++;
                    if (_matrix[j, i] == 1)
                        col++;
                }
                if (col == 1 || row == 1)
                    return true;
            }

            return false;
        }
    }
}
