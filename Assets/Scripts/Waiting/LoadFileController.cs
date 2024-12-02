using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniGLTF;
using System.IO;
using System.Threading.Tasks;

namespace UniVRM10
{
    public class LoadFileController : MonoBehaviour
    {
        public async Task<GameObject> LoadVRM(string filePath)
        {
            var instance = await Vrm10.LoadPathAsync(path: @filePath, materialGenerator: new UrpVrm10MaterialDescriptorGenerator());
            return instance.gameObject;
        }
        public async Task<Texture2D> LoadImage(string filePath){
            byte[] data=File.ReadAllBytes(filePath);
            Texture2D texture=new Texture2D(1,1);
            texture.LoadImage(data);
            texture.Apply();
            return texture;
        }
    }
}
