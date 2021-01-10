using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI messageText;
    private AsyncSceneLoader sceneLoader;
    private bool startLoading;
    private float startFillAmount, startProgressText;

    private void Awake()
    {
        DeviceControl();

        progressBar.fillAmount = 0;
        progressText.text = "%0";
        messageText.text = "Yükleniyor";
    }

    async void DeviceControl()
    {
        sceneLoader = gameObject.AddComponent<AsyncSceneLoader>();

        messageText.text = "Sunucuya Bağlanılıyor...";
        progressBar.fillAmount = 0.1f;
        progressText.text = "%10";

        var values = new Dictionary<string, string>
        {
            {"version", Application.version},
            {"device_id", SystemInfo.deviceUniqueIdentifier}
        };

        JArray response = await HTTPAuthUtil.Post(values, "auth", "devicecontrol");

        Error error = ErrorHandler.Handle(response);

        messageText.text = "Sistem Hazırlanıyor...";
        progressBar.fillAmount = 0.2f;
        progressText.text = "%20";

        if (error.isError)
        {
            if (error.errorCode == ErrorHandler.HTTPVersionNotSupported)
            {
                messageText.text = error.message;
                Debug.Log(error.errorCode + " " + error.message);
            }
            else
            {
                sceneLoader.LoadScene(Scenes.Login);
                StartLoading();
            }

            return;
        }

        User.GetInstance().SetUser(response);

        messageText.text = "Veriler Alınıyor...";
        progressBar.fillAmount = 0.3f;
        progressText.text = "%30";

        if (User.GetInstance().Race == -1)
        {
            sceneLoader.LoadScene(Scenes.Race);
            StartLoading();
        }
        else
        {
            sceneLoader.LoadScene(Scenes.Main);
            StartLoading();
        }
    }

    private void Update()
    {
        if (startLoading)
        {
            progressBar.fillAmount = Mathf.Lerp(startFillAmount, 1, sceneLoader.Progress);
            progressText.text = "%" + Mathf.Lerp(startProgressText, 100, sceneLoader.Progress);
        }
    }

    void StartLoading()
    {
        startLoading = true;
        startFillAmount = progressBar.fillAmount;
        startProgressText = progressBar.fillAmount * 100;
    }
}