using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IObjectPool
{
    [SerializeField] float _speed = 10;
    [SerializeField] int _hp = 5;
    [SerializeField] GameObject keikennti;
    SpriteRenderer _image;

    void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    void Update()
    {
        if (!IsActive) return;

        Vector3 sub = GameManager.Player.transform.position - transform.position;
        sub.Normalize();

        transform.position += sub * _speed * Time.deltaTime;
    }

    public void Damage(int x)
    {
        _hp -= x;
        StartCoroutine("Stop");

        if (_hp <= 0)
        {
            Destroy();
            //TODO
            Instantiate(keikennti,this.transform.position,Quaternion.identity);
            //GameManager.Instance.GetExperience(1);
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
        _image.enabled = true;
        _isActrive = true;
    }
    public void Destroy()
    {
        _image.enabled = false;
        _isActrive = false;
    }

    IEnumerator Stop()
    {
        //float copy = _speed;
        _speed *= -1;
        yield return new WaitForSeconds(0.25f);
        _speed *= -1;
    }
}
