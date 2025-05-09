using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterDog : Characterbase
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        enumChar = CHAR.DOG;
        HandleInput();         // �̵� �Է� ����
        HandleAnimation();     // �̵� �ִϸ��̼� ó��
        HeadSpriteFlip();    // ĳ���� �¿� ����

        Vector2 input = new Vector2(moveX, moveY);
       
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


