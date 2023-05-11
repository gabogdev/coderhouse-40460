using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitLevel : MonoBehaviour
{
    public static event Action AddNewBlockEvent;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            AddNewBlockEvent?.Invoke();
            other.transform.position = Vector3.zero;
            other.gameObject.SetActive(false);
        }
    }
}
