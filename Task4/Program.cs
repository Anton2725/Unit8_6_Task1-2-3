using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Runtime.Serialization;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    class Program
    {
        const string fileName = @"h:\projects\CDEV-9\temp\Students.dat";
        static void Main(string[] args)
        {
            /*Написать программу-загрузчик данных из бинарного формата в текст.

            На вход программа получает бинарный файл, предположительно, это база данных студентов.

            Свойства сущности Student:
                        Имя — Name(string);
                        Группа — Group(string);
                        Дата рождения — DateOfBirth(DateTime).
            
            Ваша программа должна:
            1. Создать на рабочем столе директорию Students.
            2. Внутри раскидать всех студентов из файла по группам (каждая группа-отдельный текстовый файл), 
               в файле группы студенты перечислены построчно в формате "Имя, дата рождения".
            
            Критерии оценивания
            0 баллов: задача решена неверно или отсутствует доступ к репозиторию.
            2 балла(хорошо): есть недочеты.
            4 балла(отлично): программа работает верно.*/

            /*BinaryFormatter formatter = new BinaryFormatter();

            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                Student newStudent = (Student)formatter.Deserialize(fs);
                Console.WriteLine($"Имя: {newStudent.Name} --- Группа: {newStudent.Group} --- Дата рождения: {newStudent.DateOfBirth.ToString()}");
            }
            Console.ReadLine();*/

            FileStream fs = new FileStream(fileName, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                
                var newStudent = (Student)formatter.Deserialize(fs);
                Console.WriteLine($"Имя: {newStudent.Name} --- Группа: {newStudent.Group} --- Дата рождения: {newStudent.DateOfBirth.ToString()}");

            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

        }

    }
}
