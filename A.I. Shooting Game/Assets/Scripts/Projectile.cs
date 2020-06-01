using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private Vector3 Direction;

    void SelfDestruct()
    {
        Destroy(this.gameObject);
    }

    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
        
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.velocity = Direction * 10f;
    }
    
    void Start()
    {
        Invoke(nameof(SelfDestruct), 1f);
    }
}
