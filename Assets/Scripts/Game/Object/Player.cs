using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 0.1f;
    [SerializeField] float _shootTime = 0.3f;
    [SerializeField] GameObject rollAttackObj;
    public static GameObject useRollAttackObj;
    [SerializeField] Slider hpBar;
    [SerializeField] float _maxHp = 10000;
    float hp = 0;

    [SerializeField] GameObject _razorObj;
    bool razorStart = true;

    List<ISkill> _skill = new List<ISkill>();

    float _timer = 0.0f;

    static public string bulletType = "スタンダード";

    IntervalTimer ttiimmeerr = new IntervalTimer();

    void Awake()
    {
        GameManager.Instance.SetPlayer(this);

        //初期武器
        AddSkill(1);
    }
    private void Start()
    {
        useRollAttackObj = rollAttackObj;
        hp = _maxHp;
        hpBar.maxValue = _maxHp;
        hpBar.value = hp;
        bulletType = "スタンダード";
        ttiimmeerr.Setup(2f);
    }

    private void Update()
    {
        float w = Input.GetAxis("Horizontal");
        float h = Input.GetAxis("Vertical");

        transform.position += new Vector3(w * _speed * Time.deltaTime, h * _speed * Time.deltaTime, 0);

        _skill.ForEach(s => s.Update());
        _skill.ForEach(s => Debug.Log(s));

        
        var list = GameManager.EnemyList;
        Vector3 vec;
        foreach (var e in list)
        {
            if (!e.IsActive) continue;

            vec = e.transform.position - this.transform.position;
            if (vec.magnitude < 2.5f)
            {
                hp -= 1f;
                hpBar.value = hp;
                if (hp <= 0)
                {
                    SceneManager.LoadScene("Fale");
                }
                break;
            }
        }

        if(bulletType == "反射")
        {
            if(razorStart)
            {
                StartCoroutine("Razor");
                razorStart = false;
            }
        }
    }

    public void AddSkill(int skillId)
    {
        var having = _skill.Where(s => s.SkillId == (SkillDef)skillId);
        if(having.Count() > 0)
        {
            having.Single().Levelup();
        }
        else
        {
            ISkill newSkill = null;
            switch((SkillDef)skillId)
            {
                case SkillDef.ShotBullet:
                    newSkill = new ShotBullet();
                    break;

                case SkillDef.AreaAttack:
                    newSkill = new AreaAttack();
                    break;
            }

            if (newSkill != null)
            {
                newSkill.Setup();
                _skill.Add(newSkill);
            }
        }
    }
    IEnumerator RollAttack(int x)
    {
        yield return new WaitForSeconds(x);

    }
    IEnumerator Razor()
    {
        yield return new WaitForSeconds(10);
        Instantiate(_razorObj);
        StartCoroutine("Razor");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            hp -= 1f;
            Debug.Log(1);
            if (hp <= 0)
            {
                SceneManager.LoadScene("Fale");
            }
        }
    }
}
