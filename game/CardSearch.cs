using System.Collections.Generic;
using System;

public class CardSearch
{
    public static List<int> search(string pattern, string text)
    {
        int numOfPossibleChars = 256;
        int primeNum = 353;
        int patternLength = pattern.Length;
        int textLength = text.Length;
        int hashValue4Pattern = 0;
        int hashValue4CurrentText = 0;
        int q = 1;
        List<int> answer = new List<int>();
        for (int iterator = 0; iterator < patternLength -1; iterator++)
        {
            q = (q * numOfPossibleChars) % primeNum;
        }
        for (int iterator = 0; iterator < patternLength; iterator++)
        {
            hashValue4Pattern = (numOfPossibleChars * hashValue4Pattern + pattern[iterator]) % primeNum;
            hashValue4CurrentText = (numOfPossibleChars * hashValue4CurrentText + text[iterator]) % primeNum;
        }
        for (int index = 0; index <= textLength - patternLength; index++)
        {
            if (hashValue4Pattern == hashValue4CurrentText)
            {
                bool match = true;
                for (int x = 0; x < patternLength; x++)
                {
                    if (text[index + x] != pattern[x])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    answer.Add(index);
                }
            }
            if (index < textLength - patternLength)
            {
                hashValue4CurrentText = (numOfPossibleChars * (hashValue4CurrentText - text[index] * q) + text[index + patternLength]) % primeNum;
                if (hashValue4CurrentText < 0)
                {
                    hashValue4CurrentText += primeNum;
                }
            }
        }
        return answer;
    }
    public CardSearch()
    {
        string text = "Thisisatestfortheissytime";
        string pattern = "is";
        List<int> result = search(pattern, text);
        Console.WriteLine(string.Join(" ", result));
    }
}
