using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageUI1 : MonoBehaviour
{
    public void OnClickExitToTitle()
    {
        Debug.Log("Back ��ư Ŭ����!");
        SceneManager.LoadScene("Title");
    }
}
