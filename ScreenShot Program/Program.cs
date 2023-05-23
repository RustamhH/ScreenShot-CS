using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ScreenShot_Program
{
    internal class Program
    {
        static DirectoryInfo di = Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/ScreenShots");
        static int imagecounter = 0;

        public static int Print<T>(List<T> arr, int x, int y)
        {
            int index = 0;
            while (true)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    Console.SetCursorPosition(x, y + i);
                    if (i == index)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine(arr[i]);
                }
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (index == 0) index = arr.Count - 1;
                    else index--;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (index == arr.Count - 1) index = 0;
                    else index++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return index;
                }
            }
        }

        public static void CaptureScreen()
        {
            Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            using (Graphics graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(0, 0, 0, 0, screenshot.Size);
            }
            if(File.Exists("last index.txt")) imagecounter = Convert.ToInt32(File.ReadAllText("last index.txt"));
            string fileName = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/{di.Name}/Screenshot_{++imagecounter}.png";
            
            screenshot.Save(fileName, ImageFormat.Png);
            File.WriteAllText("last index.txt", imagecounter.ToString());
            
        }

        static void Main(string[] args)
        {

            
            while(true)
            {
                int menuchoice = Print(new List<string>() { "Take a ScreenShot", "My Images", "Exit" },10,10);
                if (menuchoice == 0)
                {
                    CaptureScreen();
                }
                else if (menuchoice == 1)
                {
                    List<string> fileNames = new();
                    var allFiles = di.GetFiles();
                    foreach (var item in allFiles)
                    {
                        fileNames.Add(item.Name);
                    }
                    int fileChoice=Print(fileNames, 20, 10);
                    Process.Start(new ProcessStartInfo(allFiles[fileChoice].FullName) { UseShellExecute=true});
                }
                else break;
            }
            

























        }
    }
}