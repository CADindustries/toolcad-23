﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace toolcad23.Models
{
    internal static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void RandomlyAdd<T>(this IList<T> list, T newElement)
        {
            if (list.Count > 0)
            {
                RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
                byte[] box = new byte[1];
                provider.GetBytes(box);
                int k = box[0] % list.Count;
                list.Insert(k, newElement);
                return;
            }
            list.Add(newElement);
        }

        public static T Pop<T>(this IList<T> list, int index)
        {
            T r = list[index];
            list.RemoveAt(index);
            return r;
        }

        public static void Reset<T>(this IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = default;
            }
        }
    }
}
