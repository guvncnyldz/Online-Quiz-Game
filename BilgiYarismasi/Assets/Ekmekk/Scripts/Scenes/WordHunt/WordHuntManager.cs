﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WordHuntManager : MonoBehaviour
{
    private List<Word> words;

    //Burası ayrıca bir sınıfa koyulabilir wordholderpanel için
    private List<WordHolder> wordHolders;
    private Letterboard letterboard;
    private LetterBoardPanel letterBoardPanel;
    private WordHolderPanel wordHolderPanel;
    private EndPanel endPanel;
    private HintSystem hintSystem;
    private Timer timer;

    //Buralar düzenlenecek
    [SerializeField] private GameObject obj_word;

    private int wordCount;
    private int correctWordCount;

    private void Awake()
    {
        //wordCount = Random.Range(3, 10);
        wordCount = 3;
        letterboard = FindObjectOfType<Letterboard>();
        letterBoardPanel = FindObjectOfType<LetterBoardPanel>();
        wordHolderPanel = FindObjectOfType<WordHolderPanel>();
        endPanel = FindObjectOfType<EndPanel>();
        timer = FindObjectOfType<Timer>();
        hintSystem = FindObjectOfType<HintSystem>();

        wordHolders = new List<WordHolder>();
        letterboard.OnButtonClickUp += ControlWord;
        words = new List<Word>();
        timer.OnTimesUp += TimesUp;
    }

    private void Start()
    {
        BeginGetWord();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int hint = hintSystem.GetHintIndex();
            if (hint >= 0)
                letterboard.Hint(hint);
        }
    }

    void BeginGetWord()
    {
        if (WordPool.GetInstance.GetWordCount() < wordCount)
        {
            WordHttp.GetWord(EndGetWord, wordCount - WordPool.GetInstance.GetWordCount());
        }
    }

    void EndGetWord()
    {
        for (int i = 0; i < wordCount; i++)
        {
            Word word = WordPool.GetInstance.GetWord();
            words.Add(word);
        }

        wordHolderPanel.ChangeWordHolderPanel(() => SetWords());
        letterBoardPanel.ChangeLetterPanel(() => LetterSetter.SetLetters(words, letterboard.letterButtons, hintSystem),
            () => { timer.StartCountdown(); });
    }

    void SetWords()
    {
        for (int i = 0; i < words.Count; i++)
        {
            Transform temp = Instantiate(obj_word, wordHolderPanel.transform.GetChild(0)).transform;
            WordHolder tempWordHolder = temp.GetComponent<WordHolder>();
            tempWordHolder.SetWord(words[i].word);
            wordHolders.Add(tempWordHolder);
        }
    }

    void ControlWord(string word)
    {
        foreach (Word targetWord in words)
        {
            if (targetWord.word == word)
            {
                int wordHolderIndex = 0;
                foreach (WordHolder wordHolder in wordHolders)
                {
                    if (wordHolder.word == word)
                    {
                        wordHolder.Mark();
                        break;
                    }

                    wordHolderIndex++;
                }

                wordHolders.RemoveAt(wordHolderIndex);
                hintSystem.HuntWord(targetWord);
                letterboard.EndButtonClickUp(true);
                correctWordCount++;
                if (words.Count > 1)
                {
                    words.Remove(targetWord);
                }
                else
                {
                    EndGame(true);
                }

                return;
            }
        }

        letterboard.EndButtonClickUp(false);
    }

    void TimesUp()
    {
        letterBoardPanel.Fall();

        EndGame(false);
    }

    public void EndGame(bool isWin)
    {
        FindObjectOfType<WordHuntInput>().enabled = false;
        FindObjectOfType<MenuPopup>().gameObject.SetActive(false);

        int earningCoin = 0;

        if (isWin)
        {
            for (int i = 0; i < correctWordCount; i++)
            {
                earningCoin += Random.Range(1, 13 - wordCount);
            }
        }

        ScoreHTTP.SaveScore(correctWordCount, earningCoin, (int) GameMods.wordHunt, isWin ? 1 : 0);
        User.GetInstance().Coin += earningCoin;

        endPanel.SetValues(earningCoin, correctWordCount);
    }
}