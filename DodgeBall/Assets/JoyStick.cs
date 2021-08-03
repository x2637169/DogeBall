using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    public GameObject m_OnPad;// Joystick的GameObject
    public Image m_Bottom;// 背景图片
    public Image m_Stick;// 操纵杆图片
    private Vector3 m_BeginPos = Vector2.zero;// Joystick初始化位置
    private Vector2 m_CenterPos = Vector2.zero;
    public Vector3 m_Dir = Vector3.zero;// 最后计算相对的偏移距离，主角需要使用的位移值

    private float m_DisLimit = 1.0f;// 用于限定操纵杆在一定范围内
    private int m_MoveFingerId = -1;
    private bool isJoyStickEanbled = false;
    public bool isJoyStickCanUse = false;

    void Start()
    {
        m_DisLimit = m_Bottom.rectTransform.sizeDelta.x / 2;
        hideJoyStick();
    }

    void LateUpdate()
    {
        if (isJoyStickCanUse)
        {
            AroundByMobileInput();
        }
    }

    void AroundByMobileInput()
    {
        if (Input.touchCount > 0 && Input.touchCount <= 2)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.touches[i].phase == TouchPhase.Began)
                {
                    if (!EventSystem.current.IsPointerOverGameObject(Input.touches[i].fingerId))
                    {
                        if (!isJoyStickEanbled && Input.touches[i].position.x < Screen.width / 2 && Input.touches[i].position.y < Screen.height / 2)
                        {
                            m_MoveFingerId = Input.touches[i].fingerId;
                            m_BeginPos = Input.touches[i].position;
                            Debug.Log(m_BeginPos);
                            showJoyStick();
                        }
                    }
                }
                else if (Input.touches[i].phase == TouchPhase.Moved || Input.touches[i].phase == TouchPhase.Stationary)
                {
                    if (Input.touches[i].fingerId == m_MoveFingerId)
                    {
                        setStickCenterPos(Input.touches[i].position);
                    }
                }
                else if (Input.touches[i].phase == TouchPhase.Canceled || Input.touches[i].phase == TouchPhase.Ended)
                {
                    if (Input.touches[i].fingerId == m_MoveFingerId)
                    {
                        hideJoyStick();
                        m_MoveFingerId = -1;
                    }
                }
            }
        }
    }

    Vector2 convertTouchPosToUIPos(Vector2 touchPosition)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_OnPad.transform as RectTransform, touchPosition, null, out localPoint);
        return localPoint;
    }

    void showJoyStick()
    {
        m_Bottom.gameObject.SetActive(true);
        m_Stick.gameObject.SetActive(true);
        m_OnPad.SetActive(true);
        m_CenterPos = convertTouchPosToUIPos(m_BeginPos);
        Debug.Log(m_CenterPos);
        m_Bottom.rectTransform.localPosition = m_CenterPos;
        m_Stick.rectTransform.localPosition = m_CenterPos;
        m_Dir.x = 0;
        m_Dir.z = 0;
        m_Dir.Normalize();
        isJoyStickEanbled = true;
    }

    void hideJoyStick()
    {
        m_OnPad.SetActive(false);
        m_Bottom.rectTransform.localPosition = Vector2.zero;
        m_Stick.rectTransform.localPosition = Vector2.zero;

        m_Dir.x = 0;
        m_Dir.z = 0;
        m_Dir.Normalize();
        isJoyStickEanbled = false;
    }

    void setStickCenterPos(Vector2 touch)
    {
        Vector2 pos = convertTouchPosToUIPos(touch);

        float dis = Vector2.Distance(pos, m_CenterPos);
        if (dis > m_DisLimit)
        {
            Vector2 dir = pos - m_CenterPos;
            dir.Normalize();
            dir *= m_DisLimit;
            pos = m_CenterPos + dir;
        }
        m_Stick.transform.localPosition = pos;

        m_Dir.x = pos.x - m_CenterPos.x;
        m_Dir.z = pos.y - m_CenterPos.y;
        m_Dir.Normalize();
    }

}

//public class JoyStick : MonoBehaviour
//{
//    public GameObject joyStick;
//    public GameObject joyStickBG;
//    public Vector2 joyStickVector;
//    private Vector2 joyStickTouchPos;
//    private Vector2 joyStickOrigPos;
//    private float joyStickRadius;

//    // Start is called before the first frame update
//    void Start()
//    {
//        joyStickOrigPos = joyStickBG.transform.position;
//        joyStickRadius = joyStickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
//        Debug.Log(joyStickBG.GetComponent<RectTransform>().sizeDelta.y);
//    }

//    public void PointerDown()
//    {
//        joyStick.transform.position = Input.mousePosition;
//        joyStickBG.transform.position = Input.mousePosition;
//        joyStickTouchPos = Input.mousePosition;
//    }

//    public void Drag(BaseEventData _baseEventData)
//    {
//        PointerEventData pointerEventData = _baseEventData as PointerEventData;
//        Vector2 dragPos = pointerEventData.position;
//        joyStickVector = (dragPos - joyStickTouchPos).normalized;
//        float joyStickDist = Vector2.Distance(dragPos, joyStickTouchPos);

//        if (joyStickDist < joyStickRadius)
//        {
//            joyStick.transform.position = joyStickTouchPos + joyStickVector * joyStickDist;
//        }
//        else
//        {
//            joyStick.transform.position = joyStickTouchPos + joyStickVector * joyStickRadius;
//        }
//    }

//    public void PointerUP()
//    {
//        joyStickVector = Vector2.zero;
//        joyStick.transform.position = joyStickOrigPos;
//        joyStickBG.transform.position = joyStickOrigPos;
//    }

//}
