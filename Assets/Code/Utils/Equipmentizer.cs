using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Equipmentizer : MonoBehaviour
{
    //public GameObject target;

    // Use this for initialization
    void Start()
    {
        IEnumerable<SkinnedMeshRenderer> candidates = from SkinnedMeshRenderer s in transform.parent.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>()
                                           where s.transform.parent == transform.parent
                                           select s;
        SkinnedMeshRenderer targetRenderer = candidates.ElementAt(0);
        Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();
        foreach (Transform bone in targetRenderer.bones)
            boneMap[bone.gameObject.name] = bone;

        SkinnedMeshRenderer myRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        Transform[] newBones = new Transform[myRenderer.bones.Length];
        for (int i = 0; i < myRenderer.bones.Length; ++i)
        {
            GameObject bone = myRenderer.bones[i].gameObject;
            if (!boneMap.TryGetValue(bone.name, out newBones[i]))
            {
                Debug.Log("Unable to map bone \"" + bone.name + "\" to target skeleton.");
                break;
            }
        }
        myRenderer.bones = newBones;
    }
}
