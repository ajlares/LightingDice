using UnityEngine;

public class DoorsMove : MonoBehaviour
{
    [SerializeField] private Transform point_a;
    [SerializeField] private Transform point_b;
    [SerializeField] private float speed;
    [SerializeField] private bool switchPoint;
    [SerializeField] private bool canMove;
    void Update()
    {
        if(canMove)
        {
            float step = speed * Time.deltaTime;
            if(switchPoint)
            {
                transform.position = Vector3.MoveTowards(transform.position, point_a.position, step);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, point_a.position, step);
            }
            
        }
    }
    public void switchSide()
    {
        
    }
}
