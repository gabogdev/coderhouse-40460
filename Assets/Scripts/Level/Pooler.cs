using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private string poolerName;
    [SerializeField] private GameObject[] objectsToGenerate;
    [SerializeField] private int quantityPerObject;

    private List<GameObject> instancesCreated = new List<GameObject>();
    private GameObject poolerContainer;

    private void Awake()
    {
        poolerContainer = new GameObject($"Pooler - {poolerName}");
        PoolerCreate();
    }

    private void PoolerCreate()
    {
        for(int i = 0; i < objectsToGenerate.Length; i++)
        {
            for(int j = 0; j < quantityPerObject; j++)
            {
                instancesCreated.Add(AddInstance(objectsToGenerate[i]));
            }
        }
    }

    private GameObject AddInstance(GameObject obj)
    {
        GameObject newObj = Instantiate(obj, poolerContainer.transform);
        newObj.name = obj.name;
        newObj.SetActive(false);
        return newObj;
    }

    public GameObject GetInstancePooler(string p_name)
    {
        for(int i = 0; i < instancesCreated.Count; i++)
        {
            if(instancesCreated[i].name == p_name)
            {
                if(instancesCreated[i].activeSelf == false)
                {
                    return instancesCreated[i];
                }
            }
        }

        return null;
    }

    public GameObject GetInstancePooler()
    {
        for (int i = 0; i < instancesCreated.Count; i++)
        {
            if (instancesCreated[i].activeSelf == false)
            {
                return instancesCreated[i];
            }
        }

        return null;
    }
}
