using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    public int keyCount;
    public Text keyText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keyText.text = keyCount.ToString();

        if (keyCount == 3)
        {
            Debug.Log("You Win!");
        }
    }
}
