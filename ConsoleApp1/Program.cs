﻿using System;

namespace ConsoleApp1
{
    class Program
    {
            //У Михаила есть клетчатый лист бумаги размером N на M, где N — количество клеток в высоту, а M — количество клеток в ширину.
            //Михаил может сгибать этот лист пополам строго по клеточкам, причем сгибать он может как по горизонтали, так и по вертикали.
            //Михаилу интересно узнать, какое количество сгибаний листа он должен сделать, чтобы достичь минимальной площади. Поэтому он
            //просит вас помочь ему посчитать это значение по заданным числам N и M.

        public class Sum
        {
            private static void Main()
            {
                string str = Console.ReadLine();
                string[] a = str.Split(' ');
                int a1 = Int32.Parse(a[0]);
                int b1 = Int32.Parse(a[1]);
                int count = 0;

                while(a1 % 2 == 0 || b1 % 2 == 0)
                {
                    if (a1 % 2 == 0)
                    {
                        a1 = a1 / 2;
                        count++;
                    }
                    if (b1 % 2 == 0)
                    {
                        b1 = b1 / 2;
                        count++;
                    }
                }
                Console.WriteLine(count);
            }
        }
    }

}
