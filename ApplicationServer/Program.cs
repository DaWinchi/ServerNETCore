using System;
using System.Collections.Generic;
using WindowsLibrary;

namespace ApplicationServer
{
    
    class Program
    {
      
        static void Main(string[] args)
        {
           Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ComputerInformationApp compInfo = new ComputerInformationApp();
            compInfo.app.Run();            

        }

      
    }

    
}
