using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }
    public string CurrentStage { get; private set; } //현재 스테이지를 저장할 변수
    private int currentStageIndex = 0;

    [SerializeField] private List<string> stageList = new List<string>() //Scene을 리스트로 관리 + 인스펙터에서 확인하기 편하게.
{
    "Title", "SelectStage","Tutorial",
    "Stage 1-1", "Stage 1-2", "Stage 1-3",
    "Stage 2-1", "Stage 2-2", "Stage 2-3",
    "Stage 3-1", "Stage 3-2", "Stage 3-3"
    ,"EndingScene",
};
    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환에도 유지됨
        }

    CurrentStage = SceneManager.GetActiveScene().name;

    }


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

    public void OnClickExitStageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

}
