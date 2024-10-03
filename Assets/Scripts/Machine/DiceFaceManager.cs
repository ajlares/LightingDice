using UnityEngine;

public class DiceFaceManager : MonoBehaviour
{
    [SerializeField] private int backFaceValeu;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float fresseTime;
    [SerializeField] private float indexTime;
    [SerializeField] private bool isGrounded = false;


    private void FixedUpdate() 
    {
        if(isGrounded)
        {
            if(indexTime > fresseTime)
            {
                GameManager.instance.DicePushBack(backFaceValeu);
                FreesePosition();
                isGrounded = false;
            }
            else
            {
                indexTime += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            isGrounded = true;
        }    
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Ground"))
        {
            isGrounded = false;
        }  
    }
    private void FreesePosition()
    {
        rb.freezeRotation = true;
    }
}
