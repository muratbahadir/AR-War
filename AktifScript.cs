using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Database;


public class AktifScript : MonoBehaviour
{
    public Text mermiDurumText;
    public Text CanDurumText;
    public Text eylemler;
    Text skor1T;
    Text skor2T;
    Text uyariText;
    string player;
    string takim;
    string rakipTakim;
    float zaman;
    public GameObject vurulmaSembol;
    public GameObject uyariObje,uyarıPanel;
    public GameObject skor1;
    public GameObject skor2;
    Image vurulmaResim;
    bool oluDurumu;
    bool yaraAlma;
    bool ilk;
    DatabaseReference reference;
  
    
    void Start()
    {

        IlkAtamalar();
        DegisimleriDinle();


        
    }

    private void DegisimleriDinle()
    {
        reference.Child("Odalar").Child("Oda1").Child("Eylemler").ChildAdded += EylemEklendi;
        reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
       Child(PlayerPrefs.GetString("Player")).Child("can").ValueChanged += CanDegisimi;
        reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
       Child("TakimSkor").ValueChanged += TakimSkorDegisim;
        int takim1 = (int.Parse(PlayerPrefs.GetString("Takim").Substring(PlayerPrefs.GetString("Takim").Length - 1)) + 1) % 2;
        rakipTakim = "Takim" + takim1.ToString();

        reference.Child("Odalar").Child("Oda1").Child(rakipTakim).
       Child("TakimSkor").ValueChanged += RakipSkorDegisim;
        reference.Child("Oda1").ValueChanged += FinalEkranTransfer;
    }

    private void IlkAtamalar()
    {
        ilk = true;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        zaman = 3f;
        player = PlayerPrefs.GetString("Player");
        takim = PlayerPrefs.GetString("Takim");
        reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
        Child(PlayerPrefs.GetString("Player")).Child("Dokunulmazlik").SetValueAsync("True");
        yaraAlma = false;
        oluDurumu = false;

        skor1T = skor1.GetComponent<Text>();
        skor2T = skor2.GetComponent<Text>();
        skor1T.text = "0";
        skor2T.text = "0";

        uyariText = uyariObje.GetComponent<Text>();
        uyariObje.active = false;
        uyarıPanel.active = false;
        vurulmaResim = vurulmaSembol.GetComponent<Image>();

        vurulmaSembol.active = false;
        mermiDurumText = GetComponent<Text>();
        mermiDurumText.text = PlayerPrefs.GetString("MermiDurumu") + "/" + PlayerPrefs.GetString("YMermiDurumu");
        CanDurumText.text = PlayerPrefs.GetString("Can") + "/100";
    }












    private void EylemEklendi(object sender, ChildChangedEventArgs e)
    {
        
        eylemler.text = e.Snapshot.Value.ToString()+ "\n"+ eylemler.text ;
    }

    void CanDegisimi(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

       
       // print("Caniniz Azaldi");
        if (PlayerPrefs.GetInt("İlkDeğişim") == 0)
        {


            
                int sonCan = (int.Parse(PlayerPrefs.GetString("Can")) - 10);
                PlayerPrefs.SetString("Can", sonCan.ToString());

                if (int.Parse(PlayerPrefs.GetString("Can")) <= 0)
                {
                    //print("Öldürüldük");
                    oluDurumu = true;
                uyariText.text = "10 saniye içerisinde yeniden dirileceksiniz";
                uyariObje.active = true;
                uyarıPanel.active = true;
                zaman = 10f;
                yaraAlma = false;
                }

                else
                {
                   // print("Kursun yedin");
                    vurulmaSembol.active = true;
                    zaman = 2f;
                    yaraAlma = true;

                }

                CanDurumText.text = PlayerPrefs.GetString("Can") + "/100";




            

          
        }    
      
    }

    void TakimSkorDegisim(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }


