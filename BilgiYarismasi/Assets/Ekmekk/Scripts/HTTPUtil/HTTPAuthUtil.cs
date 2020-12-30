using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class HTTPAuthUtil
{
    private static float timeOut = 10;

    public static async Task<JArray> Post(Dictionary<string, string> values, string route, string method)
    {
        JArray jArray = new JArray();

        HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(timeOut);

        var content = new FormUrlEncodedContent(values);

        string responseString = "";

        try
        {
            var response = await client.PostAsync("http://localhost:8000/" + route + "/" + method, content);

            responseString = await response.Content.ReadAsStringAsync();

            jArray = JSONUtil.GetJArray(responseString);
        }
        catch (TaskCanceledException e)
        {
            JObject json = new JObject();
            json.Add("code", "408");
            json.Add("message", "İstek zaman aşımına uğradı");

            Debug.LogError(e);

            jArray.Add(json);
        }
        catch (Exception e)
        {
            JObject json = new JObject();
            json.Add("code", "500");
            json.Add("message", "Bilinmeyen hata");

            Debug.LogError(e);

            jArray.Add(json);
        }

        return jArray;
    }
}