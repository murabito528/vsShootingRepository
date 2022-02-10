using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSetting : MonoBehaviour
{
    bool game_now = false;
    void Awake()
    {
        Application.targetFrameRate = 30;
    }

    private void Start()
    {
        //‰¼
        PlayerController.p1hp = 500;
        P2Controller.p2hp = 500;
    }
}
