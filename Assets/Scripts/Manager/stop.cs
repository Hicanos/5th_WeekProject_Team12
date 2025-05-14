using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class stop : MonoBehaviour
{
    public void StopPlayOrQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // 에디터에서 플레이 모드 종료
#else
        Application.Quit(); // 빌드된 게임에서는 창 종료
#endif
    }
}
