using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }
    public void Exacute()
    {
        GameData.GeneralData.Levelprofil findedprofil = GameData.instance.generalData.allLevels[GameManager.instance.runTime.currentindex];
        GameObject cratedlevel = Instantiate(findedprofil.levelPrefab, Vector3.zero, Quaternion.identity);
        LevelObjects levelobject=cratedlevel.GetComponent<LevelObjects>();
        GameObject createdobject = Instantiate(GameData.instance.generalData.charackterprefab, levelobject.charackterPoint.position, Quaternion.identity);
        CameraController.instance.target = createdobject.transform;
        CameraController.instance.transform.position = createdobject.transform.position+Vector3.up*3f;
        foreach (var item in levelobject.woodPoints)
        {
            GameObject createdwood = Instantiate(GameData.instance.generalData.woodPrefab, item.position, Quaternion.identity);
        }
        foreach (var item in levelobject.coinPoints)
        {
            GameObject createdcoin = Instantiate(GameData.instance.generalData.cointprefab, item.position, Quaternion.identity);
        }
    }
     
    
}
