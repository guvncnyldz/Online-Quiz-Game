using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class JSONUtil
{
    public static JArray GetJArray(string content)
    {
        JArray jArray = new JArray();
        
        JToken token = JToken.Parse(content);

        if (token is JArray)
        {
            jArray = JArray.Parse(content);
        }
        else if (token is JObject)
        {
            string newContent = "[" + content + "]";
            jArray = JArray.Parse(newContent);
        }

        return jArray;
    }
}
