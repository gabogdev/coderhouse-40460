using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerAnimation playerAnimation;
    private Vector3 direction;
    private int currentLane = 1;

    [SerializeField] private PlayerData m_data;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameState.Start)
            playerAnimation.AnimationIdle();
        else if (GameManager.Instance.CurrentState == GameState.Play)
        {
            InputController();
            ChangeLane();
        }
    }

    private void InputController()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

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
                SoundManager.Instance.PlaySoundFX(SoundManager.Instance.jumpClip);
                direction.y = m_data.jumpForce;
                playerAnimation.AnimationJump();
            }
        }
        else
        {
            direction.y += m_data.gravity * Time.deltaTime;
        }
    }

    private void ChangeLane()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        switch (currentLane)
        {
            case 0:
                targetPosition += Vector3.left * m_data.laneDistance;
                break;

            case 2:
                targetPosition += Vector3.right * m_data.laneDistance;
                break;
        }

        direction.x = (targetPosition - transform.position).normalized.x * m_data.sideSpeed;
        direction.z = m_data.forwardSpeed;
        controller.Move(direction * Time.fixedDeltaTime);

        if (controller.isGrounded)
            playerAnimation.AnimationRun();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
        {
            if (GameManager.Instance.CurrentState != GameState.GameOver)
            {
                SoundManager.Instance.PlaySoundFX(SoundManager.Instance.colisionClip);
                playerAnimation.AnimationDead();
                GameManager.Instance.ChangeState(GameState.GameOver);
            }
        }
    }
}
