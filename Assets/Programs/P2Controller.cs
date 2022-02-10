using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Node
{
    public int x, y;
    public int risk;
    public Node parentnode;
    public int state;//0:null 1:open 2:close
    public int totalcost;
    public Node(int def_x,int def_y,Node def_parent)
    {
        x = def_x;
        y = def_y;
        risk = 0;
        state = 0;
        totalcost = 0;
        parentnode = def_parent;
    }
}

public class P2Controller : MonoBehaviour
{
    Transform tf;
    Vector3 vec_tmp;
    public static Node[][] node;
    public static int[][] split_risk;
    public static int p2hp;
    void Start()
    {
        tf = transform;
        vec_tmp.z = 1;
        route = new List<Node>();
        opennode = new List<Node>();
        closenode = new List<Node>();

        node = new Node[30][];
        for (int i = 0; i < 30; i++) {
            node[i] = new Node[50];
            for(int j = 0; j < 50; j++)
            {
                node[i][j] = new Node(i, j, null);
            }
        }
        goalnode = new Node((int)Random.Range(0, 29), (int)Random.Range(0, 49), null);

        split_risk = new int[6][];
        for(int i = 0; i < 6; i++)
        {
            split_risk[i] = new int[10];
            for(int j = 0; j < 10; j++)
            {
                split_risk[i][j] = 0;
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                node[i][j].risk = 0;
                node[i][j].totalcost = 0;
                node[i][j].state = 0;
                node[i][j].parentnode = null;
            }
        }

        if (route.Count > 0) {
            //Debug.Log("goal" + goalnode.x + "," + goalnode.y + "," + route.Count);
            //Debug.Log("moveTo" + route[0].x + "," + route[0].y + "," + route.Count);
            vec_tmp.x = (float)route[0].x / 5 - 3;
            vec_tmp.y = (float)route[0].y / 5 - 5;
            vec_tmp.z = 1;
            tf.position = vec_tmp;
            //Debug.Log("moveTo" + vec_tmp);
            route.RemoveAt(0);
        }
    }

    List<Node> route;
    List<Node> opennode;
    List<Node> closenode;
    Node startnode;
    Node goalnode;
    Node nownode;
    int frame;

