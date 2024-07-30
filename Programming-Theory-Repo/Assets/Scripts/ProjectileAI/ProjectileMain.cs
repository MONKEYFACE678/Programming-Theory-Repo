using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMain : MonoBehaviour
{
    protected int speed;
    protected string targetTag;
    protected int damage;
    float cooldown;
    bool spent;
    bool onCooldown;
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

    public void GetTargetandDam(string targetTag, int dam, float cooldown)
    {
        this.cooldown = cooldown;
        this.targetTag = targetTag;
        damage = dam;
    }

    private void Damage(GameObject target)
    {
        if (!spent)
        {
            target.GetComponent<IDamageable>().AdjustHealth(-damage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!onCooldown)
        {
            StartCoroutine(Cooldown());
            if (other.gameObject.CompareTag(targetTag))
            {
                Damage(other.gameObject);
                numEnemiesKill--;
            }
        }
        if (numEnemiesKill <= 0)
        {
            spent = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!onCooldown)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                Damage(other.gameObject);
                numEnemiesKill--;
            }
            StartCoroutine(Cooldown());
        }
        if (numEnemiesKill <= 0)
        {
            spent = true;
        }
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    IEnumerator CooldownThenDeath()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
