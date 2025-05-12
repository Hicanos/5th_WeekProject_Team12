using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterCat : Characterbase
{
    [Header("고양이 용 벽탐지")]

    [SerializeField] private Transform WallCheck;//벽 판정 위치
    [SerializeField] private float wallRayRange = 0.1f; //벽  체크 범위(raycast길이)
    [SerializeField] private LayerMask wallLayer;//벽 레이어

    protected override void Awake()
    {
        enumChar = CHAR.CAT; // 캐릭터 분류하기
        base.Awake();
        ControlKey();//분류에 따라 조작키 할당
    }
    /// <summary>
    /// 입력매서드는 여기에
    /// </summary>
    private void Update()
    {
        IsNotClimb();//벽이탈시 벽타기 해제
        MoveCall();         // 이동 입력 
        ToggleSkillCall(); //스킬 키 입력
        JumpCall();   // 점프 입력 
    }
    /// <summary>
    /// 행동매서드는 여기에
    /// </summary>
    private void FixedUpdate()
    {
        JumpAtivate();//점프 실행
        HandleJumpAnim();//점프 애니메이션
        CheckLanding();//착지 판정

        MoveActivate(moveInput); // 이동 호출 
        SpriteFlip();    // 캐릭터 좌우 반전
        HandleMoveAnim();     // 이동 애니메이션 

    }
    /*---------------------------------------스킬 관련---------------------------------------*/

    //벽타기 중인지 확인할 불
    private bool isClimb = false;

    //벽 감지용 불값
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

    //벽을 초과하여 움직일때 토클 스킬을 끄기 위한 함수
    private void IsNotClimb()
    {
        if (isToggled && !IsWallClimb())
        {
            ToggleSkillOff();
        }
    }

    //토글형 스킬 입력 감지. 감지 조건에 IsWallClimb 추가하기 위해 재정의
    protected override void ToggleSkillCall()//토글형 스킬 입력 감지
    {
        if (Input.GetKeyDown(SkillKey) && !isToggled && IsWallClimb())
        {
            StartCoroutine(SkillCoolDown(SkillCoolTime));//쿨타임 시작 
            isToggled = true;
            ToggleSkillOn();
            Debug.Log($"{Name}스킬 키 입력");
            Debug.Log($"{Name}토글스킬발동");
        }
        else if (Input.GetKeyDown(SkillKey) && isToggled)
        {

            isToggled = false;
            ToggleSkillOff();
            Debug.Log($"{Name}스킬 키 입력");
            Debug.Log($"{Name}토글스킬해제");
        }
    }
    //토글 스킬 재정의
    protected override void ToggleSkillOn()
    {
        if (!spriteRenderer.flipX)
        {
            spriteRenderer.transform.localPosition = new Vector3(0.2f, 0f, 0f);

            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, 90f); //벽에 붙듯이 보여주기 위해 z축 회전
            rb.gravityScale = 0f; //중력 0으로 만들기 
            isClimb = true;
            Anim.SetMove(false);
        }
        else
        {

            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, -90f); //벽에 붙듯이 보여주기 위해 z축 회전
            rb.gravityScale = 0f; //중력 0으로 만들기 
            isClimb = true;
            Anim.SetMove(false);
        }
    }

    protected override void ToggleSkillOff()
    {
        spriteRenderer.transform.localPosition = new Vector3(0f, 0f, 0f);
        spriteRenderer.transform.rotation = Quaternion.identity; // 각도 초기값 (0,0,0)
        rb.gravityScale = 1f;
        isClimb = false;
        Anim.SetSkill(false);

    }
    /*-------------------------------------움직임 관련-----------------------------------------*/
    //벽에 붙었을 시에 조작 방향 변경을 위한 재정의
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

    //벽에 붙었을 시에 움직이는 방향 변경을 위한 재정의
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

    //벽에 붙어있는 상황에 애니메이션 오작동을 막기위해 재정의하여 예외처리 추가
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

    //벽타기 중 점프 막기 위해 재정의 dog처럼 업데이트에서 막는게 낫나?
    protected override void JumpCall()//점프키 입력 감지
    {   //키 입력, 점프 횟수가 최대 점프보다 작을 때,벽타기 중이 아닐 때 
        if ((Input.GetKeyDown(jumpKey) && currentJumpCount < maxJumpCount) && !isClimb)
        {
            // 단순 낙하중에 점프 방지
            if (currentJumpCount == 0 && !IsGrounded())
            { return; }

            jumpReq = true;
        }
    }

    //벽에 붙어있을 때 착지판정이 일어나지 않게 하기위해 재정의하여 예외처리
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
    //벽 감지에 사용되는 raycast염색을 위해 재정의 
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
    //벽에서 이동방향으로 뒤집어 보려다 실패한 흔적 
    /*protected virtual void SpriteFlip() // 좌우반전 위한 매서드
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