        if (PlayerPrefs.GetInt("İlkDeğişim") == 0)
        {



           
            if (int.Parse(args.Snapshot.Value.ToString())==5)
            {
                
                PlayerPrefs.SetString("Winner", PlayerPrefs.GetString("Takim"));
                PlayerPrefs.SetString("Kazanan", PlayerPrefs.GetString("Takim"));
                SceneManager.LoadScene("finalEkran");
                //print("Oyun bitti kazanan:"+ PlayerPrefs.GetString("Takim"));
                
            }
            else
                skor1T.text = args.Snapshot.Value.ToString();







        }
     
    }
    void RakipSkorDegisim(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }


        if (PlayerPrefs.GetInt("İlkDeğişim") == 0)
        {




            if (int.Parse(args.Snapshot.Value.ToString()) == 5)
            {
                
                PlayerPrefs.SetString("Winner", rakipTakim);
                PlayerPrefs.SetString("Kazanan", rakipTakim);
                SceneManager.LoadScene("finalEkran");
                //print("Oyun bitti kazanan:" + rakipTakim);
                
            }
            else

                skor2T.text = args.Snapshot.Value.ToString();

        }
        
    }


    void FinalEkranTransfer(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }


        if (PlayerPrefs.GetInt("İlkDeğişim") == 0)
        {



            
            DataSnapshot snapshot = args.Snapshot;
            if (int.Parse(args.Snapshot.Child("Odalar").Child("Oda1").Child(rakipTakim).
       Child("TakimSkor").Value.ToString()) == 5 ||
       int.Parse(args.Snapshot.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
       Child("TakimSkor").Value.ToString())==5)
            {

                
             PlayerPrefs.SetString("RP1K", snapshot.Child("Odalar").Child("Oda1").Child("Takim0").
               Child("Player2").Child("ÖldürmeSayisi").Value.ToString());
             print(PlayerPrefs.GetString("RP1K")+"AS");
             PlayerPrefs.SetString("RP1D", snapshot.Child("Odalar").Child("Oda1").Child("Takim0").
             Child("Player2").Child("ÖldürülmeSayisi").Value.ToString());
             PlayerPrefs.SetString("RP2K", snapshot.Child("Odalar").Child("Oda1").Child("Takim0").
                Child("Player4").Child("ÖldürmeSayisi").Value.ToString());
             PlayerPrefs.SetString("RP2D", snapshot.Child("Odalar").Child("Oda1").Child("Takim0").
             Child("Player4").Child("ÖldürülmeSayisi").Value.ToString());
             PlayerPrefs.SetString("RPS", snapshot.Child("Odalar").Child("Oda1").Child("Takim0").
               Child("TakimSkor").Value.ToString());

             PlayerPrefs.SetString("BP1K", snapshot.Child("Odalar").Child("Oda1").Child("Takim1").
               Child("Player1").Child("ÖldürmeSayisi").Value.ToString());
             PlayerPrefs.SetString("BP1D", snapshot.Child("Odalar").Child("Oda1").Child("Takim1").
             Child("Player1").Child("ÖldürülmeSayisi").Value.ToString());
             PlayerPrefs.SetString("BP2K", snapshot.Child("Odalar").Child("Oda1").Child("Takim1").
                Child("Player3").Child("ÖldürmeSayisi").Value.ToString());
             PlayerPrefs.SetString("BP2D", snapshot.Child("Odalar").Child("Oda1").Child("Takim1").
             Child("Player3").Child("ÖldürülmeSayisi").Value.ToString());
             PlayerPrefs.SetString("BPS", snapshot.Child("Odalar").Child("Oda1").Child("Takim1").
               Child("TakimSkor").Value.ToString());
             
            }

        }
        // Do something with the data in args.Snapshot
    }



    void Update()
    {
        if(ilk)
        {
            if (zaman>0)
            {
                zaman -= Time.deltaTime;
              //  print("Dokunulmazsınız");
            }
            else
            {
               // print("Aktifsiniz");
                PlayerPrefs.SetInt("İlkDeğişim",0);
                reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
                Child(PlayerPrefs.GetString("Player")).Child("Dokunulmazlik").SetValueAsync("false");
                uyariObje.active = false;
                uyarıPanel.active = false;
                ilk = false;
            }
        }
      
       if (yaraAlma)
        {
            if (zaman>0)
            {
               
                zaman -= Time.deltaTime;
                //print("Vuruluyorsunuz");
                vurulmaResim.color = new Color(vurulmaResim.color.r, vurulmaResim.color.g, vurulmaResim.color.b, 2*zaman%1);
            }
            else
            {
                //print("zaman durdu");
                vurulmaSembol.active = false;
                uyariObje.active = false;
                uyarıPanel.active = false;
                yaraAlma = false;
            }
        }
       if(oluDurumu)
        {
            if (zaman > 0)
            {

                zaman -= Time.deltaTime;
                //print("Ölüsünüz.!");
                vurulmaSembol.active = false;
            }
            else
            {
               // print("Dirildiniz");
                reference.Child("Odalar").Child("Oda1").Child(takim).
                      Child(player).Child("ölüDurumu").SetValueAsync("Sağ");
                reference.Child("Odalar").Child("Oda1").Child(takim).
                      Child(player).Child("can").SetValueAsync("100");
                reference.Child("Odalar").Child("Oda1").Child(takim).
                      Child(player).Child("anlikMermi").SetValueAsync("30");
                reference.Child("Odalar").Child("Oda1").Child(takim).
                      Child(player).Child("yedekMermi").SetValueAsync("120");
                PlayerPrefs.SetString("MermiDurumu", "30");
                PlayerPrefs.SetString("YMermiDurumu", "120");
                PlayerPrefs.SetString("Can", "100");
                mermiDurumText.text = PlayerPrefs.GetString("MermiDurumu") + "/" + PlayerPrefs.GetString("YMermiDurumu");
                CanDurumText.text = PlayerPrefs.GetString("Can") + "/100";
                PlayerPrefs.SetInt("İlkDeğişim", -1);
                ilk = true;
                reference.Child("Odalar").Child("Oda1").Child(PlayerPrefs.GetString("Takim")).
                Child(PlayerPrefs.GetString("Player")).Child("Dokunulmazlik").SetValueAsync("true");
                yaraAlma = false;
                zaman = 3f;
                oluDurumu = false;
                vurulmaSembol.active = false;
                uyariText.text = "3 saniye boyunca dokunulmazsınız";
            }
        }

    }
    
}
