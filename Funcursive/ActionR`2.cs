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
    public static class ActionR<T1, T2>
    {
        /// <summary>
        /// Creates a recursive Action.
        /// </summary>
        /// <param name="a">The inner Action.</param>
        /// <returns>The created Action.</returns>
        public static Action<T1, T2> Create(Action<T1, T2, Action<T1, T2>> a)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }

            Action<T1, T2> outer = null;

            Action<T1, T2> inner = (v1, v2) =>
            {
                a(v1, v2, outer);
            };

            outer = inner;

            return outer;
        }

        /// <summary>
        /// Creates an async recursive Action.
        /// </summary>
        /// <param name="a">The inner Action.</param>
        /// <returns>The created Action.</returns>
        public static Func<T1, T2, Task> CreateAsync(Func<T1, T2, Func<T1, T2, Task>, Task> a)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }

            Func<T1, T2, Task> outer = null;

            Func<T1, T2, Task> inner = (v1, v2) =>
            {
                return a(v1, v2, outer);
            };

            outer = inner;

            return outer;
        }

        /// <summary>
        /// Creates and invokes a recursive Action.
        /// </summary>
        /// <param name="value1">The first value to pass into the Action.</param>
        /// <param name="value2">The second value to pass into the Action.</param>
        /// <param name="a">The inner Action.</param>
        public static void Invoke(T1 value1, T2 value2, Action<T1, T2, Action<T1, T2>> a)
        {
            Create(a)(value1, value2);
        }

        /// <summary>
        /// Creates and invokes an async recursive Action.
        /// </summary>
        /// <param name="value1">The first value to pass into the Action.</param>
        /// <param name="value2">The second value to pass into the Action.</param>
        /// <param name="a">The inner Action.</param>
        /// <returns>Returns the Action as a task.</returns>
        public static Task InvokeAsync(T1 value1, T2 value2, Func<T1, T2, Func<T1, T2, Task>, Task> a)
        {
            return CreateAsync(a)(value1, value2);
        }
    }
}