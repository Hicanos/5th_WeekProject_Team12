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
        //아이템이 null이 아니라면, FishCount와 BoneCount 확인
        if (fishCountText != null)
            fishCountText.text = $"{Item.FishCount}";
        if (boneCountText != null)
            boneCountText.text = $"{Item.BoneCount}";
        
    }
}
