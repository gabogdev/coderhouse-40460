using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagnetBooster : MonoBehaviour
{
    public static event Action<float> MagnetEvent;
    [SerializeField] private float durationTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySoundFX(SoundManager.Instance.itemClip);
            MagnetEvent?.Invoke(durationTime);
            gameObject.SetActive(false);
        }
    }
}
