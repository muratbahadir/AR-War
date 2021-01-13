using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
public class finalEkranScript : MonoBehaviour
{
    DatabaseReference reference;
    public Text Rskor, Bskor, R1k, R1d, R2k, R2d, B1k, B1d, B2k, B2d;
    public GameObject RW, BW;
    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance
        .RootReference;
        print(PlayerPrefs.GetString("RPS"));
        Rskor.text = PlayerPrefs.GetString("RPS");
        R1k.text = PlayerPrefs.GetString("RP1K");
        R1d.text = PlayerPrefs.GetString("RP1D");
        R2k.text = PlayerPrefs.GetString("RP2K");
        R2d.text = PlayerPrefs.GetString("RP2D");
        Bskor.text = PlayerPrefs.GetString("BPS");
        B1k.text = PlayerPrefs.GetString("BP1K");
        B1d.text = PlayerPrefs.GetString("BP1D");
        B2k.text = PlayerPrefs.GetString("BP2K");
        B2d.text = PlayerPrefs.GetString("BP2D");
        if(PlayerPrefs.GetString("Winner")=="Takim0")
        {
            RW.active = true;
            BW.active = false;
        }
        if (PlayerPrefs.GetString("Winner") == "Takim1")
        {
            RW.active = false;
            BW.active = true;
        }
        pasifeDusur();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void pasifeDusur()
    {
        /*
        playerBilgileri("Player1", "Takim0");
        playerBilgileri("Player3", "Takim0");
        playerBilgileri("Player2", "Takim1");
        playerBilgileri("Player4", "Takim1");
        */
        reference.Child("Odalar").Child("Oda1").Child("Takim0").
          Child("TakimSkor").SetValueAsync("0");
        reference.Child("Odalar").Child("Oda1").Child("Takim1").
          Child("TakimSkor").SetValueAsync("0");
        
    }
    void playerBilgileri(string player,string takim)
    {
        
        reference.Child("Odalar").Child("Oda1").Child(takim).
              Child(player).Child("ÖldürmeSayisi").SetValueAsync("0");
        reference.Child("Odalar").Child("Oda1").Child(takim).
              Child(player).Child("ÖldürülmeSayisi").SetValueAsync("0");
        reference.Child("Odalar").Child("Oda1").Child(takim).
              Child(player).Child("ÖldürülmeSayisi").SetValueAsync("0");
        reference.Child("Odalar").Child("Oda1").Child(takim).
              Child(player).Child("Dokunulmazlik").SetValueAsync("false");
        reference.Child("Odalar").Child("Oda1").Child(takim).
              Child(player).Child("anlikMermi").SetValueAsync("30");
        reference.Child("Odalar").Child("Oda1").Child(takim).
              Child(player).Child("yedekMermi").SetValueAsync("120");
        reference.Child("Odalar").Child("Oda1").Child(takim).
              Child(player).Child("ÖlüDurumu").SetValueAsync("Sağ");
    }
}
