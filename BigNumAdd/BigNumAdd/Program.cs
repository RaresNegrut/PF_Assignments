using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigNumAdd
{
    class Program
    {
        static int[] NumRead()
        {
            string Line = "";
            Console.WriteLine("Enter the number you want to add");
            Line = Console.ReadLine();
            int[] BigNum = new int[Line.Length];
            for (int i = 0; i < BigNum.Length; i++)
                if (!Char.IsLetter(Line[i]))
                    BigNum[i] = (int)Char.GetNumericValue(Line[Line.Length - i - 1]);
                else
                    throw new Exception("Your input contains letters or symbols!");
            return BigNum;
        }
        static void NumDisplay(int[] v)
        {
            for (int i = 0; i < v.Length; ++i)
                Console.Write(v[i]);
        }
        static int[] Add(int[] v, int[] u)
        {
            //Array.Reverse(v);
            //Array.Reverse(u);
            int index = (v.Length > u.Length) ? v.Length + 1 : u.Length + 1;
            int[] t = new int[index];
            int it = 0, iu = 0, iv = 0, sum = 0;
            while (iu < u.Length && iv < v.Length)
            {
                sum = sum + v[iv++] + u[iu++];
                t[it++] = sum % 10;
                sum /= 10;
            }
            #region Fairly Inefficient Code for The Last Digits
            while (sum != 0 && it < t.Length)
            {
                t[it] += sum;

                if (iv < v.Length)
                    t[it] += v[iv++];
                else
                    if (iu < u.Length)
                    t[it] += u[iu++];

                if (t[it] > 9)
                {
                    t[it] %= 10;
                    sum = 1;
                }
                else
                    sum = 0;
                it++;
                //sum /= 10;
            } 
            #endregion
            Array.Reverse(t);

            return t;

        }
        static void Main(string[] args)
        {
            try
            {
                int[] v = NumRead();
                int[] u = NumRead();
                int[] result = Add(v, u);
                NumDisplay(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
