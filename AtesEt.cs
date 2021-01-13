using UnityEngine;
using Firebase.Database;
using   UnityEngine.UI;

public class AtesEt : MonoBehaviour
{
   
    public GameObject hareketliMermi,ornekMermi;
    Text mermiDurumText;
    public AudioSource atesSesi;
    DatabaseReference reference;
    int mermi;
    string mermiS;

    
    public void Atesle()
    {   
        
        
        mermiDurumText = GameObject.Find("ARCamera/Canvas/MermiDurumlari").GetComponent<Text>();
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        ornekMermi = Instantiate(hareketliMermi, new Vector3(-0.138f, -0.27f, 1.80f), transform.rotation);
        mermiS = PlayerPrefs.GetString("MermiDurumu");
        mermi = int.Parse(mermiS);

        if (mermi > 0)
        {
            mermi -- ;
            mermiS = mermi.ToString();
            reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
            Child(PlayerPrefs.GetString("Player")).Child("anlikMermi").SetValueAsync(mermi.ToString());
            print("Yeni Mermi Durumu:" + mermiS);          
        } 
        PlayerPrefs.SetString("MermiDurumu", mermiS);
        mermiDurumText.text = PlayerPrefs.GetString("MermiDurumu") + "/" + PlayerPrefs.GetString("YMermiDurumu");
       
    }
    
}
