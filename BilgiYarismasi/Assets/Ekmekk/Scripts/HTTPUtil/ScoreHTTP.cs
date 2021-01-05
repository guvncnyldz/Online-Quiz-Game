using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreHTTP
{
    public static void SaveScore(int trueAnswer, int earn, int modId)
    {
        var values = new Dictionary<string, string>
        {
            {"profile_id", User.GetInstance().ProfileId},
            {"user_id", User.GetInstance().UserId},
            {"race", User.GetInstance().Race.ToString()},
            {"true_answer", trueAnswer.ToString()},
            {"earn", earn.ToString()},
            {"mod_id", modId.ToString()}
        };

        HTTPApiUtil.Put(values, "score", "savescore");
    }
}