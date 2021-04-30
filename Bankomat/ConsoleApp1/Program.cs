using System;
using System.Collections.Generic;

namespace Bankomat
{
    class Program
    {
        static void combinations(long value, long[] nominals, int i, Dictionary<long, long> nomsAndCounts)
        {
            if (value == 0)
            {
                foreach (long c in nomsAndCounts.Keys)
                {
                    if (nomsAndCounts[c] > 4 && nomsAndCounts[c] < 21 || nomsAndCounts[c] % 10 > 4 || nomsAndCounts[c] % 10 == 0)
                    {
                        Console.Write($"Номиналом {c} - {nomsAndCounts[c]} купюр" + " + ");
                    }
                    else if(nomsAndCounts[c] % 10 == 1)
                    {
                        Console.Write($"Номиналом {c} - {nomsAndCounts[c]} купюра" + " + ");
                    }
                    else
                    {
                        Console.Write($"Номиналом {c} - {nomsAndCounts[c]} купюры" + " + ");
                    }
                }
                Console.WriteLine();
                return;
            }

            for (int j = i; j < nominals.Length; ++j)
            {
                if (value / nominals[j] > 0)
                    for (long k = value / nominals[j]; k > 0; --k)
                    {
                        long nextvalue =value -nominals[j] * k;
                        nomsAndCounts[nominals[j]] = k;
                        combinations(nextvalue, nominals, j + 1, nomsAndCounts);
                        nomsAndCounts[nominals[j]] = 0;
                    }
            }
        }
        static long check_and_integ(long a)
        {
            long value;
            try
            {
                value = Convert.ToInt64(Console.ReadLine());
                if (a == 0)
                {
                    if (value <= 0)
                    {
                        Console.WriteLine("Введенное значение <= 0. Введите другое число!");
                        value = check_and_integ(a);
                    }
                }
                else {
                    if (value < 0 || value > a)
                    {
                        Console.WriteLine($"Значение {value} недопустимо, так как либо <= 0, либо >{a}. Введите другое число!");
                        value = check_and_integ(a);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Введенное значение недопустимо!Введите другое число!");
                value = check_and_integ(a);
            }
            return value;
        }
        static void Main(string[] args)
        {
            long value;
            Console.WriteLine("Введите сумму для размена: ");
            value=check_and_integ(0);
            Console.WriteLine("Введите номиналы для размена: ");

            List<long> numbers = new List<long>();
            long nextVal = -1;
            while (nextVal != 0)
            {
                nextVal = check_and_integ(value);
                if (numbers.Contains(nextVal))
                {
                    Console.WriteLine("Дублирование номиналов, номинал будет учтён только один раз");
                }
                else
                {
                    if (nextVal > 0) numbers.Add(nextVal);

                }
            }
            if (numbers.Count == 0)
            {
                Console.WriteLine("Ни один из номиналов не подошёл(");
            }

            numbers.Sort((a, b) => b.CompareTo(a));
            long[] banknotes = numbers.ToArray();
            long[] denomination = new long[banknotes.Length];
            Dictionary<long, long> denominations = new Dictionary<long, long>();
            Console.WriteLine("Комбинации: ");
            combinations(value, banknotes, 0, denominations);
        }
    }
}
