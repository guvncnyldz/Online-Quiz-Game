using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WordHuntGridUtil
{
    public static bool IsButtonsSidebySide(int index, int lastIndex, ref CellDirection currentDir)
    {
        int lastRow = Mathf.FloorToInt(lastIndex / 10);
        int currentRow = Mathf.FloorToInt(index / 10);

        if ((lastRow + 1 == currentRow || lastRow - 1 == currentRow))
        {
            if (lastIndex + 10 == index)
            {
                currentDir = CellDirection.down;
                return true;
            }

            if (lastIndex - 10 == index)
            {
                currentDir = CellDirection.up;
                return true;
            }

            if (lastIndex + 11 == index)
            {
                currentDir = CellDirection.rightDown;
                return true;
            }

            if (lastIndex - 9 == index)
            {
                currentDir = CellDirection.rightUp;
                return true;
            }

            if (lastIndex - 11 == index)
            {
                currentDir = CellDirection.leftUp;
                return true;
            }

            if (lastIndex + 9 == index)
            {
                currentDir = CellDirection.leftDown;
                return true;
            }
        }
        else if (lastRow == currentRow)
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

        if ((lastRow + 1 == currentRow || lastRow - 1 == currentRow))
        {
            if (currentDir == CellDirection.down && lastIndex + 10 == index)
            {
                return true;
            }

            if (currentDir == CellDirection.up && lastIndex - 10 == index)
            {
                return true;
            }

            if (currentDir == CellDirection.rightDown && lastIndex + 11 == index)
            {
                return true;
            }

            if (currentDir == CellDirection.rightUp && lastIndex - 9 == index)
            {
                return true;
            }

            if (currentDir == CellDirection.leftDown && lastIndex + 9 == index)
            {
                return true;
            }

            if (currentDir == CellDirection.leftUp && lastIndex - 11 == index)
            {
                return true;
            }
        }
        else if (lastRow == currentRow)
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
            else if (direction == CellDirection.left)
            {
                int lastRow = Mathf.FloorToInt(index / 10);
                index -= 1;
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
            else if (direction == CellDirection.up)
            {
                index += 10;

                if (index < 0 || buttons[index].isLetterSet)
                {
                    return false;
                }
            }
            else if (direction == CellDirection.rightDown)
            {
                int lastRow = Mathf.FloorToInt(index / 10);

                index += 11;

                if (index >= 100 || buttons[index].isLetterSet)
                {
                    return false;
                }

                int currentRow = Mathf.FloorToInt(index / 10);

                if ((lastRow + 1 != currentRow && lastRow - 1 != currentRow) || buttons[index].isLetterSet)
                    return false;
            }
            else if (direction == CellDirection.rightUp)
            {
                int lastRow = Mathf.FloorToInt(index / 10);

                index -= 9;

                if (index < 0 || buttons[index].isLetterSet)
                {
                    return false;
                }

                int currentRow = Mathf.FloorToInt(index / 10);

                if ((lastRow + 1 != currentRow && lastRow - 1 != currentRow) || buttons[index].isLetterSet)
                    return false;
            }
            else if (direction == CellDirection.leftDown)
            {
                int lastRow = Mathf.FloorToInt(index / 10);

                index += 9;

                if (index >= 100 || buttons[index].isLetterSet)
                {
                    return false;
                }

                int currentRow = Mathf.FloorToInt(index / 10);

                if ((lastRow + 1 != currentRow && lastRow - 1 != currentRow) || buttons[index].isLetterSet)
                    return false;
            }
            else if (direction == CellDirection.leftUp)
            {
                int lastRow = Mathf.FloorToInt(index / 10);

                index -= 11;

                if (index < 0 || buttons[index].isLetterSet)
                {
                    return false;
                }

                int currentRow = Mathf.FloorToInt(index / 10);

                if ((lastRow + 1 != currentRow && lastRow - 1 != currentRow) || buttons[index].isLetterSet)
                    return false;
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
            else if (direction == CellDirection.left)
            {
                index -= 1;
            }
            else if (direction == CellDirection.down)
            {
                index += 10;
            }
            else if (direction == CellDirection.up)
            {
                index -= 10;
            }
            else if (direction == CellDirection.rightDown)
            {
                index += 11;
            }
            else if (direction == CellDirection.rightUp)
            {
                index -= 9;
            }
            else if (direction == CellDirection.leftDown)
            {
                index += 9;
            }
            else if (direction == CellDirection.leftUp)
            {
                index -= 11;
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
    rightDown,
    rightUp,
    leftDown,
    leftUp
}