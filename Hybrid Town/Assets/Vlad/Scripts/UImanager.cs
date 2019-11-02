using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Text stoneTxt;
    public Text woodTxt;
    public Text ironTxt;
    
    void Update()
    {
        stoneTxt.text = GameManager.gm.stone.ToString();
        woodTxt.text = GameManager.gm.wood.ToString();
        ironTxt.text = GameManager.gm.iron.ToString();
    }
}
