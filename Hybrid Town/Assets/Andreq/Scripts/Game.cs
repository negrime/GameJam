using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject Player_1;
    [SerializeField]
    private GameObject Player_2;

    public Gun[] Player_1g;
    public Gun[] Player_2g;

    public static string namePlayer;

    public static string namePlayer1;
    public static string namePlayer2;


    private static Gun[] guns_1;
    private static Gun[] guns_2;



    void Start()
    {


        guns_1 = Player_1.GetComponentsInChildren<Gun>();
        guns_2 = Player_2.GetComponentsInChildren<Gun>();

        guns_1.ToList().ForEach(itm => itm.QueueShoot = true);
        namePlayer = Player_1.name;

        namePlayer1 = namePlayer;
        namePlayer2 = Player_2.name;

        Player_1g = guns_1;
        Player_2g = guns_2;
    }


    public static void ChangeQueue()
    {
        if(namePlayer == namePlayer1)
        {
            guns_2.ToList().ForEach(itm => {
                itm.QueueShoot = true;
            });
            guns_1.ToList().ForEach(itm => { 
                itm.QueueShoot = false;
                itm.CloseWindow();

            });
            namePlayer = namePlayer2;
        }
        else
        {
            guns_2.ToList().ForEach(itm => { 
                itm.QueueShoot = false;
                itm.CloseWindow();
            });
            guns_1.ToList().ForEach(itm => itm.QueueShoot = true);
            namePlayer = namePlayer1;
        }
    }
}
