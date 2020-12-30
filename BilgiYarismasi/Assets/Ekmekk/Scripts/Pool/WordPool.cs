using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
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
    }

    public Word GetWord()
    {
        Word word = wordPool.Pop();

        return word;
    }

    public void AddWord(JArray jWords)
    {
        foreach (JToken jWord in jWords)
        {
            Word word = new Word();
            word.Set(jWord);
            wordPool.Push(word);
        }
    }

    public int GetWordCount()
    {
        return wordPool.Count;
    }
}