using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(0.5f * rotationSpeed, 1.5f * rotationSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.inGame)
        {
            transform.Rotate(new Vector3(0f, 0f, rotationSpeed));
        }
    }
}
