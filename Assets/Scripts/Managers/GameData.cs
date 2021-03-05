using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameData : MonoBehaviour
{
    public static GameData instance;
   
    public GeneralData generalData;

    public void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }
    [System.Serializable]
    public struct GeneralData
    {
        public List<Levelprofil> allLevels;
        public GameObject charackterprefab;
        public GameObject woodPrefab;
        public GameObject cointprefab;
        [System.Serializable]
        public class Levelprofil
        {
            public string levelID;
            public GameObject levelPrefab;
        }
    }
    


}
