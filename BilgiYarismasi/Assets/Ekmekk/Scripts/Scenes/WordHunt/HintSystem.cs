using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HintSystem : MonoBehaviour
{
    private List<HintWord> hintWords;
    private int hintCount = 3;
    private Timer timer;
    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        hintWords = new List<HintWord>();
    }

    public void AddWord(Word word, int index)
    {
        HintWord hintWord = new HintWord();
        hintWord.word = word;
        hintWord.firstIndex = index;

        hintWords.Add(hintWord);
    }

    public void HuntWord(Word word)
    {
        int index = 0;

        foreach (HintWord hintWord in hintWords)
        {
            if (hintWord.word.word == word.word)
            {
                break;
            }

            index++;
        }

        hintWords.RemoveAt(index);
    }

    public int GetHintIndex()
    {
        timer.StopCountdown();
        
        bool flag = false;

        foreach (HintWord hintWord in hintWords)
        {
            if (!hintWord.isHinted)
            {
                flag = true;
                break;
            }
        }

        if (!flag)
            return -1;

        int randomHint = 0;

        do
        {
            randomHint = Random.Range(0, hintWords.Count);
        } while (hintWords[randomHint].isHinted);

        HintWord chosenHintWord = hintWords[randomHint];
        chosenHintWord.isHinted = true;
        timer.StartCountdown();
        return chosenHintWord.firstIndex;
    }
}

public class HintWord
{
    public Word word;
    public int firstIndex;
    public bool isHinted;
}