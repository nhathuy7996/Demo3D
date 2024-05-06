using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;

    Rigidbody rb;
    [SerializeField] Vector3 movement;
    [SerializeField] float speed;
    Vector3 offSet;

    [SerializeField] 
    Vector3? _target = null;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        offSet = this.transform.position - Camera.main.transform.position;
        cam = Camera.main;

        animator = GetComponentInChildren<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        mouseControl();

        //if (movement.x != 0)
        //    animator.SetLayerWeight(1, 1);
        //else
        //    animator.SetLayerWeight(1, 0);

        //movement = this.transform.forward * Input.GetAxisRaw("Vertical") * speed;

        //this.transform.Rotate(new Vector3(0, Input.GetAxisRaw("Horizontal") * 10 * Time.deltaTime,0));

        //movement.y = this.rb.velocity.y;

        //Vector3 pos = this.transform.position - offSet;
        //pos.y = cam.transform.position.y;
        //pos.x = cam.transform.position.x;

        //this.rb.velocity = movement;

        //cam.transform.position = pos;
         
        if (_target == null)
        {
            //animator.SetTrigger("Idle");
            animator.SetLayerWeight(1, 0);
            return;
        }
        if (Vector3.Distance((Vector3)_target, this.transform.position) < 1.5f)
        {
            _target = null;
            animator.SetLayerWeight(1, 0);
            return;
        }

        Vector3 dir = ((Vector3)_target - this.transform.position).normalized;

        movement = dir * speed;
        movement.y = this.rb.velocity.y;
        this.rb.velocity = movement; 
        //animator.SetTrigger("Moving");
        animator.SetLayerWeight(1, 1);

        float angle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg;
        Quaternion quaternion = Quaternion.identity;
        quaternion.eulerAngles = new Vector3(0, angle, 0);
        this.transform.rotation = quaternion;
    }

    void mouseControl()
    {
        if (!Input.GetMouseButton(0))
            return;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if( !Physics.Raycast(ray, out hit))
        {
            _target = null;
            return;
        }

        Debug.Log(hit.point);
        _target = hit.point;

       
    }

    private void OnCollisionEnter(Collision collision)
    {
     
    }
}
