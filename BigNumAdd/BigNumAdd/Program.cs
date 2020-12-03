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
            Console.WriteLine("Enter the number");
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
            Array.Reverse(v);
            for (int i = 0; i < v.Length; ++i)
                Console.Write(v[i]);
        }

        static int[] Add(int[] u, int[] v)
        {
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
            //Array.Reverse(t);

            return t;
        }

        /// <summary>
        /// <p>there is an intermediary function which takes a single digit and multiplies the array by that digit,then adds some zeros to the result, corresponding to the number multiplied by the powers of ten</p>
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <param name="pow"></param>
        /// <returns></returns>
        static int[] Array_Digit_Multiplication(int[] v, int x, int pow)
        {
            int[] tmp = new int[v.Length + pow];
            int remainder = 0;
            Array.Copy(v, 0, tmp, pow, v.Length);
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] *= x;
                remainder = tmp[i] / 10;
                tmp[i] %= 10;
                if (i < tmp.Length - 1)
                    tmp[i + 1] += remainder;
            }
            return tmp;
            //does not work at all

        }
        /// <summary>
        /// <p>takes the digits of the smaller array and multiplies them with the larger array,one by one</p>
        /// <p>the result w</p>
        /// </summary>
        /// <param name="v"></param>
        /// <param name="u"></param>
        /// <returns></returns>
        static int[] Multiply(int[] u, int[] v)
        {
            int[] result = new int[v.Length + u.Length];
            //pow is just a naming convention for the number of zeros my temporary variable will have
            int pow = 0;
            if (u.Length < v.Length)
                (u, v) = (v, u);

            for (int j = 0; j < v.Length; j++)
            {
                int[] tmp = Array_Digit_Multiplication(u, v[j], pow);
                result = Add(result, tmp);
                pow++;
            }
            //Array.Reverse(result);
            return result;
        }
        static void Main(string[] args)
        {
            try
            {
                int[] u = NumRead();
                int[] v = NumRead();
                int[] result = Multiply(u, v);
                NumDisplay(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
