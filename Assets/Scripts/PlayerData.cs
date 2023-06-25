using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerData")]

public class PlayerData : ScriptableObject
{
    public float forwardSpeed;
    public float sideSpeed;
    public float laneDistance;
    public float jumpForce;
    public float gravity;
}
