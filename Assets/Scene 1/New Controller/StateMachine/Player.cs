using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Vars
    public PlayerStateMachine StateMachine { get; private set; }

    [SerializeField] private PlayerData playerData;

    //States
    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public JumpState JumpState { get; private set; }
    public FallState FallState { get; private set; }
    public FlyState FlyState { get; private set; }
    #endregion

    #region Components
    public Animator Anim { get; private set; }

    public InputHandler InputHandler { get; private set; }

    public Rigidbody2D RB;
    #endregion

    #region Other vars
    public Vector2 CurrentVelocity { get; private set; }

    public int FacingDirection { get; private set; } 

    public bool canJump { get; private set; }  

    public bool onGround { get; private set; }


    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    //Called before Start
    //Sets state vars
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new IdleState(this, StateMachine, playerData, "idle");
        MoveState = new MoveState(this, StateMachine, playerData, "move");
        JumpState = new JumpState(this, StateMachine, playerData, "jump");
        FallState = new FallState(this, StateMachine, playerData, "fall");
        FlyState = new FlyState(this, StateMachine, playerData, "fly");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();

        Anim.SetBool("move", false);
        Anim.SetBool("jump", false);

        StateMachine.Initialize(IdleState);   //Starts in IdleState

        InputHandler = GetComponent<InputHandler>();

        FacingDirection = -1;
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;

        StateMachine.CurrentState.LogicUpdate();    //Runs logic update of current state every iteration
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();  //Runs physics update of current state every iteration

    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y); //Sets workspace to velocity parameter and current Y velocity
        RB.velocity = workspace;    //Sets RB velocity to workspace
        CurrentVelocity = workspace;    //Sets current velocity to workspace
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity); //Sets workspace to velocity parameter and current Y velocity
        RB.velocity = workspace;    //Sets RB velocity to workspace
        CurrentVelocity = workspace;    //Sets current velocity to workspace
    }
    #endregion

    #region Check Functions
    public void CheckShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)    //If receiving x input and facing a different direction
        {
            Flip();
        }
    }

    //Handles onGround with box collider
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            canJump = true;
            //Debug.Log("On Ground");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
            //Debug.Log("Off Ground");
        }
    }

    #endregion

    #region Move Functions
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void Jump()
    {
        //Debug.Log("Jump");
        if (onGround)
        {
            SetVelocityY(playerData.jumpSpeed);
        }
        else //Double jump is smaller
        {
            if (canJump && playerData.canDoubleJump)
            {
                SetVelocityY(playerData.jumpSpeed * playerData.doubleJumpMult);
            }
            canJump = false;
        }
    }

    public void Fall()
    {
        //curve fall, not linear

        workspace.Set(CurrentVelocity.x, CurrentVelocity.y - Mathf.Abs(playerData.fallSpeed - CurrentVelocity.y)*.01f); //Addds difference of terminal - current velocity to current velocity
        RB.velocity = workspace;    //Sets RB velocity to workspace
        CurrentVelocity = workspace;    //Sets current velocity to workspace
        //Debug.Log(CurrentVelocity.y);
    }

    public void Fly(float flyTime)
    {
        Debug.Log("flying");
        if (flyTime > 0)
        {
            SetVelocityY(playerData.jumpSpeed);
        }
        Debug.Log("done");
    }
    #endregion
}
