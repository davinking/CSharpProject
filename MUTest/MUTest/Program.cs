﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUTest.MuThread;
using MUTest.DelegateTest;

namespace MUTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //线程测试
            //ExecuteThread _Thread = new ExecuteThread(1000);

            TestMuTestDeleagate();

            Console.ReadLine();
        }

        public static void TestMuTestDeleagate()
        {
            MuTestDelegate mtd = new MuTestDelegate();

            mtd.Completed += new EventHandler<MuTestDelegate>(IO_Completed);
        }

        static void IO_Completed(object sender, MuTestDelegate e)
        {
            Console.WriteLine("hello IO_Completed");
        }
    }
}
