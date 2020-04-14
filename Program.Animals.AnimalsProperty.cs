namespace ClassStructure //Вариант 14
{//№ 6 Структуры, перечисления, классы контейнеры и контроллеры
    partial class Program
    {
        public abstract partial class Animals //Животные
        {
            public enum AnimalsProperty : byte
            {
                PrintNames,// 0              
                MAXBodyLength,// 1                
                TotalWeight// 2               
            }
        }
    }
}
