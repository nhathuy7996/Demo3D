using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] NavMeshPath path; 

    [SerializeField] Transform _playerTrans;

    [SerializeField] bool _isChase = false;
    [SerializeField] float _range = 1;
    float timer = 0;

    Vector3 _originPosition;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();

        _originPosition = this.transform.position;
        InvokeRepeating("Patrol", 1,2);
    }

    // Update is called once per frame
    void Update()
    {
        //Chaser();
        
    }


    void Patrol()
    {
        Vector3 pos = this.transform.position + new Vector3( Random.Range(-3,3), 0, Random.Range(-3,3));
        agent.SetDestination(pos);
    }

    void Chaser()
    {
        float distance = Vector3.Distance(_playerTrans.position, this.transform.position);
        _isChase = distance < _range;

        if (!_isChase)
        {
            timer += Time.deltaTime;
        }
        else
            timer = 0;

        if (timer >= 3)
        {
            agent.SetDestination(_originPosition);
            return;
        }

        agent.speed = 10;
        agent.SetDestination(_playerTrans.position);
        agent.CalculatePath(_playerTrans.position, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _range);
    }


}
