using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class stop : MonoBehaviour
{
    public void StopPlayOrQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // �����Ϳ��� �÷��� ��� ����
#else
        Application.Quit(); // ����� ���ӿ����� â ����
#endif
    }
}
