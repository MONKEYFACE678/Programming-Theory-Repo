using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMain : MonoBehaviour
{
    [SerializeField] protected int speed;
    [SerializeField] protected GameObject target;
    [SerializeField] protected int damage;
    bool spent;
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
            spent = true;
        }
    }

    IEnumerator CooldownThenDeath()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
