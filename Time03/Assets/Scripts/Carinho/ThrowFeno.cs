﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFeno : MonoBehaviour
{
    public float knockbackStrenght = 20.0f;
    public float knockbackHeight = 1.0f;

    private GeneralCounts Counts;
    private MovimentPlayer _mp;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
        _mp = GetComponent<MovimentPlayer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("rock"))
        {
            if(_mp.dashing)
            {
                Vector3 dir = collision.transform.position - transform.position;
                collision.rigidbody.AddForce(dir.normalized * knockbackStrenght, ForceMode.Impulse);
                collision.gameObject.GetComponent<FenoScript>().Throw();
            }
        }
    }


}
