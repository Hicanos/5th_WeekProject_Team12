using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCountUI : MonoBehaviour
{
    public Text fishCountText;
    public Text boneCountText;

    void Update()
    {
        //�������� null�� �ƴ϶��, FishCount�� BoneCount Ȯ��
        if (fishCountText != null)
            fishCountText.text = $"{Item.FishCount}";
        if (boneCountText != null)
            boneCountText.text = $"{Item.BoneCount}";
        
    }
}
