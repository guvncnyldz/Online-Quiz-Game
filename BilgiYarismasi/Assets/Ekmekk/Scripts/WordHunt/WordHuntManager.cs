using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordHuntManager : MonoBehaviour
{
    private List<Word> words;

    //Burası ayrıca bir sınıfa koyulabilir wordholderpanel için
    private List<WordHolder> wordHolders;
    private Letterboard letterboard;
    private LetterBoardPanel letterBoardPanel;
    private WordHolderPanel wordHolderPanel;
    private WorldHuntEndPanel worldHuntEndPanel;

    //Buralar düzenlenecek
    [SerializeField] private GameObject obj_word;

    private void Awake()
    {
        letterboard = FindObjectOfType<Letterboard>();
        letterBoardPanel = FindObjectOfType<LetterBoardPanel>();
        wordHolderPanel = FindObjectOfType<WordHolderPanel>();
        worldHuntEndPanel = FindObjectOfType<WorldHuntEndPanel>();

        wordHolders = new List<WordHolder>();
        letterboard.OnButtonClickUp += ControlWord;
        words = new List<Word>();
    }

    private void Start()
    {
        words = new List<Word>();
        BeginGetWord();
    }

    void BeginGetWord()
    {
        if (WordPool.GetInstance.GetWordCount() < 5)
        {
            WordPool.GetInstance.NewWord();
        }

        EndGetWord();
    }

    void EndGetWord()
    {
        for (int i = 0; i < 5; i++)
        {
            Word word = WordPool.GetInstance.GetWord();
            words.Add(word);
        }

        wordHolderPanel.ChangeWordHolderPanel(() => SetWords());
        letterBoardPanel.ChangeLetterPanel(() => LetterSetter.SetLetters(words, letterboard.letterButtons), null);
    }

    void SetWords()
    {
        for (int i = 0; i < words.Count; i++)
        {
            Transform temp = Instantiate(obj_word).transform;
            temp.SetParent(wordHolderPanel.transform.GetChild(0), false);
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
                foreach (WordHolder wordHolder in wordHolders)
                {
                    if (wordHolder.word == word)
                        wordHolder.Mark();
                }

                letterboard.EndButtonClickUp(true);

                if (words.Count > 1)
                {
                    words.Remove(targetWord);
                }
                else
                {
                    FindObjectOfType<WordHuntInput>().enabled = false;
                    worldHuntEndPanel.gameObject.SetActive(true);
                }

                return;
            }
        }

        letterboard.EndButtonClickUp(false);
    }
}