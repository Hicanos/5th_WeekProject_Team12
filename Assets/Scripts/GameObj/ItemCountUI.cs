using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCountUI : MonoBehaviour
{
    public Text fishCountText;
    public Text boneCountText;

    private void OnEnable()
    {
        Item.OnItemCountChanged += UpdateUI;  //이벤트에 접근하면 활성화
        UpdateUI();
    }

    private void OnDisable()
    {
        Item.OnItemCountChanged -= UpdateUI; //처리가 끝나면 다시 비활성화
    }

    void UpdateUI()
    {
        //아이템이 null이 아니라면, FishCount와 BoneCount 확인
        if (fishCountText != null)
            fishCountText.text = $"{Item.FishCount}";
        if (boneCountText != null)
            boneCountText.text = $"{Item.BoneCount}";

        if (Item.FishCount == 0 && Item.BoneCount == 0)
        {
            ObjManager.CheckGetObject();
        }
        else
        {
            ObjManager.LeftItem();
        }


    }
}
