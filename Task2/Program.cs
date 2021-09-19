using System;
using System.IO;

namespace Task2
{
    class Program
    {
        const string dirName = @"d:\C#\Projects\CDEV-9\temp\Dir1\";
        static void Main(string[] args)
        {
            /*Напишите программу, которая считает размер папки на диске(вместе со всеми вложенными папками и файлами).
            На вход метод принимает URL директории, в ответ — размер в байтах.

            Подсказка
            Чтобы учитывать вложенные папки, используйте рекурсию.
            Критерии оценивания
            0 баллов: задача решена неверно или отсутствует доступ к репозиторию.
            2 балла(хорошо): написан метод и код выполняет свою функцию.
            4 балла(отлично): предусмотрена обработка исключений и валидация пути.*/

            long filesLenght = GetSizeDir(dirName);
            Console.WriteLine($"Размер каталога {dirName}: {filesLenght} байт");

            Console.ReadKey();

        }

        static long GetSizeDir(string delDir)
        {
            //Console.WriteLine(delDir);
            long filesLenght = 0;

            if (Directory.Exists(delDir))
            {
                // Сначало посчитаем размер файлов в каталоге.
                string[] files = Directory.GetFiles(delDir);

                foreach (string f in files)
                {

                    FileInfo fi = new FileInfo(f);

                    if (fi.Exists)
                    {
                        //Console.WriteLine($"{f}  {fi.Length} байт");
                        filesLenght += fi.Length;
                    }
                    else
                    {
                        Console.WriteLine($"{f}  Файл не существует");
                    }
                }

                // Теперь подсчитаем размер файлов во вложенных каталогах.
                string[] dirs = Directory.GetDirectories(delDir);
                foreach (string d in dirs)
                {
                    filesLenght += GetSizeDir(d);
                }
            }
            else
            {
                Console.WriteLine($"Каталог {delDir} не существует");
            }
            return filesLenght;
        }

    }
}
