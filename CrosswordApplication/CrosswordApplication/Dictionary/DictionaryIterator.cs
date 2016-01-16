namespace CrosswordApplication.Dictionary
{
    public class DictionaryIterator
    {
        private DictionaryMask mask;
        private DictionaryWord[] dictionaryWords;
        private int length;
        private int iteration;

        public DictionaryIterator(DictionaryMask mask, DictionaryWord[] dictionaryWords)
        {
            this.mask = mask;
            this.dictionaryWords = new DictionaryWord[0];
            this.dictionaryWords = dictionaryWords;
            length = this.dictionaryWords.Length;
            iteration = 0;
        }

        public DictionaryWord Next()
        {
            iteration ++;
            return dictionaryWords[iteration - 1];
        }

        public bool HasNext()
        {
            while (iteration < length)
            {
                if (mask.IsMatch(dictionaryWords[iteration]))
                {
                    return true;
                }
                else
                {
                    iteration++;
                }
            }
            return false;
        }
    }
}
