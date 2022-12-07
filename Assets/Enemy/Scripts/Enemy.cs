using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyStateMachine esm;
    private float speed;

    [SerializeField] private Rigidbody enemyRigid;
    [SerializeField] private EnemyData enemyData;

    public ManhuntState manhuntState;
    public AttackState attackState;

    void Start()
    {
        esm = new EnemyStateMachine();
        manhuntState = new ManhuntState(this, esm);
        attackState = new AttackState(this, esm);

        esm.Initialize(manhuntState);
        speed = enemyData.Speed;
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
}
