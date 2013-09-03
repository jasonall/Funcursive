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
    /// <typeparam name="T">The type of the first parameter.</typeparam>
    public static class ActionR<T>
    {
        /// <summary>
        /// Creates a recursive Action.
        /// </summary>
        /// <param name="a">The inner Action.</param>
        /// <returns>The created Action.</returns>
        public static Action<T> Create(Action<T, Action<T>> a)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }

            Action<T> outer = null;

            Action<T> inner = v =>
            {
                a(v, outer);
            };

            outer = inner;

            return outer;
        }

        /// <summary>
        /// Creates an async recursive Action.
        /// </summary>
        /// <param name="a">The inner Action.</param>
        /// <returns>The created Action.</returns>
        public static Func<T, Task> CreateAsync(Func<T, Func<T, Task>, Task> a)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }

            Func<T, Task> outer = null;

            Func<T, Task> inner = v =>
            {
                return a(v, outer);
            };

            outer = inner;

            return outer;
        }

        /// <summary>
        /// Creates and invokes a recursive Action.
        /// </summary>
        /// <param name="value">The first value to pass into the Action.</param>
        /// <param name="a">The inner Action.</param>
        public static void Invoke(T value, Action<T, Action<T>> a)
        {
            Create(a)(value);
        }

        /// <summary>
        /// Creates and invokes an async recursive Action.
        /// </summary>
        /// <param name="value">The first value to pass into the Action.</param>
        /// <param name="a">The inner Action.</param>
        /// <returns>Returns the Action as a task.</returns>
        public static Task InvokeAsync(T value, Func<T, Func<T, Task>, Task> a)
        {
            return CreateAsync(a)(value);
        }
    }
}