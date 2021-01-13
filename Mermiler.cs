using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;
public class Mermiler : MonoBehaviour
{
    
    public GameObject mermiObje;
    float zaman;
   
   

    void Start()
    {
        zaman = 1f;
    }

    
    void Update()
    {
        
        zaman -= Time.deltaTime;
        if (zaman < 0 || PlayerPrefs.GetString("MermiDurumu") == "0")
        {
            Destroy(mermiObje);
            enabled = false;
        }

        else
            mermiObje.transform.Translate(1 * Vector3.forward * 20 * Time.deltaTime);
       
    }
   

}
