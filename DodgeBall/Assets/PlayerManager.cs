using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject respawnPlayerObj;
    [SerializeField] private List<GameObject> playerObj = new List<GameObject>();
    public MobileControl mobileControl;

    public void RespawnPlayer(int _count)
    {
        for (int i = 0; i < _count; i++)
        {
            var player = Instantiate(respawnPlayerObj, this.gameObject.transform, false);
            playerObj.Add(player);
        }
    }

    public void RemovePlayerList(GameObject _obj)
    {
        int i = playerObj.FindIndex(0, 1, x => _obj);
        if (playerObj.Count > 0 && i != -1)
        {
            playerObj.RemoveAt(i);
        }
    }

    public bool IsPlayerDead()
    {
        return playerObj.Count <= 0;
    }

    [System.Serializable]
    public class MobileControl
    {
        public Button buttonJump;
        public Button buttonCrouch;
        public JoyStick joyStick;
    }
}
