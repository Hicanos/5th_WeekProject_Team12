using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [Header("Base UI")] //인스펙터 창에서 보기 편하게 나눠주는 역할
    [SerializeField] private GameObject RC_BgImage;
    [SerializeField] private GameObject RC_CompleteImage;
    [SerializeField] private GameObject RC_CatImage;
    [SerializeField] private GameObject RC_DogImage;


    [Header("Star UI")] //인스펙터 창에서 보기 편하게 나눠주는 역할

    [SerializeField] private GameObject emptyStar1;
    [SerializeField] private GameObject emptyStar2;
    [SerializeField] private GameObject emptyStar3;
    [SerializeField] private GameObject goldStar1;
    [SerializeField] private GameObject goldStar2;
    [SerializeField] private GameObject goldStar3;

    [Header("Timer UI")]
    [SerializeField] private Text timeText;

    [Header("Popup / Message")]
    [SerializeField] private GameObject refuseMessage;

    [Header("Buttons")]
    [SerializeField] private Button nextStageBtn;
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button selectStageBtn;

    private float currentTime = 0f;
    private bool isPlaying = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        // 버튼 초기 연결
        if (nextStageBtn != null) { // 버튼이 존재할 경우에만 이벤트 연결
            nextStageBtn.onClick.AddListener(OnClickNextStage);} 
        retryBtn.onClick.AddListener(OnClickRetryStage);
        selectStageBtn.onClick.AddListener(OnClickSelectStage);
    }

    private void Update()
    {
        //if (!isPlaying) return;
        if (!isPlaying || timeText == null) return;//timeText가 null이면 Update()에서 아예 실행 안 하도록 방어
        currentTime += Time.deltaTime;
        timeText.text = currentTime.ToString("N2");
    }

    public void StartTimer()
    {
        currentTime = 0f;
        isPlaying = true;
    }

    public float StopTimer()
    {
        isPlaying = false;

        if (timeText != null)
            timeText.text = currentTime.ToString("N2");

        return currentTime;
    }

    public void DisplayStars(int starCount)
    {
        RC_BgImage.SetActive(true);
        RC_CompleteImage.SetActive(true);
        RC_CatImage.SetActive(true);
        RC_DogImage.SetActive(true);
        goldStar1.SetActive(starCount >= 1);
        goldStar2.SetActive(starCount >= 2);
        goldStar3.SetActive(starCount == 3);
        emptyStar1.SetActive(true);
        emptyStar2.SetActive(true);
        emptyStar3.SetActive(true);
    }

    public void ShowRefuseMessage()
    {
        refuseMessage.SetActive(true);
        Invoke(nameof(HideRefuseMessage), 2f);
    }

    private void HideRefuseMessage()
    {
        refuseMessage.SetActive(false);
    }

    public void OnClickNextStage()
    {
        MapManager.Instance.LoadNextStage();
    }

    public void OnClickRetryStage()
    {
        MapManager.Instance.LoadStage(MapManager.Instance.CurrentStage);
    }

    public void OnClickSelectStage()
    {
        MapManager.Instance.OnClickExitStageSelect();
    }
}
