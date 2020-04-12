﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarinhoScript : MonoBehaviour
{
    
    public float lookRadius = 10f; // se o jogador entrar nesse raio o carinho vai começar a perseguir ele
    public Transform[] carinhoHearts;
    public Transform PlayerPosition;

    private bool launching;
    private int health = 3;
    private Skills Launch;
    private Skills Orbit;
    private List<Skills> skills;  
    private HeartLaunch hlScript;
    private HeartOrbit hoScript;
    private BossSkillsCD SkillCD; 
    public float Cooldown;
    private bool SkillIsReady = true;

    private Rigidbody _rb;
    private NavMeshAgent agent;
    private int fullhealth;

    void Start()
    {
        fullhealth = health;

        agent = GetComponent<NavMeshAgent>();

        skills = new List<Skills>();

        hoScript = GetComponent<HeartOrbit>();
        Orbit = new Skills(hoScript.getProb(), hoScript.getCD(), false, hoScript.OrbitAround);
        skills.Add(Orbit);

        hlScript = GetComponent<HeartLaunch>();
        Launch = new Skills(hlScript.getProb(), hlScript.getCD(), true, hlScript.Launch);
        skills.Add(Launch);

        SkillCD = GetComponent<BossSkillsCD>();
    }

    void Update() 
    {
        float distance = Vector3.Distance(PlayerPosition.position, transform.position); //Atual distancia entre o carinho e o player

        if(agent.enabled){
            if(distance <= lookRadius){
                agent.SetDestination(PlayerPosition.position);
            }

            if(distance <= agent.stoppingDistance){
                FaceTarget();

            }
        }

        if(SkillIsReady) {
            SkillCD.ChooseSkill(skills);
            StartCoroutine(ResetCooldown());
            Debug.Log("entrei");
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (PlayerPosition.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void Damage()
    {
        health -= 1;
        carinhoHearts[health].gameObject.SetActive(false);
        if(health == fullhealth - 1) {
            //Launch.SwitchReady();
        }
        hoScript.orbitSpeed += 100.0f; 

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("rock"))
        {
            Damage();
        }
    }

    IEnumerator ResetCooldown() {
        SkillIsReady = false;
        yield return new WaitForSeconds(Cooldown);
        SkillIsReady = true;
    }
}
