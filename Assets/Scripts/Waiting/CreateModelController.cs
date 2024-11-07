using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniGLTF;
using System.IO;
using System.Threading.Tasks;

namespace UniVRM10
{
    public class CreateModelController : MonoBehaviour
    {
        async Task<GameObject> LoadVRM(string filePath)
        {
            var instance = await Vrm10.LoadPathAsync(path: @filePath, materialGenerator: new UrpVrm10MaterialDescriptorGenerator(),showMeshes:false);
            return instance.gameObject;
        }
    }
}
