using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    public List<AnimationProfil> allAnims;
    public void PlayAnim(AnimType animType)
    {
        allAnims.Find(x => x.type == animType)?.onChoosed?.Invoke();
    }

    [System.Serializable]
    public class AnimationProfil
    {
        public AnimType type;
        public System.Action onChoosed;
        public AnimationProfil(AnimType _type, System.Action _onChoosed)
        {
            type = _type;
            onChoosed = _onChoosed;
        }
    }
    public enum AnimType
    {
      footstep=0
    }
}
