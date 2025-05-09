using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageUI1 : MonoBehaviour
{
    public void OnClickExitToTitle()
    {
        Debug.Log("Back ¹öÆ° Å¬¸¯µÊ!");
        SceneManager.LoadScene("Title");
    }
}
