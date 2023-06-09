using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUiDisplay : MonoBehaviour
{
    [SerializeField]
    private int index = 0;

    [SerializeField]
    private Sprite image;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance != null){
            GameManager.Instance.onLifeChanged += updateLife;
        }
    }
    private void updateLife(int life){
        if(index < life){
            // gameObject.GetComponent<Image>().color = Color.white;
        }else{
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
