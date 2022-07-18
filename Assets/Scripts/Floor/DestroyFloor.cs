using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFloor : MonoBehaviour
{
    float DestoryTime=0;
    bool IsOn=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOn)
        {
            DestoryTime+=Time.deltaTime;
            if(DestoryTime>=1)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            DestoryTime=0;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Charator")
        {
            IsOn=!IsOn;
        }
    }
}
