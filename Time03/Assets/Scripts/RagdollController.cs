﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Collider MainCollider;
    private Collider[] AllColliders;
    private Rigidbody MainRigidbody;
    private Rigidbody[] AllRigidbodies;

    bool rag = false;

    void Awake()
    {
        MainCollider = GetComponent<Collider>();
        MainRigidbody = GetComponent<Rigidbody>();
        AllColliders = GetComponentsInChildren<Collider>(true);
        AllRigidbodies = GetComponentsInChildren<Rigidbody>();
        DoRagdoll(false);
    }

    void Update(){
        
        //Ativar ou desativar o ragdoll
        if(Input.GetKeyDown(KeyCode.LeftControl) && rag == false)
        {
            DoRagdoll(true);
            rag = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && rag == true)
        {
            DoRagdoll(false);
            rag = false;
        }
            
    }
    
    public void DoRagdoll(bool isRagdoll)
    {
        foreach (var col in AllColliders)
        {
            col.enabled = isRagdoll;
            MainCollider.enabled = !isRagdoll;
            GetComponent<Rigidbody>().useGravity = !isRagdoll;
            GetComponentInChildren<Animator>().enabled = !isRagdoll;
        }
        foreach (var body in AllRigidbodies)
        {
            body.isKinematic = !isRagdoll;
            MainRigidbody.isKinematic = isRagdoll;
        }
    }
}
