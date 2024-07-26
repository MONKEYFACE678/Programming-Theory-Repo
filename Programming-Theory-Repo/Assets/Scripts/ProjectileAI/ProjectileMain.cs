using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMain : MonoBehaviour
{
    protected int speed;
    protected GameObject target;
    protected int damage;
    bool spent;
    protected int numEnemiesKill;
    private void Start()
    {
        StartCoroutine(CooldownThenDeath());
    }
    private void Update()
    {
        if (spent)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void GetTargetandDam(GameObject target, int dam)
    {
        this.target = target;
        damage = dam;
    }

    private void Damage()
    {
        if (!spent)
        {
            target.GetComponent<IDamageable>().AdjustHealth(-damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target)
        {
            Damage();
            numEnemiesKill--;
        }
        if(numEnemiesKill <= 0)
        {
            spent = true;
        }
    }

    IEnumerator CooldownThenDeath()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
