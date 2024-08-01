using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

abstract public class CharacterMain : MonoBehaviour, IDamageable
{
    bool onCooldown;
    protected float maxHealth;
    [SerializeField] protected float health;
    protected int coolDownLength;
    protected float projCoolDown;
    protected string targetName;
    protected int minDamage;
    protected int maxDamage;
    protected int speed;
    protected bool closeToTarget;
    protected bool farToTarget;
    //only protected temp remember to private
    protected NavMeshAgent agent;
    [SerializeField] protected GameObject target;
    protected float distanceFromTarget;
    protected float prefDist;
    public bool isDead;
    private Vector3 dirToTarget;
    private Slider healthBarSlider;
    GameObject healthBar;
    GameManager gameManager;

    public void AdjustHealth(int damage)
    {
        health += damage;
    }

    //make private and nonvirtual when not temp
    protected virtual void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.autoBraking = false;
        agent.stoppingDistance = 0;
        health = maxHealth;
        HealthBarInit();
        FindTarget();
    }

    virtual protected void Update()
    {
        if(health <=0)
        {
            Die();
        }
        if (FindTarget())
        {
            distanceFromTarget = Vector3.Distance(target.transform.position, gameObject.transform.position);

            if (distanceFromTarget <= prefDist - 2)
            {
                closeToTarget = true;
            }
            else
            {
                closeToTarget = false;
            }

            if (distanceFromTarget >= prefDist + 2)
            {
                farToTarget = true;
            }
            else
            {
                farToTarget = false;
            }

            if (farToTarget)
            {
                agent.isStopped = false;
                agent.destination = target.transform.position;
            }
            else if (closeToTarget)
            {
                dirToTarget = transform.position - target.transform.position;
                agent.destination = transform.position + dirToTarget;
                agent.isStopped = false;
            }
            else
            {
                agent.isStopped = true;
            }
            if (!onCooldown)
            {
                StartCoroutine(CooldownWithAttack());
            }
            RotateTowardsTarget();

            if (target.GetComponent<CharacterMain>().isDead)
            {
                target = null;
            }
        }
        HealthBarHandling();
        if (isDead)
        {
            gameManager.killNum++;
            Destroy(gameObject);
        }
    }


    protected void Damage()
    {
        target.GetComponent<IDamageable>().AdjustHealth(-RandomizeAttackDam());
    }


    private bool FindTarget()
    {
        if (target)
        {
            return true;
        }
        if (GameObject.FindGameObjectWithTag(targetName) != null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetName);
            target = enemies[Random.Range(0, enemies.Length)];
            return true;
        }
        return false;
    }

    protected int RandomizeAttackDam()
    {
        int dam = Random.Range(minDamage, maxDamage + 1);
        return dam;
    }

    protected abstract void Attack(GameObject target);

    protected IEnumerator CooldownWithAttack()
    {
        onCooldown = true; // Set the cooldown flag
        yield return new WaitForSeconds(coolDownLength);
        if (target)
        {
            Attack(target);
        }
        onCooldown = false; // Reset the cooldown flag
    }

    protected void RotateTowardsTarget()
    {
        if (target != null)
        {
            Vector3 targetDir = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(targetDir);
            transform.rotation = rotation;
        }
    }

    protected void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            gameManager.AdjustMana(3);
        }
        Destroy(healthBar);
        isDead = true;
    }

    private void HealthBarInit()
    {
        if (gameObject.CompareTag("Skeleton"))
        {
            healthBar = (GameObject)Resources.Load("Prefabs/Skele Health bar");
        }
        else
        {
            healthBar = (GameObject)Resources.Load("Prefabs/Enemy Health bar");
        }
        healthBar = Instantiate(healthBar, GameObject.FindGameObjectWithTag("Canvas").transform);
        healthBarSlider = healthBar.GetComponent<Slider>();
        healthBarSlider.maxValue = maxHealth;
    }

    private void HealthBarHandling()
    {
        healthBarSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0));
        healthBarSlider.value = health;
    }
}
