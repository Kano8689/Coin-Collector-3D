using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    float speed = 50f;
    public GameObject Coin, dangerCoin;
    int score = 0;
    public Text ScoreBoard;
    public GameObject Play,Looser, Winner;
    public GameObject[] Level;
    int lvlNo=1;
    SceneManager sceneManager;
    
    // Start is called before the first frame update
    void Start()
    {
        lvlNo = PlayerPrefs.GetInt("LEVELNO", 1);
        Debug.Log(lvlNo);
        
        Play.SetActive(true);
        Looser.SetActive(false);
        Winner.SetActive(false);

        rb = GetComponent<Rigidbody>();

        if (lvlNo <= 3)
        {
            Level[(lvlNo - 1)].SetActive(true);
        }
        else
        {
            InvokeRepeating("generateCoin", 0f, 4.5f);
            InvokeRepeating("generateDangerCoin", 10f, 15f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ballMove();
    }

    void ballMove()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = Vector3.right * speed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = Vector3.forward * speed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = Vector3.back * speed;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            rb.velocity = Vector3.zero;
        }
    }

    void generateCoin()
    {
        float x = Random.Range(-45, 45);
        float y = 0.5f;
        float z = Random.Range(-45, 45);

        Vector3 pos = new Vector3(x, y, z);
        GameObject g = Instantiate(Coin, pos,Quaternion.identity);
    }

    void generateDangerCoin()
    {
        float x = Random.Range(-45, 45);
        float y = 0.5f;
        float z = Random.Range(-45, 45);

        Vector3 pos = new Vector3(x, y, z);
        GameObject g = Instantiate(dangerCoin, pos, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="coin")
        {
            Destroy(collision.gameObject);
            score++;
            //Debug.Log(score);
        }

        if (collision.gameObject.tag == "dangerCoin")
        {
            //Destroy(gameObject);
            Debug.Log("Looser");

            Play.SetActive(false);
            Looser.SetActive(true);

            ScoreBoard.text = "" + score;
        }

        if (lvlNo <= 3)
        {
            if (collision.gameObject.tag == "ground")
            {
                Debug.Log("Looser");

                Play.SetActive(false);
                Looser.SetActive(true);

                ScoreBoard.text = score.ToString();
            }
        }

        if(collision.gameObject.tag == "win")
        {
            Debug.Log("Winner");
            
            lvlNo++;
            PlayerPrefs.SetInt("LEVELNO", lvlNo);
            
            Play.SetActive(false);
            Winner.SetActive(true);
            Level[(lvlNo-1)].SetActive(false);

            ScoreBoard.text = score.ToString();
        }
    }

    public void onClickReplay()
    {
        SceneManager.LoadScene("Play");
    }

    public void onClickNext()
    {
        SceneManager.LoadScene("Play");
    }

}
