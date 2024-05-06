using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] GameObject _prefabObstacle;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("ranObstacle",3);
        StartCoroutine(spawObstacle());
    }
       
    public void ranObstacle()
    {
        Vector3 pos = controller.transform.position;
        pos.x = Random.Range(-4f,4f);
        pos.z += Random.Range(18,25);
        Instantiate(_prefabObstacle, pos, Quaternion.identity);

        Invoke("ranObstacle", Random.Range(1f,3f));
    }

    IEnumerator spawObstacle()
    {
        do
        {
            Vector3 pos = controller.transform.position;
            pos.x = Random.Range(-4f, 4f);
            pos.z += Random.Range(18, 25);
            Instantiate(_prefabObstacle, pos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }while (true);
    }
}
