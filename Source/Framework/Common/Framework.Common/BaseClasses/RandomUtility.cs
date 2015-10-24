using System;
using Microsoft.Practices.ObjectBuilder2;

namespace Cedar.Framework.Common.BaseClasses
{
    /// <summary>
    ///     生成随机数
    /// </summary>
    public class RandomUtility
    {
        /// <summary>
        ///     生成纯数字的随机码（一般用于验证码）
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandom(int length)
        {
            if (length < 1 || length > 10)
            {
                return "";
            }
            var ticks = Guid.NewGuid().GetHashCode();

            var rad = new Random(ticks); //实例化随机数产生器rad；

            var init = 1;
            for (var i = 1; i < length; i++)
            {
                init = init*10;
            }

            var value = rad.Next(init, init*10);

            return value.ToString();
        }

        /// <summary>
        ///     生成12纯数字的随机码（礼券编号用）
        /// </summary>
        /// <returns></returns>
        public static string GetRandomCode()
        {
            var ticks1 = Guid.NewGuid().GetHashCode();
            var ticks2 = Guid.NewGuid().GetHashCode();
            var random1 = new Random(ticks1);
            var random2 = new Random(ticks2);
            return random1.Next(100000, 999999).ToString() + random2.Next(100000, 999999);
        }

        /// <summary>
        ///     生成纯数字的随机码
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GetRandomEx(int size)
        {
            var sequence = new int[size];
            var output = new int[size];


            for (var i = 0; i < size; i++)
            {
                sequence[i] = i;
            }

            //用GUID的hashcode不会出现重复数
            var ticks = Guid.NewGuid().GetHashCode();
            //var ticks = DateTime.Now.Ticks;

            var random = new Random(ticks);

            var end = size - 1;

            for (var i = 0; i < size; i++)
            {
                var num = random.Next(0, end + 1);
                output[i] = sequence[num];
                sequence[num] = sequence[end];
                end--;
            }

            return output.JoinStrings("");
        }
    }
}