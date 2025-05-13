using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStarUi : MonoBehaviour
{
    private UIManager uiManager;
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();  // UIManager를 찾습니다
    }

    // 테스트용으로 호출할 메서드
    public void TestDisplayStars(int starCount)
    {
        uiManager.DisplayStars(starCount);  // 별을 표시하는 메서드 호출

    }
}