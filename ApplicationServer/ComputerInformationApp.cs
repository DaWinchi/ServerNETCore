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
            listWnd.List.Add("Характеристики системы");
            listWnd.List.Add("Список процессов");
            listWnd.List.Add("Сетевая статистика");

            btnExit = new ButtonObject(mainWindow.Left + 31, mainWindow.Top + 3, 7, 1, false, true, "Выход");
            btnExit.ButtonClicked += BtnExit_ButtonClicked;

            mainWindow.AddChildren(btnExit);
            mainWindow.AddChildren(listWnd);
        }

        private void InitializeCharacterWindow()
        {
            characterWindow = new Window(50, 10, 50, 10, "Характеристики системы", false, 1, ref app);
        }

        /*Обработка нажатия кнопки закрытия приложения*/
        private void BtnExit_ButtonClicked(object sender, EventArgs e)
        {
            app.exit = true;
        }
    }
}
