using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMain : MonoBehaviour
{
    [SerializeField] protected int speed;
    [SerializeField] protected GameObject target;
    [SerializeField] protected int damage;
    private void Start()
    {
        CooldownThenDeath();
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void GetTargetandDam(GameObject target, int dam)
    {
        this.target = target;
        damage = dam;
    }

    private void Damage()
    {
        target.GetComponent<IDamageable>().AdjustHealth(-damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target)
        {
            Damage();
            Destroy(gameObject);
        }
    }

    IEnumerator CooldownThenDeath()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
