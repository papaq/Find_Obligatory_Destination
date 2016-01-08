using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoryDestinationAppL4
{
    internal class Matrix
    {
        private readonly int _n;
        private readonly int _detail;
        private readonly int[,] _matrix;

        public Matrix(int n, int detail)
        {
            _n = n;
            _detail = detail;
            _matrix = new int[n, n];
        }

        private void FillMatrix()
        {
            var rnd = new Random();
            var lastElem = _n*_n;
            var dots = lastElem * _detail / 100;
            if (lastElem / dots > 2)
            {
                while (dots > 0)
                {
                    var elem = rnd.Next(0, lastElem);
                    _matrix[elem % _n, elem / _n] = 1;
                    dots--;
                }
            }
            else
            {
                FillWith(1);
                while (dots < lastElem)
                {
                    var elem = rnd.Next(0, lastElem);
                    _matrix[elem % _n, elem / _n] = 0;
                    dots++;
                }
            }
        }

        private void FillWith(int with)
        {
            for (var i = 0; i < _n; i++)
                for (var j = 0; j < _n; j++)
                    _matrix[i, j] = with;
        }

        private int FindOblig()
        {
            FillMatrix();
            var obligs = 0;

            for (var i = 0; i < _n; i++)
            {
                var wannie = OnlyOneRow(i);

                // Theo 2
                if (wannie < -1 && CheckTheo2(i))
                    obligs++;

                // Theo 1
                else
                    if (wannie > -1 && OnlyOneCol(wannie) == i)
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

        private bool CheckTheo2(int row)
        {
            for (var i = 0; i < row; i++)
                if (_matrix[row, i] == 1 && OnlyOneCol(i) < -1)
                    return false;
            return true;
        }
    }
}
