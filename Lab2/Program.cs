using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab2
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.InputEncoding = UTF8Encoding.UTF8;

            string choice;
            do
            {
                Console.WriteLine("\nДля виконання блоку 1 (варіант 1) введіть 1");
                Console.WriteLine("Для виконання блоку 2 (варіант 6) введіть 2");
                Console.WriteLine("Для виконання блоку 3 (варіант 2) введіть 3");
                Console.WriteLine("Для виконання блоку 4 (варіант 3) введіть 4");
                Console.WriteLine("Для виходу з програми введіть 0");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Виконую блок 1");
                        DoBlock_1();
                        break;
                    case "2":
                        Console.WriteLine("Виконую блок 2");
                        DoBlock_2();
                        break;
                    case "3":
                        Console.WriteLine("Виконую блок 3");
                        DoBlock_3();
                        break;
                    case "4":
                        Console.WriteLine("Виконую блок 4");
                        DoBlock_4();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Команда ''{0}'' не розпізнана. Зробіть, будь ласка, вибір із 1, 2, 3, 4, 0.", choice);
                        break;
                }
            } while (choice != "0");
        }
        static void DoBlock_1()
        {
            Console.WriteLine("1. Знайти мінімальний елемент у кожному рядку. Вивести його значення та індекси.");
            int[,] nums = FillTheArray();

            Console.WriteLine("Ваш масив:");
            Print2DArray(nums);

            int arrayLength = nums.GetLength(0);
            int subArrayLength = nums.GetLength(1);
            int min;
            for (int i = 0; i < arrayLength; i++)
            {
                min = nums[i, 0];
                for (int j = 0; j < subArrayLength; j++)
                {
                    if (min > nums[i, j])
                    {
                        min = nums[i, j];
                    }
                }
                Console.Write($"В рядку #{i + 1} мінімальний елемент {min} має індекси:");
                for (int j = 0; j < subArrayLength; j++)
                {
                    if (min == nums[i, j])
                    {
                        Console.Write($" [{i + 1}, {j + 1}]");
                    }
                }
                Console.WriteLine(".");
            }
        }
        static void DoBlock_2()
        {
            Console.WriteLine("6. Обміняти місцями перший з максимальних і перший (технічно 0-ий) елементи в кожному рядку матриці.");
            int[,] nums = FillTheArray();

            Console.WriteLine("Ваш масив:");
            Print2DArray(nums);

            int arrayLength = nums.GetLength(0);
            int subArrayLength = nums.GetLength(1);
            int max;
            for (int i = 0; i < arrayLength; i++)
            {
                max = 0;
                for (int j = 1; j < subArrayLength; j++)
                {
                    if (nums[i, max] < nums[i, j])
                    {
                        max = j;
                    }
                }
                (nums[i, max], nums[i, 0]) = (nums[i, 0], nums[i, max]);
            }

            Console.WriteLine("Матриця, в якій перший з максимальних і перший (технічно 0-й) елементи поміняні місцями:");
            Print2DArray(nums);
        }
        static void DoBlock_3()
        {
            Console.WriteLine("2. Упорядкувати за неспаданням рядок з мінімальною сумою елементів (якщо рядків з однаковою мінімальною сумою кілька, то впорядкувати кожен з них).");
            int[,] nums = FillTheArray();

            Console.WriteLine("Ваш масив:");
            Print2DArray(nums);

            int arrayLength = nums.GetLength(0);
            int subArrayLength = nums.GetLength(1);
            int sum;
            int min = 0;
            int[] indexes = new int[arrayLength];
            for (int i = 0; i < arrayLength; i++)
            {
                sum = 0;
                for (int j = 0; j < subArrayLength; j++)
                {
                    sum += nums[i, j];
                }
                indexes[i] = sum;
                if (i == 0)
                {
                    min = sum;
                }
                else
                {
                    min = Math.Min(min, sum);
                }
            }
            Console.WriteLine("Матриця з відсортованими за неспаданням рядками з мінімальною сумою елементів:");
            for (int i = 0; i < arrayLength; i++)
            {
                if (indexes[i] == min)
                {
                    SelectionSortTheRow(nums, i, subArrayLength);
                }
            }
            Print2DArray(nums);
        }
        static void SelectionSortTheRow(int[,] nums, int index, int n)
        {
            for (int i = 0; i < n - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (nums[index, j] < nums[index, min])
                    {
                        min = j;
                    }
                }
                (nums[index, min], nums[index, i]) = (nums[index, i], nums[index, min]);
            }
        }
        static void DoBlock_4()
        {
            Console.WriteLine("3. Упорядкувати стовпчики матриці за неспаданням мінімального елемента.");
            int[,] nums = FillTheArray();

            Console.WriteLine("Ваш масив:");
            Print2DArray(nums);

            int arrayLength = nums.GetLength(0);
            int subArrayLength = nums.GetLength(1);
            int[] min = new int[subArrayLength];
            int min_element;
            for (int i = 0; i < subArrayLength; i++)
            {
                min_element = nums[0, i];
                for (int j = 0; j < arrayLength; j++)
                {
                    if (min_element > nums[j, i])
                    {
                        min_element = nums[j, i];
                    }
                }
                min[i] = min_element;
            }
            //for (int i = 0; i < subArrayLength; i++)
            //{
            //    for (int j = 0; j < subArrayLength - i - 1; j++)
            //    {
            //        if (min[j] > min[j + 1])
            //        {
            //            (min[j], min[j + 1]) = (min[j + 1], min[j]);
            //            SwapСolumns(nums, j, arrayLength);
            //        }
            //    }
            //}
            //for (int i = 0; i < subArrayLength - 1; i++)
            //{
            //    int smallest = i;
            //    for (int j = i + 1; j < subArrayLength; j++)
            //    {
            //        if (min[smallest] > min[j])
            //        {
            //            smallest = j;
            //        }
            //    }
            //    (min[smallest], min[i]) = (min[i], min[smallest]);
            //    for (int k = 0; k < arrayLength; k++)
            //    {
            //        (nums[k, i], nums[k, smallest]) = (nums[k, smallest], nums[k, i]);
            //    }
            //}
            SelectionSortTheColumns(nums, min, subArrayLength, arrayLength);
            Console.WriteLine("Матриця з відсортованими стовпцями за неспаданням мінімального елемента:");
            Print2DArray(nums);
        }
        static void SelectionSortTheColumns(int[,] nums, int[] min, int n, int m)
        {
            for (int i = 0; i < n - 1; i++)
            {
                int smallest = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (min[smallest] > min[j])
                    {
                        smallest = j;
                    }
                }
                (min[smallest], min[i]) = (min[i], min[smallest]);
                for (int k = 0; k < m; k++)
                {
                    (nums[k, i], nums[k, smallest]) = (nums[k, smallest], nums[k, i]);
                }
            }
        }
        static int[,] FillTheArray()
        {
            Console.Write("Вкажіть скільки підмасивів буде у прямокутному двовимірному масиві: ");
            int n = int.Parse(Console.ReadLine());

            Console.Write("Вкажіть скільки елементів буде у кожному підмасиві: ");
            int m = int.Parse(Console.ReadLine());
            int[,] arr = new int[n, m];

            Console.WriteLine("Почергово введіть кожен підмасив двовимірного масиву (Числа кожного підмасива розділені пробілом):");
            for (int i = 0; i < n; i++)
            {
                int[] row = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int j = 0; j < m; j++)
                    arr[i, j] = row[j];
            }
            return arr;
        }

        static void Print2DArray(int[,] arr)
        {
            int maxLength = 0;
            int arrayLength = arr.GetLength(0);
            int subArrayLength = arr.GetLength(1);

            for (int i = 0; i < arrayLength; ++i)
            {
                for (int j = 0; j < subArrayLength; ++j)
                {
                    int length = arr[i, j].ToString().Length;
                    if (length > maxLength)
                        maxLength = length;
                }
            }

            for (int i = 0; i < arrayLength; ++i)
            {
                for (int j = 0; j < subArrayLength; ++j)
                    Console.Write(arr[i, j].ToString().PadLeft(maxLength + 1));
                Console.WriteLine();
            }
        }
    }
}