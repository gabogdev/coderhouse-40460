using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerAnimation playerAnimation;
    private Vector3 direction;
    [SerializeField] private float forwardSpeed = 10;
    [SerializeField] private float sideSpeed = 15;
    [SerializeField] private GameManager gameManager;

    private int currentLane = 1;
    [SerializeField] private float laneDistance = 2.7f;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float gravity = -20;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    /*
    private void FixedUpdate()
    {
        if (gameManager.CurrentState == GameState.Play)
            ChangeLane();
    }
    */
    private void Update()
    {
        if (gameManager.CurrentState == GameState.Start)
            playerAnimation.AnimationIdle();
        else if (gameManager.CurrentState == GameState.Play)
        {
            InputController();
            ChangeLane();
        }
    }

    private void InputController()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentLane++;
            if (currentLane > 2)
                currentLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentLane--;
            if (currentLane < 0)
                currentLane = 0;
        }

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction.y = jumpForce;
                playerAnimation.AnimationJump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
    }

    private void ChangeLane()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        switch (currentLane)
        {
            case 0:
                targetPosition += Vector3.left * laneDistance;
                break;

            case 2:
                targetPosition += Vector3.right * laneDistance;
                break;
        }

        direction.x = (targetPosition - transform.position).normalized.x * sideSpeed;
        direction.z = forwardSpeed;
        controller.Move(direction * Time.fixedDeltaTime);

        if (controller.isGrounded)
            playerAnimation.AnimationRun();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
        {
            if (gameManager.CurrentState != GameState.GameOver)
            {
                playerAnimation.AnimationDead();
                gameManager.ChangeState(GameState.GameOver);
            }
        }

        if (hit.collider.CompareTag("UnderConstruction"))
        {
            if (gameManager.CurrentState != GameState.Start)
            {
                playerAnimation.AnimationIdle();
                gameManager.ChangeState(GameState.Start);
            }
        }
    }
}
