using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Application.targetFrameRate = 60;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ProcessingStageClear(bool gotLegacy, bool gotAllObjects, float clearTime, float timeLimit) 
    {
        int starCount = 0;
        if (gotLegacy) starCount++;
        if (gotAllObjects) starCount++;
        if (clearTime <= timeLimit) starCount++;

        string stage = MapManager.Instance.CurrentStage;
        DataManager.Instance.UpdateStarCount(stage, starCount);
        UIManager.Instance.DisplayStars(starCount);
    }
}

