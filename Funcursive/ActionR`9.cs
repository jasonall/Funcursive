namespace Funcursive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// An Action that can be called recursively.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter.</typeparam>
    public static class ActionR<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        /// <summary>
        /// Creates a recursive Action.
        /// </summary>
        /// <param name="a">The inner Action.</param>
        /// <returns>The created Action.</returns>
        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> Create(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> a)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }

            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> outer = null;

            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> inner = (v1, v2, v3, v4, v5, v6, v7, v8, v9) =>
            {
                a(v1, v2, v3, v4, v5, v6, v7, v8, v9, outer);
            };

            outer = inner;

            return outer;
        }

        /// <summary>
        /// Creates an async recursive Action.
        /// </summary>
        /// <param name="a">The inner Action.</param>
        /// <returns>The created Action.</returns>
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> CreateAsync(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task>, Task> a)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }

            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> outer = null;

            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> inner = (v1, v2, v3, v4, v5, v6, v7, v8, v9) =>
            {
                return a(v1, v2, v3, v4, v5, v6, v7, v8, v9, outer);
            };

            outer = inner;

            return outer;
        }

        /// <summary>
        /// Creates and invokes a recursive Action.
        /// </summary>
        /// <param name="value1">The first value to pass into the Action.</param>
        /// <param name="value2">The second value to pass into the Action.</param>
        /// <param name="value3">The third value to pass into the Action.</param>
        /// <param name="value4">The fourth value to pass into the Action.</param>
        /// <param name="value5">The fifth value to pass into the Action.</param>
        /// <param name="value6">The sixth value to pass into the Action.</param>
        /// <param name="value7">The seventh value to pass into the Action.</param>
        /// <param name="value8">The eighth value to pass into the Action.</param>
        /// <param name="value9">The ninth value to pass into the Action.</param>
        /// <param name="a">The inner Action.</param>
        public static void Invoke(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> a)
        {
            Create(a)(value1, value2, value3, value4, value5, value6, value7, value8, value9);
        }

        /// <summary>
        /// Creates and invokes an async recursive Action.
        /// </summary>
        /// <param name="value1">The first value to pass into the Action.</param>
        /// <param name="value2">The second value to pass into the Action.</param>
        /// <param name="value3">The third value to pass into the Action.</param>
        /// <param name="value4">The fourth value to pass into the Action.</param>
        /// <param name="value5">The fifth value to pass into the Action.</param>
        /// <param name="value6">The sixth value to pass into the Action.</param>
        /// <param name="value7">The seventh value to pass into the Action.</param>
        /// <param name="value8">The eighth value to pass into the Action.</param>
        /// <param name="value9">The ninth value to pass into the Action.</param>
        /// <param name="a">The inner Action.</param>
        /// <returns>Returns the Action as a task.</returns>
        public static Task InvokeAsync(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task>, Task> a)
        {
            return CreateAsync(a)(value1, value2, value3, value4, value5, value6, value7, value8, value9);
        }
    }
}