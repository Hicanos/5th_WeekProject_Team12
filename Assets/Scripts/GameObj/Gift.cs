using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] public int LegacyID;

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        //개 or 고양이 확인 (Player Tag확인)
        if (collision.CompareTag("Player"))
        {
            ObjManager.Instance.CollectLegacy(LegacyID);
            Destroy(gameObject);
        }
    }
}
