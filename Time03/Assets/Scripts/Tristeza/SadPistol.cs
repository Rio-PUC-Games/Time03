using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SadPistol : MonoBehaviour
{
    public int NumeroBullets;

    [Range(0,100)]
    public float Probabilidade;

    public float Cooldown;

    public GameObject PrefabBulletGota;
    public GameObject Player;
    private NavMeshAgent agent;
    public float telegraph;
    private float bulletDuration;
    public float bulletSpeed;
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        bulletDuration = PrefabBulletGota.GetComponent<BulletMove>().bulletDuration;
    }


    void Update()
    {

    }

    public void Pistol()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        agent.isStopped = true;

        yield return new WaitForSeconds(telegraph);

        int Arc = 360 / NumeroBullets;
        for (int i = 0; i < 360; i += Arc)
        {
            GameObject bullet = Instantiate(PrefabBulletGota, transform.position, Quaternion.AngleAxis(i, Vector3.up));
            bullet.GetComponent<BulletMove>().speed = bulletSpeed;
        }

        agent.isStopped = false;
    }

    public float getProb()
    {
    	return Probabilidade;
    }

    public float getCD()
    {
    	return Cooldown;
    }
}
