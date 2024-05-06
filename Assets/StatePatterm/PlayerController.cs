using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateTest
{
    [RequireComponent(typeof(StateManager))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] StateManager _stateManager;
        [SerializeField] float _speed = 10; 
        // Start is called before the first frame update
        void Start()
        {
            _stateManager = this.GetComponent<StateManager>();
            _stateManager.ChangeState(new MovingState(this));
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                _stateManager.ChangeState(new FlyingState(this));

            if (Input.GetKeyDown(KeyCode.Escape))
                _stateManager.ChangeState(new MovingState(this));
        }

        public void Moving(Vector3 movement)
        {
            this.transform.Translate(movement * Time.deltaTime * _speed);
        }

        public void Flying(Vector3 movement)
        {
            this.transform.Translate(movement * Time.deltaTime * _speed);
        }
    }
}

