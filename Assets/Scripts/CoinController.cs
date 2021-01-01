using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void FixedUpdate()
    {
        if (!GameManager.instance.magnetActive) return;

        if(Vector3.Distance(transform.position,_player.position)
            < GameManager.instance.magnetDistance)
        {
            var direction = (_player.position - transform.position).normalized;
            transform.position += direction * GameManager.instance.magnetSpeed;
        }
        
    }
}
