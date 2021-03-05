using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            OnSceneLoaded();

            PlayerPrefs.SetInt("level", 0);
            runTime.currentindex = PlayerPrefs.GetInt("level", 0);
            runTime.cointcount=PlayerPrefs.GetInt("coin", 0);
        }
        else
        {
            if(instance!=this)
            {
                Destroy(gameObject);
            }
        }
        SetUpCollactableAction();
       
    }
    public RunTime runTime;
    public List<CollectableAction> collectableActions = new List<CollectableAction>();

    public void StartGame()
    {
        runTime.isgameComplated = false;
        runTime.isgameOver = false;
        runTime.isgameStarted = true;
        LevelManager.instance.Exacute();
        UIManager.instance.UpdatelevelNameText("LEVEL"+(runTime.currentindex+1));
        UIManager.instance.ShowHideGameFailedWindow(false);
        UIManager.instance.ShowHideGameComplatedScreen(false);
        UIManager.instance.UpdateCoinAmount(runTime.cointcount);

    }
    public IEnumerator GameComplatedStepFirst()
    {
        runTime.isgameComplated = true;
        AudioManager.instance.PlayAudio("win");
        yield return new WaitForSeconds(5f);
        UIManager.instance.ShowHideGameComplatedScreen(true);
        PlayerPrefs.SetInt("coin", runTime.cointcount);

    }
   
    public IEnumerator GameOver()
    {
        runTime.isgameOver = true;
        AudioManager.instance.PlayAudio("lose");
        yield return new WaitForSeconds(3.5f);
        UIManager.instance.ShowHideGameFailedWindow(true);
       
    }
    
    public void SetUpCollactableAction()
    {
        
        collectableActions.Add(new CollectableAction()
        {
            type = typeof(CollactableWood),
            action = (int x) => 
            {
                Charackter.instance.woodFoot.RiseFoot(x *0.25f);
                AudioManager.instance.PlayAudio("wood");
            }
        });
        collectableActions.Add(new CollectableAction()
        {
            type = typeof(CollactableCoin),
            action = (int x) =>
            {
                runTime.cointcount += x;
                AudioManager.instance.PlayAudio("coin");
                UIManager.instance.UpdateCoinAmount(runTime.cointcount);
            }
        });
    }
   
    public void LoadScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
      UIManager.instance.ShowHideLoading(true);
    }
    public void OnSceneLoaded()
    {
        SceneManager.sceneLoaded += (Scene x, LoadSceneMode arg1) =>
        {
            if (x.buildIndex == 0)
            {
                UIManager.instance.InGameToMenu();
            }
            else
            {
                UIManager.instance.MenuToIngame();
                StartGame();
            }
            UIManager.instance.ShowHideLoading(false);

        };
    }
    public void NextLevel()
    {
        runTime.currentindex++;
        if (runTime.currentindex>=GameData.instance.generalData.allLevels.Count)
        {
            runTime.currentindex = 0;
        }
        PlayerPrefs.SetInt("level", runTime.currentindex);
        LoadScene(1);
    }
    public void ReStartLevel()
    {
        LoadScene(1);
    }




    [System.Serializable]
   public struct RunTime
    {
        public bool  isgameStarted;
        public bool isgameOver;
        public bool isgameComplated;
        public int cointcount;
        public int currentindex;
        public float currentDistance;
        public float maxDistance;
        
     
        public float DistanceNormalized
        {
            get
            {
                return currentDistance / maxDistance;
            }
        }
          
    }
    public struct CollectableAction
    {
        public System.Type type;
        public System.Action<int> action;
    }
}
