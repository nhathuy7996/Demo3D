using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateManager))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] NavMeshPath path; 

    [SerializeField] Transform _playerTrans;

    [SerializeField] bool _isChase = false;
    [SerializeField] float _range = 1;
    float timer = 0;

    Vector3 _originPosition;

    [SerializeField] StateManager _stateManager; 

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();

        _originPosition = this.transform.position;
        _stateManager = GetComponent<StateManager>();
        _stateManager.ChangeState(new EnemyPatrolState(this));
    }

    // Update is called once per frame
    public void Update()
    {
        float distance = Vector3.Distance(_playerTrans.position, this.transform.position);
        Debug.LogError(distance);
        _isChase = distance < _range;

        if (_isChase)
        {
            _stateManager.ChangeState(new EnemyChaserState(this));
            timer = 0;
            return;
        }

        timer += Time.deltaTime;
        if(timer < 3)
            return;

        if (_stateManager.CurrentState != typeof(EnemyPatrolState)) {
            _stateManager.ChangeState(new EnemyReturnOrigin(this));
            return;
        }
 
    }

    public bool isReactTarget()
    {
        return Vector3.Distance(this.transform.position, agent.destination) <= 0.5f;
    }


    public void Patrol()
    {
        Vector3 pos = this.transform.position + new Vector3( Random.Range(-3,3), 0, Random.Range(-3,3));
        agent.SetDestination(pos);
        agent.CalculatePath(pos, path);
    }

    public void homeComing()
    {
        agent.speed = 2;
        agent.SetDestination(_originPosition);
        agent.CalculatePath(_originPosition, path);
    }

    public void Chaser()
    {   
        agent.speed = 10;
        agent.SetDestination(_playerTrans.position);
        agent.CalculatePath(_playerTrans.position, path);
    }
 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _range);
    }


}