    void LateUpdate()
    {
        if (frame > 5)
        {
            //goalnode = new Node((int)Random.Range(0, 29), (int)Random.Range(0, 49), null);
            findwaypoint();
            frame = 0;
        }
        else
        {
            frame++;
            return;
        }

        startnode = node[(int)Mathf.Clamp(Mathf.Round((tf.position.x + 3) * 5), 0, 29)][(int)Mathf.Clamp((int)Mathf.Round((tf.position.y + 5) * 5), 0, 49)];
        if (goalnode.x != startnode.x && goalnode.y != startnode.y)
        {
            opennode.Clear();
            closenode.Clear();
            route.Clear();

            startnode.state = 1;
            opennode.Add(startnode);
            nownode = opennode[0];

            while (goalnode != nownode && opennode.Count > 0)
            {
                nownode = opennode[0];
                //Debug.Log(nownode.x+","+nownode.y);
                for (int i = 1; i < opennode.Count; i++)
                {
                    if (opennode[i].totalcost < nownode.totalcost)
                    {
                        nownode = opennode[i];
                    }
                }
                if (nownode.x == goalnode.x && nownode.y == goalnode.y)
                {
                    goalnode = nownode;
                    //Debug.Log("goal");
                    break;
                }

                if (nownode.x < 29)//‰E
                {
                    if (node[nownode.x + 1][nownode.y].state == 0)
                    {
                        int dx = Mathf.Abs((nownode.x + 1) - goalnode.x);
                        int dy = Mathf.Abs(nownode.y - goalnode.y);

                        if (dx > dy)
                        {
                            node[nownode.x + 1][nownode.y].totalcost = nownode.totalcost + node[nownode.x + 1][nownode.y].risk + dx;
                        }
                        else
                        {
                            node[nownode.x + 1][nownode.y].totalcost = nownode.totalcost + node[nownode.x + 1][nownode.y].risk + dy;
                        }
                        node[nownode.x + 1][nownode.y].state = 1;
                        node[nownode.x + 1][nownode.y].parentnode = nownode;
                        opennode.Add(node[nownode.x + 1][nownode.y]);
                    }
                }

                if (nownode.x > 0)//¶
                {
                    if (node[nownode.x - 1][nownode.y].state == 0)
                    {
                        int dx = Mathf.Abs((nownode.x - 1) - goalnode.x);
                        int dy = Mathf.Abs(nownode.y - goalnode.y);

                        if (dx > dy)
                        {
                            node[nownode.x - 1][nownode.y].totalcost = nownode.totalcost + node[nownode.x - 1][nownode.y].risk + dx;
                        }
                        else
                        {
                            node[nownode.x - 1][nownode.y].totalcost = nownode.totalcost + node[nownode.x - 1][nownode.y].risk + dy;
                        }
                        node[nownode.x - 1][nownode.y].state = 1;
                        node[nownode.x - 1][nownode.y].parentnode = nownode;
                        opennode.Add(node[nownode.x - 1][nownode.y]);
                    }
                }

                if (nownode.y < 49)//ã
                {
                    if (node[nownode.x][nownode.y + 1].state == 0)
                    {
                        int dx = Mathf.Abs(nownode.x - goalnode.x);
                        int dy = Mathf.Abs((nownode.y + 1) - goalnode.y);

                        if (dx > dy)
                        {
                            node[nownode.x][nownode.y + 1].totalcost = nownode.totalcost + node[nownode.x][nownode.y + 1].risk + dx;
                        }
                        else
                        {
                            node[nownode.x][nownode.y + 1].totalcost = nownode.totalcost + node[nownode.x][nownode.y + 1].risk + dy;
                        }
                        node[nownode.x][nownode.y + 1].state = 1;
                        node[nownode.x][nownode.y + 1].parentnode = nownode;
                        opennode.Add(node[nownode.x][nownode.y + 1]);
                    }
                }

                if (nownode.y > 0)//‰º
                {
                    if (node[nownode.x][nownode.y - 1].state == 0)
                    {
                        int dx = Mathf.Abs(nownode.x - goalnode.x);
                        int dy = Mathf.Abs((nownode.y - 1) - goalnode.y);

                        if (dx > dy)
                        {
                            node[nownode.x][nownode.y - 1].totalcost = nownode.totalcost + node[nownode.x][nownode.y - 1].risk + dx;
                        }
                        else
                        {
                            node[nownode.x][nownode.y - 1].totalcost = nownode.totalcost + node[nownode.x][nownode.y - 1].risk + dy;
                        }
                        node[nownode.x][nownode.y - 1].state = 1;
                        node[nownode.x][nownode.y - 1].parentnode = nownode;
                        opennode.Add(node[nownode.x][nownode.y - 1]);
                    }
                }
                
                if (nownode.x < 29 && nownode.y < 49)//‰Eã
                {
                    if (node[nownode.x + 1][nownode.y + 1].state == 0)
                    {
                        int dx = Mathf.Abs((nownode.x + 1) - goalnode.x);
                        int dy = Mathf.Abs((nownode.y + 1) - goalnode.y);

                        if (dx > dy)
                        {
                            node[nownode.x + 1][nownode.y + 1].totalcost = nownode.totalcost + node[nownode.x + 1][nownode.y + 1].risk + dx;
                        }
                        else
                        {
                            node[nownode.x + 1][nownode.y + 1].totalcost = nownode.totalcost + node[nownode.x + 1][nownode.y + 1].risk + dy;
                        }
                        node[nownode.x + 1][nownode.y + 1].state = 1;
                        node[nownode.x + 1][nownode.y + 1].parentnode = nownode;
                        opennode.Add(node[nownode.x + 1][nownode.y + 1]);
                    }
                }
                if (nownode.x < 29 && nownode.y > 0)//‰E‰º
                {
                    if (node[nownode.x + 1][nownode.y - 1].state == 0)
                    {
                        int dx = Mathf.Abs((nownode.x + 1) - goalnode.x);
                        int dy = Mathf.Abs((nownode.y - 1) - goalnode.y);

                        if (dx > dy)
                        {
                            node[nownode.x + 1][nownode.y - 1].totalcost = nownode.totalcost + node[nownode.x + 1][nownode.y - 1].risk + dx;
                        }
                        else
                        {
                            node[nownode.x + 1][nownode.y - 1].totalcost = nownode.totalcost + node[nownode.x + 1][nownode.y - 1].risk + dy;
                        }
                        node[nownode.x + 1][nownode.y - 1].state = 1;
                        node[nownode.x + 1][nownode.y - 1].parentnode = nownode;
                        opennode.Add(node[nownode.x + 1][nownode.y - 1]);
                    }
                }
                if (nownode.x > 0 && nownode.y > 0)//¶‰º
                {
                    if (node[nownode.x - 1][nownode.y - 1].state == 0)
                    {
                        int dx = Mathf.Abs((nownode.x - 1) - goalnode.x);
                        int dy = Mathf.Abs((nownode.y - 1) - goalnode.y);

                        if (dx > dy)
                        {
                            node[nownode.x - 1][nownode.y - 1].totalcost = nownode.totalcost + node[nownode.x - 1][nownode.y - 1].risk + dx;
                        }
                        else
                        {
                            node[nownode.x - 1][nownode.y - 1].totalcost = nownode.totalcost + node[nownode.x - 1][nownode.y - 1].risk + dy;
                        }
                        node[nownode.x - 1][nownode.y - 1].state = 1;
                        node[nownode.x - 1][nownode.y - 1].parentnode = nownode;
                        opennode.Add(node[nownode.x - 1][nownode.y - 1]);
                    }
                }
                if (nownode.x > 0 && nownode.y < 49)//¶ã
                {
                    if (node[nownode.x - 1][nownode.y + 1].state == 0)
                    {
                        int dx = Mathf.Abs((nownode.x - 1) - goalnode.x);
                        int dy = Mathf.Abs((nownode.y + 1) - goalnode.y);

                        if (dx > dy)
                        {
                            node[nownode.x - 1][nownode.y + 1].totalcost = nownode.totalcost + node[nownode.x - 1][nownode.y + 1].risk + dx;
                        }
                        else
                        {
                            node[nownode.x - 1][nownode.y + 1].totalcost = nownode.totalcost + node[nownode.x - 1][nownode.y + 1].risk + dy;
                        }
                        node[nownode.x - 1][nownode.y + 1].state = 1;
                        node[nownode.x - 1][nownode.y + 1].parentnode = nownode;
                        opennode.Add(node[nownode.x - 1][nownode.y + 1]);
                    }
                }
                
                nownode.state = 2;
                /*
                if (nownode == node[nownode.x][nownode.y]) {
                    Debug.Log("remove");
                }
                */
                opennode.Remove(nownode);
                closenode.Add(nownode);
            }
            route.Add(goalnode.parentnode);
            //Debug.Log(startnode.x + ","  + startnode.y + "," +node[startnode.x][startnode.y].parentnode);
            while (route[route.Count - 1].parentnode != null)
            {
                //Debug.Log(route.Count);
                //Debug.Log(route.Count + "," +route[route.Count - 1].parentnode.x + "," + route[route.Count - 1].parentnode.y);
                route.Add(route[route.Count - 1].parentnode);
            }
            route.RemoveAt(route.Count-1);
            route.Reverse();
        }
    }
    void findwaypoint()
    {
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                split_risk[i][j] = 0;
                split_risk[i][j] += Mathf.Max((j-2)*1,0);
                if (i == 0 || i == 5) split_risk[i][j] += 10;
                if (i == 1 || i == 4) split_risk[i][j] += 5;
                if (j == 0) split_risk[i][j] += 5; 
                for (int k = 0; k < 5; k++)
                {
                    for(int l = 0; l < 5; l++)
                    {
                        split_risk[i][j] += node[i * 5 + k][j * 5 + l].risk;
                    }
                }
            }
        }
        int min_index_i = 0;
        int min_index_j = 0;
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                if (split_risk[min_index_i][min_index_j] > split_risk[i][j])
                {
                    min_index_i = i;
                    min_index_j = j;
                }
                else 
                {
                    if (split_risk[min_index_i][min_index_j] == split_risk[i][j])
                    {
                        if (Mathf.Abs(tf.position.x - (i-9)) + Mathf.Abs(tf.position.y - (j - 15)) < Mathf.Abs(tf.position.x - (min_index_i - 9)) + Mathf.Abs(tf.position.y - (min_index_j - 15)))
                        {
                            min_index_i = i;
                            min_index_j = j;
                        }
                    }
                }
            }
        }
        goalnode = node[min_index_i * 5 + Random.Range(0, 5)][min_index_j * 5 + Random.Range(0, 5)];

    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (!EditorApplication.isPlaying)
        {
            return;
        }
        for(int i = 0; i < 30; i++)
        {
            for(int j = 0; j < 50; j++)
            {
                Gizmos.color = new Color(1, 0, 0, Mathf.Clamp01(node[i][j].risk * 0.1f));
                Gizmos.DrawCube(new Vector2(-3 + 0.2f*i+0.1f, -5 + 0.2f*j+0.1f), new Vector2(0.19f, 0.19f));
            }
        }
        
        for (int i = 0; i < route.Count; i++)
        {
            //Debug.Log("draw");
            Gizmos.color = new Color(0, 0, 1);
            Gizmos.DrawCube(new Vector2((float)route[i].x / 5 - 3, (float)route[i].y / 5 - 5), new Vector2(0.19f, 0.19f));
        }

        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                Gizmos.color = new Color(1, 0, 0, Mathf.Clamp01(split_risk[i][j] * 0.01f));
                Gizmos.DrawCube(new Vector2(-2.5f + i, -4.5f + j), new Vector2(0.95f, 0.95f));
            }
        }
    }
#endif
}
