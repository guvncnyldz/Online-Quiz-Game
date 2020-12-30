using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class ErrorHandler
{
    public const string Unauthorized = "401";
    public const string Forbidden = "403";
    public const string NotFound = "404";
    public const string InternalServerError = "500";
    public const string HTTPVersionNotSupported = "505";

    private static string[] errorCodes =
        {Unauthorized, Forbidden, NotFound, InternalServerError, HTTPVersionNotSupported};

    public static Error Handle(JArray value)
    {
        Error error = new Error();

        if (value[0]["code"] != null)
        {
            string currentCore = value[0]["code"].ToString();

            foreach (string code in errorCodes)
            {
                if (currentCore.Equals(code))
                {
                    error.isError = true;
                    error.errorCode = code;
                    error.message = value[0]["message"].ToString();
                }
            }
        }

        return error;
    }
}

public struct Error
{
    public bool isError;
    public string errorCode;
    public string message;
}