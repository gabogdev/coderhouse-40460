using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/LevelData")]

public class LevelData : ScriptableObject
{
    public int InitBlocks;
    public int maxWagonsBlocks;
    public int maxTrainsBlocks;
    public int maxResetTrainsBlocks;
    public int normalBlockLength;
    public int trainBlockLength;
}
