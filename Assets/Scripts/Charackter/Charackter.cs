using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Charackter : MonoBehaviour
{
    public static Charackter instance;
    [Header("Components")]
    public WoodFoot woodFoot;
    public Rigidbody rb;
    public CapsuleCollider capsuleCollider;
    public Animator anim;
    public float rightleftSpeed;
    public float forwardSpeed;
    public JoyStickControl joyStick;
    public AnimationListener animationListener;
    public Transform footsoleRight;
    public Transform footsoleLeft;
   

    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        SetUpAnimationListener();

        GameManager.instance.runTime.maxDistance = Vector3.Distance(LevelObjects.instance.finishPoint.position, transform.position);
    }
    public void SetUpAnimationListener()
    {
        animationListener.allAnims.Add(new AnimationListener.AnimationProfil(AnimationListener.AnimType.footstep, () => 
        {
            AudioManager.instance.PlayAudio("footstep");

        }));
    }

    public void Update()
    {
        if (GameManager.instance.runTime.isgameComplated || GameManager.instance.runTime.isgameOver || !GameManager.instance.runTime.isgameStarted) return;
       
        Movement(joyStick.Result);
        GameManager.instance.runTime.currentDistance = GameManager.instance.runTime.maxDistance- Vector3.Distance(LevelObjects.instance.finishPoint.position, transform.position);
        UIManager.instance.UpdateComplatedBar(GameManager.instance.runTime.DistanceNormalized);
    }
    public void Movement(float x=0)
    {
         
          
        rb.velocity = new Vector3(rightleftSpeed*x, rb.velocity.y, forwardSpeed);

        Vector3 target=rb.velocity.normalized;
          float target_y= Mathf.Atan2(target.x,target.z) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, target_y, 0), 0.05f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CollactableBase>())
        {
            other.GetComponent<CollactableBase>().CollectObject();

        }
        else if(other.GetComponent<Obstacle>())
        {

            ParticleManager.instance.ShowParticle("obstacle", other.transform.position);
            AudioManager.instance.PlayAudio("obstacle");
            woodFoot.FallFoot(0.25f*other.GetComponent<Obstacle>().amount);
            Destroy(other.gameObject);
            StartCoroutine(Trip());
        }
        else if(other.gameObject.CompareTag("finishpoint"))
        {
            ParticleManager.instance.ShowParticle("confetti", LevelObjects.instance.finishPoint.position);
            StartCoroutine(ComplateEvents());

        }
    }

    private IEnumerator ComplateEvents()
    {
        StartCoroutine(GameManager.instance.GameComplatedStepFirst());

        forwardSpeed = 0;
        yield return new WaitForSeconds(1f);
       List<LevelObjects.XESScore> findedxscores= LevelObjects.instance.allX.FindAll(x => x.minPoint <=Mathf.Abs(woodFoot.delay));
        if(findedxscores.Count>0)
        {
            woodFoot.LeftFootParent.SetParent(null);
            woodFoot.RightFootParent.SetParent(null);

            findedxscores.Sort((y, x) => x.minPoint.CompareTo(y.minPoint));
            Transform jumpoint = findedxscores[0].point;
            Vector3 targetpos = (jumpoint.position+transform.position)/2;
            targetpos += Vector3.up * 5f;
            woodFoot.ResetFoot();
            transform.DOMove(targetpos, 0.5f).OnComplete(() =>
            {
                transform.DOMove(jumpoint.position, 0.4f).OnComplete(() =>
                {
                    StartDance();
                });
            });
            yield return new WaitForSeconds(4f);
        }
        yield return new WaitForSeconds(1f);
    
    }


    public void StartDance()
    {
        anim.SetTrigger("dance");
    }
    public void Falling()
    {
        anim.SetTrigger("fall");
    }
    public IEnumerator Trip()
    {
        float tempspeed = forwardSpeed;
        forwardSpeed = 0;
        if ( woodFoot.delay > 0)
        {
            anim.SetTrigger("fall");
           
           StartCoroutine(GameManager.instance.GameOver());
            yield return new WaitForSeconds(3f);
        }
        else
        {
            anim.SetTrigger("trip");
            yield return new WaitForSeconds(1f);
            forwardSpeed = tempspeed;

        }

    }
   
}
