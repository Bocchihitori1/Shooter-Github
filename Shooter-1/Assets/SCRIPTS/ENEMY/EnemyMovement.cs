using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int speed = 3;
    private Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        if (target != null)
        {
           transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
