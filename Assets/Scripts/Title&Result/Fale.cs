using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fale : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text text;
    void Start()
    {
        text.text = $"You survived {TimeScoreEXP.minutes} minutes {TimeScoreEXP.intTime} seconds";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
