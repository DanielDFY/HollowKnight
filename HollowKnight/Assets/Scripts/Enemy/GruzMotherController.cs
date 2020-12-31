using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GruzMotherController : EnemyController
{
    public float speed = 1f;
    private int animationScaredTrigger;
    private EnemyState currentState;
    private Vector3 destination;
    public Transform[] crashPoints;
    private bool isDesc;//冲撞顺序,是往左冲还是往右冲,因为冲撞点按顺序上下交错排列
    private bool isFacingLeft;//是否面朝左
    private int currentCrashPoint, crashCount, maxCrashCount;
    public float behaveIntervalLeast;
    public float behaveIntervalMost;
    private Transform _playerTransform;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PolyNavAgent agent {
        get { return _agent != null ? _agent : _agent = GetComponent<PolyNavAgent>(); }
    }
    private PolyNavAgent _agent;
    //敌人状态枚举变量
    public enum EnemyState 
    {
        SLEEP, FLY, ATTACK_READY, ATTACK_PATHFINDING, ATTACK, CRASH, DEAD,
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // Debug.Log(_playerTransform.position);
        // _playerTransform = GlobalController.Instance.player.GetComponent<Transform>();
        _transform = gameObject.GetComponent<Transform>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animationScaredTrigger = Animator.StringToHash("Scared");
        Debug.Log(_animator);
    }
    private void OnEnable() {
        if(currentState == EnemyState.SLEEP) {
            SwitchState(EnemyState.FLY);
        }
        agent.OnDestinationReached += WhenDesinationReached;
    }

    private void OnDisable() {
        agent.OnDestinationReached -= WhenDesinationReached;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentState);
        if(health <= 0) {
            Debug.Log("Enemy is Dead Now");
            if(currentState != EnemyState.DEAD) {
                SwitchState(EnemyState.DEAD);
            }

            return;
        } else {
            // if(currentState == EnemyState.SLEEP && Math.Abs(_playerTransform.position.x - _transform.position.x) <= 1) {
            //     SwitchState(EnemyState.FLY);
            // }
            //可能因为玩家动了,boss也要改变方向
            if(currentState == EnemyState.FLY) {
                if(_transform.localScale.x < 0) {
                    isFacingLeft = true;
                } else if(_transform.localScale.x > 0){
                    isFacingLeft = false;
                }
                if(_playerTransform.position.x > _transform.position.x && isFacingLeft) {
                    Flip();
                } else if(_playerTransform.position.x < _transform.position.x && !isFacingLeft) {
                    Flip();
                }
            }
            // if(Math.Abs(_transform.position.x - destination.x)<=1 && Math.Abs(_transform.position.y - destination.y)<=2) {
            //     WhenDesinationReached();
            // } else if(currentState == EnemyState.FLY || currentState == EnemyState.SLEEP) {
            //     Vector2 newVec = _rigidbody.velocity;
            //     float xForward = destination.x - _transform.position.x;
            //     float yForward = destination.y - _transform.position.y;
            //     newVec.x = xForward * speed;
            //     newVec.y = yForward * speed;
            //     _rigidbody.velocity = newVec;
            // }
        }
        
    }

    void Flip() {
        Vector3 vector = _transform.localScale;
        vector.x = vector.x *= -1;
        _transform.localScale = vector;
    }

    public void SwitchState(EnemyState state)
    {
        switch (currentState)
        {
            case EnemyState.SLEEP:
                ExitSleepState();
                break;
            case EnemyState.FLY:
                ExitFlyState();
                break;
            case EnemyState.ATTACK_READY:
                ExitAttackReadyState();
                break;
            case EnemyState.ATTACK_PATHFINDING:
                ExitAttackPathFindingState();
                break;
            case EnemyState.ATTACK:
                ExitAttackState();
                break;
            case EnemyState.CRASH:
                ExitCrashState();
                break;
            case EnemyState.DEAD:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case EnemyState.SLEEP:
                EnterSleepState();
                break;
            case EnemyState.FLY:
                EnterFlyState();
                break;
            case EnemyState.ATTACK_READY:
                EnterAttackReadyState();
                break;
            case EnemyState.ATTACK_PATHFINDING:
                EnterAttackPathFindingState();
                break;
            case EnemyState.ATTACK:
                EnterAttackState();
                break;
            case EnemyState.CRASH:
                EnterCrashState();
                break;
            case EnemyState.DEAD:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void EnterSleepState()
    {

    }

    private void ExitSleepState()
    {
        GetComponent<Collider2D>().isTrigger = false;
        _animator.SetTrigger("Scared");

        destination = _playerTransform.position + Vector3.up * 6;
    }

    private void EnterFlyState()
    {
        agent.maxSpeed = 3.5f;
        Debug.Log(_playerTransform.position + Vector3.up*5);
        agent.SetDestination(_playerTransform.position + Vector3.up*5);
        Debug.Log(agent.activePath);
    }

    private void ExitFlyState()
    {

    }

    private void EnterAttackReadyState()
    {
        _animator.SetTrigger("AttackReady");
        float minDistance = 0;
        for (int i = 0; i < crashPoints.Length; i++)
        {
            if (i % 2 == 0)
            {
                float distance = Vector2.Distance(_transform.position, crashPoints[i].position);
                if (minDistance == 0 || distance < minDistance)
                {
                    minDistance = distance;
                    currentCrashPoint = i;
                }
            }
        }

        // 冲撞次数归零
        crashCount = 0;
        // 冲撞计数
        crashCount++;
        // 设定目的地
        agent.SetDestination(crashPoints[currentCrashPoint].position);
        agent.maxSpeed = 50;
        
        // 根据角色横轴相对方位，决定冲撞顺序
        if (_playerTransform.position.x > _transform.position.x)
        {
            //往右冲,往大的冲撞点冲
            isDesc = false;
        }
        else
        {
            //往左冲
            isDesc = true;
        }
    }

    private void ExitAttackReadyState()
    {
        if(currentState == EnemyState.ATTACK_READY) {
            agent.Stop();
        }
    }

    private void EnterAttackPathFindingState()
    {
        crashCount++;
        if (crashCount > maxCrashCount)
        {
            SwitchState(EnemyState.FLY);
            return;
        }
        // 根据顺序或倒序决定下次冲撞点
        if (isDesc)
        {
            currentCrashPoint--;
        }
        else
        {
            currentCrashPoint++;
        }
        if (!isDesc && currentCrashPoint >= crashPoints.Length)
        {
            isDesc = true;
            currentCrashPoint = crashPoints.Length - 2;
        }
        else if (isDesc && currentCrashPoint < 0)
        {
            isDesc = false;
            currentCrashPoint = 1;
        }
        Debug.Log(currentCrashPoint);
        agent.SetDestination(crashPoints[currentCrashPoint].position);
        // destination = crashPoints[currentCrashPoint].position;
    }

    private void ExitAttackPathFindingState()
    {

    }

    private void EnterAttackState()
    {
        if (currentCrashPoint % 2 == 0)
        {
            // coliisionEffect.transform.localRotation = Quaternion.Euler(-180, 0, 0);
        }
        else
        {
            // coliisionEffect.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        _animator.SetTrigger("Attack");
    }

    private void ExitAttackState()
    {

    }

    private void EnterCrashState()
    {

    }

    private void ExitCrashState()
    {

    }

    private void EnterDeadState()
    {
        GetComponent<PolyNavAgent>().enabled = false;
        _animator.SetTrigger("Dead");
    }

    private void ExitDeadState()
    {

    }

    public override float behaveInterval()
    {
        return UnityEngine.Random.Range(behaveIntervalLeast, behaveIntervalMost);
    }

    protected override void die() {
        SwitchState(EnemyState.DEAD);
    }
    public override void hurt(int damage) {
        if(currentState == EnemyState.SLEEP) {
            SwitchState(EnemyState.FLY);
        }
        health = Math.Max(health - damage, 0);

    }
    private void OnCollisionEnter2D(Collision2D collision) {
        string layerName = LayerMask.LayerToName(collision.collider.gameObject.layer);
        Debug.Log("hit");
        Debug.Log(layerName);
        
        if(currentState == EnemyState.SLEEP && layerName == "Player") {
            Debug.Log("sleep to fly");
            SwitchState(EnemyState.FLY);
        }
    }
    private void WhenDesinationReached() {
        Debug.Log("reached");
        Debug.Log(currentState);
        switch (currentState)
        {
            case EnemyState.FLY:
                SwitchState(EnemyState.ATTACK_READY);
                break;
            case EnemyState.ATTACK_READY:
                // SwitchState(EnemyState.ATTACK_PATHFINDING);
                // break;
            case EnemyState.ATTACK_PATHFINDING:
                SwitchState(EnemyState.ATTACK);
                break;
        }
    }
}
