using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module11DZ
{
    enum Gender
    {
        Male,
        Female
    }
    enum EducationForm
    {
        FullTime,
        PartTime
    }
    class Student
    {
        public string FullName { get; set; }
        public string Group { get; set; }
        public double AverageGrade { get; set; }
        public double FamIncome { get; set; }
        public int FamMembers { get; set; }
        public Gender Gender { get; set; }
        public EducationForm EducationForm { get; set; }
    }

    class Database
    {
        private List<Student> students = new List<Student>();

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public void DisplayStudents(List<Student> studentList)
        {
            foreach (var student in studentList)
            {
                Console.WriteLine($"Full Name: {student.FullName}");
                Console.WriteLine($"Group: {student.Group}");
                Console.WriteLine($"Average Grade: {student.AverageGrade}");
                Console.WriteLine($"Family Income: {student.FamIncome}");
                Console.WriteLine($"Family Members: {student.FamMembers}");
                Console.WriteLine($"Gender: {student.Gender}");
                Console.WriteLine($"Education Form: {student.EducationForm}");
                Console.WriteLine();
            }
        }

        public List<Student> GetTopStudents(int count, bool highestGrade)
        {
            var sortedList = highestGrade ?
                students.OrderByDescending(student => student.AverageGrade) :
                students.OrderBy(student => student.AverageGrade);

            return sortedList.Take(count).ToList();
        }

        public List<Student> GetBottomStudents(int count)
        {
            return students.OrderBy(student => student.AverageGrade).Take(count).ToList();
        }

        public List<Student> GetLowIncomeIncompleteFamily(int count)
        {
            return students
                .Where(student => student.FamMembers < 3)
                .OrderBy(student => student.FamIncome)
                .Take(count)
                .ToList();
        }
    }

    class Program
    {
        static void Main()
        {
            Database database = new Database();
            Console.WriteLine("Введите информацию об ученике: ");
            Console.Write("Полное имя: ");
            string fullName = Console.ReadLine();

            Console.Write("Группа: ");
            string group = Console.ReadLine();

            Console.Write("Средняя оценка: ");
            double averageGrade = Convert.ToDouble(Console.ReadLine());

            Console.Write("Cемейный доход: ");
            double familyIncome = Convert.ToDouble(Console.ReadLine());

            Console.Write("Члены семьи: ");
            int familyMembers = Convert.ToInt32(Console.ReadLine());

            Console.Write("Пол (Male/Female): ");
            Gender gender = (Gender)Enum.Parse(typeof(Gender), Console.ReadLine(), true);

            Console.Write("Форма обучения (FullTime/PartTime): ");
            EducationForm educationForm = (EducationForm)Enum.Parse(typeof(EducationForm), Console.ReadLine(), true);

            Student student = new Student
            {
                FullName = fullName,
                Group = group,
                AverageGrade = averageGrade,
                FamIncome = familyIncome,
                FamMembers = familyMembers,
                Gender = gender,
                EducationForm = educationForm
            };

            database.AddStudent(student);

            Console.WriteLine("\nИнформация о студенте: ");
            database.DisplayStudents(database.GetTopStudents(1, true));

            Console.WriteLine("Топ-10 учеников с наивысшей оценкой: ");
            var topStudentsHighGrade = database.GetTopStudents(10, true);
            database.DisplayStudents(topStudentsHighGrade);

            Console.WriteLine("Топ-10 учеников с самой низкой оценкой: ");
            var topStudentsLowGrade = database.GetBottomStudents(10);
            database.DisplayStudents(topStudentsLowGrade);

            Console.WriteLine("Топ-3 студентов с самым низким доходом и неполной семьей: ");
            var topLowIncomeIncompleteFamily = database.GetLowIncomeIncompleteFamily(3);
            database.DisplayStudents(topLowIncomeIncompleteFamily);
        }
    }
}
