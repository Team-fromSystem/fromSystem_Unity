using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSingleton : Singleton<ModelSingleton>
{
    public List<ModelManager> modelData;
}
