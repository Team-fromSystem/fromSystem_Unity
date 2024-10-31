// using Google.XR.ARCoreExtensions;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StreetscapeSpawner : MonoBehaviour
{
    // [SerializeField] private ARStreetscapeGeometryManager aRStreetscapeGeometryManager;
    // [SerializeField] private Material material;

    // readonly Dictionary<string, GameObject> instances = new();

    // void Start()
    // {
    //     aRStreetscapeGeometryManager.StreetscapeGeometriesChanged += OnChangeStreetscapeGeometry;
    // }

    // void OnChangeStreetscapeGeometry(ARStreetscapeGeometriesChangedEventArgs eventArgs)
    // {
    //     DestroyStreetscape(eventArgs.Removed);
    //     SpawnStreetscape(eventArgs.Added);
    //     UpdateStreetscape(eventArgs.Updated);
    // }

    // void SpawnStreetscape(List<ARStreetscapeGeometry> streetscapeGeometries)
    // {
    //     foreach (ARStreetscapeGeometry streetscapeGeometry in streetscapeGeometries)
    //     {
    //         var trackableId = streetscapeGeometry.trackableId.ToString();
    //         var renderObject = InstantiateStreetscapeGeometry(trackableId, streetscapeGeometry);
    //         instances.Add(trackableId, renderObject);
    //     }
    // }

    // void DestroyStreetscape(List<ARStreetscapeGeometry> streetscapeGeometries)
    // {
    //     foreach (ARStreetscapeGeometry streetscapeGeometry in streetscapeGeometries)
    //     {
    //         var trackableId = streetscapeGeometry.trackableId.ToString();
    //         if (!instances.ContainsKey(trackableId)) continue;

    //         var instance = instances[trackableId];
    //         DestroyInstance(instance);
    //         instances.Remove(trackableId);
    //     }
    // }

    // void UpdateStreetscape(List<ARStreetscapeGeometry> streetscapeGeometries)
    // {
    //     foreach (ARStreetscapeGeometry streetscapeGeometry in streetscapeGeometries)
    //     {
    //         var trackableId = streetscapeGeometry.trackableId.ToString();
    //         if (instances.ContainsKey(trackableId))
    //         {
    //             var renderObject = instances[trackableId];
    //             renderObject.transform.SetPositionAndRotation(streetscapeGeometry.pose.position, streetscapeGeometry.pose.rotation);
    //         }
    //     }
    // }

    // GameObject InstantiateStreetscapeGeometry(string name, ARStreetscapeGeometry geometry)
    // {
    //     GameObject renderObject = new(name, typeof(MeshFilter), typeof(MeshRenderer));
    //     renderObject.transform.SetPositionAndRotation(geometry.pose.position, geometry.pose.rotation);
    //     renderObject.GetComponent<MeshFilter>().mesh = geometry.mesh;
    //     var meshRenderer = renderObject.GetComponent<MeshRenderer>();
    //     Material[] materials = new Material[meshRenderer.materials.Length];
    //     Array.Fill(materials, material);
    //     meshRenderer.materials = materials;

    //     return renderObject;
    // }

    // void DestroyInstance(GameObject instance)
    // {
    //     if (instance.TryGetComponent<MeshFilter>(out var mesh))
    //     {
    //         Destroy(mesh);
    //     }
    //     if (instance.TryGetComponent<Renderer>(out var renderer))
    //     {
    //         foreach (var material in renderer.materials)
    //         {
    //             Destroy(material);
    //         }
    //     }
    //     Destroy(instance);
    // }
}