using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterCat : Characterbase
{
    [Header("����� �� ��Ž��")]

    [SerializeField] private Transform WallCheck;//�� ���� ��ġ
    [SerializeField] private float wallRayRange = 0.1f; //��  üũ ����(raycast����)
    [SerializeField] private LayerMask wallLayer;//�� ���̾�

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
    /*---------------------------------------��ų ����---------------------------------------*/

    //��Ÿ�� ������ Ȯ���� ��
    private bool isClimb = false;

    //�� ������ �Ұ�
    private bool IsWallClimb()
    {
        if (!spriteRenderer.flipX)
        {
            RaycastHit2D hit = Physics2D.Raycast(WallCheck.position, Vector2.right, wallRayRange, wallLayer);
            return hit.collider != null;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(WallCheck.position, Vector2.left, wallRayRange, wallLayer);
            return hit.collider != null;
        }
    }

    //���� �ʰ��Ͽ� �����϶� ��Ŭ ��ų�� ���� ���� �Լ�
    private void IsNotClimb()
    {
        if (isToggled && !IsWallClimb())
        {
            ToggleSkillOff();
        }
    }

    //����� ��ų �Է� ����. ���� ���ǿ� IsWallClimb �߰��ϱ� ���� ������
    protected override void ToggleSkillCall()//����� ��ų �Է� ����
    {
        if (Input.GetKeyDown(SkillKey) && !isToggled && IsWallClimb())
        {
            StartCoroutine(SkillCoolDown(SkillCoolTime));//��Ÿ�� ���� 
            isToggled = true;
            ToggleSkillOn();
            Debug.Log($"{Name}��ų Ű �Է�");
            Debug.Log($"{Name}��۽�ų�ߵ�");
        }
        else if (Input.GetKeyDown(SkillKey) && isToggled)
        {

            isToggled = false;
            ToggleSkillOff();
            Debug.Log($"{Name}��ų Ű �Է�");
            Debug.Log($"{Name}��۽�ų����");
        }
    }
    //��� ��ų ������
    protected override void ToggleSkillOn()
    {
        if (!spriteRenderer.flipX)
        {
            spriteRenderer.transform.localPosition = new Vector3(0.2f, 0f, 0f);

            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, 90f); //���� �ٵ��� �����ֱ� ���� z�� ȸ��
            rb.gravityScale = 0f; //�߷� 0���� ����� 
            isClimb = true;
            Anim.SetMove(false);
        }
        else
        {

            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, -90f); //���� �ٵ��� �����ֱ� ���� z�� ȸ��
            rb.gravityScale = 0f; //�߷� 0���� ����� 
            isClimb = true;
            Anim.SetMove(false);
        }
    }

    protected override void ToggleSkillOff()
    {
        spriteRenderer.transform.localPosition = new Vector3(0f, 0f, 0f);
        spriteRenderer.transform.rotation = Quaternion.identity; // ���� �ʱⰪ (0,0,0)
        rb.gravityScale = 1f;
        isClimb = false;
        Anim.SetSkill(false);

    }
    /*-------------------------------------������ ����-----------------------------------------*/
    //���� �پ��� �ÿ� ���� ���� ������ ���� ������
    protected override void MoveCall()
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

    //���� �پ��� �ÿ� �����̴� ���� ������ ���� ������
    protected override void MoveActivate(Vector2 input)
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

    //���� �پ��ִ� ��Ȳ�� �ִϸ��̼� ���۵��� �������� �������Ͽ� ����ó�� �߰�
    protected override void HandleJumpAnim()
    {

        bool isJump = (currentJumpCount > 0 && !isClimb) || (!isClimb && currentJumpCount == 0 && !IsGrounded());
        Anim.SetJump(isJump);

    }
    protected override void HandleMoveAnim()
    {
        if (isClimb)
        {
            bool isMoving = moveY != 0;
            Anim.SetSkill(isMoving);
        }
        else
        {
            bool isMoving = moveX != 0;
            Anim.SetMove(isMoving);
        }
    }

    //��Ÿ�� �� ���� ���� ���� ������ dogó�� ������Ʈ���� ���°� ����?
    protected override void JumpCall()//����Ű �Է� ����
    {   //Ű �Է�, ���� Ƚ���� �ִ� �������� ���� ��,��Ÿ�� ���� �ƴ� �� 
        if ((Input.GetKeyDown(jumpKey) && currentJumpCount < maxJumpCount) && !isClimb)
        {
            // �ܼ� �����߿� ���� ����
            if (currentJumpCount == 0 && !IsGrounded())
            { return; }

            jumpReq = true;
        }
    }

    //���� �پ����� �� ���������� �Ͼ�� �ʰ� �ϱ����� �������Ͽ� ����ó��
    protected override bool IsGrounded()
    {
        if (!isClimb)
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundRayRange, groundLayer);
            return hit.collider != null;
        }
        else { return false; }
    }
    /*-------------------------------------etc.-----------------------------------------*/
    //�� ������ ���Ǵ� raycast������ ���� ������ 
    protected override void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundRayRange);
        if (WallCheck == null || spriteRenderer == null) return;
        if (!spriteRenderer.flipX)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(WallCheck.position, WallCheck.position + Vector3.right * wallRayRange);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(WallCheck.position, WallCheck.position + Vector3.left * wallRayRange);
        }
    }
    //������ �̵��������� ������ ������ ������ ���� 
    /*protected virtual void SpriteFlip() // �¿���� ���� �ż���
    {
        if (isClimb)
        {
            if (moveY != 0)
            { bool updown = moveY >0;
                if (spriteRenderer != null)
                    spriteRenderer.flipY = !updown;
                
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
        }*/

}

