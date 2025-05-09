using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterCat : Characterbase
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        enumChar = CHAR.CAT;
        HandleInput();         // �̵� �Է� ����
        HandleAnimation();     // �̵� �ִϸ��̼� ó��
        HeadSpriteFlip();    // ĳ���� �¿� ����

        Vector2 input = new Vector2(moveX, moveY);

        /*bool gr = IsGrounded();*/
        Move(input); // BaseController�� �̵� ó�� ȣ��
        HandleJump();   // ���� �Է� ó��
        if (Mathf.Abs(rb.velocity.y) < 0.01f && currentJumpCount > 0 && IsGrounded())
        {
            currentJumpCount = 0;
            Anim.SetJump(false);
            Debug.Log("����!");
        }
     }
}