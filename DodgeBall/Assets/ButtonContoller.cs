using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonContoller : MonoBehaviour
{
    public Buttons buttons;

    public void EnableButton(GameObject _button, bool _enable)
    {
        _button.SetActive(_enable);
    }

    [System.Serializable]
    public class Buttons
    {
        public GameObject StartButton;
        public GameObject ReStatrButton;
        public GameObject Jump;
        public GameObject Crouch;
    }
}
