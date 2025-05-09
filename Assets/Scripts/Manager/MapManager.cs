using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
     protected Rigidbody2D _doorRigidbody;
   bool isPlayerInRange = false;
   public GameObject interactionPopup2;


    protected virtual void Awake()
    {
        _doorRigidbody = GetComponent<Rigidbody2D>();
 
    }

    protected void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("SelectStage"); 
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionPopup2.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 벗어나면 팝업 숨김
            isPlayerInRange = false;
            interactionPopup2.SetActive(false);
        }
    }
}
