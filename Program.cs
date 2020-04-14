using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ClassStructure //Вариант 14
{//№ 6 Структуры, перечисления, классы контейнеры и контроллеры
    partial class Program
    {/*Задание 
    1) К предыдущей лабораторной работе (л.р. 5) добавьте к существующим классам перечисление и структуру. 
    2) Один из классов сделайте partial и разместите его в разных файлах. 
    3) Определить класс-Контейнер (указан в вариантах жирным шрифтом) для хранения разных типов объектов (в пределах иерархии) 
    в виде списка или массива (использовать абстрактный тип данных). 
    Класс-контейнер должен содержать методы get и set для управления списком/массивом, 
    методы для добавления и удаления объектов в список/массив, метод для вывода списка на консоль. 
    4) Определить управляющий класс-Контроллер, который управляет объектом-контейнером и реализовать в нем запросы по варианту. 
    При необходимости используйте стандартные интерфейсы (IComparable, ICloneable,….)*/

        /*Создать Зоопарк. 
          Найти средний вес животных заданного вида в зоопарке, 
          количество хищных птиц, 
          вывести всех животных отсортированных по году рождения.*/

        public interface IOrganism
        {
            void GetInfo();
            public abstract string ToString();
            public double GetWeight();
        }
        public class ZooController : ZooContainer<Animals>, IComparable
        {                   
            public new int Count { get; set; }          

            public ZooController() { Count++; }
           
            public int CompareTo(object obj)// определяет среднее количество животных заданного вида в зоопарке
            {
                ZooController ZC = obj as ZooController;

                if (ZC != null)
                {
                    if (this.Count < ZC.Count)
                    {
                        return -1;
                    }
                    else if (this.Count > ZC.Count)
                    {
                        return 1;
                    }
                    else
                        return 0;
                }
                else
                {
                    throw new Exception("Параметр должен быть типа ZooController!");
                }
            }

            // Вывод списка, класса-контроллера, на консоль
            public static void Listing()
            {                                
                foreach (var item in ListOfAnimals)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.WriteLine();
            }
            public override double GetAverageWeight(Animals[] A)
            {
                double totalWeight = 0;
                int count = 0;
                foreach (var item in A)
                {
                    totalWeight += item.Weight;
                    count++;
                }
                double AverageWeight = totalWeight / count;
                return AverageWeight;
            }

            public override int GetCount(Animals[] A)
            {
                int сount = 0;
                foreach (var item in A)
                {
                    сount++;
                }
                return сount;
            }

            public override void GetyearOfBirth(Animals[] A)
            {
                foreach (var item in A)
                {
                    Console.WriteLine($"Время создания: {item.Age}");
                }
            }
        }
        public abstract class ZooContainer<T> : IEnumerable<T> where T : Animals
        {                            
            public static List<T> ListOfAnimals { get; private set; }          
            public int Count { get { return ListOfAnimals.Count; } }
           
            public ZooContainer()
            {
                ListOfAnimals = new List<T>();
            }
            public T this[int index]
            {
                get
                {
                    if (index < 0 || index >= Count)
                        throw new IndexOutOfRangeException();
                    return ListOfAnimals[index];
                }
                set
                {
                    if (index < 0 || index >= Count)
                        throw new IndexOutOfRangeException();
                    ListOfAnimals[index] = value;
                }
            }
            public T GetByName(string name)
            {
                return
                ListOfAnimals.FirstOrDefault(
                h => string.Compare(h.Name, name,
               StringComparison.InvariantCultureIgnoreCase) == 0);
            }
            public void Add(T animal)
            {
                ListOfAnimals.Add(animal);
            }
            public T Remove(T animal)
            {
                var element = ListOfAnimals.FirstOrDefault(h => h == animal);
                if (element != null)
                {
                    ListOfAnimals.Remove(element);
                    return element;
                }
                throw new NullReferenceException("В экземпляре объекта не задана ссылка на объект!");
            }
            public void Sort()
            {
                ListOfAnimals.Sort();
            }
            public IEnumerator<T> GetEnumerator()
            {
                return ListOfAnimals.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public abstract double GetAverageWeight(Animals[] A);// средний вес животных заданного вида в зоопарке
            public abstract int GetCount(Animals[] A);// количество хищных птиц
            public abstract void GetyearOfBirth(Animals[] A);// вывести всех животных отсортированных по году рождения   
        }

        public abstract partial class Animals : IComparable<Animals>
        {
            public new abstract string ToString();           
            public abstract void ApplyOperation(Animals[] A, AnimalsProperty AP);           

            public string Name;
            public float BodyLength;
            public double Weight;

            public DateTime Age { get; set; }

            public Animals() { Name = "disenabled"; BodyLength = 0; Weight = 0; Age = DateTime.Now; }

            public Animals(string Name)
            {
                this.Name = Name;
                BodyLength = 0;
                Weight = 0;
                Age = DateTime.Now;
            }

            public Animals(string Name, float BodyLength)
            {
                this.Name = Name;
                this.BodyLength = BodyLength;
                Weight = 0;
                Age = DateTime.Now;
            }
            public Animals(string Name, float BodyLength, double Weight)
            {
                this.Name = Name;
                this.BodyLength = BodyLength;
                this.Weight = Weight;
                Age = DateTime.Now;
            }

            public virtual double GetWeight()
            {
                return Weight;
            }

            public static ref double Find(double Weight, Animals[] A)
            {
                for (int i = 0; i < A.Length; i++)
                {
                    if (A[i].Weight == Weight)
                    {
                        return ref A[i].Weight; // возвращается ссылка на адрес, а не само значение
                    }
                }
                throw new IndexOutOfRangeException("Животное с таким весом не найдено!");
            }

            public int CompareTo(Animals other)
            {
                return string.Compare(other.Name, Name, StringComparison.InvariantCultureIgnoreCase);
            }

            struct Animal
            {
                public string Name;
                public float BodyLength;
                public int Weight;

                public string FullInfo
                {
                    get { return string.Format("Название: {0} Длина тела: {1} Вес: {2}", Name, BodyLength, Weight); }
                }
                public Animal(string Name)
                {
                    this.Name = Name;
                    BodyLength = 0;
                    Weight = 0;
                }
                public Animal(string Name, float BodyLength)
                {
                    this.Name = Name;
                    this.BodyLength = BodyLength;
                    Weight = 0;
                }
                public Animal(string Name, float BodyLength, int Weight)
                {
                    this.Name = Name;
                    this.BodyLength = BodyLength;
                    this.Weight = Weight;
                }

                public void DisplayInfo()
                {
                    Console.WriteLine($"Название : {Name};\t Длина тела = {BodyLength};\t Вес = {Weight}.");
                }
            }
        }
        public class Mammals : Animals, IOrganism
        {
            public DateTime Age { get; private set; }

            public Mammals(string Name) : base(Name)
            {
                BodyLength = 0;
                Weight = 0;
                Age = DateTime.Now;
            }
            public Mammals(string Name, float BodyLength) : base(Name)
            {
                this.BodyLength = BodyLength;
                Weight = 0;
                Age = DateTime.Now;
            }
            public Mammals(string Name, float BodyLength, double Weight) : base(Name)
            {
                this.BodyLength = BodyLength;
                this.Weight = Weight;
                Age = DateTime.Now;
            }
            public override double GetWeight()
            {
                return Weight;
            }
            public void GetInfo()
            {
                Console.WriteLine($"Название животного {Name} Длина тела: {BodyLength} Вес: {Weight} Возраст: {Age}");
            }

            public override string ToString()
            {
                return string.Format("Млекопитающее: {0}; Длина тела = {1}; Вес = {2}.", Name, BodyLength, Weight);
            }

            public override void ApplyOperation(Animals[] A, AnimalsProperty AP)
            {
                switch (AP)
                {
                    case AnimalsProperty.PrintNames:
                        foreach (var item in A)
                        {
                            Console.Write($" {item.Name},");
                        }
                        break;
                    case AnimalsProperty.MAXBodyLength:
                        float max = A[0].BodyLength;
                        for (int i = 0; i < A.Length; i++)
                        {
                            if (max < A[i].BodyLength)
                            {
                                max = A[i].BodyLength;
                            }
                        }
                        Console.WriteLine($" Максимальная длина тела: {max}");
                        break;
                    case AnimalsProperty.TotalWeight:
                        double count = 0;
                        foreach (var item in A)
                        {
                            count += item.Weight;
                        }
                        Console.WriteLine($" Общий вес животных равен: {count}");
                        break;
                }
            }

            struct Mammal
            {
                public string MammalName;
                public float BodyLength;
                public int Weight;

                public Mammal(string MammalName)
                {
                    this.MammalName = MammalName;
                    BodyLength = 0;
                    Weight = 0;
                }
                public Mammal(string MammalName, float BodyLength)
                {
                    this.MammalName = MammalName;
                    this.BodyLength = BodyLength;
                    Weight = 0;
                }
                public Mammal(string MammalName, float BodyLength, int Weight)
                {
                    this.MammalName = MammalName;
                    this.BodyLength = BodyLength;
                    this.Weight = Weight;
                }

                public void DisplayInfo()
                {
                    Console.WriteLine($"Название : {MammalName};\t Длина тела = {BodyLength};\t Вес = {Weight}.");
                }
            }           
        }
        sealed class Crocodile
        {
            public string Name;
            public float BodyLength;
            public double Weight;

            public Crocodile() { Name = "disenabled"; BodyLength = 0; Weight = 0; }
            public Crocodile(string Name)
            {
                this.Name = Name;
                BodyLength = 0;
                Weight = 0;
            }
            public Crocodile(string Name, float BodyLength)
            {
                this.Name = Name;
                this.BodyLength = BodyLength;
                Weight = 0;
            }
            public Crocodile(string Name, float BodyLength, double Weight)
            {
                this.Name = Name;
                this.BodyLength = BodyLength;
                this.Weight = Weight;
            }         
            public override string ToString()
            {
                return string.Format("Рептилия: {0}; Длина тела = {1}; Вес = {2}.", Name, BodyLength, Weight);
            }
            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }
            public override bool Equals(Object obj)
            {
                if (obj == null || GetType() != obj.GetType()) return false;

                Crocodile temp = (Crocodile)obj;
                return Name == temp.Name &&
                BodyLength == temp.BodyLength &&
                Weight == temp.Weight;
                //return base.Equals(temp);
            }
            public new static bool ReferenceEquals(Object objA, Object objB)
            {
                return objA == objB;
            }

            // Finalize(), GetType(), Clone()          
            struct Croco
            {
                public string CrocodileName;
                public float BodyLength;
                public double Weight;

                public Croco(string CrocodileName)
                {
                    this.CrocodileName = CrocodileName;
                    BodyLength = 0;
                    Weight = 0;
                }
                public Croco(string CrocodileName, float BodyLength)
                {
                    this.CrocodileName = CrocodileName;
                    this.BodyLength = BodyLength;
                    Weight = 0;
                }
                public Croco(string CrocodileName, float BodyLength, int Weight)
                {
                    this.CrocodileName = CrocodileName;
                    this.BodyLength = BodyLength;
                    this.Weight = Weight;
                }

                public void DisplayInfo()
                {
                    Console.WriteLine($"Название : {CrocodileName}; Длина тела = {BodyLength}; Вес = {Weight} Хэш: {GetHashCode()}.");
                }
            }
        }
        public class Birds : Animals, IOrganism
        {
            public Birds(string Name) : base(Name)
            {
                BodyLength = 0;
                Weight = 0;
            }
            public Birds(string Name, float BodyLength) : base(Name)
            {
                this.BodyLength = BodyLength;
                Weight = 0;
            }
            public Birds(string Name, float BodyLength, double Weight) : base(Name)
            {
                this.BodyLength = BodyLength;
                this.Weight = Weight;
            }
            public override double GetWeight()
            {
                return Weight;
            }
            public void GetInfo()
            {
                Console.WriteLine($"Название животного {Name} Длина тела: {BodyLength} Вес: {Weight}");
            }

            public override string ToString()
            {
                return string.Format("Птица: {0}; Длина тела = {1}; Вес = {2}.", Name, BodyLength, Weight);
            }

            public override void ApplyOperation(Animals[] A, AnimalsProperty AP)
            {
                switch (AP)
                {
                    case AnimalsProperty.PrintNames:
                        foreach (var item in A)
                        {
                            Console.Write($" {item.Name},");
                        }
                        break;
                    case AnimalsProperty.MAXBodyLength:
                        float max = A[0].BodyLength;
                        for (int i = 0; i < A.Length; i++)
                        {
                            if (max < A[i].BodyLength)
                            {
                                max = A[i].BodyLength;
                            }
                        }
                        Console.WriteLine($"Максимальная длина тела: {max}");
                        break;
                    case AnimalsProperty.TotalWeight:
                        double count = 0;
                        foreach (var item in A)
                        {
                            count += item.Weight;
                        }
                        Console.WriteLine($"Общий вес животных равен: {count}");
                        break;
                }
            }

            struct Bird
            {
                public string BirdName;
                public float BodyLength;
                public double Weight;

                public Bird(string BirdName)
                {
                    this.BirdName = BirdName;
                    BodyLength = 0;
                    Weight = 0;
                }
                public Bird(string BirdName, float BodyLength)
                {
                    this.BirdName = BirdName;
                    this.BodyLength = BodyLength;
                    Weight = 0;
                }
                public Bird(string BirdName, float BodyLength, int Weight)
                {
                    this.BirdName = BirdName;
                    this.BodyLength = BodyLength;
                    this.Weight = Weight;
                }

                public void DisplayInfo()
                {
                    Console.WriteLine($"Название : {BirdName};\t Длина тела = {BodyLength};\t Вес = {Weight}.");
                }
            }
        }
        public class Fish : Animals, IOrganism
        {
            public Fish(string Name) : base(Name)
            {
                BodyLength = 0;
                Weight = 0;
            }
            public Fish(string Name, float BodyLength) : base(Name)
            {
                this.BodyLength = BodyLength;
                Weight = 0;
            }
            public Fish(string Name, float BodyLength, int Weight) : base(Name)
            {
                this.BodyLength = BodyLength;
                this.Weight = Weight;
            }
            public override double GetWeight()
            {
                return Weight;
            }
            public void GetInfo()
            {
                Console.WriteLine($"Название животного {Name} Длина тела: {BodyLength} Вес: {Weight}");
            }
            public override string ToString()
            {
                return string.Format("Рыба: {0}; Длина тела = {1}; Вес = {2}.", Name, BodyLength, Weight);
            }

            public override void ApplyOperation(Animals[] A, AnimalsProperty AP)
            {
                switch (AP)
                {
                    case AnimalsProperty.PrintNames:
                        foreach (var item in A)
                        {
                            Console.Write($" {item.Name},");
                        }
                        break;
                    case AnimalsProperty.MAXBodyLength:
                        float max = A[0].BodyLength;
                        for (int i = 0; i < A.Length; i++)
                        {
                            if (max < A[i].BodyLength)
                            {
                                max = A[i].BodyLength;
                            }
                        }
                        Console.WriteLine($"Максимальная длина тела: {max}");
                        break;
                    case AnimalsProperty.TotalWeight:
                        double count = 0;
                        foreach (var item in A)
                        {
                            count += item.Weight;
                        }
                        Console.WriteLine($"Общий вес животных равен: {count}");
                        break;
                }
            }

            struct fish
            {
                public string FishName;
                public float BodyLength;
                public int Weight;

                public fish(string FishName)
                {
                    this.FishName = FishName;
                    BodyLength = 0;
                    Weight = 0;
                }
                public fish(string FishName, float BodyLength)
                {
                    this.FishName = FishName;
                    this.BodyLength = BodyLength;
                    Weight = 0;
                }
                public fish(string FishName, float BodyLength, int Weight)
                {
                    this.FishName = FishName;
                    this.BodyLength = BodyLength;
                    this.Weight = Weight;
                }

                public void DisplayInfo()
                {
                    Console.WriteLine($"Название : {FishName};\t Длина тела = {BodyLength};\t Вес = {Weight}.");
                }
            }
        }
        static void Main(string[] args)
        {           
            ZooController AnimalsController = new ZooController();

            // Создание массива класса Animals
            Animals[] animals = new Mammals[5];
            animals[0] = new Mammals("Лев", 2.5f, 190);
            animals[1] = new Mammals("Медоед", 0.77f, 16);
            animals[2] = new Mammals("Амурский тигр", 2.7f, 170);
            animals[3] = new Mammals("Гепард", 1.1f, 21);
            animals[4] = new Mammals("Бурый медведь", 2, 500);

            // Добавление в цикле в список, класса-контроллера, массива Animals + вывод массива на консоль   
            Console.WriteLine("Создание массива класса Animals... " +
            "\nДобавление в цикле в список, класса-контроллера, массива Animals и вывод массива на консоль...\n");
            for (int i = 0; i < animals.Length; i++)
            {
                ZooController.ListOfAnimals.Add(animals[i]);
                Console.WriteLine(animals[i].ToString());
            }                
            Console.WriteLine();

            // Вывод списка, класса-контроллера, на консоль
            Console.WriteLine("Вывод списка, класса-контроллера, на консоль...\n");
            foreach (var item in AnimalsController)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();

            // Вывод только имён из массива
            Console.WriteLine("Вывод только имён из массива...");
            foreach (var item in animals)
            {
                item.ApplyOperation(animals, Animals.AnimalsProperty.PrintNames);
                break;
            }
            Console.WriteLine();

            // Вывод максимальной длины тела среди всех животных
            Console.WriteLine("\nВывод максимальной длины тела среди всех животных...");
            foreach (var item in animals)
            {
                item.ApplyOperation(animals, Animals.AnimalsProperty.MAXBodyLength);
                break;
            }
            Console.WriteLine();

            // Вывод общего веса животных из массива
            Console.WriteLine("Вывод общего веса животных из массива...");
            foreach (var item in animals)
            {
                item.ApplyOperation(animals, Animals.AnimalsProperty.TotalWeight);
                break;
            }
            Console.WriteLine();

            // Обращение к элементу списка, класса-контроллера, по имени экземпляра класса Animals
            Console.WriteLine("Обращение к элементу списка, класса-контроллера, по имени экземпляра класса Animals - 'Амурский тигр'...");
            Console.WriteLine(AnimalsController.GetByName("Амурский тигр").ToString());
            Console.WriteLine();

            // Удаление экземпляра класса Animals из списка, класса-контроллера
            Console.WriteLine("Удаление экземпляра класса Animals с именем 'Гепард' из списка, класса-контроллера...");
            Console.WriteLine(AnimalsController.Remove(animals[3]).ToString());
            Console.WriteLine();

            ZooController.Listing();

            // Сортировка списка, класса-контроллера
            Console.WriteLine("Сортировка списка, класса-контроллера... \nОтсортированный список:");
            AnimalsController.Sort();
            Console.WriteLine();
            ZooController.Listing();

            Console.WriteLine(new string('-',20));

            // Получение среднего веса животных заданного вида в зоопарке
            Console.WriteLine("Получение среднего веса животных заданного вида в зоопарке...");
            Console.WriteLine($"Средний вес животных в зоопарке: {AnimalsController.GetAverageWeight(animals)}");
            Console.WriteLine();

            // Получение количества животных в списке
            Console.WriteLine("Получение количества животных в списке...");
            Console.WriteLine($"Количество животных в списке: {AnimalsController.GetCount(animals)}");
            Console.WriteLine();

            // Вывод всех животных отсортированных по году рождения
            Console.WriteLine("Вывод всех животных отсортированных по году рождения...");
            AnimalsController.GetyearOfBirth(animals);                              


            Console.ReadKey();
        }
    }
    public static class Printer
    {
        public static object iAmPrinting(this string mammals)
        {
            return mammals.ToString();
        }

        public static string ToStr(this string mammals)
        {
            return mammals.ToString();
        }
    }
}
