using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickControl : MonoBehaviour
{
    public float Result
    {
        get
        {
            return result;
        }
    }
    private float result;

    private float minMoveDistance;
    private float maxDistance;
    private Vector3 firstPos;
    private Vector3 swipePos;
    public System.Action swipeUp;
    private bool canSwipe;

    public void Start()
    {
        minMoveDistance = Mathf.Sqrt(Screen.width * Screen.height) * 0.05f;
        maxDistance = minMoveDistance * 3;

    }
    public void Update()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                if (Input.touchCount > 0)
                {
                    Touch item = Input.GetTouch(0);
                    switch (item.phase)
                    {
                        case TouchPhase.Began:
                            firstPos = item.position;
                            swipePos = Input.mousePosition;
                            canSwipe = true;

                            break;
                        case TouchPhase.Moved:
                            float xdist = item.position.x - firstPos.x;
                            if (Mathf.Abs(xdist) > minMoveDistance)
                            {
                                result = Mathf.Clamp(xdist / maxDistance, -1, 1);
                                firstPos = Input.mousePosition;
                            }

                            if (canSwipe)
                            {
                                float ydis = item.position.y - swipePos.y;
                                Debug.Log(ydis + "in max :" + maxDistance);
                                if (ydis >= maxDistance)
                                {
                                    Debug.Log("irem");
                                    canSwipe = false;
                                    swipeUp?.Invoke();
                                }
                            }

                            break;
                        case TouchPhase.Ended:
                        case TouchPhase.Canceled:
                            result = 0;
                            break;
                    }
                }
                break;
            default:
                if (Input.GetMouseButtonDown(0))//tek dokunma
                {
                    firstPos = Input.mousePosition;
                    swipePos = Input.mousePosition;
                    canSwipe = true;
                }
                else if (Input.GetMouseButton(0))//sürükleme
                {
                    float xdist = Input.mousePosition.x - firstPos.x;

                    if (Mathf.Abs(xdist) > minMoveDistance)
                    {
                        result = Mathf.Clamp(xdist / maxDistance, -1, 1);
                        firstPos = Input.mousePosition;

                    }

                    if (canSwipe)
                    {
                        float ydis = Input.mousePosition.y - swipePos.y;
                        Debug.Log(ydis + "in max :" + maxDistance);
                        if (ydis >= maxDistance)
                        {
                            Debug.Log("irem");
                            canSwipe = false;
                            swipeUp?.Invoke();
                        }
                    }

                }
                else if (Input.GetMouseButtonUp(0))//kalkma
                {
                    result = 0;
                }
                break;
        }


    }

}
