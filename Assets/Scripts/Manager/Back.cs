using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Back : MonoBehaviour
{
    public void OnClickExitStageSelect()
    {
        //Debug.Log("Back ��ư Ŭ����!");
        SceneManager.LoadScene("StageSelect");
    }
}
