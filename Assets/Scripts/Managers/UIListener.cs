using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIListener : MonoBehaviour
{
     public void SceneLoadListener(int index)
    {
        switch(index)
        {
            case 0:
                GameManager.instance.LoadScene(0);
                break;
            case 1:
                GameManager.instance.LoadScene(1);
                break;
        }
    }
    public void InGameButtonClicks(int index)
    {
        switch(index)
        {
            case 0:
                GameManager.instance.NextLevel();
                break;
            case 1:
                GameManager.instance.ReStartLevel();
                break;
        }
    }

}
