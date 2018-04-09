using System;
using System.Collections.Generic;
using System.Text;
using WindowsLibrary;

namespace ApplicationServer
{
    public class ComputerInformationApp
    {
        public ComputerInformationApp()
        {
            InitializeApplication();
        }

        /*Приложение*/
        public Application app;

        /********Главное окно**********/
        Window mainWindow;

        ListObject listWnd;  //Список для выбора между просмотром требуемых характеристик
        ButtonObject btnExit; //Кнопка закрытия приложения

        /******************************/

        /******Окно характеристик системы****/
        Window characterWindow;

        /************************************/

        private void InitializeApplication()
        {
            app = new Application();
            app.GlobalBackgroundColor = ConsoleColor.Gray;
            InitializeMainWindow();
            InitializeCharacterWindow();

            app.AddWindow(mainWindow);


        }

        private void InitializeMainWindow()
        {
            //Создал окно
            mainWindow = new Window(2, 2, 40, 5, "Информация о компьютере", true, 0, ref app);


            listWnd = new ListObject(mainWindow.Left + 2, mainWindow.Top + 1, 29, 3, true, true);
            listWnd.List = new List<string>();
            listWnd.ButtonClicked += ListWnd_ButtonClicked;
            listWnd.List.Add("Характеристики системы");
            listWnd.List.Add("Список процессов");
            listWnd.List.Add("Сетевая статистика");

            btnExit = new ButtonObject(mainWindow.Left + 31, mainWindow.Top + 3, 7, 1, false, true, "Выход");
            btnExit.ButtonClicked += BtnExit_ButtonClicked;

            mainWindow.AddChildren(btnExit);
            mainWindow.AddChildren(listWnd);
        }

        private void ListWnd_ButtonClicked(object sender, EventArgs e)
        {
            if (((ListObject)sender).ActiveLine == 0)
            {
                characterWindow.Update();
                app.AddWindow(characterWindow);

            }
        }
        private void InitializeCharacterWindow()
        {
            characterWindow = new Window(40, 10, 70, 20, "Характеристики системы", false, 1, ref app);

            #region Операционная система
            LabelObject labelOS = new LabelObject(characterWindow.Left + 2, characterWindow.Top + 2,
                                    21, 1, false, false, "Операционная система:");
            labelOS.BackgroundColor = ConsoleColor.Black;
            labelOS.TextColor = ConsoleColor.White;
            

            LabelObject labelOSinfo = new LabelObject(characterWindow.Left + 2 + 24, characterWindow.Top + 2,
                                    40, 2, false, false, Environment.OSVersion.VersionString +
                                    " " + Environment.OSVersion.Platform.ToString());
            labelOSinfo.BackgroundColor = ConsoleColor.Black;
            labelOSinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Разрядность
            LabelObject labelBit = new LabelObject(labelOS.Left, labelOSinfo.Top + 2,
                                   21, 1, false, false, "Разрядность:");
            labelBit.BackgroundColor = ConsoleColor.Black;
            labelBit.TextColor = ConsoleColor.White;

            bool Is64 = Environment.Is64BitOperatingSystem;
            string bit;
            if (Is64) bit = "x64";
            else bit = "x86";
            

            LabelObject labelBitinfo = new LabelObject(labelOSinfo.Left, labelOSinfo.Top + 2,
                                    40, 2, false, false, bit);
            labelBitinfo.BackgroundColor = ConsoleColor.Black;
            labelBitinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Имя компьютера
            LabelObject labelMachineName = new LabelObject(labelOS.Left, labelBit.Top + 2,
                                    21, 1, false, false, "Имя компьютера:");
            labelMachineName.BackgroundColor = ConsoleColor.Black;
            labelMachineName.TextColor = ConsoleColor.White;
            

            LabelObject labelMachineNameinfo = new LabelObject(labelOSinfo.Left, labelBit.Top + 2,
                                    40, 2, false, false, Environment.MachineName);
            labelMachineNameinfo.BackgroundColor = ConsoleColor.Black;
            labelMachineNameinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Имя пользователя
            LabelObject labelUserName = new LabelObject(labelOS.Left, labelMachineName.Top + 2,
                                   21, 1, false, false, "Имя пользователя:");
            labelUserName.BackgroundColor = ConsoleColor.Black;
            labelUserName.TextColor = ConsoleColor.White;


            LabelObject labelUserNameinfo = new LabelObject(labelOSinfo.Left, labelMachineName.Top + 2,
                                    40, 2, false, false, Environment.UserName);
            labelUserNameinfo.BackgroundColor = ConsoleColor.Black;
            labelUserNameinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Системный каталог
            LabelObject labelCatalog = new LabelObject(labelOS.Left, labelUserNameinfo.Top + 2,
                                   21, 1, false, false, "Системный каталог:");
            labelCatalog.BackgroundColor = ConsoleColor.Black;
            labelCatalog.TextColor = ConsoleColor.White;


            LabelObject labelCataloginfo = new LabelObject(labelOSinfo.Left, labelUserNameinfo.Top + 2,
                                    40, 2, false, false, Environment.SystemDirectory);
            labelCataloginfo.BackgroundColor = ConsoleColor.Black;
            labelCataloginfo.TextColor = ConsoleColor.White;
            #endregion

           

            characterWindow.AddChildren(labelOS);
            characterWindow.AddChildren(labelOSinfo);
            characterWindow.AddChildren(labelBit);
            characterWindow.AddChildren(labelBitinfo);
            characterWindow.AddChildren(labelMachineName);
            characterWindow.AddChildren(labelMachineNameinfo);
            characterWindow.AddChildren(labelUserName);
            characterWindow.AddChildren(labelUserNameinfo);
            characterWindow.AddChildren(labelCatalog);
            characterWindow.AddChildren(labelCataloginfo);
          
        }

        /*Обработка нажатия кнопки закрытия приложения*/
        private void BtnExit_ButtonClicked(object sender, EventArgs e)
        {
            app.exit = true;
        }
    }
}
