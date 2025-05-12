using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }
    public string CurrentStage { get; private set; } //현재 스테이지를 저장할 변수
    //protected Rigidbody2D _doorRigidbody;
    bool isPlayerInRange = false;
    //public GameObject interactionPopup2;
    private int currentStageIndex = 0;

    protected virtual void Awake()
    {
       // _doorRigidbody = GetComponent<Rigidbody2D>();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환에도 유지됨
        }

    }
    protected void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("SelectStage");
        }
    }

    public void LoadStage(string _stageName) //스테이지를 불러오는 함수
    {
        CurrentStage = _stageName;
        SceneManager.LoadScene(_stageName);
    }

    [SerializeField] private List<string> stageList = new List<string>() //Scene을 리스트로 관리 + 인스펙터에서 확인하기 편하게.
{
    "Title", "SelectStage","Tutorial",
    "Stage_1_1", "Stage_1_2", "Stage_1_3",
    "Stage_2_1", "Stage_2_2", "Stage_2_3",
    "Stage_3_1", "Stage_3_2", "Stage_3_3"
    ,"EndingScene",
};


    public void LoadNextStage() //다음 스테이지를 불러오는 함수
    {
        currentStageIndex++;
        if (currentStageIndex < stageList.Count)
        {
            LoadStage(stageList[currentStageIndex]);
        }
        else
        {
            // 마지막 스테이지 이후 처리
            SceneManager.LoadScene("EndingScene");
        }
    }


    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         isPlayerInRange = true;
    //         interactionPopup2.SetActive(true);
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         // 플레이어가 벗어나면 팝업 숨김
    //         isPlayerInRange = false;
    //         interactionPopup2.SetActive(false);
    //     }
    // }
}
