using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LetterSetter
{
    public static void SetLetters(List<Word> words, LetterButton[] buttons, HintSystem hintSystem)
    {
        bool isWordSet = false;

        foreach (Word word in words)
        {
            isWordSet = false;
            int tryCount = 0;
            do
            {
                tryCount++;
                if (tryCount >= 2000)
                {
                    SceneManager.LoadScene((int) Scenes.Fail);
                    break;
                }

                int index = Random.Range(0, 100);

                if (!buttons[index].isLetterSet)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        List<CellDirection> directions = new List<CellDirection>()
                        {
                            CellDirection.right,
                            CellDirection.down,
                            CellDirection.rightDown,
                            CellDirection.rightUp
                        };

                        CellDirection randDir = directions[Random.Range(0, directions.Count)];
                        //CellDirection randDir = directions[Random.Range(2, directions.Count)];
                        //CellDirection randDir = directions[2];

                        if (WordHuntGridUtil.IsButtonsEmpty(index, word, randDir, buttons))
                        {
                            hintSystem.AddWord(word, index);
                            WordHuntGridUtil.SetLetter(index, word, randDir, buttons);
                            isWordSet = true;
                            break;
                        }
                        else
                        {
                            directions.Remove(randDir);
                        }
                    }
                }
            } while (!isWordSet);
        }

        SetRandom(buttons);
    }

    static void SetRandom(LetterButton[] buttons)
    {
        string chars = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";

        foreach (LetterButton button in buttons)
        {
            if (!button.isLetterSet)
            {
                char c = chars[Random.Range(0, chars.Length)];
                button.SetLetter(c);
            }
        }
    }
}