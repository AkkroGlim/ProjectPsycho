using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int _MaxHealth;
    [SerializeField] private int _Health;

    public int CurrentHealth { get => _Health; private set => _Health = value; }
    public int MaxHealth { get => _MaxHealth; private set => _MaxHealth = value; }

    private EnemyStateMachine esm;
    private float speed;

    [SerializeField] private Rigidbody enemyRigid;
    [SerializeField] private EnemyMoveSo enemyMove;
    [SerializeField] private AudioSource enemyAudio;
    [SerializeField] private AudioClip[] enemyClips;

    public ManhuntState manhuntState;
    public AttackState attackState;
    public DeathState deathState;

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    void Start()
    {
        esm = new EnemyStateMachine();
        manhuntState = new ManhuntState(this, esm);
        attackState = new AttackState(this, esm);
        deathState = new DeathState(this, esm);

        OnDeath += Die;

        esm.Initialize(manhuntState);
        speed = enemyMove.Speed;
        CurrentHealth = MaxHealth;
    }


    private void Update()
    {
        esm.currentState.HandleInput();
        esm.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        esm.currentState.PhisicsUpdate();
    }

    public void Manhunt(float direction)
    {
        Vector3 targetVelocity = direction * speed * transform.forward * Time.deltaTime;
        targetVelocity.y = enemyRigid.velocity.y;
        enemyRigid.velocity = targetVelocity;
    }

    public void RemoveEnemyVelocity()
    {
        enemyRigid.velocity = Vector3.zero;
    }
    // Переделать под ScriptableObject все , что ниже
    public void TakeDamage(int Damage)
    {
        int damageTaken = Mathf.Clamp(Damage, 0, CurrentHealth);

        CurrentHealth -= damageTaken;
        
        if (damageTaken != 0)
        {
            enemyAudio.PlayOneShot(enemyClips[0]);
            OnTakeDamage?.Invoke(damageTaken);
        }

        if (CurrentHealth == 0 && damageTaken != 0)
        {
            enemyAudio.PlayOneShot(enemyClips[1]);
            OnDeath?.Invoke(transform.position);
        }
    }

    private void Die(Vector3 position)
    {
        
        enemyRigid.constraints = RigidbodyConstraints.None;
        enemyRigid.AddForce(Vector3.forward);
        esm.ChangeState(deathState);
    }
}
