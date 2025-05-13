using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScretTile : MonoBehaviour
{
    public Tilemap tRenderer;
    [SerializeField] private LayerMask cat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & cat) != 0)
        {
            Color color = tRenderer.color;
            color.a = 0.4f;
            tRenderer.color = color;
            Debug.Log("�۵�");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & cat) != 0)
        {
            Color color = tRenderer.color;
            color.a = 1f;
            tRenderer.color = color;
            Debug.Log("�۵�����");
        }
    }
}
