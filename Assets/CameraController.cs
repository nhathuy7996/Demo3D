using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField]
    Vector3 offset = Vector3.zero;
    [SerializeField] float Speed;
    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - controller.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = controller.transform.position + offset;

        this.transform.position = Vector3.Lerp(this.transform.position, pos, Speed * Time.deltaTime);
    }
}
