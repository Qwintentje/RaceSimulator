﻿using Controller;

namespace Main;

public class Program
{
    static void Main(string[] args)
    {
        Data.Initialize();
        Data.NextRace();

        for (; ; )
        {
            Thread.Sleep(100);
        }
    }


}