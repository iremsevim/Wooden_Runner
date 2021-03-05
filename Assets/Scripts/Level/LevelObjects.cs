using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjects : MonoBehaviour
{
    public static LevelObjects instance;
    public Transform charackterPoint;
    public List<Transform> woodPoints;
    public List<Transform> coinPoints;
    public Transform finishPoint;
    public List<XESScore> allX;

    private void Awake()
    {
        instance = this;
    }
    [System.Serializable]
    public class XESScore
    {
        public float minPoint;
        public Transform point;
    }
}
