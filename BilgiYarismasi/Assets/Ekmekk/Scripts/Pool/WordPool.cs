using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordPool
{
    private static WordPool instance;

    public static WordPool GetInstance
    {
        get
        {
            if (instance == null)
                instance = new WordPool();

            return instance;
        }
    }

    private Stack<Word> wordPool;

    WordPool()
    {
        wordPool = new Stack<Word>();

        NewWord();
    }

    public Word GetWord()
    {
        Word word = wordPool.Pop();
  
        return word;
    }

    public void AddWord(Word word)
    {
        wordPool.Push(word);
    }

    public int GetWordCount()
    {
        return wordPool.Count;
    }

    public void NewWord()
    {
        Word word = new Word();
        word.word = "EKMEK";
        word.length = 5;
        AddWord(word);
        word = new Word();
        word.word = "YAZILIM";
        word.length = 7;
        AddWord(word);
        word = new Word();
        word.word = "MASKE";
        word.length = 5;
        AddWord(word);
        word = new Word();
        word.word = "SODA";
        word.length = 4;
        AddWord(word);
        word = new Word();
        word.word = "GÖZLÜK";
        word.length = 6;
        AddWord(word);
    }
}
