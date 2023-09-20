using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army
    {//Army matrix is needed if friendly to each other/ like move trough ok?
        public string leader;
        public int benders;
        public int levees;

        //public int supplies;
        public int movementPoints = 3;
        public int CurrentMovementPoints = 3;
        //public GameObject Armypicture;

        public Army(string newleader)
        {
            leader = newleader;
        }


        public int addbenders(int amount)
        {
            benders += amount;
            return benders;
        }

        public int addlevees(int amount)
        {
            levees += amount;
            return levees;
        } 

        static public bool endturnArmies(){
            
            return true;
        }
    }
