using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Blocks initBlock;
    [SerializeField] private LevelData m_levelData;  
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

        for(int i = 0; i < m_levelData.InitBlocks; i++)
        {
            CreateBlock();
        }
    }

    private void CreateBlock()
    {
        if(createdBlocks >= m_levelData.maxTrainsBlocks)
        {
            if(createdBlocks < m_levelData.maxTrainsBlocks + 1)
            {
                AddBlock(BlockType.Trains, m_levelData.normalBlockLength);
            }
            else
            {
                AddBlock(BlockType.Trains, m_levelData.trainBlockLength);
            }

            if(createdBlocks == m_levelData.maxTrainsBlocks + m_levelData.maxResetTrainsBlocks)
            {
                createdBlocks = 0;
            }
        }
        else if(createdBlocks >= m_levelData.maxWagonsBlocks)
        {
            AddBlock(BlockType.Wagons, m_levelData.normalBlockLength);
        }
        else
        {
            if(createdBlocks == m_levelData.maxWagonsBlocks - 1)
            {
                AddBlock(BlockType.Normal, m_levelData.normalBlockLength, true);
            }
            else
            {
                if(lastBlock.BlockTypes == BlockType.Trains)
                {
                    AddBlock(BlockType.Normal, m_levelData.trainBlockLength);
                }
                else
                {
                    AddBlock(BlockType.Normal, m_levelData.normalBlockLength);
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

        if (newBlock != null)
        {
            newBlock.InitBlock();
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
