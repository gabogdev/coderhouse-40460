using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBooster : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private float durationTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySoundFX(SoundManager.Instance.itemClip);
            GameManager.Instance.MultiplierValue = value;
            GameManager.Instance.InitScoreBooster(durationTime);
            gameObject.SetActive(false);
        }
    }
}
