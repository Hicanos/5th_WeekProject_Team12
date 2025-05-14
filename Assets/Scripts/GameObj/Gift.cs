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
            if(ObjManager.Instance != null)
            {
            ObjManager.Instance.CollectLegacy(LegacyID);
            }
            else{
                Debug.Log("ObjManager.Instance가 null입니다");
            }
            Destroy(gameObject);
        }
    }
}
