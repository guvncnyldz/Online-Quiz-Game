using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class HTTPApiUtil
{
    private static float timeOut = 10;

    public static async Task<JArray> Post(Dictionary<string, string> values, string route, string method)
    {
        JArray jArray = new JArray();

        HttpClient client = new HttpClient()
        {
            //DefaultRequestHeaders = {{"x-access-token", User.GetInstance().Token}}
            DefaultRequestHeaders =
            {
                {
                    "x-access-token",
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkZXZpY2VfaWQiOiJEcG9zdDIiLCJpYXQiOjE2MTAxNjcwODYsImV4cCI6NDc2NTkyNzA4Nn0.2YZdbqMJ_yImKBmMGF8YED8rtRjF-r6kUI_WtlIutzw"
                }
            }
        };

        client.Timeout = TimeSpan.FromSeconds(timeOut);

        var content = new FormUrlEncodedContent(values);

        string responseString = "";

        try
        {
            var response = await client.PostAsync("http://" + Config.serverIP + ":" + Config.port + "/api/" + route + "/" + method,
                content);

            responseString = await response.Content.ReadAsStringAsync();

            jArray = JSONUtil.GetJArray(responseString);
        }
        catch (TaskCanceledException e)
        {
            JObject json = new JObject();
            json.Add("code", "408");
            json.Add("message", "İstek zaman aşımına uğradı");

            Debug.LogError(responseString);
            Debug.LogError(e);

            jArray.Add(json);
        }
        catch (Exception e)
        {
            JObject json = new JObject();
            json.Add("code", "500");
            json.Add("message", "Bilinmeyen hata");

            Debug.LogError(responseString);
            Debug.LogError(e);

            jArray.Add(json);
        }

        return jArray;
    }

    public static async Task<JArray> Put(Dictionary<string, string> values, string route, string method)
    {
        JArray jArray = new JArray();

        HttpClient client = new HttpClient()
        {
            DefaultRequestHeaders = {{"x-access-token", User.GetInstance().Token}}
            /*DefaultRequestHeaders =
            {
                {
                    "x-access-token",
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkZXZpY2VfaWQiOiJEcG9zdCIsImlhdCI6MTYwOTM0NzQxOSwiZXhwIjo0NzY1MTA3NDE5fQ.fg92ZNfXjlQ3nYl06tgpDQsNsPZyyAK--oj8pw2_MR8"
                }
            }*/
        };

        client.Timeout = TimeSpan.FromSeconds(timeOut);

        var content = new FormUrlEncodedContent(values);

        string responseString = "";

        try
        {
            var response = await client.PutAsync("http://" + Config.serverIP + ":" + Config.port + "/api/" + route + "/" + method,
                content);

            responseString = await response.Content.ReadAsStringAsync();

            jArray = JSONUtil.GetJArray(responseString);
        }
        catch (TaskCanceledException e)
        {
            JObject json = new JObject();
            json.Add("code", "408");
            json.Add("message", "İstek zaman aşımına uğradı");

            Debug.LogError(responseString);
            Debug.LogError(e);

            jArray.Add(json);
        }
        catch (Exception e)
        {
            JObject json = new JObject();
            json.Add("code", "500");
            json.Add("message", "Bilinmeyen hata");

            Debug.LogError(responseString);
            Debug.LogError(e);

            jArray.Add(json);
        }

        return jArray;
    }
}