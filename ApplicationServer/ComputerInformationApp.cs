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
                foreach(Window win in app.windows)
                {
                    if (win.IdentificationNumber == 1) win.TimerStart(1000);
                }

            }
        }
        private void InitializeCharacterWindow()
        {
            characterWindow = new Window(40, 10, 70, 20, "Характеристики системы", false, 1, ref app);
            characterWindow.TimerTick += CharacterWindow_TimerTick;
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

            #region Число ядер процессора
            LabelObject labelCore = new LabelObject(labelOS.Left, labelBitinfo.Top + 2,
                                   21, 1, false, false, "Число ядер:");
            labelCore.BackgroundColor = ConsoleColor.Black;
            labelCore.TextColor = ConsoleColor.White;

            LabelObject labelCoreinfo = new LabelObject(labelOSinfo.Left, labelBitinfo.Top + 2,
                        40, 2, false, false, Environment.ProcessorCount.ToString());
            labelCoreinfo.BackgroundColor = ConsoleColor.Black;
            labelCoreinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Имя компьютера
            LabelObject labelMachineName = new LabelObject(labelOS.Left, labelCoreinfo.Top + 2,
                                    21, 1, false, false, "Имя компьютера:");
            labelMachineName.BackgroundColor = ConsoleColor.Black;
            labelMachineName.TextColor = ConsoleColor.White;


            LabelObject labelMachineNameinfo = new LabelObject(labelOSinfo.Left, labelCoreinfo.Top + 2,
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

            #region Системное время
            LabelObject labelTime = new LabelObject(labelOS.Left, labelCataloginfo.Top + 2,
                                   21, 1, false, false, "Системное время:");
            labelTime.BackgroundColor = ConsoleColor.Black;
            labelTime.TextColor = ConsoleColor.White;


            LabelObject labelTimeinfo = new LabelObject(labelOSinfo.Left, labelCataloginfo.Top + 2,
                                    40, 2, false, false, DateTime.Now.ToLongTimeString());
            labelTimeinfo.BackgroundColor = ConsoleColor.Black;
            labelTimeinfo.TextColor = ConsoleColor.White;
            #endregion

            ButtonObject btnExitCharacterWindow = new ButtonObject(characterWindow.Left + characterWindow.Width - 10,
                                            characterWindow.Top + characterWindow.Height - 2, 9, 1, true, false, "Закрыть");
            btnExitCharacterWindow.ButtonClicked += BtnExitCharacterWindow_ButtonClicked;

            characterWindow.AddChildren(labelOS);
            characterWindow.AddChildren(labelOSinfo);
            characterWindow.AddChildren(labelBit);
            characterWindow.AddChildren(labelBitinfo);
            characterWindow.AddChildren(labelCore);
            characterWindow.AddChildren(labelCoreinfo);
            characterWindow.AddChildren(labelMachineName);
            characterWindow.AddChildren(labelMachineNameinfo);
            characterWindow.AddChildren(labelUserName);
            characterWindow.AddChildren(labelUserNameinfo);
            characterWindow.AddChildren(labelCatalog);
            characterWindow.AddChildren(labelCataloginfo);
            characterWindow.AddChildren(labelTime);
            characterWindow.AddChildren(labelTimeinfo);
            characterWindow.AddChildren(btnExitCharacterWindow);
        }

        private void BtnExitCharacterWindow_ButtonClicked(object sender, EventArgs e)
        {
            foreach (Window win in app.windows)
            {
                if (win.IdentificationNumber == 1) win.CloseWindow();
            }
            
        }

        private void CharacterWindow_TimerTick(object sender, EventArgs e)
        {
           foreach(Window win in app.windows)
            {
                if(win.IdentificationNumber==1)
                {
                    ((LabelObject)win.Children[13]).Text = DateTime.Now.ToLongTimeString();
                    win.UpdateChildren(13);
                }
            }
        }

        /*Обработка нажатия кнопки закрытия приложения*/
        private void BtnExit_ButtonClicked(object sender, EventArgs e)
        {
            app.exit = true;
        }
    }
}
