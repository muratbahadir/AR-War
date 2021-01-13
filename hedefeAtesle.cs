using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
public class hedefeAtesle : MonoBehaviour
{
    DatabaseReference reference;
     public string vurulan;
     public GameObject nişan;
     string vuran;
     string takim,myTeam;
  
    public void Ates()
    {   
        PlayerPrefs.SetString("false", "RakipAktif");
        int takim1 = int.Parse(vurulan.Substring(vurulan.Length - 1)) % 2; 
        takim ="Takim"+ takim1.ToString();
        myTeam= PlayerPrefs.GetString("Takim");
        vuran = PlayerPrefs.GetString("Player");
        
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        
        reference
       .GetValueAsync().ContinueWith(task =>
       {
           if (task.IsFaulted)
           {
               // Handle the error...
           }
           else if (task.IsCompleted)
           {
               DataSnapshot snapshot = task.Result;
               string dokunulmazlik= snapshot.Child("Odalar").Child("Oda1").Child(takim).
               Child(vurulan).Child("Dokunulmazlik").Value.ToString();
               string ölüDurumu= snapshot.Child("Odalar").Child("Oda1").Child(takim).
               Child(vurulan).Child("ölüDurumu").Value.ToString();
                
               if(dokunulmazlik=="false" && ölüDurumu=="Sağ")
               {
                   print("Aktif kişi vuruldu");
                   string can = snapshot.Child("Odalar").Child("Oda1").Child(takim).
                                 Child(vurulan).Child("can").Value.ToString();
                   if (int.Parse(can) > 0)
                   {
                       can = (int.Parse(can) - 10).ToString();
                       if (int.Parse(can) <= 0)
                       {
                           reference.Child("Odalar").Child("Oda1").Child(takim).
                            Child(vurulan).Child("ölüDurumu").SetValueAsync("Ölü");
                           string takimSkor = snapshot.Child("Odalar").Child("Oda1").Child(myTeam).
                            Child("TakimSkor").Value.ToString();
                           takimSkor = (int.Parse(takimSkor) + 1).ToString();
                           if (takimSkor=="5")
                           {
                               
                              snapshot.Child("Odalar").Child("Oda1").Child("Eylemler").Reference.RemoveValueAsync();
                           }
                           reference.Child("Odalar").Child("Oda1").Child(myTeam).
                           Child("TakimSkor").SetValueAsync(takimSkor);

                           string öldürülmeS= snapshot.Child("Odalar").Child("Oda1").Child(takim).
                            Child(vurulan).Child("ÖldürülmeSayisi").Value.ToString();
                           öldürülmeS = (int.Parse(öldürülmeS) + 1).ToString();
                           reference.Child("Odalar").Child("Oda1").Child(takim).
                            Child(vurulan).Child("ÖldürülmeSayisi").SetValueAsync(öldürülmeS);
                           string öldürmeS = snapshot.Child("Odalar").Child("Oda1").Child(myTeam).
                                            Child(vuran).Child("ÖldürmeSayisi").Value.ToString();
                           öldürmeS = (int.Parse(öldürmeS) + 1).ToString();
                           reference.Child("Odalar").Child("Oda1").Child(myTeam).
                           Child(vuran).Child("ÖldürmeSayisi").SetValueAsync(öldürmeS);
                           reference.Child("Odalar").Child("Oda1").Child("Eylemler").Push().SetValueAsync(vuran + " oyuncusu " + vurulan + " oyuncusunu öldürdü " );

                       }

                       reference.Child("Odalar").Child("Oda1").Child(takim).
                            Child(vurulan).Child("can").SetValueAsync(can);
                   }
                   
               }
               else
               {
                  // print("Oyuncu Aktif değil!");
               }
               
           }
              
       });

    }
}