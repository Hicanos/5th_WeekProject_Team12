using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private Text timeText;
    [SerializeField] private Text bestTimeText;
    private float currentTime = 0f;
    private bool isPlaying = true;
    

    void Start()
    {
        string currentStage = MapManager.Instance.CurrentStage; //MapManager에서 현재 스테이지의 정보를 받아서
        float bestTime = BestTimeManager.Instance.GetBestTime(currentStage); // 현재 스테이지를 GetBestTime에 key값으로 넣고, 해당하는 value를 bestTime으로 받아옴.

        bestTimeText.text = bestTime.ToString("N2");
    }

    void Update()
    {
        if (!isPlaying) return;

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
        return currentTime;
    }

}
