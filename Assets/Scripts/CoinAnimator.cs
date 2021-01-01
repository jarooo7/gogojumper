using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimator : MonoBehaviour
{
    public float rotationSpeed;
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, rotationSpeed, 0f));
    }
}
