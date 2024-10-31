using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ImmersalData")]
public class ImmersalData : ScriptableObject
{
    public List<ImmersalManager> immersalManagers;
    public ImmersalManager chosenImmersalManager;
}
