using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public ParticleSystem dust;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    // A variable to store rotation
    Quaternion m_Rotation = Quaternion.identity;
    // Start is called before the first frame update

    void CreateDust()
    {
        dust.Play();
    }
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Set Movement Vector
        m_Movement.Set(horizontal, 0f, vertical);

        // Normalize for consistent diagonal movement
        m_Movement.Normalize();

        // Check if there is either horizontal or vertical input and set isWalking
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        if (isWalking)
        {
            CreateDust();
        }
        // Set the bool of the animator component accordingly
        m_Animator.SetBool("IsWalking", isWalking);

        // Create a Vector3 that stores the location of transform.forward rotated around towards m_Movement by an angle of turnSpeed radians * Time.deltaTime and a magnitude of 0f
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

       // Store Rotation
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    // Method to change how root motion is applied from the Animator
    void OnAnimatorMove()
    {
        // Moving RigidBody to new position + the m_movement vector multiplied by the magnitude of the Animator's deltaPosition
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);

        m_Rigidbody.MoveRotation(m_Rotation);
        
    }

}
