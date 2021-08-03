using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private Vector2 direction = Vector2.one.normalized;
    [SerializeField] private float speed;
    [SerializeField] private Collider2D ballCollider;
    [SerializeField] private SpriteRenderer ballSprite;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pongClip;
    [SerializeField] public OutRange outRange = new OutRange(0, 0, 0, 0);
    private bool canMove = false;

    private void Awake()
    {
        ballSprite.color = new Color(1, 1, 1, 0.5f);
        canMove = false;
        ballCollider.enabled = false;
        StartCoroutine(StartMove());
        float x = Random.Range(0.5f, 1);
        float y = Random.Range(0.5f, 1);
        x = Random.Range(0, 2) == 0 ? x : -x;
        y = Random.Range(0, 2) == 0 ? y : -y;
        direction = new Vector2(x, y);
    }

    private IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1f);
        ballSprite.color = new Color(1, 1, 1, 1f);
        ballCollider.enabled = true;
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            transform.Translate(direction * speed * Time.fixedDeltaTime);
        }

        if (OutOfRange())
        {
            BallManager ballManager = FindObjectOfType<BallManager>();
            if (ballManager != null)
            {
                ballManager.BallIsRemove();
                Destroy(this.gameObject);
            }
        }
    }

    private bool OutOfRange()
    {
        Vector3 pos = this.transform.position;
        return (pos.x < outRange.minX || pos.x > outRange.maxX || pos.y < outRange.minY || pos.y > outRange.maxY);
    }

    public void AddSpeed(float _additionalSpeed)
    {
        speed = _additionalSpeed;
        if (speed >= 20) speed = 20;
    }

    private void ReboundX()
    {
        direction.x = -direction.x;
    }

    private void ReboundY()
    {
        direction.y = -direction.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //bool isRebound = false;

        if (other.CompareTag("UpWall") || other.CompareTag("DownWall") || other.CompareTag("Ball"))
        {
            ReboundY();
            //isRebound = true;
        }

        if (other.CompareTag("LeftWall") || other.CompareTag("RightWall") || other.CompareTag("Ball"))
        {
            ReboundX();
            //isRebound = true;
        }

        //if (isRebound) PlayPongSound();
    }

    private void PlayPongSound()
    {
        audioSource.PlayOneShot(pongClip);
    }

    [System.Serializable]
    public class OutRange
    {
        public float minX = 0f;
        public float maxX = 0f;
        public float minY = 0f;
        public float maxY = 0f;

        public OutRange(float _minX, float _maxX, float _minY, float _maxY)
        {
            minX = _minX;
            maxX = _maxX;
            minY = _minY;
            maxY = _maxY;
        }
    }
}
