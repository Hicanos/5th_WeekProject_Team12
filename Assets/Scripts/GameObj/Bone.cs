using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int charLayer = collision.gameObject.layer;
        string Namechar = LayerMask.LayerToName(charLayer);
        Debug.Log($"�ε��� ��:{Namechar} ");
        if (charLayer == LayerMask.NameToLayer("Dog") && collision.CompareTag("Player"))
        {
            GetItem();
        }
        //���� �ƴϸ� �ƹ��� ȿ�� ����
    }

    public void GetItem()
    {
        Item.BoneCount--;
        Destroy(gameObject);
    }
}
