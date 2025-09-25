using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    public int keyCount;
    public Text keyText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keyText.text = keyCount.ToString();
    }
}
