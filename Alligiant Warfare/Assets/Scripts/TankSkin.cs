using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSkin : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject pauseMenu;
    public int tankNumber;
    void Start()
    {
        tankNumber = 1;
    }
    void Update()
    {
        this.GetComponent<SpriteRenderer>().sprite = sprites[tankNumber - 1];
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ChangeSkin(int number)
    {
        tankNumber = number;
    }
}
