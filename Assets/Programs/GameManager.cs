using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool game_now = false;
    public static int game_end = -1;
    public static int p1invincible;
    public static int p2invincible;
    public static int p1lv;//lvの最大値は8
    public static int p2lv;
    public static int p1nextexp;//経験値量はレベルごとに+30
    public static int p2nextexp;
    public static int p1exp;//ボムは経験値を消費?
    public static int p2exp;
    public static int p1chain;
    public static int p2chain;

    void Awake()
    {
        Application.targetFrameRate = 30;
    }

    private void Start()
    {
        //仮
        PlayerController.p1hp = 600;
        P2Controller.p2hp = 600;
        PlayerController.p1score = 0;
        P2Controller.p2score = 0;
        p1invincible = 0;
        p2invincible = 0;
        p1exp = 0;
        p2exp = 0;
        p1nextexp = 30;
        p2nextexp = 30;
        p1lv = 0;
        p2lv = 0;
        p1chain = 0;
        p2chain = 0;
    }

    private void Update()
    {
        p1invincible--;
        p2invincible--;

        if (PlayerController.p1hp <= 0 && game_end == -1)
        {
            game_end = 120;
            P2Controller.p2score = (int)(P2Controller.p2score*1.5f);
        }
        if(P2Controller.p2hp <= 0 && game_end == -1)
        {
            game_end = 120;
            PlayerController.p1score = (int)(PlayerController.p1score * 1.5f);
        }
        if (game_end == 0)
        {

        }
        if (game_end > 1) game_end--;

        if (p1lv<8&&p1nextexp < p1exp)
        {
            p1lv++;
            p1exp = 0;
            p1nextexp += 30;
        }
        if (p2lv < 8 && p2nextexp < p2exp)
        {
            p2lv++;
            p2exp = 0;
            p2nextexp += 30;
        }
    }

    static public void Ebullethitp1()
    {
        if (GameManager.p1invincible > 0)
        {
            return;
        }

        if (PlayerController.p1hp > 400)
        {
            PlayerController.p1hp = 400;
        }
        else if (PlayerController.p1hp > 200)
        {
            PlayerController.p1hp = 200;
        }
        else
        {
            PlayerController.p1hp = 0;
        }

        GameManager.p1chain = 0;
        GameManager.p1invincible = 150;
    }

    static public void Ebullethitp2()
    {
        if (GameManager.p2invincible > 0)
        {
            return;
        }
        if (P2Controller.p2hp > 400)
        {
            P2Controller.p2hp = 400;
        }
        else if (P2Controller.p2hp > 200)
        {
            P2Controller.p2hp = 200;
        }
        else
        {
            P2Controller.p2hp = 0;
        }

        GameManager.p2chain = 0;
        GameManager.p2invincible = 150;
    }
}
