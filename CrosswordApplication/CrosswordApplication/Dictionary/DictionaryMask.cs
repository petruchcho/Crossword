namespace CrosswordApplication.Dictionary
{
    public class DictionaryMask
    {
        private string mask;

        public DictionaryMask(string mask)
        {
            this.mask = mask;
        }

        public bool IsMatch(DictionaryWord dictionaryWord)
        {
            string word = dictionaryWord.Word;
            if (word.Length != mask.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < mask.Length; i++)
                {
                    if ((mask[i] != '*') && (mask[i] != word[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
