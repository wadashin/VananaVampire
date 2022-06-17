using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeScoreEXP : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text _time;
    [SerializeField] Slider _expObj;
    public static Slider _exp1;

    float countTime = 0;
    public static int intTime = 0;
    public static int minutes;

    static int sliderExp = 0;

    private void Awake()
    {
        _exp1 = _expObj;
    }

    void Start()
    {
        _exp1.maxValue = GameData.LevelTable[1];
        _exp1.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        intTime = (int)countTime;
        if (countTime >= 60)
        {
            countTime = 0;
            minutes++;
            if(minutes >= 10)
            {
                SceneManager.LoadScene("Clear");
            }
        }
        _time.text = $"{minutes.ToString("D2")}:{intTime.ToString("D2")}";
    }

    public static void EXPPlus()
    {
        sliderExp++;
        _exp1.value = sliderExp;
    }
    public static void LevelUp(int x)
    {
        sliderExp = 0;
        _exp1.value = sliderExp;
        _exp1.maxValue = GameData.LevelTable[x] - GameData.LevelTable[x - 1];
    }
}
