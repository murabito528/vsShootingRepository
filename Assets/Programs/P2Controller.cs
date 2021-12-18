using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Controller : MonoBehaviour
{
    public static int[][] risk;
    void Start()
    {
        risk = new int[30][];
        for (int i = 0; i < 30; i++) {
            risk[i] = new int[50];
        }
    }
    
    void Update()
    {
        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                risk[i][j] = 0;
            }
        }
    }

    void LateUpdate()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        for(int i = 0; i < 30; i++)
        {
            for(int j = 0; j < 50; j++)
            {
                Gizmos.color = new Color(1, 0, 0, Mathf.Clamp01(risk[i][j] * 0.1f));
                Gizmos.DrawCube(new Vector2(-3 + 0.2f*i+0.1f, -5 + 0.2f*j+0.1f), new Vector2(0.19f, 0.19f));
            }
        }
    }
}
