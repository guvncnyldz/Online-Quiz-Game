using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WordHuntManager : MonoBehaviour
{
    private List<Word> words;
    [SerializeField] private Button btn_hint;
    private TextMeshProUGUI txt_hint;

    private List<WordHolder> wordHolders;
    private Letterboard letterboard;
    private LetterBoardPanel letterBoardPanel;
    private WordHolderPanel wordHolderPanel;
    private EndPanel endPanel;
    private HintSystem hintSystem;
    private Timer timer;

    //Buralar düzenlenecek
    [SerializeField] private GameObject obj_word;
    [SerializeField] private EarningData earningData;

    private int wordCount;
    private int correctWordCount;

    private void Awake()
    {
        letterboard = FindObjectOfType<Letterboard>();
        letterBoardPanel = FindObjectOfType<LetterBoardPanel>();
        wordHolderPanel = FindObjectOfType<WordHolderPanel>();
        endPanel = FindObjectOfType<EndPanel>();
        timer = FindObjectOfType<Timer>();
        hintSystem = FindObjectOfType<HintSystem>();

        wordCount = Random.Range(3, 10);
        timer.SetTime(wordCount * 18f);

        wordHolders = new List<WordHolder>();
        letterboard.OnButtonClickUp += ControlWord;
        words = new List<Word>();
        timer.OnTimesUp += TimesUp;

        txt_hint = btn_hint.GetComponentInChildren<TextMeshProUGUI>();
        txt_hint.text = hintSystem.hintCount.ToString();
        btn_hint.enabled = false;

        btn_hint.onClick.AddListener(() =>
        {
            HintRewardedAd.instance.OnEarnReward = () =>
            {
                int hint = hintSystem.GetHintIndex();
                if (hint >= 0)
                    letterboard.Hint(hint);

                txt_hint.text = hintSystem.hintCount.ToString();

                if (hintSystem.hintCount == 0)
                {
                    btn_hint.enabled = false;
                }
            };
            
            HintRewardedAd.instance.UserChoseToWatchAd();
        });
    }

    private void Start()
    {
        BeginGetWord();
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
            () =>
            {
                btn_hint.enabled = true;
                timer.StartCountdown();
            });
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

    void ControlWord(string word, int firstIndex)
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
                hintSystem.HuntWord(targetWord, firstIndex);
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

        timer.StopCountdown();
        btn_hint.enabled = false;

        int earningCoin = 0;

        if (isWin)
        {
            for (int i = 0; i < correctWordCount; i++)
            {
                earningCoin += Random.Range(earningData.minCostPerWord, earningData.maxCostPerWord);
            }
        }

        ScoreHTTP.SaveScore(correctWordCount, earningCoin, (int) GameMods.wordHunt, isWin ? 1 : 0);
        User.GetInstance().Coin += earningCoin;

        endPanel.SetValues(earningCoin, correctWordCount, isWin);
    }
}