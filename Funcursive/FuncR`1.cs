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
    /// <typeparam name="T">The type of the first parameter.</typeparam>
    /// <typeparam name="TResult">The type of the return value.</typeparam>
    public static class FuncR<T, TResult>
    {
        /// <summary>
        /// Creates a recursive Func.
        /// </summary>
        /// <param name="f">The inner Func.</param>
        /// <returns>The created Func.</returns>
        public static Func<T, TResult> Create(Func<T, Func<T, TResult>, TResult> f)
        {
            return Create<TResult>(f);
        }

        /// <summary>
        /// Creates an async recursive Func.
        /// </summary>
        /// <param name="f">The inner Func.</param>
        /// <returns>The created Func.</returns>
        public static Func<T, Task<TResult>> Create(Func<T, Func<T, Task<TResult>>, Task<TResult>> f)
        {
            return Create<Task<TResult>>(f);
        }

        /// <summary>
        /// Creates and invokes a recursive Func.
        /// </summary>
        /// <param name="value">The first value to pass into the Func.</param>
        /// <param name="f">The inner Func.</param>
        /// <returns>Returns the result of the Func.</returns>
        public static TResult Invoke(T value, Func<T, Func<T, TResult>, TResult> f)
        {
            return Create(f)(value);
        }

        /// <summary>
        /// Creates and invokes an async recursive Func.
        /// </summary>
        /// <param name="value">The first value to pass into the Func.</param>
        /// <param name="f">The inner Func.</param>
        /// <returns>Returns the result of the Func as a task.</returns>
        public static Task<TResult> InvokeAsync(T value, Func<T, Func<T, Task<TResult>>, Task<TResult>> f)
        {
            return Create(f)(value);
        }

        /// <summary>
        /// Creates a recursive Func.
        /// </summary>
        /// <param name="f">The inner Func.</param>
        /// <returns>The created Func.</returns>
        /// <typeparam name="TResultWrapper">Type type of the return value.</typeparam>
        private static Func<T, TResultWrapper> Create<TResultWrapper>(Func<T, Func<T, TResultWrapper>, TResultWrapper> f)
        {
            if (f == null)
            {
                throw new ArgumentNullException("f");
            }

            Func<T, TResultWrapper> outer = null;

            Func<T, TResultWrapper> inner = v =>
            {
                return f(v, outer);
            };

            outer = inner;

            return outer;
        }
    }
}