using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [Header("Base UI")] //인스펙터 창에서 보기 편하게 나눠주는 역할
    [SerializeField] private GameObject RC_BgImage;
    [SerializeField] private GameObject RC_CompleteImage;
    [SerializeField] private GameObject RC_CatImage;
    [SerializeField] private GameObject RC_DogImage;
    [SerializeField] public GameObject CR;
    [SerializeField] public GameObject MainCanvas;
    [SerializeField] public GameObject tBtn;
    [SerializeField] public GameObject bBtn;
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
   
    [SerializeField] private Button[] retryBtn;
    [SerializeField] private Button[] selectStageBtn;
    [SerializeField] private Button titleStageBtn;
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
            DontDestroyOnLoad(this.gameObject);
        }
        // 버튼 초기 연결
        

        foreach (Button btn in retryBtn)
        { btn.onClick.AddListener(OnClickRetryStage); }
        foreach (Button btn in selectStageBtn)
        { btn.onClick.AddListener(OnClickSelectStage); }
        titleStageBtn.onClick.AddListener(OnClickStageTitle);
    }

    private void Update()
    {
        //if (!isPlaying) return;
        if (!isPlaying || timeText == null) return;//timeText가 null이면 Update()에서 아예 실행 안 하도록 방어
        currentTime += Time.deltaTime;
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "SelectStage")
        {
            timeText.text = currentTime.ToString("N2");
        }
        else
        {
            currentTime = 0;
            timeText.text = "0";
        }
        UnityEngine.Debug.Log(timeText.text);
    }
    private void OnDisable()
    {

        this.enabled = true;
        //SceneManager.sceneLoaded -= OnSceneLoaded;

    }
    ////컴포넌트가 켜질 때 호출됨
    //private void OnEnable()
    //{
    //    //SceneManager.sceneLoaded += OnSceneLoaded;
    //    //씬이 바뀔 때마다 OnSceneLoaded()가 호출되도록 이벤트 등록
    //}

    ////씬이 새로 로드되면 자동 실행됨
    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    //씬이 바뀔 때마다 UI를 초기화
    //    StartTimer();
    //}
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
        CR.SetActive(true);
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

  
   
    public void OnClickRetryStage()
    {
        MapManager.Instance.LoadStage(MapManager.Instance.CurrentStage);
        CR.SetActive(false);
        MainCanvas.SetActive(true);
    }

    public void OnClickSelectStage()
    {
        MapManager.Instance.OnClickExitStageSelect();
        CR.SetActive(false);
        MainCanvas.SetActive(false);
        tBtn.SetActive(true);
        bBtn.SetActive(false);
    }
    public void OnClickTutorialStageSelect()
    {
        MapManager.Instance.LoadSceneTutorial();
        CR.SetActive(false);
        MainCanvas.SetActive(true);
       
    }

    public void OnClickStageTitle()
    { 
    MapManager.Instance.LoadSceneTiltle();
        tBtn.SetActive(false);
    }
}
