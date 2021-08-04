using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private Transform respawnParent;
    [SerializeField] private GameObject respawnObj;
    [SerializeField] private List<GameObject> balls;
    [SerializeField] private RespawnPosition respawnPosLeft;
    [SerializeField] private RespawnPosition respawnPosRight;
    [SerializeField] private RespawnPosition respawnPosUp;
    [SerializeField] private OutRange ballOutRange;
    [SerializeField] private float ballSpeed = 8f;
    [SerializeField] private float ballMaxSpeed = 20f;
    [SerializeField] private int ballMaxCount = 100;
    [SerializeField] private bool isBallRemove = false;

    /// <summary>
    /// 生成物件在自訂重生區域位置，並將此物件設為子物件
    /// </summary>
    public void AddBall(int _count)
    {
        Debug.Log("AddBall : " + _count);

        for (int i = 0; i < _count; i++)
        {
            if (balls.Count >= ballMaxCount) break;
            GameObject obj = Instantiate(respawnObj, GetPostiton(), Quaternion.identity, respawnParent);
            BallController ballController = obj.GetComponent<BallController>();
            ballController.outRange = new BallController.OutRange(ballOutRange.minX, ballOutRange.maxX, ballOutRange.minY, ballOutRange.maxY);
            ballController.AddSpeed(ballSpeed);
            balls.Add(obj);
        }
    }

    /// <summary>
    /// 增加所有物件的速度
    /// </summary>
    public void AddBallsSpeed(float _additionalSpeed)
    {
        Debug.Log("AddBallsSpeed : " + _additionalSpeed);
        ballSpeed += _additionalSpeed;
        if (ballSpeed > ballMaxSpeed) ballSpeed = ballMaxSpeed;
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<BallController>().AddSpeed(ballSpeed);
        }
    }

    public void BallIsRemove()
    {
        if (!isBallRemove)
        {
            isBallRemove = true;
            StartCoroutine(CheckIsBallRemove());
        }
    }


    private IEnumerator CheckIsBallRemove()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] == null)
            {
                Debug.Log(i);
                balls.RemoveAt(i);
                AddBall(1);
            }
        }

        isBallRemove = false;

        yield return null;
    }

    /// <summary>
    /// 隨機取得多個自訂重生區域的其中一個最大最小值，並返回隨機取得最大最小值的隨機亂數Vector3
    /// </summary>
    /// <returns>Vector3</returns>
    public Vector3 GetPostiton()
    {
        Vector3 direction = Vector3.zero;
        int i = Random.Range(0, 3);
        RespawnPosition respawnPos;

        switch (i)
        {
            case 0:
                respawnPos = respawnPosLeft;
                break;
            case 1:
                respawnPos = respawnPosRight;
                break;
            case 2:
                respawnPos = respawnPosUp;
                break;
            default:
                respawnPos = respawnPosLeft;
                break;
        }

        if (respawnPos != null)
        {
            float x = Random.Range(respawnPos.minX, respawnPos.maxX);
            float y = Random.Range(respawnPos.minY, respawnPos.maxY);
            direction = new Vector3(x, y, 0);
        }

        return direction;
    }

    [System.Serializable]
    public class RespawnPosition
    {
        public float minX, maxX;
        public float minY, maxY;
    }

    [System.Serializable]
    public class OutRange
    {
        public float minX = 0f;
        public float maxX = 0f;
        public float minY = 0f;
        public float maxY = 0f;
    }

}