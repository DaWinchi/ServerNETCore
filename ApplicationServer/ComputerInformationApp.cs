using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
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

        Window processWindow;

        Window networkWindow;
        List<NetworkInterface> networkInterfaces;
        int currentInterface;
        long oldBytesRecived;
        long oldBytesSent;

        private void InitializeApplication()
        {
            app = new Application();
            app.GlobalBackgroundColor = ConsoleColor.DarkCyan;
            InitializeMainWindow();
            InitializeCharacterWindow();
            InitializeProcessWindow();
            InitializeNetworkWindow();

            app.AddWindow(mainWindow);


        }

        private void InitializeMainWindow()
        {
            //Создал окно
            mainWindow = new Window(2, 2, 40, 5, "Информация о компьютере", true, 0, ref app);
            mainWindow.BackgroundColor = ConsoleColor.DarkRed;
            mainWindow.TextColor = ConsoleColor.White;

            listWnd = new ListObject(mainWindow.Left + 2, mainWindow.Top + 1, 29, 3, true, true);
            listWnd.BackgroundActiveColor = ConsoleColor.White;
            listWnd.BackgroundColor = ConsoleColor.DarkRed;

            listWnd.List = new List<string>();
            listWnd.ButtonClicked += ListWnd_ButtonClicked;
            listWnd.List.Add("Информация о системе");
            listWnd.List.Add("Информация о процессах");
            listWnd.List.Add("Сетевая статистика");

            btnExit = new ButtonObject(mainWindow.Left + 31, mainWindow.Top + 3, 7, 1, false, true, "Выход");
            btnExit.BackgroundColor = ConsoleColor.Red;
            btnExit.TextColor = ConsoleColor.DarkRed;
            btnExit.BackgroundActiveColor = ConsoleColor.White;
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
                foreach (Window win in app.windows)
                {
                    if (win.IdentificationNumber == 1) win.TimerStart(1000);
                }

            }
            if (((ListObject)sender).ActiveLine == 1)
            {
                processWindow.Update();
                app.AddWindow(processWindow);


            }
            if (((ListObject)sender).ActiveLine == 2)
            {
                networkWindow.Update();
                app.AddWindow(networkWindow);
                foreach (Window win in app.windows)
                {
                    if (win.IdentificationNumber == 3) win.TimerStart(1000);
                }

            }
        }
        private void InitializeCharacterWindow()
        {
            characterWindow = new Window(74, 2, 70, 16, "Информация о системе", false, 1, ref app);
            characterWindow.BackgroundColor = ConsoleColor.DarkRed;
            characterWindow.TextColor = ConsoleColor.White;
            characterWindow.TimerTick += CharacterWindow_TimerTick;
            #region Операционная система
            LabelObject labelOS = new LabelObject(characterWindow.Left + 2, characterWindow.Top + 2,
                                    21, 1, false, false, "Операционная система:");
            labelOS.BackgroundColor = ConsoleColor.DarkRed;
            labelOS.TextColor = ConsoleColor.White;


            LabelObject labelOSinfo = new LabelObject(characterWindow.Left + 2 + 24, characterWindow.Top + 2,
                                    40, 2, false, false, Environment.OSVersion.VersionString +
                                    " " + Environment.OSVersion.Platform.ToString());
            labelOSinfo.BackgroundColor = ConsoleColor.DarkRed;
            labelOSinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Разрядность
            LabelObject labelBit = new LabelObject(labelOS.Left, labelOSinfo.Top + 2,
                                   21, 1, false, false, "Разрядность:");
            labelBit.BackgroundColor = ConsoleColor.DarkRed;
            labelBit.TextColor = ConsoleColor.White;

            bool Is64 = Environment.Is64BitOperatingSystem;
            string bit;
            if (Is64) bit = "x64";
            else bit = "x86";


            LabelObject labelBitinfo = new LabelObject(labelOSinfo.Left, labelOSinfo.Top + 2,
                                    40, 2, false, false, bit);
            labelBitinfo.BackgroundColor = ConsoleColor.DarkRed;
            labelBitinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Число ядер процессора
            LabelObject labelCore = new LabelObject(labelOS.Left, labelBitinfo.Top + 2,
                                   21, 1, false, false, "Число ядер:");
            labelCore.BackgroundColor = ConsoleColor.DarkRed;
            labelCore.TextColor = ConsoleColor.White;

            LabelObject labelCoreinfo = new LabelObject(labelOSinfo.Left, labelBitinfo.Top + 2,
                        40, 2, false, false, Environment.ProcessorCount.ToString());
            labelCoreinfo.BackgroundColor = ConsoleColor.DarkRed;
            labelCoreinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Имя компьютера
            LabelObject labelMachineName = new LabelObject(labelOS.Left, labelCoreinfo.Top + 2,
                                    21, 1, false, false, "Имя компьютера:");
            labelMachineName.BackgroundColor = ConsoleColor.DarkRed;
            labelMachineName.TextColor = ConsoleColor.White;


            LabelObject labelMachineNameinfo = new LabelObject(labelOSinfo.Left, labelCoreinfo.Top + 2,
                                    40, 2, false, false, Environment.MachineName);
            labelMachineNameinfo.BackgroundColor = ConsoleColor.DarkRed;
            labelMachineNameinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Имя пользователя
            LabelObject labelUserName = new LabelObject(labelOS.Left, labelMachineName.Top + 2,
                                   21, 1, false, false, "Имя пользователя:");
            labelUserName.BackgroundColor = ConsoleColor.DarkRed;
            labelUserName.TextColor = ConsoleColor.White;


            LabelObject labelUserNameinfo = new LabelObject(labelOSinfo.Left, labelMachineName.Top + 2,
                                    40, 2, false, false, Environment.UserName);
            labelUserNameinfo.BackgroundColor = ConsoleColor.DarkRed;
            labelUserNameinfo.TextColor = ConsoleColor.White;
            #endregion           

            #region Системное время
            LabelObject labelTime = new LabelObject(labelOS.Left, labelUserNameinfo.Top + 2,
                                   21, 1, false, false, "Системное время:");
            labelTime.BackgroundColor = ConsoleColor.DarkRed;
            labelTime.TextColor = ConsoleColor.White;


            LabelObject labelTimeinfo = new LabelObject(labelOSinfo.Left, labelUserNameinfo.Top + 2,
                                    40, 2, false, false, DateTime.Now.ToLongTimeString());
            labelTimeinfo.BackgroundColor = ConsoleColor.DarkRed;
            labelTimeinfo.TextColor = ConsoleColor.White;
            #endregion

            ButtonObject btnExitCharacterWindow = new ButtonObject(characterWindow.Left + characterWindow.Width - 11,
                                            characterWindow.Top + characterWindow.Height - 2, 9, 1, true, false, "Закрыть");
            btnExitCharacterWindow.TextColor = ConsoleColor.Black;
            btnExitCharacterWindow.BackgroundColor = ConsoleColor.Red;
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
            foreach (Window win in app.windows)
            {
                if (win.IdentificationNumber == 1)
                {
                    ((LabelObject)win.Children[11]).Text = DateTime.Now.ToLongTimeString();
                    win.UpdateChildren(11);
                }
            }
        }

        /*Обработка нажатия кнопки закрытия приложения*/
        private void BtnExit_ButtonClicked(object sender, EventArgs e)
        {
            app.exit = true;
        }

        private void InitializeProcessWindow()
        {
            processWindow = new Window(74, 20, 60, 18, "Информация о процессах", false, 2, ref app);
            processWindow.BackgroundColor = ConsoleColor.DarkRed;
            processWindow.TextColor = ConsoleColor.White;

            ButtonObject btnExitProcessWindow = new ButtonObject(processWindow.Left + processWindow.Width - 12,
                processWindow.Top + processWindow.Height - 3, 9, 1, false, false, "Закрыть");
            btnExitProcessWindow.ButtonClicked += BtnExitProcessWindow_ButtonClicked;
            btnExitProcessWindow.BackgroundColor = ConsoleColor.Red;

            #region Заголовок "Процессы"
            LabelObject labelProcess = new LabelObject(processWindow.Left + 4, processWindow.Top + 2,
                                    10, 1, false, false, "Процессы");
            labelProcess.BackgroundColor = ConsoleColor.DarkRed;
            labelProcess.TextColor = ConsoleColor.White;
            #endregion


            #region Список процессов
            ListObject listProcess = new ListObject(processWindow.Left + 2, processWindow.Top + 3,
                20, 10, true, false);
            listProcess.BackgroundColor = ConsoleColor.DarkRed;
            listProcess.BackgroundActiveColor = ConsoleColor.White;
            listProcess.List = new List<string>();

            Process[] process = Process.GetProcesses();
            List<string> allprocess = new List<string>();

            for (int i = 0; i < process.Length; i++)
            {
                allprocess.Add(process[i].ProcessName);
            }
            listProcess.List.AddRange(allprocess);
            #endregion

            processWindow.AddChildren(labelProcess);
            processWindow.AddChildren(listProcess);
            processWindow.AddChildren(btnExitProcessWindow);
        }

        private void BtnExitProcessWindow_ButtonClicked(object sender, EventArgs e)
        {
            foreach (Window win in app.windows)
            {
                if (win.IdentificationNumber == 2) win.CloseWindow();
            }
        }

        private void InitializeNetworkWindow()
        {
            /*Нахожу поднятые интерфейсы*/
            networkInterfaces = new List<NetworkInterface>();
            NetworkInterface[] allInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            for (int i = 0; i < allInterfaces.Length; ++i)
            {
                if (allInterfaces[i].OperationalStatus == OperationalStatus.Up)
                    networkInterfaces.Add(allInterfaces[i]);
            }
            currentInterface = 0;

            networkWindow = new Window(2, 14, 70, 24, "Сетевая статистика", false, 3, ref app);
            networkWindow.TimerTick += NetworkWindow_TimerTick;
            networkWindow.BackgroundColor = ConsoleColor.DarkRed;
            ButtonObject btnExitNetwork = new ButtonObject(networkWindow.Left + networkWindow.Width - 12
                                , networkWindow.Top + networkWindow.Height - 2, 9, 1, true, false, "Закрыть");
            btnExitNetwork.BackgroundColor = ConsoleColor.Red;
            btnExitNetwork.ButtonClicked += BtnExit_ButtonClicked1;


            LabelObject labelName = new LabelObject(networkWindow.Left + 2, networkWindow.Top + 2, 40, 1, false, false, "Интерфейсы");
            labelName.BackgroundColor = ConsoleColor.Green;
            labelName.TextColor = ConsoleColor.Black;

            ListObject listInterfaces = new ListObject(networkWindow.Left + 2, networkWindow.Top + 3, 66, 3, false, false);
            listInterfaces.BackgroundColor = ConsoleColor.DarkRed;
            listInterfaces.BackgroundActiveColor = ConsoleColor.White;
            listInterfaces.TextColor = ConsoleColor.White;

            listInterfaces.List = new List<string>();
            for (int i = 0; i < networkInterfaces.Count; ++i)
            {
                listInterfaces.List.Add(networkInterfaces[i].Name);
            }

            /////////////////////////////////////////Заголовки подписей/////////////////
            LabelObject labelTitle = new LabelObject(listInterfaces.Left, listInterfaces.Top + 4, 9, 1, false, false, "Название: ");
            labelTitle.BackgroundColor = ConsoleColor.DarkRed;
            labelTitle.TextColor = ConsoleColor.White;


            LabelObject labelDescription = new LabelObject(listInterfaces.Left, labelTitle.Top + 1, 9, 1, false, false, "Описание: ");
            labelDescription.BackgroundColor = ConsoleColor.DarkRed;
            labelDescription.TextColor = ConsoleColor.White;

            LabelObject labelMAC = new LabelObject(listInterfaces.Left, labelTitle.Top + 2, 10, 1, false, false, "MAC-адрес: ");
            labelMAC.BackgroundColor = ConsoleColor.DarkRed;
            labelMAC.TextColor = ConsoleColor.White;

            LabelObject labelStatistics = new LabelObject(labelMAC.Left, labelMAC.Top + 2, 40, 1, false, false, "Статистика");
            labelStatistics.BackgroundColor = ConsoleColor.Green;
            labelStatistics.TextColor = ConsoleColor.Black;

            LabelObject labelDownload = new LabelObject(listInterfaces.Left, labelStatistics.Top + 1, 34, 1, false, false, "Пакетов принято(входящий трафик): ");
            labelDownload.BackgroundColor = ConsoleColor.DarkRed;
            labelDownload.TextColor = ConsoleColor.White;

            LabelObject labelUpload = new LabelObject(listInterfaces.Left, labelDownload.Top + 1, 39, 1, false, false, "Пакетов отправлено(исходящий трафик): ");
            labelUpload.BackgroundColor = ConsoleColor.DarkRed;
            labelUpload.TextColor = ConsoleColor.White;

            LabelObject labelUploadSpeed = new LabelObject(listInterfaces.Left, labelUpload.Top + 2, 40, 1, false, false, "Скорость загрузки:");
            labelUploadSpeed.BackgroundColor = ConsoleColor.DarkRed;
            labelUploadSpeed.TextColor = ConsoleColor.White;
            ///////////////////////////////////////////Вставляемая информация/////////////////
            LabelObject labelTitleinfo = new LabelObject(labelTitle.Left + labelTitle.Width + 1, labelTitle.Top, 40, 1, false, false,
                networkInterfaces[listInterfaces.ActiveLine].Name);
            labelTitleinfo.BackgroundColor = ConsoleColor.DarkRed;
            labelTitleinfo.TextColor = ConsoleColor.White;

            LabelObject labelDescriptioninfo = new LabelObject(labelDescription.Left + labelTitle.Width + 1, labelTitle.Top + 1, 55, 1, false, false,
                networkInterfaces[listInterfaces.ActiveLine].Description);
            labelDescriptioninfo.BackgroundColor = ConsoleColor.DarkRed;
            labelDescriptioninfo.TextColor = ConsoleColor.White;

            LabelObject labelMACinfo = new LabelObject(labelMAC.Left + labelTitle.Width + 2, labelMAC.Top, 55, 1, false, false,
                networkInterfaces[listInterfaces.ActiveLine].GetPhysicalAddress().ToString());
            labelMACinfo.BackgroundColor = ConsoleColor.DarkRed;
            labelMACinfo.TextColor = ConsoleColor.White;

            var ipstat = networkInterfaces[currentInterface].GetIPv4Statistics();
            oldBytesRecived = ipstat.BytesReceived;

            oldBytesSent = ipstat.BytesSent;

            LabelObject labelDownloadinfo = new LabelObject(labelDownload.Left + labelDownload.Width, labelDownload.Top, 20, 1, false, false,
                ipstat.UnicastPacketsReceived.ToString() + "/" + (ipstat.BytesReceived / 1024 / 1024).ToString() +
                " Мбайт");
            labelDownloadinfo.BackgroundColor = ConsoleColor.DarkRed;
            labelDownloadinfo.TextColor = ConsoleColor.White;

            LabelObject labelUploadinfo = new LabelObject(labelUpload.Left + labelUpload.Width, labelUpload.Top, 20, 1, false, false,
                ipstat.UnicastPacketsSent.ToString() + "/" + (ipstat.BytesSent / 1024 / 1024).ToString() +
                " Мбайт");
            labelUploadinfo.BackgroundColor = ConsoleColor.DarkRed;
            labelUploadinfo.TextColor = ConsoleColor.White;

            ProgressObject progressOutput = new ProgressObject(labelStatistics.Left, labelStatistics.Top + 5, 50, 2, false, false);
            progressOutput.TextColor = ConsoleColor.White;
            progressOutput.BackgroundTextColor = ConsoleColor.DarkRed;
            progressOutput.BackgroundColor = ConsoleColor.DarkCyan;
            progressOutput.PercentColor = ConsoleColor.Yellow;
            progressOutput.Min = "0";
            progressOutput.Max = (networkInterfaces[currentInterface].Speed / 1000 / 1000).ToString() + " Мбит";

            networkWindow.AddChildren(btnExitNetwork);
            networkWindow.AddChildren(labelName);
            networkWindow.AddChildren(listInterfaces);
            networkWindow.AddChildren(labelTitle);
            networkWindow.AddChildren(labelDescription);
            networkWindow.AddChildren(labelMAC);
            networkWindow.AddChildren(labelStatistics);
            networkWindow.AddChildren(labelDownload);
            networkWindow.AddChildren(labelUpload);
            networkWindow.AddChildren(labelUploadSpeed);
            networkWindow.AddChildren(labelTitleinfo);
            networkWindow.AddChildren(labelDescriptioninfo);
            networkWindow.AddChildren(labelMACinfo);
            networkWindow.AddChildren(labelDownloadinfo);
            networkWindow.AddChildren(labelUploadinfo);
            networkWindow.AddChildren(progressOutput);
        }

        private void NetworkWindow_TimerTick(object sender, EventArgs e)
        {
            foreach (Window win in app.windows)
            {
                if (win.IdentificationNumber == 3)
                {
                    var ipstat = networkInterfaces[currentInterface].GetIPv4Statistics();
                    long bytesrecived = ipstat.BytesReceived;
                    long bytessent = ipstat.BytesSent;
                    long packsrecived = ipstat.UnicastPacketsReceived;
                    long packssent = ipstat.UnicastPacketsSent;

                    long speedIn =  bytesrecived- oldBytesRecived;
                    long speedOut = bytessent - oldBytesSent;


                    ((LabelObject)win.Children[13]).Text = ipstat.UnicastPacketsReceived.ToString() + "/" +
                        (ipstat.BytesReceived / 1024 / 1024).ToString() + " Мбайт";
                    ((LabelObject)win.Children[14]).Text = ipstat.UnicastPacketsSent.ToString() + "/" +
                        (ipstat.BytesSent / 1024 / 1024).ToString() + " Мбайт";
                    win.UpdateChildren(13);
                    win.UpdateChildren(14);
                }
            }
        }

        private void BtnExit_ButtonClicked1(object sender, EventArgs e)
        {
            foreach (Window win in app.windows)
            {
                if (win.IdentificationNumber == 3) win.CloseWindow();
            }
        }
    }
}
