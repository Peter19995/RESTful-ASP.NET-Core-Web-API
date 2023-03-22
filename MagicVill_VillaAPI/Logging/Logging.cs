﻿namespace MagicVill_VillaAPI.Logging
{
    public class Logging : ILogging
    {
        public  void Log(string message, string type)
        {
            if(type == "error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("Error - "+message);
            }
            else
            {
                Console.Error.WriteLine(message);
            }
        }
    }
}
