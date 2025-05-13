using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStarUi : MonoBehaviour
{
    private UIManager uiManager;
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();  // UIManager�� ã���ϴ�
    }

    // �׽�Ʈ������ ȣ���� �޼���
    public void TestDisplayStars(int starCount)
    {
        uiManager.DisplayStars(starCount);  // ���� ǥ���ϴ� �޼��� ȣ��

    }
}