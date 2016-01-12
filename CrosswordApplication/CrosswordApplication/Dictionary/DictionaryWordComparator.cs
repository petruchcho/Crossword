using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApplication.Dictionary
{
    public class DictionaryWordComparator : IComparer<DictionaryWord>
    {
        public enum SortDirection
        {
            Ascending,
            Descending
        }

        public enum SortBy
        {
            Alphabet,
            LetterCount
        }

        private readonly SortDirection _sortDirection;
        private readonly SortBy _sortBy;

        public DictionaryWordComparator(SortDirection sortDirection, SortBy sortBy)
        {
            this._sortBy = sortBy;
            this._sortDirection = sortDirection;
        }

        public int Compare(DictionaryWord x, DictionaryWord y)
        {
            int mul = 1;
            if (_sortDirection == SortDirection.Descending)
            {
                mul = -1;
            }

            if (_sortBy == SortBy.LetterCount)
            {
                if (x.Word.Length > y.Word.Length)
                {
                    return mul * 1;
                }
                else if (x.Word.Length < y.Word.Length)
                {
                    return mul * -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return mul * String.Compare(x.Word, y.Word);
            }
        }
    }
}
