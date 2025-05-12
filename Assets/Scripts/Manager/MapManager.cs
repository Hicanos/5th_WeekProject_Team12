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

    [SerializeField]
    private List<string> stageList = new List<string>() //Scene을 리스트로 관리 + 인스펙터에서 확인하기 편하게.
{
    "Title", "SelectStage","Tutorial",
    "Stage_1_1", "Stage_1_2", "Stage_1_3",
    "Stage_2_1", "Stage_2_2", "Stage_2_3",
    "Stage_3_1", "Stage_3_2", "Stage_3_3"
    ,"EndingScene",
};
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
    // protected void Update()
    // {
    //     if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
    //     {
    //         SceneManager.LoadScene("SelectStage");
    //     }
    // }

    public void LoadStage(string stageName) //현재 스테이지를 불러오는 함수
    {
        CurrentStage = stageName;

        int index = stageList.IndexOf(stageName); //IndexOf를 통해 리스트 안에서 해당 값이 몇번째인지 찾아냄.
        if (index != -1)
        {
            currentStageIndex = index;
        }

        SceneManager.LoadScene(stageName);
    }

    public void LoadStageByName(string stageName)
    {
        if (stageList.Contains(stageName))
        {
            LoadStage(stageName);
        }
        else
        {
            Debug.Log($"{stageName}이 리스트에 없다!");
        }
    }

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

}
