using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int charLayer = collision.gameObject.layer;
        string Namechar = LayerMask.LayerToName(charLayer);
        Debug.Log($"부딪힌 놈:{Namechar} ");
        if(charLayer == LayerMask.NameToLayer("Cat")&& collision.CompareTag("Player"))
        {
            GetItem();
        }
        //고양이가 아니면 아무런 효과 없음
    }

    public void GetItem()
    {
        Item.FishCount--;
        Destroy(gameObject);
    }
}
