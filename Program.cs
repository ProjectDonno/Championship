using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectChampionship
{
    class Program
    {
        // ВСЕМОГУЩИЙ РЫЧАГ
        static bool isWorking = true;

        /// <summary>
        /// Кастомный ReadLine скрывает напечатанные символы
        /// </summary>
        /// <returns>истинный текст, введённый userom</returns>
        static string PasswordReadLine()
        {
            // "запоминание" стартовых координат курсора
            int xStart = Console.CursorLeft;   // "Console.CursorLeft" - возвращает или задает позицию столбца курсора в буферной области 
            int y = Console.CursorTop;         // "Console.CursorTop" - возвращает или задает позицию строки курсора в буферной области
            // текущее положение курсора относительно от стартового
            int x = 0;
            string text = "";
            string text_supposed = "";
            char chZ = '*';
            // "флаг поднят"
            bool IsWorking = true;
            // каркас метода
            while (IsWorking)
            {
                // при наличии "true" в скобках, буквы не отображаются на экране;
                ConsoleKeyInfo info = Console.ReadKey(true);
                // "запоминание" буквы
                char ch = info.KeyChar;
                // анализ "управляющих клавиш" (клавиш без букв)
                switch (info.Key)
                {
                    case ConsoleKey.Enter:
                        // обрыв цикла ("спуск флага")
                        IsWorking = false;
                        break;
                    case ConsoleKey.Backspace:
                        if (x > 0)
                        {
                            // удаление буквы в положении курсора
                            text = text.Remove(x - 1, 1);
                            text_supposed = text_supposed.Remove(x - 1, 1);
                            // смещене курсора влево
                            x--;
                        }
                        break;
                    case ConsoleKey.Delete:
                        if (x < text.Length)
                        {
                            text = text.Remove(x, 1);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        // запрет на "смещение левее стартовой позиции"
                        if (x > 0)
                            x--;
                        break;
                    case ConsoleKey.RightArrow:
                        // запрет на "смещение правее имеющегося текста"
                        if (x < text.Length)
                            x++;
                        break;
                    default:
                        // если клавиша - не "управляющая"
                        if (!char.IsControl(ch))
                        {
                            if (text.Length < 8)
                            {
                                // проверка, является ли символ цифрой

                                // "сложение" в строку
                                text = text.Insert(x, ch + "");
                                text_supposed = text_supposed.Insert(x, chZ + "");
                                // при добавлении буквы, происходит смещение курсора вправо
                                x++;

                            }
                        }
                        break;
                }
                // отключение мерцания курсора в самом начале строки 
                Console.CursorVisible = false;
                // вывод текста со стартовой позиции 
                Console.SetCursorPosition(xStart, y);
                Console.Write(text_supposed + " ");
                // возвращение курсора в текущее положение
                Console.SetCursorPosition(xStart + x, y);
                Console.CursorVisible = true;
            }
            // переход на следущую строку 
            Console.SetCursorPosition(0, y + 1);
            string result = null;
            if (text.Length > 0)
            {
                result = text;
            }
            return result;
        }

        /// <summary>
        /// Возвращает логин и пароль usera
        /// </summary>
        /// <param name="Login">логин</param>
        /// <param name="Password">пароль</param>
        static void СomparisonLoginandPassword(ref string Login, ref string Password)
        {
            Console.SetCursorPosition(48, 11);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Логин: ");
            Console.SetCursorPosition(48, 12);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Login = Console.ReadLine();

            Console.SetCursorPosition(48, 15);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Пароль: ");
            Console.SetCursorPosition(48, 16);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Password = PasswordReadLine();
        }

        /// <summary>
        /// Верификация логина и пароля usera
        /// </summary>
        /// <param name="AccessLevel">уровень доступа, введённый userom</param>
        /// <param name="Login">логин, введённый userom</param>
        /// <param name="Password">пароль, введённый userom</param>
        /// <returns></returns>
        static bool Verification(string AccessLevel, string Login, string Password)
        {
            string staff = null;
            string[] LoginonStaff;
            string[] PasswordonStaff;
            bool value = false;
            if (AccessLevel == "A")
            {
                // путь
                staff = @"Аuthorization Database\A access level\staff.txt";

                FileStream fs = new FileStream(staff, FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                // прочитали первую строчку и узнали кол-во userov c уровнем доступа "А"
                string st = sr.ReadLine();
                int number = int.Parse(st);

                // создали массивы с нужной длинной
                LoginonStaff = new string[number];
                PasswordonStaff = new string[number];

                // записали в массивы данные из staff.txt
                for (int i = 0; i < number; i++)
                {
                    st = sr.ReadLine();
                    // разбил строки по пробелу
                    string[] parts = st.Split(' ');
                    if (parts.Length != 2)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(48, 7);
                        Console.WriteLine("Ошибка верификации");
                        isWorking = false;
                        break;
                    }
                    LoginonStaff[i] = parts[0];
                    PasswordonStaff[i] = parts[1];
                }

                // закрыли читателя и файл
                sr.Close();
                fs.Close();

                for (int i = 0; i < number; i++)
                {
                    if ((Login == LoginonStaff[i]) || (Password == PasswordonStaff[i]))
                    {
                        value = true;
                    }
                    else
                    {
                        if (i == (number - 1))
                        {
                            value = false;
                        }
                        else
                        {
                            i++;
                        }
                    }
                }

            }
            else if (AccessLevel == "B")
            {
                // путь
                staff = @"Аuthorization Database\B access level\staff.txt";

                FileStream fs = new FileStream(staff, FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                // прочитали первую строчку и узнали кол-во userov c уровнем доступа "В"
                string st = sr.ReadLine();
                int number = int.Parse(st);

                // создали массивы с нужной длинной
                LoginonStaff = new string[number];
                PasswordonStaff = new string[number];

                // записали в массивы данные из staff.txt
                for (int i = 0; i < number; i++)
                {
                    st = sr.ReadLine();
                    // разбил строки по пробелу
                    string[] parts = st.Split(' ');
                    if (parts.Length != 2)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(48, 7);
                        Console.WriteLine("Ошибка верификации");
                        isWorking = false;
                        break;
                    }
                    LoginonStaff[i] = parts[0];
                    PasswordonStaff[i] = parts[1];
                }

                // закрыли читателя и файл
                sr.Close();
                fs.Close();

                for (int i = 0; i < number; i++)
                {
                    if ((Login == LoginonStaff[i]) || (Password == PasswordonStaff[i]))
                    {
                        value = true;
                    }
                    else
                    {
                        if (i == (number - 1))
                        {
                            value = false;
                        }
                        else
                        {
                            i++;
                        }
                    }
                }

            }
            else if (AccessLevel == "C")
            {
                // путь
                staff = @"Аuthorization Database\C access level\staff.txt";

                FileStream fs = new FileStream(staff, FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                // прочитали первую строчку и узнали кол-во userov c уровнем доступа "С"
                string st = sr.ReadLine();
                int number = int.Parse(st);

                // создали массивы с нужной длинной
                LoginonStaff = new string[number];
                PasswordonStaff = new string[number];

                // записали в массивы данные из staff.txt
                for (int i = 0; i < number; i++)
                {
                    st = sr.ReadLine();
                    // разбил строки по пробелу
                    string[] parts = st.Split(' ');
                    if (parts.Length != 2)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(48, 7);
                        Console.WriteLine("Ошибка верификации");
                        isWorking = false;
                        break;
                    }
                    LoginonStaff[i] = parts[0];
                    PasswordonStaff[i] = parts[1];
                }

                // закрыли читателя и файл
                sr.Close();
                fs.Close();

                for (int i = 0; i < number; i++)
                {
                    if ((Login == LoginonStaff[i]) || (Password == PasswordonStaff[i]))
                    {
                        value = true;
                    }
                    else
                    {
                        if (i == (number - 1))
                        {
                            value = false;
                        }
                        else
                        {
                            i++;
                        }
                    }
                }

            }
            return value;
        }

        /// <summary>
        /// Проверка существования файла
        /// </summary>
        /// <param name="FullNameFile">полное имя файла, который нужно проверить</param>
        static void CheckFile(string FullNameFile)
        {
            if (!File.Exists(FullNameFile))
            {
                FileStream fs = new FileStream(FullNameFile, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("0");
                sw.Close();
                fs.Close();
            };
        }

        //------------------------------
        static string[] NameSportCont;
        static string[] Date;
        static string[] Time;
        //------------------------------

        /// <summary>
        /// выгрузка соревнований
        /// </summary>
        static void LoadSportCont(string fname)
        {
            //Console.WriteLine("Загружается список соревнований...");

            if (!File.Exists(fname))
            {
                Console.WriteLine("Файл \"{0}\" не найден!", fname);
                return;
            }
            FileStream fs = new FileStream(fname, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string st = sr.ReadLine();
            int number = int.Parse(st);

            NameSportCont = new string[number];
            Date = new string[number];
            Time = new string[number];

            for (int i = 0; i < number; i++)
            {
                st = sr.ReadLine();
                // разбил строки по пробелу
                string[] parts = st.Split('&');
                if (parts.Length != 3)
                {
                    Console.WriteLine("Error чтения соревнований");
                    Console.WriteLine(st);
                    break;
                }
                NameSportCont[i] = parts[0];
                Date[i] = parts[1];
                Time[i] = parts[2];
            }
            sr.Close();
            fs.Close();
        }

        /// <summary>
        /// Создание соревнования
        /// </summary>
        /// <param name="nameSportCont">название дисциплины</param>
        /// <param name="date">дата проведения</param>
        /// <param name="time">время проведения</param>
        static void AddSportingContest(string nameSportCont, string date, string time)
        {
            int number = NameSportCont.Length + 1;

            string[] tempNameSportCont = new string[number];

            string[] tempDate = new string[number];

            string[] tempTime = new string[number];

            for (int i = 0; i < number - 1; i++)
            {
                tempNameSportCont[i] = NameSportCont[i];
                tempDate[i] = Date[i];
                tempTime[i] = Time[i];
            }
            tempNameSportCont[number - 1] = nameSportCont;
            tempDate[number - 1] = date;
            tempTime[number - 1] = time;
            //-----------------------------------------------------------------------------------------------------
            NameSportCont = tempNameSportCont;
            Date = tempDate;
            Time = tempTime;
        }

        /// <summary>
        /// сохранение изменёного списка сорев
        /// </summary>
        static void SaveSportCont(string FullNameFile)
        {
            FileStream fs = new FileStream(FullNameFile, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(NameSportCont.Length);
            for (int i = 0; i < NameSportCont.Length; i++)
            {
                sw.WriteLine("{0}&{1}&{2}", NameSportCont[i], Date[i], Time[i]);

            }
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// "Ручной" ReadLine
        /// </summary>
        /// <returns>текст, который ввёл user</returns>
        static string CustomReadLine_V1()
        {

            // переменная, в которую поместится результат работы метода
            string text = "";
            // каркас метода
            while (true)
            {
                char ch = '*';
                Console.ReadKey(); // "железная" функция
                // при наличии "true" в скобках, буквы не отображаются на экране;
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    // переход на следущую строку
                    Console.WriteLine();

                }
                if (info.Key == ConsoleKey.F1)
                {
                    // переход на следущую строку
                    Console.WriteLine();
                    // обрыв цикла
                    break;
                }
                if ((info.Key != ConsoleKey.Enter) && (info.Key != ConsoleKey.F1))
                {
                    // "запоминание" буквы
                    ch = info.KeyChar;
                }

                // отображение буквы
                Console.Write(ch);
                // "сложение" в строку
                text += ch;
            }
            return text;
        }

        /// <summary>
        /// установка цвета фона
        /// </summary>
        static void DrawBackground()
        {
            //-------------------------------------------------
            int a = Console.WindowHeight; // 30
            int b = Console.WindowWidth; // 120 
            for (int i = 0; i < b; i++)
            {
                for (int j = 0; j < a; j++)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                }
            }
            for (int i = 0; i < b; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
            }
            //-------------------------------------------------
        }

        /// <summary>
        /// Отрисовка меню
        /// </summary>
        /// <param name="x">позиция курсора по оси OX</param>
        /// <param name="y">позиция курсора по оси OY</param>
        /// <param name="menu">меню, которое нужно отрисовать</param>
        /// <param name="active">активный элемент</param>
        static void DrawVerticalMenu(int x, int y, string[] menu, int active)
        {


            // раскраска 
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Cyan;

            for (int i = 0; i < menu.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(menu[i]);
            }

            // Один закрасим 
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(x, y + active);
            Console.WriteLine(menu[active]);
        }

        /// <summary>
        /// отрисовка окошка
        /// </summary>
        /// <param name="x">координаты левой верхней точки по оси абсцисс</param>
        /// <param name="y">координаты левой верхней точки по оси ординат</param>
        /// <param name="w">ширина</param>
        /// <param name="h">высота</param>
        static void DrawWindow(int x, int y, int w, int h)
        {
            // Псевдографика:
            // 1. Включаем NumPad;
            // 2. Пишем число (Идентификационный номер) элемента;
            // 3. Выделяем элемент;
            // 4. Зажимая клавишу "Alt" слева, набираем этот же номер на NumPade;
            // 5. Отпускаем "Alt";


            Console.SetCursorPosition(x, y);
            Console.Write("╔");                        // ИН - 201
            Console.SetCursorPosition(x + w, y);
            Console.Write("╗");                        // ИН - 187
            Console.SetCursorPosition(x, y + h);
            Console.Write("╚");                        // ИН - 200
            Console.SetCursorPosition(x + w, y + h);
            Console.Write("╝");                        // ИН - 188

            for (int i = 1; i < w; i++)
            {
                Console.SetCursorPosition(x + i, y);
                Console.Write("═");                    // ИН - 205
                Console.SetCursorPosition(x + i, y + h);
                Console.Write("═");                    // ИН - 205
            }
            for (int j = 1; j < h; j++)
            {
                Console.SetCursorPosition(x, y + j);
                Console.Write("║");                    // ИН - 186
                Console.SetCursorPosition(x + w, y + j);
                Console.Write("║");                    // ИН - 186


            }
        }

        /// <summary>
        /// Выбор элемента из меню
        /// </summary>
        /// <param name="x">позиция курсора по оси OX</param>
        /// <param name="y">позиция курсора по оси OY</param>
        /// <param name="menu">меню, с которым работаем</param>
        /// <returns>возвращает "активный" элемент</returns>
        static int Select(int x, int y, string[] menu)
        {
            // запоминаем цвета, которые были 
            ConsoleColor fore = Console.ForegroundColor;
            ConsoleColor back = Console.BackgroundColor;

            bool isWorking = true;
            int active = 0;
            while (isWorking)
            {
                // нарисовали менюшку 
                DrawVerticalMenu(x, y, menu, active);

                // Ждём действий пользователя - нажатие клавиши
                ConsoleKeyInfo info = Console.ReadKey(true);

                // проанализировать варианты клавиши
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (active > 0) active--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (active < menu.Length - 1) active++;
                        break;
                    case ConsoleKey.Enter:
                        isWorking = false;
                        break;
                }
            }

            // после сeлекта курсор ставится под меню
            Console.SetCursorPosition(0, y + menu.Length);

            // восстановим цвета, которые были 
            Console.BackgroundColor = back;
            Console.ForegroundColor = fore;

            return active;
        }










        static void Main(string[] args)
        {

            while (isWorking)
            {
                DrawWindow(42, 2, 28, 25);
                Console.SetCursorPosition(50, 00);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Авторизация");

                Console.SetCursorPosition(48, 7);
                Console.WriteLine("Уровень доступа: ");
                Console.SetCursorPosition(48, 8);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                string AccessLevel = Console.ReadLine();

                if ((AccessLevel == "A") || (AccessLevel == "B") || (AccessLevel == "C"))
                {
                    string Login = null;
                    string Password = null;
                    bool value = false;
                    if (AccessLevel == "A")
                    {
                        // получили логин и пароль
                        СomparisonLoginandPassword(ref Login, ref Password);

                        // верификация 
                        value = Verification(AccessLevel, Login, Password);

                        if (value == false)
                        {
                            Console.Clear();

                            Console.SetCursorPosition(48, 7);
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("Доступ запрещён!");

                            isWorking = false;
                        }
                        else
                        { // верификация пройдена

                            Console.Clear();

                            Console.SetCursorPosition(46, 0);
                            Console.WriteLine("Вы зашли в систему как \"Тренер\"");

                            Console.SetCursorPosition(48, 10);
                            Console.WriteLine("Желаете продолжить работу?");
                            string[] answer =
                            {
                                "Да",
                                "Нет"
                            };
                            int active = Select(40, 12, answer);

                            if (active == 0)
                            {
                                Console.Clear();

                                string st = null;
                                string nameSportCont = null;
                                string date = null;
                                string time = null;
                                string FullNameFile = @"Module Сoacher\sporting contest.txt";

                                Console.SetCursorPosition(43, 00);
                                Console.WriteLine("Создание соревнования");

                                Console.SetCursorPosition(48, 7);
                                Console.WriteLine("Название: ");
                                Console.SetCursorPosition(48, 8);
                                nameSportCont = Console.ReadLine();

                                Console.SetCursorPosition(48, 11);
                                Console.WriteLine("Дата: ");
                                Console.SetCursorPosition(48, 12);
                                date = Console.ReadLine();

                                Console.SetCursorPosition(48, 15);
                                Console.WriteLine("Время: ");
                                Console.SetCursorPosition(48, 16);
                                time = Console.ReadLine();

                                CheckFile(FullNameFile);

                                LoadSportCont(FullNameFile);
                                AddSportingContest(nameSportCont, date, time);
                                SaveSportCont(FullNameFile);

                                Console.Clear();

                                Console.SetCursorPosition(43, 00);
                                Console.WriteLine("Создание соревнования");

                                Console.SetCursorPosition(48, 7);
                                Console.WriteLine("Cписок участников: ");
                                Console.SetCursorPosition(48, 8);

                                string listParticipants = CustomReadLine_V1();


                                Console.SetCursorPosition(75, 20);
                                Console.WriteLine("F1 - завершение работы модуля \"Тренер\"");

                                // Console.SetCursorPosition(0, 0);
                                FullNameFile = @"Module Сoacher\list of participants.txt";

                                CheckFile(FullNameFile);
                                LoadSportCont(FullNameFile);
                                SaveSportCont(FullNameFile);
                                // AddSportingContest();


                                // FileStream fs = new FileStream(FullNameFile, FileMode.Append);
                                //  StreamWriter sw = new StreamWriter(fs);

                                // sw1.WriteLine("*");
                                // Console.WriteLine("Участники данного соревнования:");




                                //                              sw.Write("{0}&", str);


                                //sw1.WriteLine("*");

                                Console.WriteLine();

                                //   sw.Close();
                                //   fs.Close();
                            }
                            else
                            {
                                Console.Clear();
                                isWorking = false;
                            }

                        }








                    }
                    else if (AccessLevel == "B")
                    {
                        // получили логин и пароль
                        СomparisonLoginandPassword(ref Login, ref Password);

                        // верификация 
                        value = Verification(AccessLevel, Login, Password);

                        if (value == false)
                        {
                            Console.Clear();

                            Console.SetCursorPosition(48, 7);
                            Console.WriteLine("Доступ запрещён!");

                            isWorking = false;
                        }
                        else
                        { // верификация пройдена

                        }
                    }
                    else if (AccessLevel == "C")
                    {
                        // получили логин и пароль
                        СomparisonLoginandPassword(ref Login, ref Password);

                        // верификация 
                        value = Verification(AccessLevel, Login, Password);

                        if (value == false)
                        {
                            Console.Clear();

                            Console.SetCursorPosition(48, 7);
                            Console.WriteLine("Доступ запрещён!");

                            isWorking = false;
                        }
                        else
                        { // верификация пройдена

                        }
                    }
                }
                else
                { // доступ запрещён

                    Console.Clear();

                    Console.SetCursorPosition(50, 00);
                    Console.WriteLine("Авторизация");

                    Console.SetCursorPosition(48, 7);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Доступ запрещён!");

                    //Console.ReadKey(true);
                    isWorking = false;

                }
                Console.ReadLine();
            }
        }
    }
}