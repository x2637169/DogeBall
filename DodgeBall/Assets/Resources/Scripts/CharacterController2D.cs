using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Collider2D m_GroundCheckCollider;                // A collider that will be disabled when crouching

    [SerializeField] private bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    [SerializeField] private bool m_wasCrouching = false;


    [SerializeField] private ParticleSystem particleDead;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private PlayerAudioClip playerAudioClip;
    [SerializeField] private bool isDead = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_Grounded = IsGrounded();
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.05f;
        RaycastHit2D raycastHit = Physics2D.Raycast(m_GroundCheckCollider.bounds.center, Vector2.down, m_GroundCheckCollider.bounds.extents.y + extraHeight, m_WhatIsGround);
        //DrawIsGroundedRay(raycastHit, extraHeight);
        return raycastHit.collider != null;
    }

    private void DrawIsGroundedRay(RaycastHit2D raycastHit, float extraHeight)
    {
        Color rayColor;
        rayColor = raycastHit.collider == null ? Color.red : Color.green;
        Debug.DrawRay(m_GroundCheckCollider.bounds.center, Vector2.down * (m_GroundCheckCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.Log(raycastHit.collider);
    }

    public void Move(float move, bool crouch, bool jump)
    {
        if (!isDead)
        {
            // If the player should jump...
            if (m_Grounded)
            {
                m_wasCrouching = false;

                if (jump)
                {
                    playerAudio.PlayOneShot(playerAudioClip.Jump);
                    // Add a vertical force to the player.
                    m_Grounded = false;
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                }
            }

            // If the player should jump...
            if (!m_Grounded)
            {
                if (crouch)
                {
                    if (!m_wasCrouching)
                    {
                        m_Rigidbody2D.AddForce(new Vector2(0f, -m_JumpForce));
                        m_wasCrouching = true;
                    }
                }
            }

            //only control the player if grounded or airControl is turned on
            if ((m_Grounded || m_AirControl))
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
        }

    }

    public void Dead()
    {
        if (!isDead)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            m_Rigidbody2D.simulated = false;
            playerAudio.PlayOneShot(playerAudioClip.Dead);
            particleDead.Play();
            isDead = true;
            Destroy(this.gameObject, 1f);
        }
    }

    [System.Serializable]
    public class PlayerAudioClip
    {
        public AudioClip Jump;
        public AudioClip Dead;
    }

}