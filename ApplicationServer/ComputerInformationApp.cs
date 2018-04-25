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
        List<Process> process;
        List<ProcessModuleCollection> processModule;

        Window networkWindow;
        List<NetworkInterface> networkInterfaces;
        int currentInterface;
        long oldBytesRecived;
        long oldBytesSent;
        long MaxSpeed;

        private void InitializeApplication()
        {
            app = new Application();
            app.GlobalBackgroundColor = ConsoleColor.DarkGray;
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
            mainWindow.BackgroundColor = ConsoleColor.Blue;
            mainWindow.TextColor = ConsoleColor.White;

            listWnd = new ListObject(mainWindow.Left + 2, mainWindow.Top + 1, 29, 3, true, true);
            listWnd.BackgroundActiveColor = ConsoleColor.White;
            listWnd.BackgroundColor = ConsoleColor.Blue;

            listWnd.List = new List<string>();
            listWnd.ButtonClicked += ListWnd_ButtonClicked;
            listWnd.List.Add("Информация о системе");
            listWnd.List.Add("Информация о процессах");
            listWnd.List.Add("Сетевая статистика");

            btnExit = new ButtonObject(mainWindow.Left + 31, mainWindow.Top + 3, 7, 1, false, true, "Выход");
            btnExit.BackgroundColor = ConsoleColor.DarkGray;
            btnExit.TextColor = ConsoleColor.Black;
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
            characterWindow.BackgroundColor = ConsoleColor.Blue;
            characterWindow.TextColor = ConsoleColor.White;
            characterWindow.TimerTick += CharacterWindow_TimerTick;
            #region Операционная система
            LabelObject labelOS = new LabelObject(characterWindow.Left + 2, characterWindow.Top + 2,
                                    21, 1, false, false, "Операционная система:");
            labelOS.BackgroundColor = ConsoleColor.Blue;
            labelOS.TextColor = ConsoleColor.White;


            LabelObject labelOSinfo = new LabelObject(characterWindow.Left + 2 + 24, characterWindow.Top + 2,
                                    40, 2, false, false, Environment.OSVersion.VersionString +
                                    " " + Environment.OSVersion.Platform.ToString());
            labelOSinfo.BackgroundColor = ConsoleColor.Blue;
            labelOSinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Разрядность
            LabelObject labelBit = new LabelObject(labelOS.Left, labelOSinfo.Top + 2,
                                   21, 1, false, false, "Разрядность:");
            labelBit.BackgroundColor = ConsoleColor.Blue;
            labelBit.TextColor = ConsoleColor.White;

            bool Is64 = Environment.Is64BitOperatingSystem;
            string bit;
            if (Is64) bit = "x64";
            else bit = "x86";


            LabelObject labelBitinfo = new LabelObject(labelOSinfo.Left, labelOSinfo.Top + 2,
                                    40, 2, false, false, bit);
            labelBitinfo.BackgroundColor = ConsoleColor.Blue;
            labelBitinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Число ядер процессора
            LabelObject labelCore = new LabelObject(labelOS.Left, labelBitinfo.Top + 2,
                                   21, 1, false, false, "Число ядер:");
            labelCore.BackgroundColor = ConsoleColor.Blue;
            labelCore.TextColor = ConsoleColor.White;

            LabelObject labelCoreinfo = new LabelObject(labelOSinfo.Left, labelBitinfo.Top + 2,
                        40, 2, false, false, Environment.ProcessorCount.ToString());
            labelCoreinfo.BackgroundColor = ConsoleColor.Blue;
            labelCoreinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Имя компьютера
            LabelObject labelMachineName = new LabelObject(labelOS.Left, labelCoreinfo.Top + 2,
                                    21, 1, false, false, "Имя компьютера:");
            labelMachineName.BackgroundColor = ConsoleColor.Blue;
            labelMachineName.TextColor = ConsoleColor.White;


            LabelObject labelMachineNameinfo = new LabelObject(labelOSinfo.Left, labelCoreinfo.Top + 2,
                                    40, 2, false, false, Environment.MachineName);
            labelMachineNameinfo.BackgroundColor = ConsoleColor.Blue;
            labelMachineNameinfo.TextColor = ConsoleColor.White;
            #endregion

            #region Имя пользователя
            LabelObject labelUserName = new LabelObject(labelOS.Left, labelMachineName.Top + 2,
                                   21, 1, false, false, "Имя пользователя:");
            labelUserName.BackgroundColor = ConsoleColor.Blue;
            labelUserName.TextColor = ConsoleColor.White;


            LabelObject labelUserNameinfo = new LabelObject(labelOSinfo.Left, labelMachineName.Top + 2,
                                    40, 2, false, false, Environment.UserName);
            labelUserNameinfo.BackgroundColor = ConsoleColor.Blue;
            labelUserNameinfo.TextColor = ConsoleColor.White;
            #endregion           

            #region Системное время
            LabelObject labelTime = new LabelObject(labelOS.Left, labelUserNameinfo.Top + 2,
                                   21, 1, false, false, "Системное время:");
            labelTime.BackgroundColor = ConsoleColor.Blue;
            labelTime.TextColor = ConsoleColor.White;


            LabelObject labelTimeinfo = new LabelObject(labelOSinfo.Left, labelUserNameinfo.Top + 2,
                                    40, 2, false, false, DateTime.Now.ToLongTimeString());
            labelTimeinfo.BackgroundColor = ConsoleColor.Blue;
            labelTimeinfo.TextColor = ConsoleColor.White;
            #endregion

            ButtonObject btnExitCharacterWindow = new ButtonObject(characterWindow.Left + characterWindow.Width - 11,
                                            characterWindow.Top + characterWindow.Height - 2, 9, 1, true, false, "Закрыть");
            btnExitCharacterWindow.TextColor = ConsoleColor.Black;
            btnExitCharacterWindow.BackgroundColor = ConsoleColor.DarkGray;
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
            processWindow = new Window(74, 20, 70, 18, "Информация о процессах", false, 2, ref app);
            processWindow.BackgroundColor = ConsoleColor.Blue;
            processWindow.TextColor = ConsoleColor.White;

            ButtonObject btnExitProcessWindow = new ButtonObject(processWindow.Left + processWindow.Width - 11,
                processWindow.Top + processWindow.Height - 2, 9, 1, false, false, "Закрыть");
            btnExitProcessWindow.ButtonClicked += BtnExitProcessWindow_ButtonClicked;
            btnExitProcessWindow.BackgroundColor = ConsoleColor.DarkGray;

            ButtonObject btnUpdateProcess = new ButtonObject(processWindow.Left + processWindow.Width - 31,
                processWindow.Top + processWindow.Height - 2, 19, 1, false, false, "Обновить процессы");
            btnUpdateProcess.ButtonClicked += BtnUpdateProcess_ButtonClicked;
            
            btnUpdateProcess.BackgroundColor = ConsoleColor.DarkGray;

            #region Заголовок "Процессы"
            LabelObject labelProcess = new LabelObject(processWindow.Left + 2, processWindow.Top + 2,
                                    20, 1, false, false, "Процессы");
            labelProcess.BackgroundColor = ConsoleColor.Gray;
            labelProcess.TextColor = ConsoleColor.Black;
            #endregion


            #region Список процессов
            ListObject listProcess = new ListObject(processWindow.Left + 2, labelProcess.Top + 2,
                20, 10, true, false);
            listProcess.BackgroundColor = ConsoleColor.Blue;
            listProcess.BackgroundActiveColor = ConsoleColor.White;
            listProcess.ButtonClicked += ListProcess_ButtonClicked;
            listProcess.List = new List<string>();

            Process[] allprocess = Process.GetProcesses();
            processModule = new List<ProcessModuleCollection>();
            process = new List<Process>();
            for (int i = 0; i < allprocess.Length; i++)
            {
                try
                {
                    processModule.Add(allprocess[i].Modules);
                    process.Add(allprocess[i]);
                }
                catch { continue; }
            }
            for (int i = 0; i < process.Count; ++i)
            {
                listProcess.List.Add(process[i].ProcessName);
            }
            #endregion

            #region Список модулей для выбранного процесса
            LabelObject labelModule = new LabelObject(labelProcess.Left + labelProcess.Width + 1, labelProcess.Top,
                45, 1, false, false, "Список модулей для " + process[listProcess.ActiveLine].ProcessName);
            labelModule.BackgroundColor = ConsoleColor.Gray;
            labelModule.TextColor = ConsoleColor.Black;

            #endregion

            #region Список модулей
            ListObject listModule = new ListObject(labelModule.Left, labelModule.Top + 2,
                45, 10, false, false);
            listModule.BackgroundColor = ConsoleColor.Blue;
            listModule.BackgroundActiveColor = ConsoleColor.White;
            listModule.List = new List<string>();
            ProcessModuleCollection pm = processModule[listProcess.ActiveLine];

            for (int i = 0; i < pm.Count; ++i)
            {
                listModule.List.Add(pm[i].FileName);
            }
            #endregion

            processWindow.AddChildren(labelProcess);
            processWindow.AddChildren(labelModule);
            processWindow.AddChildren(listProcess);
            processWindow.AddChildren(listModule);
            processWindow.AddChildren(btnExitProcessWindow);
            processWindow.AddChildren(btnUpdateProcess);
        }

        private void BtnUpdateProcess_ButtonClicked(object sender, EventArgs e)
        {
            Process[] allprocess = Process.GetProcesses();
            processModule = new List<ProcessModuleCollection>();
            process = new List<Process>();
            for (int i = 0; i < allprocess.Length; i++)
            {
                try
                {
                    processModule.Add(allprocess[i].Modules);
                    process.Add(allprocess[i]);
                }
                catch { continue; }
            }

            foreach(Window win in app.windows)
            {
                if (win.IdentificationNumber == 2)
                {
                    ((ListObject)win.Children[2]).List.Clear();
                    for (int i = 0; i < process.Count; ++i)
                    {
                        ((ListObject)win.Children[2]).List.Add(process[i].ProcessName);
                    }
                    win.UpdateChildren(2);
                }
                     
            }
        }

        private void ListProcess_ButtonClicked(object sender, EventArgs e)
        {
            foreach (Window win in app.windows)
            {
                if (win.IdentificationNumber == 2)
                {
                    int num = ((ListObject)sender).ActiveLine;
                    ((LabelObject)win.Children[1]).Text = "Список модулей для " + process[num].ProcessName;
                    try
                    {
                        ((ListObject)win.Children[3]).List.Clear();
                        ProcessModuleCollection pm = processModule[num];
                        for (int i = 0; i < pm.Count; ++i)
                        {
                            ((ListObject)win.Children[3]).List.Add(pm[i].FileName);
                        }
                    }
                    catch
                    {
                        ((ListObject)win.Children[3]).List.Clear();
                        ((ListObject)win.Children[3]).List.Add("Модули не загружены");

                    }
                    win.UpdateChildren(1);
                    win.UpdateChildren(3);

                }
            }
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
            networkWindow.BackgroundColor = ConsoleColor.Blue;
            ButtonObject btnExitNetwork = new ButtonObject(networkWindow.Left + networkWindow.Width - 12
                                , networkWindow.Top + networkWindow.Height - 2, 9, 1, true, false, "Закрыть");
            btnExitNetwork.BackgroundColor = ConsoleColor.DarkGray;
            btnExitNetwork.ButtonClicked += BtnExit_ButtonClicked1;


            LabelObject labelName = new LabelObject(networkWindow.Left + 2, networkWindow.Top + 2, 40, 1, false, false, "Интерфейсы");
            labelName.BackgroundColor = ConsoleColor.Gray;
            labelName.TextColor = ConsoleColor.Black;

            ListObject listInterfaces = new ListObject(networkWindow.Left + 2, networkWindow.Top + 3, 66, 2, false, false);
            listInterfaces.ButtonClicked += ListInterfaces_ButtonClicked;
            listInterfaces.BackgroundColor = ConsoleColor.Blue;
            listInterfaces.BackgroundActiveColor = ConsoleColor.White;
            listInterfaces.TextColor = ConsoleColor.White;

            listInterfaces.List = new List<string>();
            for (int i = 0; i < networkInterfaces.Count; ++i)
            {
                listInterfaces.List.Add(networkInterfaces[i].Name);
            }

            /////////////////////////////////////////Заголовки подписей/////////////////
            LabelObject labelTitle = new LabelObject(listInterfaces.Left, listInterfaces.Top + 3, 9, 1, false, false, "Название: ");
            labelTitle.BackgroundColor = ConsoleColor.Blue;
            labelTitle.TextColor = ConsoleColor.White;


            LabelObject labelDescription = new LabelObject(listInterfaces.Left, labelTitle.Top + 1, 9, 1, false, false, "Описание: ");
            labelDescription.BackgroundColor = ConsoleColor.Blue;
            labelDescription.TextColor = ConsoleColor.White;

            LabelObject labelMAC = new LabelObject(listInterfaces.Left, labelTitle.Top + 2, 10, 1, false, false, "MAC-адрес: ");
            labelMAC.BackgroundColor = ConsoleColor.Blue;
            labelMAC.TextColor = ConsoleColor.White;

            LabelObject labelStatistics = new LabelObject(labelMAC.Left, labelMAC.Top + 2, 40, 1, false, false, "Статистика");
            labelStatistics.BackgroundColor = ConsoleColor.Gray;
            labelStatistics.TextColor = ConsoleColor.Black;

            LabelObject labelDownload = new LabelObject(listInterfaces.Left, labelStatistics.Top + 1, 34, 1, false, false, "Пакетов принято(входящий трафик): ");
            labelDownload.BackgroundColor = ConsoleColor.Blue;
            labelDownload.TextColor = ConsoleColor.White;

            LabelObject labelUpload = new LabelObject(listInterfaces.Left, labelDownload.Top + 1, 39, 1, false, false, "Пакетов отправлено(исходящий трафик): ");
            labelUpload.BackgroundColor = ConsoleColor.Blue;
            labelUpload.TextColor = ConsoleColor.White;

            LabelObject labelDownloadSpeed = new LabelObject(listInterfaces.Left, labelUpload.Top + 2, 40, 1, false, false, "Скорость получения:");
            labelDownloadSpeed.BackgroundColor = ConsoleColor.Blue;
            labelDownloadSpeed.TextColor = ConsoleColor.White;

            LabelObject labelUploadSpeed = new LabelObject(listInterfaces.Left, labelDownloadSpeed.Top + 4, 40, 1, false, false, "Скорость отдачи:");
            labelUploadSpeed.BackgroundColor = ConsoleColor.Blue;
            labelUploadSpeed.TextColor = ConsoleColor.White;
            ///////////////////////////////////////////Вставляемая информация/////////////////
            LabelObject labelTitleinfo = new LabelObject(labelTitle.Left + labelTitle.Width + 1, labelTitle.Top, 40, 1, false, false,
                networkInterfaces[listInterfaces.ActiveLine].Name);
            labelTitleinfo.BackgroundColor = ConsoleColor.Blue;
            labelTitleinfo.TextColor = ConsoleColor.White;

            LabelObject labelDescriptioninfo = new LabelObject(labelDescription.Left + labelTitle.Width + 1, labelTitle.Top + 1, 55, 1, false, false,
                networkInterfaces[listInterfaces.ActiveLine].Description);
            labelDescriptioninfo.BackgroundColor = ConsoleColor.Blue;
            labelDescriptioninfo.TextColor = ConsoleColor.White;

            LabelObject labelMACinfo = new LabelObject(labelMAC.Left + labelTitle.Width + 2, labelMAC.Top, 55, 1, false, false,
                networkInterfaces[listInterfaces.ActiveLine].GetPhysicalAddress().ToString());
            labelMACinfo.BackgroundColor = ConsoleColor.Blue;
            labelMACinfo.TextColor = ConsoleColor.White;

            var ipstat = networkInterfaces[currentInterface].GetIPv4Statistics();
            oldBytesRecived = ipstat.BytesReceived;

            oldBytesSent = ipstat.BytesSent;

            LabelObject labelDownloadinfo = new LabelObject(labelDownload.Left + labelDownload.Width, labelDownload.Top, 20, 1, false, false,
                ipstat.UnicastPacketsReceived.ToString() + "/" + (ipstat.BytesReceived / 1024 / 1024).ToString() +
                " Мбайт");
            labelDownloadinfo.BackgroundColor = ConsoleColor.Blue;
            labelDownloadinfo.TextColor = ConsoleColor.White;

            LabelObject labelUploadinfo = new LabelObject(labelUpload.Left + labelUpload.Width, labelUpload.Top, 20, 1, false, false,
                ipstat.UnicastPacketsSent.ToString() + "/" + (ipstat.BytesSent / 1024 / 1024).ToString() +
                " Мбайт");
            labelUploadinfo.BackgroundColor = ConsoleColor.Blue;
            labelUploadinfo.TextColor = ConsoleColor.White;


            MaxSpeed = 100;

            ProgressObject progressInput = new ProgressObject(labelStatistics.Left, labelDownloadSpeed.Top + 1, 50, 2, false, false);
            progressInput.TextColor = ConsoleColor.White;
            progressInput.BackgroundTextColor = ConsoleColor.Blue;
            progressInput.BackgroundColor = ConsoleColor.DarkCyan;
            progressInput.PercentColor = ConsoleColor.Yellow;
            progressInput.Min = "0";
            progressInput.Max = MaxSpeed.ToString() + " Мбит/c";

            ProgressObject progressOutput = new ProgressObject(labelStatistics.Left, labelUploadSpeed.Top + 1, 50, 2, false, false);
            progressOutput.TextColor = ConsoleColor.White;
            progressOutput.BackgroundTextColor = ConsoleColor.Blue;
            progressOutput.BackgroundColor = ConsoleColor.DarkCyan;
            progressOutput.PercentColor = ConsoleColor.Yellow;
            progressOutput.Min = "0";
            progressOutput.Max = MaxSpeed.ToString() + " Мбит/c";


            networkWindow.AddChildren(btnExitNetwork);
            networkWindow.AddChildren(labelName);
            networkWindow.AddChildren(listInterfaces);
            networkWindow.AddChildren(labelTitle);
            networkWindow.AddChildren(labelDescription);
            networkWindow.AddChildren(labelMAC);
            networkWindow.AddChildren(labelStatistics);
            networkWindow.AddChildren(labelDownload);
            networkWindow.AddChildren(labelUpload);
            networkWindow.AddChildren(labelDownloadSpeed);
            networkWindow.AddChildren(labelUploadSpeed);
            networkWindow.AddChildren(labelTitleinfo);
            networkWindow.AddChildren(labelDescriptioninfo);
            networkWindow.AddChildren(labelMACinfo);
            networkWindow.AddChildren(labelDownloadinfo);
            networkWindow.AddChildren(labelUploadinfo);
            networkWindow.AddChildren(progressOutput);
            networkWindow.AddChildren(progressInput);
        }

        private void ListInterfaces_ButtonClicked(object sender, EventArgs e)
        {
            currentInterface = ((ListObject)sender).ActiveLine;
            var ipstat = networkInterfaces[currentInterface].GetIPv4Statistics();
            oldBytesRecived = ipstat.BytesReceived;
            oldBytesSent = ipstat.BytesSent;
            foreach (Window win in app.windows)
            {
                if (win.IdentificationNumber == 3)
                {
                    ((LabelObject)win.Children[11]).Text = networkInterfaces[currentInterface].Name;
                    ((LabelObject)win.Children[12]).Text = networkInterfaces[currentInterface].Description;
                    ((LabelObject)win.Children[13]).Text = networkInterfaces[currentInterface].GetPhysicalAddress().ToString();
                    ((LabelObject)win.Children[14]).Text = ipstat.UnicastPacketsReceived.ToString() + "/"
                        + (ipstat.BytesReceived / 1024 / 1024).ToString() + " Мбайт";
                    ((LabelObject)win.Children[15]).Text = ipstat.UnicastPacketsSent.ToString() + "/" +
                        (ipstat.BytesSent / 1024 / 1024).ToString() + " Мбайт";

                    win.UpdateChildren(11);
                    win.UpdateChildren(12);
                    win.UpdateChildren(13);
                    win.UpdateChildren(14);
                    win.UpdateChildren(15);
                    win.UpdateChildren(16);
                    win.UpdateChildren(17);
                }
            }
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

                    double speedIn = (((double)bytesrecived - (double)oldBytesRecived) * 8 / 1000 / 1000);
                    double speedOut = (double)((bytessent - oldBytesSent) * 8f / 1000f / 1000f);

                    int percentIn = (int)(speedIn / MaxSpeed * 100);
                    int percentOut = (int)(speedOut / MaxSpeed * 100);

                    string speedI, speedO;
                    if (speedIn < 1) { speedI = (speedIn * 1000).ToString("F1") + " Кбит/с "; }
                    else { speedI = (speedIn).ToString("F1") + " Mбит/c "; }

                    if (speedOut < 1) { speedO = (speedOut * 1000).ToString("F1") + " Кбит/c "; }
                    else { speedO = (speedOut).ToString("F1") + " Mбит/c "; }

                    oldBytesRecived = bytesrecived;
                    oldBytesSent = bytessent;

                    ((LabelObject)win.Children[14]).Text = packsrecived + "/" +
                        (bytesrecived / 1024 / 1024).ToString() + " Мбайт";
                    ((LabelObject)win.Children[15]).Text = packssent + "/" +
                        (bytessent / 1024 / 1024).ToString() + " Мбайт";
                    ((ProgressObject)win.Children[16]).Value = speedO;
                    ((ProgressObject)win.Children[16]).Percent = percentOut;
                    ((ProgressObject)win.Children[17]).Value = speedI;
                    ((ProgressObject)win.Children[17]).Percent = percentIn;
                    win.UpdateChildren(14);
                    win.UpdateChildren(15);
                    win.UpdateChildren(16);
                    win.UpdateChildren(17);
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
