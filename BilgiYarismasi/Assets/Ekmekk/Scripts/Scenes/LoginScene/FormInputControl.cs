using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class FormInputControl
{
    public static bool PasswordControl(string password,Action<string> fail)
    {
        if (password == "")
        {
            fail?.Invoke("Şifre boş bırakılamaz!");
            return false;
        }
        
        if (!IsAllowed(password))
        {
            fail?.Invoke(@"Lütfen geçerli karakter giriniz!\nA-Z a-z 0-9 !#$%&'*+-/=?^_`{|}~.");
            return false;
        }

        return true;
    }

    public static bool EmailControl(string email,Action<string> fail)
    {
        bool isAllowed = false;

        foreach (char character in email)
        {
            if (character == '@')
            {
                isAllowed = true;
            }
        }

        if (!isAllowed)
        {
            fail?.Invoke("Lütfen geçerli bir email girin!");
        }
        
        return isAllowed;
    }

    public static bool NicknameControl(string nickname,Action<string> fail)
    {
        if (nickname == "")
        {
            fail?.Invoke("Kullanıcı adı boş bırakılamaz!");
            return false;
        }
        
        if (!IsAllowed(nickname))
        {
            fail?.Invoke(@"Lütfen geçerli karakter giriniz!\nA-Z a-z 0-9 !#$%&'*+-/=?^_`{|}~.");
            return false;
        }
        
        return true;
    }

    //Util
    static bool IsAllowed(string value)
    {
        string allowedChars = "!#$%&'*+-/=?^_`{|}~.1234567890ABCDEFGHIJKLMNOPRSTUVYZabcdefghijklmnoprstuvyz";
        bool wrongChar;
        
        foreach (char character in value)
        {
            wrongChar = true;
            for (int i = 0; i < allowedChars.Length; i++)
            {
                if (character == allowedChars[i])
                {
                    wrongChar = false;
                    break;
                }
            }

            if (wrongChar == true)
            {
                return false;
            }
        }
        
        return true;
    }
}