using System;

public class Solution
{
    public int MaxVowels(string s, int k)
    {
        int currentVowelCount = 0;

        for (int i = 0; i < k; i++)
        {
            if (IsVowel(s[i]))
                currentVowelCount++;
        }

        int max = currentVowelCount;

        for (int i = k; i < s.Length; i++)
        {
            if (IsVowel(s[i]))
                currentVowelCount++;
            if (IsVowel(s[i - k]))
                currentVowelCount--;

            max = Math.Max(max, currentVowelCount);
            if (max == k)
                return k;
        }

        return max;
    }

    private static bool IsVowel(char letter)
        => letter is 'a' or 'e' or 'i' or 'o' or 'u';
}
