using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tunnelers {
    
    [System.Serializable]
    public class Config
    {
        public int width;
        public int height;
    }
    
    class P
    {
        public P(int a, int b, Vector2Int d)
        {
            x = a;
            y = b;
            dir = d;
        }
        public int x, y, z = 0;
        public Vector2Int dir;
    }

    public Config c;
    public byte[,] map;
    Vector2Int[] dirs = { Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.up };
    public Vector2Int begin;
    public Vector2Int end;
    Vector2Int n;
    public int tCount;

    public void Generate()
    {
        map = new byte[c.width, c.height];
        int limit = Mathf.CeilToInt((c.height) * (c.width) * .25f) - 1,
        count = 2;
        begin = new Vector2Int(Random.Range(2, c.width - 2), 2 - 1);

        List<P> vec = new List<P>
        {
            new P(begin.x, begin.y, dirs[3]),
            new P(Random.Range(2, c.width - 2), c.height - 2, dirs[0])
        };

        end = new Vector2Int(vec[1].x, vec[1].y);

        for(int i = 0; i < vec.Count; i++) map[vec[i].x, vec[i].y] = 1;
        for (int j = 0; j < 5; j++)
            vec.Add(new P(Random.Range(2 + 5, c.width - 5 - 2), Random.Range(2 + 5, c.width - 5 - 2), dirs[Random.Range(0, 4)]));
        while (count < limit && vec.Count > 0)
            for(int m = vec.Count - 1; m >= 0; m--)
            {
                int x = vec[m].x + vec[m].dir.x;
                int y = vec[m].y + vec[m].dir.y;
                if(!InBounds(x, y))
                {
                    vec[m].dir = RandomOrtoDir(vec[m].dir);
                    vec[m].z = 0;
                    x = vec[m].x + vec[m].dir.x;
                    y = vec[m].y + vec[m].dir.y;
                    if (!InBounds(x, y))
                    {
                        vec[m].dir = vec[m].dir * -1;
                        x = vec[m].x + vec[m].dir.x;
                        y = vec[m].y + vec[m].dir.y;
                    }
                }
                vec[m].x = x;
                vec[m].y = y;
                vec[m].z++;
                if (CheckArea(vec[m].x, vec[m].y))
                {
                    map[n.x, n.y] = 1;
                    count++;
                    if (Random.Range(0, 100) < (vec[m].z * 5) && vec[m].z >= 3)
                    {
                        if (Random.Range(0, 2) == 0) vec.Add(new P(vec[m].x, vec[m].y, vec[m].dir));
                        vec[m].dir = RandomOrtoDir(vec[m].dir);
                        vec[m].z = 0;
                        if (Random.Range(0, 2) == 0) vec.Add(new P(vec[m].x, vec[m].y, vec[m].dir * -1));
                    }
                }
                else vec.RemoveAt(m);
            }
        CheckQ();
        if (tCount != count) Generate();
    }
    
    bool CheckArea(int x, int y)
    {
        if (!InBounds(x, y) || map[x, y] != 0) return false;
        n = new Vector2Int(x, y);
        return true;
    }

    Vector2Int RandomOrtoDir(Vector2Int d)
    {
        if (Random.Range(0, 2) == 0) d *= -1;
        return new Vector2Int(d.y, d.x);
    }

    public bool InBounds(int x, int y)
    {
        return !(x < 2 || y < 2 || x >= (c.width - 2) || y >= (c.height - 2));
    }

    void CheckQ()
    {
        List<Vector2Int> nextToCheck = new List<Vector2Int>(),
            checkingNow = new List<Vector2Int> { begin };
        Vector2Int posCheck;
        tCount = 0;
        while(checkingNow.Count > 0)
        {
            nextToCheck.Clear();
            for (int i = 0; i < checkingNow.Count; i++)
                for (int d = 0; d < 4; d++)
                {
                    posCheck = checkingNow[i] + dirs[d];
                    if (map[posCheck.x, posCheck.y] == 1)
                    {
                        nextToCheck.Add(posCheck);
                        tCount++;
                        map[posCheck.x, posCheck.y] = 2;
                    }
                }
            checkingNow = new List<Vector2Int>(nextToCheck);
        }
    }

}
