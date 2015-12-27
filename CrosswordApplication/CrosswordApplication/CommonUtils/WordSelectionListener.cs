using System;
using CrosswordApplication.Crossword;

namespace CrosswordApplication.CommonUtils
{
    class WordSelectionListener
    {
        private readonly Action<CrosswordWord> _wordSelectionAction;

        public WordSelectionListener(Action<CrosswordWord> wordSelectionAction)
        {
            this._wordSelectionAction = wordSelectionAction;
        }

        public void OnWordSelected(CrosswordWord word)
        {
            if (_wordSelectionAction != null)
            {
                _wordSelectionAction.Invoke(word);
            }
        }
    }
}
