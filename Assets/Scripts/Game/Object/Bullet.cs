using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour, IObjectPool
{
    [SerializeField] float _speed = 20;
    static float speed;

    SpriteRenderer _image;
    Enemy _target;
    Vector3 _shootVec;

    static IntervalTimer _t = new IntervalTimer();

    float _timer = 0.0f;

    bool attack = true;

    string mode;

    Vector3 view;

    static float power = 1;

    static float bulletTime = 5f;

    void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        speed = _speed;
        power = 1;
        bulletTime = 5f;
    }

    private void OnEnable()
    {
        mode = Player.bulletType;
    }

    public void Shoot(Enemy target)
    {
        _target = target;
        if (_target == null) return;

        _shootVec = _target.transform.position - GameManager.Player.transform.position;
        _shootVec.Normalize();
    }

    void Update()
    {
        mode = Player.bulletType;
        if (!_isActrive) return;

        switch(mode)
        {
            case "スタンダード":
                StandardBullet();
                break;
            case "反射":
                Mirror();
                break;
            case "回転":
                Roll();
                break;
            case "爆発":
                Explosion();
                break;
        }
    }

    //ObjectPool
    bool _isActrive = false;
    public bool IsActive => _isActrive;
    public void DisactiveForInstantiate()
    {
        _image.enabled = false;
        _isActrive = false;
    }
    public void Create()
    {
        _timer = 0.0f;
        _image.enabled = true;
        _isActrive = true;
    }
    public void Destroy()
    {
        _image.enabled = false;
        _isActrive = false;
    }

    void StandardBullet()
    {

        transform.position += _shootVec * speed * Time.deltaTime;

        var list = GameManager.EnemyList;
        _target = null;
        Vector3 vec;
        foreach (var e in list)
        {
            if (!e.IsActive) continue;

            vec = e.transform.position - this.transform.position;
            if (vec.magnitude < 1.5f)
            {
                e.Damage((int)(power * 2));
                Destroy();
                break;
            }
        }
        _timer += Time.deltaTime;
        if (_timer > 2f)
        {
            Destroy();
        }
    }

    void Mirror()
    {

        transform.position += _shootVec * speed * Time.deltaTime;


        var list = GameManager.EnemyList;
        _target = null;
        Vector3 vec;
        foreach (var e in list)
        {
            if (!e.IsActive) continue;

            vec = e.transform.position - this.transform.position;
            if (vec.magnitude < 1.5f)
            {
                if (attack)
                {
                    e.Damage(1);
                    attack = false;
                }
                //Destroy();
                break;
            }
        }
        view = Camera.main.WorldToViewportPoint(this.transform.position);

        // もし移動後のビューポート座標が０から１の範囲ならば
        if (0 >= view.x)
        {
            _shootVec = new Vector3(1, 0, 0);
            _shootVec.Normalize();
        }
        else if (view.x >= 1)
        {
            _shootVec = new Vector3(-1, 0, 0);
            _shootVec.Normalize();
        }
        else if (0 >= view.y)
        {
            _shootVec = new Vector3(0, 1, 0);
            _shootVec.Normalize();
        }
        else if (view.y >= 1)
        {
            _shootVec = new Vector3(0, -1, 0);
            _shootVec.Normalize();
        }



        if (!attack)
        {
            StartCoroutine("AttackChange");
        }

        _timer += Time.deltaTime;
        if (_timer > bulletTime)
        {
            Destroy();
        }
    }

    void Roll()
    {
        _timer += Time.deltaTime;
        if (_timer > 0f)
        {
            Destroy();
        }
    }

    void Explosion()
    {
        transform.position += _shootVec * speed * Time.deltaTime;

        var list = GameManager.EnemyList;
        _target = null;
        Vector3 vec;
        foreach (var e in list)
        {
            if (!e.IsActive) continue;

            vec = e.transform.position - this.transform.position;
            if (vec.magnitude < 3f)
            {
                e.Damage(1);
                Destroy();
            }
        }
        _timer += Time.deltaTime;
        if (_timer > 2f)
        {
            Destroy();
        }
    }

    IEnumerator AttackChange()
    {
        yield return new WaitForSeconds(0.5f);
        attack = true;
    }

    public static void PowerUpBullet()
    {
        power += power / 10;
    }

    public static void SpeedUpBullet()
    {
        speed += 2;
    }

    public static void RapidUpBullet()
    {
        _t.Setup(2f);
    }
    public static void LifeTimeUpBullet()
    {
        bulletTime += 1;
    }
}
