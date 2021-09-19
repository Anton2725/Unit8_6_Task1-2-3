using System;
using System.IO;

namespace Task1
{
    class Program
    {
        const string dirName = @"d:\C#\Projects\CDEV-9\temp\Dir1\";
        static void Main(string[] args)
        {
            /* Напишите программу, которая чистит нужную нам папку от файлов и папок, которые не использовались более 30 минут.
            На вход программа принимает путь до папки.

            При разработке постарайтесь предусмотреть возможные ошибки(нет прав доступа, папка по заданному адресу не существует, 
            передан некорректный путь) и уведомить об этом пользователя.

            Критерии оценивания
            0 баллов: задача решена неверно или отсутствует доступ к репозиторию.
            2 балла(хорошо): код должен удалять папки рекурсивно (если в нашей папке лежит папка со вложенными файлами, 
              не должно возникнуть проблем с её удалением).
            4 балла(отлично): предусмотрена проверка на наличие папки по заданному пути(строчка if directory.Exists) ; 
              предусмотрена обработка исключений при доступе к папке (блок try-catch, а также логирует исключение в консоль).*/

            TryClearCatalog(dirName);

            Console.ReadKey();

        }
        static bool ValidTimeInterval(DateTime date1, DateTime date2)
        {
            TimeSpan interval = date1 - date2;
            if (interval.TotalMinutes > 30) return true;

            return false;
        }
        static bool TryClearCatalog(string delDir)
        {
            //Console.WriteLine(delDir);

            if (Directory.Exists(delDir))
            {

                // Сначало удалим файлы в каталоге.
                string[] files = Directory.GetFiles(delDir);

                foreach (string f in files)
                {
                    //Console.WriteLine(f);

                    if (File.Exists(f))
                    {
                        //if (ValidTimeInterval(DateTime.Now, File.GetCreationTime(f)))
                        if (ValidTimeInterval(DateTime.Now, File.GetLastAccessTime(f)))
                        {
                            try
                            {
                                File.Delete(f);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Не удалось удалить файл по причине: {ex.Message}");
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Не удалось удалить файл так как он еще не устарел");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Файл не существует");
                    }
                }

                // Теперь удалим каталоги.
                string[] dirs = Directory.GetDirectories(delDir);
                foreach (string d in dirs)
                {
                    if (TryClearCatalog(d))
                    {
                        //if (ValidTimeInterval(DateTime.Now, Directory.GetCreationTime(d)))
                        if (ValidTimeInterval(DateTime.Now, Directory.GetLastAccessTime(d)))
                        {
                            try
                            {
                                Directory.Delete(d);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Не удалось удалить каталог по причине: {ex.Message}");
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Не удалось удалить каталог так как он еще не устарел");
                            return false;
                        }
                    }
                    else return false;
                }
            }
            else
            {
                Console.WriteLine($"Каталог {delDir} не существует");
                return false;
            }
            return true;
        }
    }
}