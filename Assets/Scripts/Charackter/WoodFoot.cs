using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFoot : MonoBehaviour
{
    public Transform LeftFootParent;
    public Transform RightFootParent;
    public Transform rightWoodFoot;
    public Transform leftWoodFoot;

    private float originalHeight;
    private Vector3 originalCenter; 

    public float delay;

    

    private void Start()
    {
        originalHeight = Charackter.instance.capsuleCollider.height;
        originalCenter = Charackter.instance.capsuleCollider.center;
    }

    public void RiseFoot(float amount)
    {
        delay -= amount * .5f;

        rightWoodFoot.localScale += (Vector3.up * amount);
        // rightWoodFoot.position -= (Vector3.up * amount*0.5f);
     
      
        leftWoodFoot.localScale += (Vector3.up * amount);
       // leftWoodFoot.position -= (Vector3.up * amount * 0.5f);
      
        Charackter.instance.capsuleCollider.height += amount * 1f;
        Charackter.instance.capsuleCollider.center -= (Vector3.up * amount * 0.5f);


    }

    public void ResetFoot()
    {
        if (delay >= 0) return;
        FallFoot(Mathf.Abs(delay*2));
        Charackter.instance.capsuleCollider.height = originalHeight;
        Charackter.instance.capsuleCollider.center = originalCenter;
    }


    public void Update()
    {
        rightWoodFoot.localPosition = new Vector3(0, delay, 0);
        leftWoodFoot.localPosition = new Vector3(0, delay, 0);
    }
    public void FallFoot(float amount)
    {
        delay += amount * .5f;


        rightWoodFoot.localScale -= (Vector3.up * amount);
        //rightWoodFoot.position += (Vector3.up * amount * 0.5f);

        leftWoodFoot.localScale -= (Vector3.up * amount);
        // leftWoodFoot.position += (Vector3.up * amount * 0.5f);

        Charackter.instance.capsuleCollider.height -= amount * 1f;
        Charackter.instance.capsuleCollider.center += (Vector3.up * amount * 0.5f);

       
      
    }
}
