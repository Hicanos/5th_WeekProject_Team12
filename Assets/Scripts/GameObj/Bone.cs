using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int charLayer = collision.gameObject.layer;
        string Namechar = LayerMask.LayerToName(charLayer);
        Debug.Log($"부딪힌 놈:{Namechar} ");
        if (charLayer == LayerMask.NameToLayer("Dog") && collision.CompareTag("Player"))
        {
            GetItem();
        }
        //개가 아니면 아무런 효과 없음
    }

    public void GetItem()
    {
        Item.BoneCount--;
        Destroy(gameObject);
    }
}
