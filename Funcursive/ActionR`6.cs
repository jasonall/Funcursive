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
    public static class ActionR<T1, T2, T3, T4, T5, T6>
    {
        /// <summary>
        /// Creates a recursive Action.
        /// </summary>
        /// <param name="a">The inner Action.</param>
        /// <returns>The created Action.</returns>
        public static Action<T1, T2, T3, T4, T5, T6> Create(Action<T1, T2, T3, T4, T5, T6, Action<T1, T2, T3, T4, T5, T6>> a)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }

            Action<T1, T2, T3, T4, T5, T6> outer = null;

            Action<T1, T2, T3, T4, T5, T6> inner = (v1, v2, v3, v4, v5, v6) =>
            {
                a(v1, v2, v3, v4, v5, v6, outer);
            };

            outer = inner;

            return outer;
        }

        /// <summary>
        /// Creates an async recursive Action.
        /// </summary>
        /// <param name="a">The inner Action.</param>
        /// <returns>The created Action.</returns>
        public static Func<T1, T2, T3, T4, T5, T6, Task> Create(Func<T1, T2, T3, T4, T5, T6, Func<T1, T2, T3, T4, T5, T6, Task>, Task> a)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }

            Func<T1, T2, T3, T4, T5, T6, Task> outer = null;

            Func<T1, T2, T3, T4, T5, T6, Task> inner = (v1, v2, v3, v4, v5, v6) =>
            {
                return a(v1, v2, v3, v4, v5, v6, outer);
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
        /// <param name="a">The inner Action.</param>
        public static void Invoke(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, Action<T1, T2, T3, T4, T5, T6, Action<T1, T2, T3, T4, T5, T6>> a)
        {
            Create(a)(value1, value2, value3, value4, value5, value6);
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
        /// <param name="a">The inner Action.</param>
        /// <returns>Returns the Action as a task.</returns>
        public static Task InvokeAsync(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, Func<T1, T2, T3, T4, T5, T6, Func<T1, T2, T3, T4, T5, T6, Task>, Task> a)
        {
            return Create(a)(value1, value2, value3, value4, value5, value6);
        }
    }
}