namespace Funcursive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A Func that can be called recursively.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter.</typeparam>
    /// <typeparam name="TResult">The type of the return value.</typeparam>
    public static class FuncR<T1, T2, T3, T4, T5, T6, TResult>
    {
        /// <summary>
        /// Creates a recursive Func.
        /// </summary>
        /// <param name="f">The inner Func.</param>
        /// <returns>The created Func.</returns>
        public static Func<T1, T2, T3, T4, T5, T6, TResult> Create(Func<T1, T2, T3, T4, T5, T6, Func<T1, T2, T3, T4, T5, T6, TResult>, TResult> f)
        {
            return Create<TResult>(f);
        }

        /// <summary>
        /// Creates an async recursive Func.
        /// </summary>
        /// <param name="f">The inner Func.</param>
        /// <returns>The created Func.</returns>
        public static Func<T1, T2, T3, T4, T5, T6, Task<TResult>> Create(Func<T1, T2, T3, T4, T5, T6, Func<T1, T2, T3, T4, T5, T6, Task<TResult>>, Task<TResult>> f)
        {
            return Create<Task<TResult>>(f);
        }

        /// <summary>
        /// Creates and invokes a recursive Func.
        /// </summary>
        /// <param name="value1">The first value to pass into the Func.</param>
        /// <param name="value2">The second value to pass into the Func.</param>
        /// <param name="value3">The third value to pass into the Func.</param>
        /// <param name="value4">The fourth value to pass into the Func.</param>
        /// <param name="value5">The fifth value to pass into the Func.</param>
        /// <param name="value6">The sixth value to pass into the Func.</param>
        /// <param name="f">The inner Func.</param>
        /// <returns>Returns the result of the Func.</returns>
        public static TResult Invoke(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, Func<T1, T2, T3, T4, T5, T6, Func<T1, T2, T3, T4, T5, T6, TResult>, TResult> f)
        {
            return Create(f)(value1, value2, value3, value4, value5, value6);
        }

        /// <summary>
        /// Creates and invokes an async recursive Func.
        /// </summary>
        /// <param name="value1">The first value to pass into the Func.</param>
        /// <param name="value2">The second value to pass into the Func.</param>
        /// <param name="value3">The third value to pass into the Func.</param>
        /// <param name="value4">The fourth value to pass into the Func.</param>
        /// <param name="value5">The fifth value to pass into the Func.</param>
        /// <param name="value6">The sixth value to pass into the Func.</param>
        /// <param name="f">The inner Func.</param>
        /// <returns>Returns the result of the Func as a task.</returns>
        public static Task<TResult> InvokeAsync(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, Func<T1, T2, T3, T4, T5, T6, Func<T1, T2, T3, T4, T5, T6, Task<TResult>>, Task<TResult>> f)
        {
            return Create(f)(value1, value2, value3, value4, value5, value6);
        }

        /// <summary>
        /// Creates a recursive Func.
        /// </summary>
        /// <param name="f">The inner Func.</param>
        /// <returns>The created Func.</returns>
        /// <typeparam name="TResultWrapper">Type type of the return value.</typeparam>
        private static Func<T1, T2, T3, T4, T5, T6, TResultWrapper> Create<TResultWrapper>(Func<T1, T2, T3, T4, T5, T6, Func<T1, T2, T3, T4, T5, T6, TResultWrapper>, TResultWrapper> f)
        {
            if (f == null)
            {
                throw new ArgumentNullException("f");
            }

            Func<T1, T2, T3, T4, T5, T6, TResultWrapper> outer = null;

            Func<T1, T2, T3, T4, T5, T6, TResultWrapper> inner = (v1, v2, v3, v4, v5, v6) =>
            {
                return f(v1, v2, v3, v4, v5, v6, outer);
            };

            outer = inner;

            return outer;
        }
    }
}