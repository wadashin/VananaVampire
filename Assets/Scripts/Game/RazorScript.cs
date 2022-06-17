using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 a;
    Vector2 b;
    Vector2 c;
    Vector2 d;
    Vector2 e;
    Vector2 f;
    Vector2 g;
    Vector2 h;
    Vector2 i;
    Vector2 j;

    Vector2 nextPos;


    //List cc = new List<Vector2> { };
    Vector2[] array = new Vector2[10];

    void Start()
    {
        a = GameManager.Player.transform.position;
        j = new Vector2(a.x + Random.Range(0, 50), a.y + 30);
        nextPos = GameManager.Player.transform.position;
        array = new Vector2[10] { a,b,c,d,e,f,g,h,i,j};
        StartCoroutine("RazerShoot");
    }

    // Update is called once per frame
    void Update()
    {
        a = GameManager.Player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, nextPos, 4f);

        var list = GameManager.EnemyList;
        Vector3 vec;
        foreach (var e in list)
        {
            if (!e.IsActive) continue;

            vec = e.transform.position - this.transform.position;
            if (vec.magnitude < 2f)
            {
                e.Damage(10);
                break;
            }
        }
    }
    IEnumerator RazerShoot()
    {
        yield return new WaitForSeconds(0.15f);
        b = new Vector2(a.x + Random.Range(0, 50), a.y + 30);
        nextPos = b;
        yield return new WaitForSeconds(0.15f);
        c = new Vector2(a.x + Random.Range(-50, 0), a.y - 30);
        nextPos = c;
        yield return new WaitForSeconds(0.15f);
        d = new Vector2(a.x + Random.Range(-50, 0), a.y + 30);
        nextPos = d;
        yield return new WaitForSeconds(0.15f);
        e = new Vector2(a.x + Random.Range(0, 50), a.y - 30);
        nextPos = e;
        yield return new WaitForSeconds(0.15f);
        f = new Vector2(a.x + Random.Range(0, 50), a.y + 30);
        nextPos = f;
        yield return new WaitForSeconds(0.15f);
        g = new Vector2(a.x + Random.Range(-50, 0), a.y - 30);
        nextPos = g;
        yield return new WaitForSeconds(0.15f);
        h = new Vector2(a.x + Random.Range(-50, 0), a.y + 30);
        nextPos = h;
        yield return new WaitForSeconds(0.15f);
        i = new Vector2(a.x + Random.Range(0, 50), a.y - 30);
        nextPos = i;
        yield return new WaitForSeconds(0.15f);
        j = new Vector2(a.x + Random.Range(0, 50), a.y + 30);
        nextPos = j;
        yield return new WaitForSeconds(0.15f);
        Destroy(this.gameObject);
    }
}
