using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLogic.Extensions
{
    public static class ArrayExtension
    {
        public static byte[,] ToByteArray(this int[,] source)
        {
            byte[,] result = null;
            if (source != null)
            {
                int l1 = source.GetLength(0);
                int l2 = source.GetLength(1);
                result = new byte[l1, l2];
                for (int i = 0; i < l1; i++)
                {
                    for (int j = 0; j < l2; j++)
                    {
                        int p = source[i, j];
                        if (p > 0 && p < 256)
                        {
                            result[i, j] = (byte) p;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            return result;
        }
    }
}
