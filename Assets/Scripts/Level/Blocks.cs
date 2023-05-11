using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlockType
{
    Normal,
    Wagons,
    Trains
}

public class Blocks : MonoBehaviour
{
    [SerializeField] private BlockType blockType;
    [SerializeField] private bool itHasRamp;

    public BlockType BlockTypes => blockType;
    public bool ItHasRamp => itHasRamp;
}
