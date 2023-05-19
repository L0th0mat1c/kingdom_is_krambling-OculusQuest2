using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopupManager : MonoBehaviour
{
    [Header("Params")]
    public GameObject damagePopupPrefab;

    //Instance Singleton
    private static DamagePopupManager instance = null;
    public static DamagePopupManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this){
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
    }

    /// <summary>
    /// Cette fonction permet de créer une popup de dégâts au dessus de l'objet ciblé
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="damage"></param>
    /// <param name="upOffset"></param>
    public void newDamagePopup(Transform pos, int damage, int upOffset = 1) {
        GameObject damagePopup = Instantiate(damagePopupPrefab, new Vector3(pos.position.x + Random.Range(-0.2f, 0.2f), pos.position.y + upOffset + Random.Range(-0.2f, 0.2f), pos.position.z + Random.Range(-0.2f, 0.2f)), pos.rotation);
        damagePopup.transform.LookAt(Camera.main.transform);
        Transform child = damagePopup.transform.GetChild(0);
        if(child != null) {
            TextMeshProUGUI tmp = child.GetComponent<TextMeshProUGUI>();
            if(tmp != null)
                tmp.text = damage.ToString();
        }
        Destroy(damagePopup, 1f);
    }
}
