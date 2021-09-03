using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp_Animation : MonoBehaviour
{
    float add;
    private void OnEnable()
    {
        add = 0;
        this.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }
    void Update()
    {
        if (transform.localScale.x < 0.8f)
        {
            add += Time.deltaTime;
            this.transform.localScale = new Vector3(add + 0.4f, add + 0.4f, add + 0.4f);
        }
    }
}
