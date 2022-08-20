using Pistol.Models;
using System;

namespace Pistol
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Main:
                Console.Clear();
                MainMenu();

                //Console.WriteLine("0) exit \n1) Main");
                //int choise = -1;
                //while (choise != 0 && choise != 1)
                //    TryInt(out choise);
                //if (choise == 0)
                //    Exit();
                //else
                //    goto Main;
        }





        public static void MainMenu()
        {
            int choise=-1;
            Console.WriteLine("1) Create custom weapon  \n2) Choose advance weapon  \n0) Exit");
            InputArrows();
            while (choise < 0 || choise > 2)
                TryInt(out choise);

            if (choise == 1)
                CustomWeapon();
            else if (choise == 0)
                Exit();
            
                
        }









        public static void CustomWeapon()
        {
            int MagazineCapacity = -1, LeftBulletCount = -1, FireMode = -1;
            float DischargeTime = -1;
            string FireModeName = "";
            bool Test = true;           // (inverse) test for if input is false


            //cls
            Console.WriteLine("Please input fields \n");
            while (Test)
            {
                Console.Write("Magazine capacity: ");
                InputArrows();
                Test = !int.TryParse(Console.ReadLine(), out MagazineCapacity);
                if (Test || MagazineCapacity < 0)
                    PEC(ref Test);
            }
            if (MagazineCapacity == 0 || MagazineCapacity == 1)
            {
                Console.WriteLine("Weapon only have a single fire mode");
                FireModeName = "Single";
                DischargeTime = 0;
                MagazineCapacity = 1;
                LeftBulletCount = 1;
                goto Bottom;
            }

            Test = true;
            while (Test)
            {
                Console.Write("Bullet count in magazine: ");
                InputArrows();
                Test = !int.TryParse(Console.ReadLine(), out LeftBulletCount);
                if (Test || LeftBulletCount < 0 || LeftBulletCount > MagazineCapacity)
                    PEC(ref Test);
            }
            

            Test = true;
            while (Test)
            {
                Console.Write("Discharge time: ");
                InputArrows();
                Test = !Single.TryParse(Console.ReadLine(), out DischargeTime);
                if (Test || DischargeTime <= 0)
                    PEC(ref Test);

            }


            Test = true;
            while (Test)
            {
                Console.WriteLine("Choose fire mode:  \n1) Single  \n2) Auto");
                InputArrows();
                Test = !int.TryParse(Console.ReadLine(), out FireMode);
                if (Test || FireMode < 1 || FireMode > 2)
                    PEC(ref Test);
                else
                {
                    if (FireMode == 1)
                        FireModeName = "Single";
                    else
                        FireModeName = "Auto";
                }
            }

            Bottom:
                Weapon Gun = new Weapon(MagazineCapacity, LeftBulletCount, DischargeTime, FireModeName);
                CommandMenu(Gun);
        }








        public static void CommandMenu(Weapon Gun)
        {
            Commands:
                Console.Clear();
                int choise = -1;
                Console.WriteLine("0) Info \n1) Shoot \n2) Fire \n3) Get Remain Bullet Count \n4) Reload \n5) Change Fire Mode \n6) Exit \n7) Edit");
                while (!(choise >= 0 && choise < 8))
                    TryInt(out choise);

                Console.Clear();
                switch(choise)
                {
                    case 0:
                        Gun.GetInfo();
                        break;
                    case 1:
                        Gun.Shoot();
                        break;
                    case 2:
                        Gun.Fire();
                        break;
                    case 3:
                    Console.WriteLine(Gun.GetRemainBulletCount()); 
                        break;
                    case 4:
                        Gun.Reload();
                        break;
                    case 5:
                        Gun.ChangeFireMode();
                        break;
                    case 6:
                        Exit();
                        return;
                        break;
                    case 7:
                        Edit(Gun);
                        break;
                    default:
                        Console.WriteLine("Error 404");
                        break;
                }

                choise = -1;
                Console.WriteLine("0) Back  \n1) Exit");
                while (choise != 0 && choise != 1)
                    TryInt(out choise);
                if (choise == 0)
                    goto Commands;
                else
                    Exit();


        }






        public static void Edit(Weapon Gun)
        {
            char choise = ' ';
            Console.WriteLine("T) Magazine capacity \nS) Bullets count \nD) Discharge time");
            while (choise != 'T' && choise != 'S' && choise != 'D')
                TryChar(out choise);


            switch (choise)
            {
                case 'T':
                    Gun.ChangeMagCap();
                    break;
                case 'S':
                    Gun.ChangeBulletCount();
                    break;
                case 'D':
                    Gun.ChangeDischargeTime();
                    break;
            }

        }














        // little help methods

        public static void PEC(ref bool test)           // please enter correctly
        {
            Console.WriteLine("Please enter correctly");
            test = true;
        }


        public static void InputArrows()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(">>> ");
            Console.ForegroundColor = ConsoleColor.White;
        }
        

        public static void TryInt(out int choise)
        {

            while (!int.TryParse(Console.ReadLine(), out choise))
            {
                InputArrows();
                Console.WriteLine("Please enter correctly");
            }
        }

        public static void TryChar(out char choise)
        {
            while (!Char.TryParse(Console.ReadLine(), out choise))
            {
                Console.WriteLine("Please enter correctly");
            }
            choise = Char.ToUpper(choise);
        }


        public static void Exit()
        {
            Console.WriteLine("Thanks for using");
        }


    }
}
