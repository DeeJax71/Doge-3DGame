using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColorChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] platforms; 

    // Start is called before the first frame update
    void Start()
    {
        PlatformColorChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlatformColorChange()
    {
        int randomNumber = Random.Range(0, 3), i = 0;

        foreach (MeshRenderer f in platforms)
        {
            switch (randomNumber)
            {
                case 0:
                    platforms[i].material.color = new Color(1f, 0, 0, 1f);
                    break;
                case 1:
                    platforms[i].material.color = new Color(0, 1f, 0, 1f);
                    break;
                case 2:
                    platforms[i].material.color = new Color(0, 0, 1f, 1f);
                    break;                
            }
            i++;
        }
    }
}
