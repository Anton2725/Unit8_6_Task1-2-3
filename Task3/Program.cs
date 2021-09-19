using System;
using System.IO;

namespace Task3
{
    class Program
    {
        const string dirName = @"d:\C#\Projects\CDEV-9\temp\Dir1\";
        static void Main(string[] args)
        {
            /*Доработайте программу из задания 1, используя ваш метод из задания 2.

            При запуске программа должна:

            1. Показать, сколько весит папка до очистки.Использовать метод из задания 2.
            2. Выполнить очистку.
            3. Показать сколько файлов удалено и сколько места освобождено.
            4. Показать, сколько папка весит после очистки.

            Критерии оценивания
            0 баллов: задача решена неверно или отсутствует доступ к репозиторию.
            2 балла(хорошо): написан метод и код выполняет свою функцию.
            4 балла(отлично): предусмотрена обработка исключений и валидация пути.*/

            long filesLenght = GetSizeDir(dirName);
            Console.WriteLine($"Размер каталога {dirName} до очистки: {filesLenght} байт");

            TryClearCatalog(dirName, out int countFiles, out long freeSpace);
            Console.WriteLine($"Удалено файлов: {countFiles}");
            Console.WriteLine($"Очищено места: {freeSpace} байт");

            filesLenght = GetSizeDir(dirName);
            Console.WriteLine($"Размер каталога {dirName} после очистки: {filesLenght} байт");

            Console.ReadKey();

        }

        static bool TryClearCatalog(string delDir, out int countFiles, out long freeSpace)
        {
            //Console.WriteLine(delDir);
            int countFilesTemp = 0;
            long freeSpaceTemp = 0;

            if (Directory.Exists(delDir))
            {

                // Сначало удалим файлы в каталоге.
                string[] files = Directory.GetFiles(delDir);

                foreach (string f in files)
                {
                    //Console.WriteLine(f);

                    FileInfo fi = new FileInfo(f);

                    if (fi.Exists)
                    {
                        try
                        {
                            var freeSpaceTemp0 = fi.Length;
                            fi.Delete();
                            countFilesTemp++;
                            freeSpaceTemp += freeSpaceTemp0;
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine($"Не удалось удалить файл по причине: {ex.Message}");
                            countFiles = countFilesTemp;
                            freeSpace = freeSpaceTemp;
                            //result = false;
                            return false;
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"Файл не существует");
                    }
                }

                // Теперь удалим каталоги.
                string[] dirs = Directory.GetDirectories(delDir);
                foreach (string d in dirs)
                {
                    if (TryClearCatalog(d, out int countFiles1, out long freeSpace1))
                    {
                        countFilesTemp += countFiles1;
                        freeSpaceTemp += freeSpace1;

                        try
                        {
                            Directory.Delete(d);
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine($"Не удалось удалить каталог по причине: {ex.Message}");
                            countFiles = countFilesTemp;
                            freeSpace = freeSpaceTemp;
                            return false;
                        }
                    }
                    else
                    {
                        countFiles = countFilesTemp;
                        freeSpace = freeSpaceTemp;
                        return false;
                    }
                }
            }
            else
            {
                //Console.WriteLine($"Каталог {delDir} не существует");
                countFiles = countFilesTemp;
                freeSpace = freeSpaceTemp;
                return false;
            }

            countFiles = countFilesTemp;
            freeSpace = freeSpaceTemp;
            return true;
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
                        //Console.WriteLine($"{f}  Файл не существует");
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
                //Console.WriteLine($"Каталог {delDir} не существует");
            }
            return filesLenght;
        }

    }
}
