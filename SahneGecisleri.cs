using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Database;
public class SahneGecisleri : MonoBehaviour
{
    string mermi;
    DatabaseReference reference;
    public void MenuyeDon()
    {
        SceneManager.LoadScene("MenuSahne");
    }
    public void oyunaBasla()
    {
        SceneManager.LoadScene("OdaGir");
    }
    public void odalariBul()
    {
        SceneManager.LoadScene("OdaBulKarakterSec");
    }
    public void OyuncuSec(string player)
    {


        AtamaYap(player);
        VTyaz();
        SceneManager.LoadScene("OyunEkrani");
    }
    void VTyaz()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
                    Child(PlayerPrefs.GetString("Player")).Child("anlikMermi").SetValueAsync("30");
        reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
             Child(PlayerPrefs.GetString("Player")).Child("yedekMermi").SetValueAsync("120");
        reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
             Child(PlayerPrefs.GetString("Player")).Child("Dokunulmazlik").SetValueAsync("false");
        reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
             Child(PlayerPrefs.GetString("Player")).Child("can").SetValueAsync("100");
        reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
             Child(PlayerPrefs.GetString("Player")).Child("ÖldürmeSayisi").SetValueAsync("0");
        reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
             Child(PlayerPrefs.GetString("Player")).Child("ÖldürülmeSayisi").SetValueAsync("0");
        reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
             Child(PlayerPrefs.GetString("Player")).Child("ölüDurumu").SetValueAsync("Sağ");

    }
    void AtamaYap(string player)
    {
        PlayerPrefs.SetString("Player", player);
        int takim1 = int.Parse(player.Substring(player.Length - 1)) % 2;
        PlayerPrefs.SetString("Takim", "Takim" + takim1.ToString());
        PlayerPrefs.SetString("Öldürülme", "0");
        PlayerPrefs.SetString("Öldürme", "0");
        PlayerPrefs.SetString("MermiDurumu", "30");
        PlayerPrefs.SetString("YMermiDurumu", "160");
        PlayerPrefs.SetInt("İlkDeğişim", -1);
        PlayerPrefs.SetString("Can", "100");
    }

   
}
