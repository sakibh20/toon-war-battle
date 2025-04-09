using UnityEngine;

namespace skb_sec._Project.Scripts.Economy
{
    public static class ShortCount
    {
        public static string Shorten(int score)
        {
            float scor = score;
            string result;
            string[] ScoreNames = new string[] { "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", };
            int i;
 
            for (i = 0; i < ScoreNames.Length; i++)
                if (scor < 900)
                    break;
                else scor = Mathf.Floor(scor / 100f) / 10f;
 
            if (scor == Mathf.Floor(scor))
                result = scor.ToString() + ScoreNames[i];
            else result = scor.ToString("F1") + ScoreNames[i];
            return result;
        }
    }
}
