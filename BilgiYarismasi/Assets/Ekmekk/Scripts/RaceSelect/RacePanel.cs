using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RacePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI raceName, raceDetail, raceNameBlackscreen;
    [SerializeField] private Button btn_select;
    [SerializeField] private Image blackScreen;
    [SerializeField] private RectTransform iconHolder;

    private Tween detailDoText;

    private RaceSelector raceSelector;

    private void Awake()
    {
        btn_select.onClick.AddListener(SelectRace);

        raceSelector = FindObjectOfType<RaceSelector>();
    }

    public void SetDetail(string name, Color color, string detail)
    {
        raceName.DOColor(color, 0);
        raceName.DOFade(0, 0).OnComplete(() => raceName.text = name);
        raceName.DOFade(1, 1);

        raceDetail.text = "";

        if (detailDoText != null && detailDoText.IsActive())
            detailDoText.Kill();

        detailDoText = raceDetail.DOText(detail, 1);
    }

    public void SetDetailDirect(string name, Color color, string detail)
    {
        raceName.color = color;
        raceName.text = name;
        raceDetail.text = detail;
    }

    void SelectRace()
    {
        raceSelector.LockButtons(false);
        btn_select.enabled = false;

        RacesIndex selectedRace = raceSelector.selectedRace;

        blackScreen.gameObject.SetActive(true);

        Image holder = iconHolder.GetComponent<Image>();
        holder.sprite = raceSelector.selectedButton.GetComponent<Image>().sprite;
        holder.enabled = true;

        holder.rectTransform.DOSizeDelta(new Vector2(180, 180), 2);
        holder.rectTransform.DOAnchorPos(new Vector2(0, 55), 2).OnComplete(() =>
        {
            raceNameBlackscreen.color = Races.races[selectedRace].color;
            raceNameBlackscreen.DOText(Races.races[selectedRace].name, 10).SetSpeedBased();
            holder.rectTransform.DOAnchorPos(new Vector2(0, holder.rectTransform.anchoredPosition.y + 125), 2f)
                .SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutQuad).OnComplete(() =>
                {
                    //OnComplete daha sonra olmayacak
                    User.GetInstance.race = selectedRace;
                    LoginHttp.AddRace(User.GetInstance.nickname, selectedRace,
                        () => SceneManager.LoadScene((int) Scenes.Main));
                });
        }).SetEase(Ease.OutQuad);
    }
}