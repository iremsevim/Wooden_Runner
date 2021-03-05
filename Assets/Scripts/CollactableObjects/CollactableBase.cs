using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollactableBase : MonoBehaviour
{
    public int amount;
    public float rotateSpeed;
    public Vector3 rotateAxis;

    public virtual void CollectObject()
    {
        if (!GameManager.instance.collectableActions.Any(x => x.type == GetType())) return;
        GameManager.instance.collectableActions.Find(x => x.type == GetType()).action?.Invoke(amount);
        Destroy(gameObject);
    }
    public void Update()
    {
        transform.Rotate(rotateAxis * rotateSpeed * Time.deltaTime);
    }
}
