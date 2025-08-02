using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    private new Rigidbody rigidbody;
    private Animator animator;

    private Vector3 inputAxes;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (DialogueManager.instance.isActive)
        {
            inputAxes = Vector3.zero;

            return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        inputAxes = new Vector3(x, 0f, y);
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = inputAxes.normalized * speed;
        Vector3 deltaVelocity = targetVelocity - rigidbody.linearVelocity;
        float rate = deltaVelocity.sqrMagnitude > 0.1f ? acceleration : deceleration;

        rigidbody.AddForce(deltaVelocity * rate, ForceMode.Acceleration);

        Vector3 currentVelocity = rigidbody.linearVelocity;
        float velocityMagnitude = currentVelocity.magnitude;

        animator.SetFloat("Speed", velocityMagnitude);
        animator.SetFloat("MoveX", currentVelocity.x);
        animator.SetFloat("MoveY", currentVelocity.z);

        Vector3 moveDirection = currentVelocity / velocityMagnitude; // Dividing since magnitude is already calculated.

        if (velocityMagnitude > 0.1)
        {
            animator.SetFloat("LastDirectionX", moveDirection.x);
            animator.SetFloat("LastDirectionY", moveDirection.z);
        }
    }
}