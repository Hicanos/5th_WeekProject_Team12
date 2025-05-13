using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 별을 나타낼 GameObject들
    [SerializeField] private GameObject goldStar1;
    [SerializeField] private GameObject goldStar2;
    [SerializeField] private GameObject goldStar3;
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
        // 먼저 모든 별을 끈다.
        goldStar1.SetActive(false);
        goldStar2.SetActive(false);
        goldStar3.SetActive(false);
        switch (starCount)
        {
            case 1:
                goldStar1.SetActive(true);
                break;
            case 2:
                goldStar1.SetActive(true);
                goldStar2.SetActive(true);
                break;
            case 3:
                goldStar1.SetActive(true);
                goldStar2.SetActive(true);
                goldStar3.SetActive(true);
                break;
            default:
                break;
        }

    }


}
