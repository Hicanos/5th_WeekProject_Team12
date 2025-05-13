using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private Text timeText;

    private float currentTime = 0f;
    private bool isPlaying = true;


    void Start()
    {

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

    public void DisplayStars(int starCount)
    {
        switch (starCount)
        {
            case 1: 
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }

    }


}
