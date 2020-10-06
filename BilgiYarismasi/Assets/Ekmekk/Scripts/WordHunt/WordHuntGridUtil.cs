using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WordHuntGridUtil
{
    public static bool IsButtonsSidebySide(int index, int lastIndex, ref CellDirection currentDir)
    {
        int lastRow = Mathf.FloorToInt(lastIndex / 10);
        int currentRow = Mathf.FloorToInt(lastIndex / 10);

        if (lastIndex + 10 == index)
        {
            currentDir = CellDirection.down;
            return true;
        }

        if (lastIndex - 10 == index)
        {
            currentDir = CellDirection.down;
            return true;
        }

        if (lastRow == currentRow)
        {
            if (lastIndex + 1 == index)
            {
                currentDir = CellDirection.right;
                return true;
            }

            if (lastIndex - 1 == index)
            {
                currentDir = CellDirection.left;
                return true;
            }
        }

        return false;
    }

    public static bool IsButtonSameDir(int index, int lastIndex, CellDirection currentDir)
    {
        int lastRow = Mathf.FloorToInt(lastIndex / 10);
        int currentRow = Mathf.FloorToInt(index / 10);

        if (currentDir == CellDirection.down && lastIndex + 10 == index)
        {
            return true;
        }

        if (currentDir == CellDirection.down && lastIndex - 10 == index)
        {
            return true;
        }

        if (lastRow == currentRow)
        {
            if (currentDir == CellDirection.right && lastIndex + 1 == index)
            {
                return true;
            }

            if (currentDir == CellDirection.left && lastIndex - 1 == index)
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsButtonsEmpty(int index, Word word, CellDirection direction, LetterButton[] buttons)
    {
        for (int i = 0; i < word.length; i++)
        {
            if (direction == CellDirection.right)
            {
                int lastRow = Mathf.FloorToInt(index / 10);
                index += 1;
                int currentRow = Mathf.FloorToInt(index / 10);

                if (lastRow != currentRow || buttons[index].isLetterSet)
                    return false;
            }
            else if (direction == CellDirection.down)
            {
                index += 10;

                if (index >= 100 || buttons[index].isLetterSet)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static void SetLetter(int index, Word word, CellDirection direction, LetterButton[] buttons)
    {
        for (int i = 0; i < word.length; i++)
        {
            buttons[index].SetLetter(word.word[i]);

            if (direction == CellDirection.right)
            {
                index += 1;
            }

            if (direction == CellDirection.down)
            {
                index += 10;
            }
        }
    }
}

public enum CellDirection
{
    up,
    down,
    left,
    right,
}