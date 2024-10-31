using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using VRM;
using UniGLTF;
using System.IO;



namespace UniVRM10
{
    public class ImportVRM : MonoBehaviour
    {
        [SerializeField] private string path;

        // Start is called before the first frame update
        void Start()
        {
            Load();
        }

        // Update is called once per frame
        void Update() { }

        async void Load()
        {
            Debug.Log(path);
            var instance =
        await Vrm10.LoadPathAsync(path: @path,
            materialGenerator: new UrpVrm10MaterialDescriptorGenerator());
        }
    }
}
