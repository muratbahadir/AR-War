using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;
public class mermiYenile : MonoBehaviour
{
    DatabaseReference reference;
    int mermi;
    int yMermi;
    public Text MermiText;
    public AudioSource ses;

    public void SarjorTak()
    {
        
        yMermi = int.Parse(PlayerPrefs.GetString("YMermiDurumu"));
        mermi = int.Parse(PlayerPrefs.GetString("MermiDurumu"));
        int yMermi2 = yMermi;
        ses.Play();
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        mermiKontrolveDegistir(yMermi2);
        MermiText.text = mermi.ToString() + " / " + yMermi.ToString();
        PlayerPrefs.SetString("MermiDurumu", mermi.ToString());
        PlayerPrefs.SetString("YMermiDurumu", yMermi.ToString());
    }

    private void mermiKontrolveDegistir(int yMermi2)
    {
        if (yMermi2 > 0)
        {
            if (yMermi2>=30)
            {
                yMermi -= (30 - mermi);
                mermi = 30;
            }
            else
            {
                if(yMermi2< 30 - mermi)
                {
                    yMermi = 0;
                    mermi += yMermi2;
                }
                else
                {
                    yMermi -= (30 - mermi);
                    mermi = 30;
                }

            }
            PlayerPrefs.SetString("MermiDurumu", mermi.ToString());
            PlayerPrefs.SetString("YMermiDurumu", (yMermi).ToString());
            reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
                Child(PlayerPrefs.GetString("Player")).Child("anlikMermi").SetValueAsync(mermi.ToString());
            reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
                Child(PlayerPrefs.GetString("Player")).Child("yedekMermi").SetValueAsync((yMermi).ToString());
        }
       
    }

}
