using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUiDisplay : MonoBehaviour
{
    [SerializeField]
    private int index = 0;
    
    void Start()
    {
        if(GameManager.Instance != null){
            GameManager.Instance.onLifeChanged += updateLife;
        }
    }
    private void updateLife(int life){
        if(index >= life)
           gameObject.SetActive(false);
    }
}
