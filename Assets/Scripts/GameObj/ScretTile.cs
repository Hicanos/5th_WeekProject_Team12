using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScretTile : MonoBehaviour
{
    public Tilemap tRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Color color = tRenderer.color;
            color.a = 0.4f;
            tRenderer.color = color;
            Debug.Log("작동");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Color color = tRenderer.color;
            color.a = 1f;
            tRenderer.color = color;
            Debug.Log("작동해제");
        }
    }
}
