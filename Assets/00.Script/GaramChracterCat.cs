using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterCat : Characterbase
{
    protected override void Awake()
    {
        enumChar = CHAR.CAT; // ĳ���� �з��ϱ�
        base.Awake();
        ControlKey();//�з��� ���� ����Ű �Ҵ�
    }
    /// <summary>
    /// �Է¸ż���� ���⿡
    /// </summary>
    private void Update()
    {
        IsNotClimb();//����Ż�� ��Ÿ�� ����
        MoveCall();         // �̵� �Է� 
        ToggleSkillCall(); //��ų Ű �Է�
        JumpCall();   // ���� �Է� 
    }
    /// <summary>
    /// �ൿ�ż���� ���⿡
    /// </summary>
    private void FixedUpdate()
    {
        JumpAtivate();//���� ����
        HandleJumpAnim();//���� �ִϸ��̼�
        CheckLanding();//���� ����

        MoveActivate(moveInput); // �̵� ȣ�� 
        SpriteFlip();    // ĳ���� �¿� ����
        HandleMoveAnim();     // �̵� �ִϸ��̼� 
        
    }




    protected override void ToggleSkillOn()
    {
        if (!spriteRenderer.flipX)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f); //���� �ٵ��� �����ֱ� ���� z�� ȸ��
            rb.gravityScale = 0f; //�߷� 0���� ����� 
            isClimb = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f); //���� �ٵ��� �����ֱ� ���� z�� ȸ��
            rb.gravityScale = 0f; //�߷� 0���� ����� 
            isClimb = true;
        }
    }
    protected override void ToggleSkillOff()
    {
        transform.rotation = Quaternion.identity; // ���� �ʱⰪ (0,0,0)
        rb.gravityScale = 1f;
        isClimb = false;
        
    }

    protected override void MoveCall()//���� �پ��� �ÿ� ���� ���� ������ ���� ������
    {
        if (!isClimb)
        {
            moveX = 0;

            if (Input.GetKey(leftKey)) moveX -= 1;
            if (Input.GetKey(rightKey)) moveX += 1;
            moveInput = new Vector2(moveX, moveY).normalized;
        }
        else
        {
            if (!spriteRenderer.flipX)
            {
                moveY = 0;

                if (Input.GetKey(leftKey)) moveY -= 1;
                if (Input.GetKey(rightKey)) moveY += 1;
                moveInput = new Vector2(moveX, moveY).normalized;
            }
            else
            {
                moveY = 0;

                if (Input.GetKey(leftKey)) moveY += 1;
                if (Input.GetKey(rightKey)) moveY -= 1;
                moveInput = new Vector2(moveX, moveY).normalized;
            }
        }
    }
    /// <summary> �̵� ó��: �ӵ� ���� �� ���� ���� </summary>
    protected override void MoveActivate(Vector2 input)//���� �پ��� �ÿ� ���� ���� ������ ���� ������
    {
        if (!isClimb)
        {
            Vector2 velocity = rb.velocity;
            velocity.x = input.x * moveSpeed;

            rb.velocity = velocity;
        }
        else
        {
            Vector2 velocity = rb.velocity;
            velocity.y = input.y * moveSpeed;

            rb.velocity = velocity;
        }



    }
    protected override void HandleJumpAnim()
    {

        bool isJump = currentJumpCount > 0&&!isClimb || (isClimb&&currentJumpCount == 0 && !IsGrounded());
        Anim.SetJump(isJump);

    }
    protected override void HandleMoveAnim()
    {
        if (isClimb) {
            bool isMoving = moveY != 0;
            Anim.SetMove(isMoving);
        }
        else
        {
            bool isMoving = moveX != 0;
            Anim.SetMove(isMoving);
        }
    }
    protected virtual void SpriteFlip() // �¿���� ���� �ż���
    {
        if (isClimb)
        {
            if (moveY != 0)
            { bool updown = moveY >0;
                if (spriteRenderer != null)
                    spriteRenderer.flipY = updown;
            }
            if (moveY != 0)
                if (headPivot != null)
                {
                    headPivot.localScale = new Vector3(moveY < 0 ? -1f : 1f, 1f, 1f);
                }
        }
        else
        {
            if (moveX != 0)
                Anim.SetFlip(moveX < 0);
            if (moveX != 0)
                if (headPivot != null)
                {
                    headPivot.localScale = new Vector3(moveX < 0 ? -1f : 1f, 1f, 1f);
                }
        }

    }

}