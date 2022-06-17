using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    float view;
    [SerializeField] GameObject _banana;
    [SerializeField] float a = 0;
    [SerializeField] float b = 0;
    [SerializeField] float c = 0;
    [SerializeField] float e = 0;
    int maxBanana = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("BananaRain");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Wanana");
        }
    }
    IEnumerator BananaRain()
    {
        yield return new WaitForSeconds(1.5f);
        float random = Random.Range(a, b);
        float random2 = Random.Range(0, 90);
        Instantiate(_banana,new Vector2(random, c), Quaternion.identity);
        maxBanana++;
        if (maxBanana < 50)
        {
            StartCoroutine("BananaRain");
        }
    }

    
}
