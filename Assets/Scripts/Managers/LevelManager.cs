using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int InitBlocks = 5;
    [SerializeField] private int maxWagonsBlocks = 5;
    [SerializeField] private int maxTrainsBlocks = 10;
    [SerializeField] private int maxResetTrainsBlocks = 2;

    [SerializeField] private Blocks initBlock;
    [SerializeField] private int normalBlockLength = 40;
    [SerializeField] private int trainBlockLength = 40;
    [SerializeField] private Blocks[] blocks;

    private List<Blocks> normalBlocksList = new List<Blocks>();
    private List<Blocks> wagonsBlocksList = new List<Blocks>();
    private List<Blocks> trainBlocksList = new List<Blocks>();
    private List<Blocks> rampBlocksList = new List<Blocks>();

    private Pooler pooler;
    private Blocks lastBlock;
    private int createdBlocks;

    private void Awake()
    {
        pooler = GetComponent<Pooler>();
    }

    private void Start()
    {
        FillBlocks();
        lastBlock = initBlock;

        for(int i = 0; i < InitBlocks; i++)
        {
            CreateBlock();
        }
    }

    private void CreateBlock()
    {
        if(createdBlocks >= maxTrainsBlocks)
        {
            if(createdBlocks < maxTrainsBlocks + 1)
            {
                AddBlock(BlockType.Trains, normalBlockLength);
            }
            else
            {
                AddBlock(BlockType.Trains, trainBlockLength);
            }

            if(createdBlocks == maxTrainsBlocks + maxResetTrainsBlocks)
            {
                createdBlocks = 0;
            }
        }
        else if(createdBlocks >= maxWagonsBlocks)
        {
            AddBlock(BlockType.Wagons, normalBlockLength);
        }
        else
        {
            if(createdBlocks == maxWagonsBlocks - 1)
            {
                AddBlock(BlockType.Normal, normalBlockLength, true);
            }
            else
            {
                if(lastBlock.BlockTypes == BlockType.Trains)
                {
                    AddBlock(BlockType.Normal, trainBlockLength);
                }
                else
                {
                    AddBlock(BlockType.Normal, normalBlockLength);
                }
            }
        }
    }

    private void AddBlock(BlockType p_type, float p_long, bool p_ramp = false)
    {
        Blocks newBlock = GetBlockType(p_type, p_ramp);
        newBlock.transform.position = PositionNewBlock(p_long);
        lastBlock = newBlock;
        createdBlocks++;
    }

    private Blocks GetBlockType(BlockType type, bool ramp = false)
    {
        Blocks newBlock = null;

        if (ramp)
        {
            newBlock = GetPoolerInstance(rampBlocksList);
        }
        else
        {
            switch (type)
            {
                case BlockType.Normal:
                    newBlock = GetPoolerInstance(normalBlocksList);
                    break;

                case BlockType.Wagons:
                    newBlock = GetPoolerInstance(wagonsBlocksList);
                    break;

                case BlockType.Trains:
                    newBlock = GetPoolerInstance(trainBlocksList);
                    break;
            }
        }

        return newBlock;
    }

    private Blocks GetPoolerInstance(List<Blocks> p_list)
    {
        int randomBlock = Random.Range(0, p_list.Count);
        string blockName = p_list[randomBlock].name;
        GameObject instance = pooler.GetInstancePooler(blockName);
        instance.SetActive(true);
        Blocks p_block = instance.GetComponent<Blocks>();
        return p_block;
    }

    private Vector3 PositionNewBlock(float p_long)
    {
        return lastBlock.transform.position + Vector3.forward * p_long;
    }

    private void FillBlocks()
    {
        foreach(Blocks block in blocks)
        {
            switch (block.BlockTypes)
            {
                case BlockType.Normal:
                    normalBlocksList.Add(block);
                    if (block.ItHasRamp)
                    {
                        rampBlocksList.Add(block);
                    }
                    break;

                case BlockType.Wagons:
                    wagonsBlocksList.Add(block);
                    break;

                case BlockType.Trains:
                    trainBlocksList.Add(block);
                    break;
            }
        }
    }

    private void AddNewBlockRequest()
    {
        CreateBlock();
    }

    private void OnEnable()
    {
        LimitLevel.AddNewBlockEvent += AddNewBlockRequest;
    }

    private void OnDisable()
    {
        LimitLevel.AddNewBlockEvent -= AddNewBlockRequest;
    }
}
