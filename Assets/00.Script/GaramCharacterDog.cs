using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterDog : Characterbase
{
    
    protected override void Awake()
    {
        enumChar = CHAR.DOG;
        base.Awake();
        ControlKey();
    }

    private void Update()
    {

        HandleInput();         // �̵� �Է� ����


        Vector2 input = new Vector2(moveX, moveY);
        Move(input); // BaseController�� �̵� ó�� ȣ��

        SkillCall();

        HandleJump();   // ���� �Է� ó��


    }
    private void FixedUpdate()
    {
        HandleAnimation();     // �̵� �ִϸ��̼� ó��
        SkillActivate();
        HeadSpriteFlip();    // ĳ���� �¿� ����
        CheckLanding();

    }


    protected override void Skill()
    {

        Vector2 currentLook; /*= new Vector2(moveX, 0).normalized;*/
        if (this.spriteRenderer.flipX == false)
        {
            currentLook = new Vector2(1, 0);
        }
        else { currentLook = new Vector2(-1, 0); }
        rb.velocity = Vector2.zero; //  �ӵ� �ʱ�ȭ
            rb.AddForce(currentLook * 50f, ForceMode2D.Impulse);

        //��ų ������ �־� �¿� ���� Update���� �������̴� ���� �ʿ�
        //����ó�� �ܼ�ȭ�� ���ؼ� "���¸ӽ�" ����ϱ� 
    }
}



