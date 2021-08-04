using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController2D playerController;
    [SerializeField] private JoyStick joyStick;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private Button buttonJump;
    [SerializeField] private Button buttonCrouch;
    [SerializeField] private bool isJump = false;
    [SerializeField] private bool isCrouch = false;
    private float horizontalMove = 0f;

    [SerializeField] private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        joyStick = playerManager.mobileControl.joyStick;
        buttonJump = playerManager.mobileControl.buttonJump;
        buttonCrouch = playerManager.mobileControl.buttonCrouch;
        AddButtonListener();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            isCrouch = true;
        }
#endif
        //#elif UNITY_ANDROID
        horizontalMove = joyStick.m_Dir.x * moveSpeed;
    }

    private void AddButtonListener()
    {
        buttonJump.onClick.AddListener(delegate () { isJump = true; });
        buttonCrouch.onClick.AddListener(delegate () { isCrouch = true; });
    }

    private void FixedUpdate()
    {
        playerController.Move(horizontalMove * Time.fixedDeltaTime, isCrouch, isJump);
        isCrouch = false;
        isJump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") || collision.CompareTag("Spike"))
        {
            playerController.Dead();
            playerManager.RemovePlayerList(this.gameObject);
        }
    }

}
