using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Answer : MonoBehaviour
{
    const float CHOICEINPOSYX = -70.96f;
    const float CHOICEOUTPOSX = -225;
    const float ANSWERINPOSX = 37.795f;
    const float ANSWEROUTPOSX = 323;

    [SerializeField] private TextMeshProUGUI txt_answer;
    public Image img_choice, img_answer;

    [SerializeField] private Sprite correctChoice, wrongChoice, defaulChoice;

    private Button btn_answer;

    public int buttonId;

    public Action<int> OnClickAnswer;

    private void Awake()
    {
        defaulChoice = img_choice.sprite;

        btn_answer = GetComponent<Button>();
        btn_answer.onClick.AddListener(() => OnClickAnswer?.Invoke(buttonId));

        img_choice.rectTransform.anchoredPosition = new Vector2(CHOICEOUTPOSX, 0);
        img_answer.rectTransform.anchoredPosition = new Vector2(ANSWEROUTPOSX, 0);

        btn_answer.enabled = false;
    }

    public void ResultAnswer(bool isCorrect)
    {
        img_choice.transform.DOScale(new Vector3(0, 0, 1f), 0.25f).OnComplete(() =>
        {
            Sprite sprite = isCorrect ? correctChoice : wrongChoice;
            img_choice.sprite = sprite;

            img_choice.transform.DOScale(new Vector3(1, 1, 1), 0.25f);
        });
    }

    public void JokerEffect(Sprite effectSprite)
    {
        img_choice.sprite = effectSprite;
    }

    public void SetAnswer(string answer)
    {
        txt_answer.text = answer;
        img_choice.sprite = defaulChoice;
    }

    public void Disappear(Action onComplete)
    {
        btn_answer.enabled = false;

        img_answer.rectTransform.DOAnchorPos(new Vector2(ANSWEROUTPOSX, 0), 0.25f).SetEase(Ease.Linear);
        img_choice.rectTransform.DOAnchorPos(new Vector2(CHOICEOUTPOSX, 0), 0.25f).SetEase(Ease.Linear)
            .OnComplete(() => onComplete?.Invoke());
    }

    public void Appear(Action onComplete)
    {
        img_answer.rectTransform.DOAnchorPos(new Vector2(ANSWERINPOSX, 0), 0.25f).SetDelay(0.25f).SetEase(Ease.Linear);
        img_choice.rectTransform.DOAnchorPos(new Vector2(CHOICEINPOSYX, 0), 0.25f).SetDelay(0.25f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
                btn_answer.enabled = true;
            });
    }

    public void Fall()
    {
        int[] rotateDir = {-1, 1};
        int dirChoice = rotateDir[Random.Range(0, 2)];
        int dirAnswer = rotateDir[Random.Range(0, 2)];

        Vector3 eulerAngles = transform.eulerAngles;

        Vector3 posTarget = new Vector3(0, -800, 0);
        img_answer.rectTransform.DOAnchorPos(posTarget, 0.5f).SetEase(Ease.InCubic);
        img_choice.rectTransform.DOAnchorPos(posTarget, 0.5f).SetEase(Ease.InCubic);

        Vector3 rotateTargetAnswer = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z + 75 * dirAnswer);
        img_answer.rectTransform.DORotate(rotateTargetAnswer, 0.5f)
            .SetEase(Ease.InCubic).OnComplete(() => { transform.eulerAngles = new Vector3(0, 0, 0); });
        Vector3 rotateTargetChoice = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z + 75 * dirChoice);
        img_answer.rectTransform.DORotate(rotateTargetChoice, 0.5f)
            .SetEase(Ease.InCubic).OnComplete(() => { transform.eulerAngles = new Vector3(0, 0, 0); });
    }
}