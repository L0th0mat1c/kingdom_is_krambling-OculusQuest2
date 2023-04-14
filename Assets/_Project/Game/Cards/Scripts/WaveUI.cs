using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using MoreMountains.Tools;

public class WaveUI : MonoBehaviour
{

    public TextMeshProUGUI UIWaveCount;
    [SerializeField]
    private MMProgressBar progressBar;


    private void Start() {
        progressBar.UpdateBar(0,0,WaveManager.Instance.FullWaveTime);

        WaveManager.Instance.OnWaveTimeUpdated += UpdateWaveTimerBar;
        Debug.Log("Set up done");
    }

    private void UpdateWaveTimerBar(int _time){
        Debug.Log("time -> last " + _time);
        progressBar.UpdateBar01((float)_time / WaveManager.Instance.FullWaveTime);
    }
}


