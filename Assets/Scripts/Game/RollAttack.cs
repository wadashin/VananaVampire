using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var list = GameManager.EnemyList;
        Vector3 vec;
        foreach (var e in list)
        {
            if (!e.IsActive) continue;

            vec = e.transform.position - this.transform.position;
            if (vec.magnitude < 1.5f)
            {
                e.Damage(1);
                break;
            }
        }
    }
}
