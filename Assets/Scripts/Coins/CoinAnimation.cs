using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] private bool setRotation;
    [SerializeField] private bool setScale;

    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speedRotation;

    [SerializeField] private Vector3 scaleInit;
    [SerializeField] private Vector3 scaleEnd;
    [SerializeField] private float speedScale;
    [SerializeField] private float scaleRatio;

    private void Update()
    {
        if (setRotation)
        {
            transform.Rotate(rotation * speedRotation * Time.deltaTime);
        }
    }
}
