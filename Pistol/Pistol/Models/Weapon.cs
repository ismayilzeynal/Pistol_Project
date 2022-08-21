using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;                             // Sleep() üçün 

namespace Pistol.Models
{
    class Weapon
    {
        //ctor
        public Weapon(int MagazineCapacity, int LeftBulletCount, float DischargeTime, string FireMode)
        {
            this.MagazineCapacity = MagazineCapacity;
            this.LeftBulletCount = LeftBulletCount;
            this.DischargeTime = DischargeTime;
            this.FireMode = FireMode;
        }


        //fields
        private int _magazineCapacity;
        private int _leftBulletCount;
        private float _dischargeTime;
        private string _fireMode;


        // props
        public int MagazineCapacity
        {
            get
            {
                return _magazineCapacity;
            }
            set
            {
                if (value >= 0)
                    _magazineCapacity = value;
            }
        }


        public int LeftBulletCount
        {
            get
            {
                return _leftBulletCount;
            }
            set
            {
                if (value >= 0 && value <= MagazineCapacity)
                    _leftBulletCount = value;
            }
        }



        public float DischargeTime
        {
            get
            {
                return _dischargeTime;
            }
            set
            {
                if (value > 0)
                    _dischargeTime = value;
            }
        }



        public string FireMode
        {
            get
            {
                return _fireMode;
            }
            set
            {
                if (value == "Single" || value == "Auto")
                    _fireMode = value;
            }
        }



        // methods
        public void Shoot()
        {
            if (LeftBulletCount > 0)
            {
                if (FireMode == "Auto")
                {
                    LeftBulletCount--;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("a shot fired\n");
                    Thread.Sleep((int)(1000 * DischargeTime / MagazineCapacity));
                }
                if (LeftBulletCount == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bullets over\n");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
                Reload();
        }



        public void Fire()
        {
            if (LeftBulletCount > 0)
            {
                float time = DischargeTime / MagazineCapacity * LeftBulletCount;
                Thread.Sleep((int)time * 1000);
                Console.WriteLine($"{time} sec");
                LeftBulletCount = 0;
            }
            else
                Reload();
        }


        public int GetRemainBulletCount()
        {
            return MagazineCapacity - LeftBulletCount;
        }


        public void Reload()
        {
            if (LeftBulletCount == MagazineCapacity)
            {
                Console.WriteLine("You can't reload");
                return;
            }
            Console.WriteLine("Reloading...\n");
            Thread.Sleep(3000);
            LeftBulletCount = MagazineCapacity;
            Console.WriteLine("Loaded\n");
        }


        public void ChangeFireMode()
        {
            if (MagazineCapacity == 0)
                Console.WriteLine("You can't change fire mode");
            else
            {
                if (FireMode == "Single")
                    FireMode = "Auto";
                else
                    FireMode = "Single";

                Console.WriteLine($"Fire mode: {FireMode}");
            }
        }



        public void GetInfo()
        {
            Console.WriteLine($"Magazine capacity: {MagazineCapacity} \t Left bullet count: {LeftBulletCount} \t Discharge time: {DischargeTime} \t Fire mode: {FireMode}");
        }




        // Change Methods

        public void ChangeMagCap()
        {
            Console.WriteLine($"Currently magazine capacity: {this.MagazineCapacity}");
            Console.Write("New capacity: ");

        TopMag:
            bool Test = true;
            int MagCap = -1;
            while (Test)
            {
                Program.InputArrows();
                Test = !int.TryParse(Console.ReadLine(), out MagCap);
                if (Test || MagCap < 0)
                    Program.PEC(ref Test);
            }

            if (MagCap == MagazineCapacity)
            {
                Console.WriteLine("New and old can't be same. Please re enter");
                goto TopMag;
            }

            MagazineCapacity = MagCap;
            if (LeftBulletCount > MagazineCapacity)
                LeftBulletCount = MagazineCapacity;
        }


        public void ChangeBulletCount()
        {
            Console.WriteLine($"Currently bullet count: {this.LeftBulletCount}");
            Console.Write("New count: ");

        TopBullet:
            bool Test = true;
            int LeftBullet = -1;
            while (Test)
            {
                Program.InputArrows();
                Test = !int.TryParse(Console.ReadLine(), out LeftBullet);
                if (Test || LeftBullet < 0 || LeftBullet > MagazineCapacity)
                    Program.PEC(ref Test);
            }

            if (LeftBullet == LeftBulletCount)
            {
                Console.WriteLine("New and old can't be same. Please re enter");
                goto TopBullet;
            }

            LeftBulletCount = LeftBullet;
        }


        public void ChangeDischargeTime()
        {
            Console.WriteLine($"Currently discharge time: {this.DischargeTime}");
            Console.Write("New discharge time: ");

        TopDischarge:
            bool Test = true;
            float DisChTime = -1;
            while (Test)
            {
                Program.InputArrows();
                Test = !Single.TryParse(Console.ReadLine(), out DisChTime);
                if (Test || DisChTime < 0)
                    Program.PEC(ref Test);
            }

            if (DisChTime == DischargeTime)
            {
                Console.WriteLine("New and old can't be same. Please re enter");
                goto TopDischarge;
            }

            DischargeTime = DisChTime;
        }
    }
}
