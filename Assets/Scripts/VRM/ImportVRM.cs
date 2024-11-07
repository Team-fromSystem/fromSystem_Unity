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
        [SerializeField] private GameObject gameObject;

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
            var instance = await Vrm10.LoadPathAsync(path: @path, materialGenerator: new UrpVrm10MaterialDescriptorGenerator(),showMeshes:true);
            // this.gameObject=instance.gameObject;
            // gameObject.layer=LayerMask.NameToLayer("NewModel");
            // var children = new GameObject[gameObject.transform.childCount];
            // for(var i=0;i<children.Length;i++){
            //     var child = gameObject.transform.GetChild(i).gameObject;
            //     child.layer=LayerMask.NameToLayer("NewModel");
            // }
            // var newModel = instance.GetComponent<RuntimeGltfInstance>();
            // newModel.ShowMeshes();
        }
    }
}
