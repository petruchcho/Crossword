namespace CrosswordApplication.Dictionary
{
    public class DictionaryMask
    {
        private string _mask;

        public DictionaryMask(string mask)
        {
            _mask = mask;
            while ((_mask.IndexOf("**") > -1) || (_mask.IndexOf("?*") > -1) || (_mask.IndexOf("*?") > -1))
            {
                _mask = _mask.Replace("**", "*");
                _mask = _mask.Replace("?*", "*");
                _mask = _mask.Replace("*?", "*");
            }

            string news = _mask;
        }

        public bool IsMatch(DictionaryWord dictionaryWord)
        {
            string word = dictionaryWord.Word.ToLower();
            return _IsMatch(_mask, word);
        }

        private bool _IsMatch(string mask, string word)
        {
            if (mask[0] == '?')
            {
                if (mask.Length > 1)
                {
                    return _IsMatch(mask.Remove(0, 1), word.Remove(0, 1));
                }
                else
                {
                    return word.Length == 1;
                }
            }
            else if (mask[0] == '*')
            {
                if ((mask.Length == 1) && (word.Length > 0))
                {
                    return true;
                }
                else
                {
                    if (word.Length == 0)
                    {
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (_IsMatch(mask.Remove(0, 1), word.Remove(0, i + 1)))
                            {
                                return true;
                            }
                        }

                        return false;
                    }
                }
            }
            else if ((word.Length > 0) && (mask[0] == word[0]))
            {
                if (mask.Length > 1)
                {
                    return _IsMatch(mask.Remove(0, 1), word.Remove(0, 1));
                }
                else
                {
                    if (word.Length > 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
            }
            else
            {
                return false;
            }
        }
    }
}
