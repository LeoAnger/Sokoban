using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 测试
    public void btnFunc()
    {
        // animator.SetBool("targetInBox", !animator.GetBool("targetInBox"));
        Win win = FindObjectOfType<Win>();
        win.transform.position = new Vector3(8, -5);
    }
}
