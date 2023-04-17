using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator m_Animator;
    Vector3 m_Movement;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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

        // Set the bool of the animator component accordingly
        m_Animator.SetBool("IsWalking", isWalking);
    }
}
