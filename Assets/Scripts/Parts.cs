using UnityEngine;
using System.Collections.Generic;

public class Parts : MonoBehaviour
{
    private Transform FindChildByName(string ThisName, Transform ThisGObj)
    {
        Transform ReturnObj;

        // If the name match, we're return it
        if (ThisGObj.name == ThisName)
            return ThisGObj.transform;

        // Else, we go continue the search horizontaly and verticaly
        foreach (Transform child in ThisGObj)
        {
            ReturnObj = FindChildByName(ThisName, child);

            if (ReturnObj != null)
                return ReturnObj;
        }

        return null;
    }

    // Use this for initialization
    void Start()
    {
        var skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(true);
        var combineInstances = new CombineInstance[skinnedMeshRenderers.Length];
        int i = 0;

        var cmgo = new GameObject();
        cmgo.name = "_Combined Mesh";
        cmgo.transform.parent = transform;

        List<Transform> newBones = new List<Transform>();
        List<Material> newMaterials = new List<Material>();

        while (i < skinnedMeshRenderers.Length)
        {
            combineInstances[i].mesh = skinnedMeshRenderers[i].sharedMesh;
            //combineInstances[i].transform = skinnedMeshRenderers[i].transform.localToWorldMatrix;
            combineInstances[i].transform = Matrix4x4.identity;

            //

            // Bone 복제
            var rootBoneCloned = Instantiate(skinnedMeshRenderers[i].rootBone) as Transform;
            rootBoneCloned.name = rootBoneCloned.name.Replace("(Clone)", "");
            rootBoneCloned.parent = transform;

            foreach (var b in skinnedMeshRenderers[i].bones)
            {
                newBones.Add(FindChildByName(b.name, rootBoneCloned));
            }

            newMaterials.AddRange(skinnedMeshRenderers[i].sharedMaterials);
            
            Destroy(skinnedMeshRenderers[i].transform.parent.gameObject); //.SetActive(false);
            
            //for (int j = 0; j < combineInstances[i].mesh.boneWeights.Length; ++j)
            //{
            //    ++combineInstances[i].mesh.boneWeights[j].boneIndex0;
            //}
            i++;
        }

        cmgo.AddComponent<SkinnedMeshRenderer>();
        cmgo.GetComponent<SkinnedMeshRenderer>().sharedMesh = new Mesh();
        cmgo.GetComponent<SkinnedMeshRenderer>().sharedMesh.name = "Combined";
        cmgo.GetComponent<SkinnedMeshRenderer>().sharedMesh.CombineMeshes(combineInstances);
        cmgo.GetComponent<SkinnedMeshRenderer>().sharedMaterials = newMaterials.ToArray();
        cmgo.GetComponent<SkinnedMeshRenderer>().rootBone = transform.Find("Bip01");
        cmgo.GetComponent<SkinnedMeshRenderer>().bones = newBones.ToArray();
        //cmgo.transform.rotation = Quaternion.Euler(270, 0, 0);
        //transform.gameObject.SetActive(true);
        GetComponent<Animator>().avatar = AvatarBuilder.BuildGenericAvatar(gameObject, "Bip01");
        GetComponent<Animator>().avatar.name = "Combined Avatar";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
