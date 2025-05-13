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
        Item.OnItemCountChanged += UpdateUI;  //�̺�Ʈ�� �����ϸ� Ȱ��ȭ
        UpdateUI();
    }

    private void OnDisable()
    {
        Item.OnItemCountChanged -= UpdateUI; //ó���� ������ �ٽ� ��Ȱ��ȭ
    }

    void UpdateUI()
    {
        //�������� null�� �ƴ϶��, FishCount�� BoneCount Ȯ��
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
