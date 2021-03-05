using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    public List<ParticleProfile> particleProfiles;

    public void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
       
    }
    public void ShowParticle(string ID,Vector3 target)
    {
        Debug.Log("Particle");
      ParticleProfile findedparticle=particleProfiles.Find(x => x.ID == ID);
        GameObject created = Instantiate(findedparticle.particle, target, Quaternion.identity);
        Destroy(created.gameObject, 2f);
    }


    [System.Serializable]
    public struct ParticleProfile
    {
        public string ID;
        public GameObject particle;
    }

}
