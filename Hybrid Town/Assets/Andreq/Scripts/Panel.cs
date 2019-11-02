using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    public abstract bool GetMessage(GameObject sender);
}
