using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillSelect : MonoBehaviour
{
    [SerializeField] List<GameObject> _selectList;

    List<SkillSelectTable> _selectTable = new List<SkillSelectTable>();
    List<UnityEngine.UI.Text> _selectText = new List<UnityEngine.UI.Text>();
    CanvasGroup _canvas;

    public GameObject treeCanvas;


    bool _startEvent = false;

    int selectNum;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
        //_canvas.alpha = 0;
    }

    void Start()
    {
        treeCanvas.SetActive(false);
        selectNum = 0;

        for (int i = 0; i < _selectList.Count; ++i)
        {
            _selectTable.Add(null);
            _selectText.Add(_selectList[i].GetComponentInChildren<UnityEngine.UI.Text>());
            {
                var index = i;
                var btn = _selectList[i].GetComponentInChildren<UnityEngine.UI.Button>();
                btn.onClick.AddListener(() =>
                {
                    if (_canvas.alpha == 0) return;
                    OnClick(index);
                });
            }
        }
    }

    private void Update()
    {
        if (_startEvent)
        {
            SelectStart();
            _startEvent = false;
        }
    }

    public void SelectStartDelay()
    {
        _startEvent = true;
    }

    public void SelectStart()
    {
        _canvas.alpha = 1;

        List<SkillSelectTable> table = new List<SkillSelectTable>();
        var list = GameData.SkillSelectTable.Where(s => GameManager.Level >= s.Level);

        int totalProb = list.Sum(s => s.Probability);
        int rand = Random.Range(0, totalProb);

        for (int i = 0; i < _selectList.Count; ++i)
        {
            _selectTable[i] = null;
            _selectText[i].text = "";
        }

        for (int i = 0; i < _selectList.Count; ++i)
        {
            foreach (var s in list)
            {
                if (rand < s.Probability)
                {
                    _selectTable[i] = s;
                    _selectText[i].text = s.Name;
                    list = list.Where(ls => !(ls.Type == s.Type && ls.TargetId == s.TargetId));
                    break;
                }
                rand -= s.Probability;
            }
        }
    }

    public void OnClick(int index)
    {
        GameManager.Instance.LevelUpSelect(_selectTable[index]);

        _canvas.alpha = 0;
    }
    public void ChangeStandard()
    {
        if (selectNum == 0)
        {
            Player.bulletType = "スタンダード";
            selectNum = 11;
            Time.timeScale = 1;
            treeCanvas.SetActive(false);
        }
    }
    public void ChangeMirror()
    {
        if (selectNum == 0)
        {
            Player.bulletType = "反射";
            selectNum = 21;
            Time.timeScale = 1;
            treeCanvas.SetActive(false);
        }
    }
    public void ChangeRoll()
    {
        if (selectNum == 0)
        {
            Player.bulletType = "回転";
            Player.useRollAttackObj.SetActive(true);
            selectNum = 31;
            Time.timeScale = 1;
            treeCanvas.SetActive(false);
        }
    }
    public void ChangeExplosion()
    {
        if (selectNum == 0)
        {
            Player.bulletType = "爆発";
            selectNum = 41;
            Time.timeScale = 1;
            treeCanvas.SetActive(false);
        }
    }

    public void SpeedUp()
    {
        if (selectNum == 11 || selectNum == 21 || selectNum == 41)
        {
            Time.timeScale = 1;
            Bullet.SpeedUpBullet();
            treeCanvas.SetActive(false);
        }
    }
    public void PowerUp()
    {
        if (selectNum == 11)
        {
            Time.timeScale = 1;
            Bullet.PowerUpBullet();
            treeCanvas.SetActive(false);
        }
    }
    public void RapidUp()
    {
        if (selectNum == 41)
        {
            Time.timeScale = 1;
            Bullet.RapidUpBullet();
            treeCanvas.SetActive(false);
        }
    }
    public void LifeUp()
    {
        if (selectNum == 21)
        {
            Time.timeScale = 1;
            Bullet.RapidUpBullet();
            treeCanvas.SetActive(false);
        }
    }

    public void RollUp()
    {
        if (selectNum == 31)
        {
            Time.timeScale = 1;
            Rotasion.speed += 0.2f;
            treeCanvas.SetActive(false);
        }
    }
}
