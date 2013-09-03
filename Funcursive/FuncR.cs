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
    /// <typeparam name="TResult">The type of the return value.</typeparam>
    public static class FuncR<TResult>
    {
        /// <summary>
        /// Creates a recursive Func.
        /// </summary>
        /// <param name="f">The inner Func.</param>
        /// <returns>The created Func.</returns>
        public static Func<TResult> Create(Func<Func<TResult>, TResult> f)
        {
            return Create<TResult>(f);
        }

        /// <summary>
        /// Creates an async recursive Func.
        /// </summary>
        /// <param name="f">The inner Func.</param>
        /// <returns>The created Func.</returns>
        public static Func<Task<TResult>> CreateAsync(Func<Func<Task<TResult>>, Task<TResult>> f)
        {
            return Create<Task<TResult>>(f);
        }

        /// <summary>
        /// Creates and invokes a recursive Func.
        /// </summary>
        /// <param name="f">The inner Func.</param>
        /// <returns>Returns the result of the Func.</returns>
        public static TResult Invoke(Func<Func<TResult>, TResult> f)
        {
            return Create(f)();
        }

        /// <summary>
        /// Creates and invokes an async recursive Func.
        /// </summary>
        /// <param name="f">The inner Func.</param>
        /// <returns>Returns the result of the Func as a task.</returns>
        public static Task<TResult> InvokeAsync(Func<Func<Task<TResult>>, Task<TResult>> f)
        {
            return CreateAsync(f)();
        }

        /// <summary>
        /// Creates a recursive Func.
        /// </summary>
        /// <param name="f">The inner Func.</param>
        /// <returns>The created Func.</returns>
        /// <typeparam name="TResultWrapper">Type type of the return value.</typeparam>
        private static Func<TResultWrapper> Create<TResultWrapper>(Func<Func<TResultWrapper>, TResultWrapper> f)
        {
            if (f == null)
            {
                throw new ArgumentNullException("f");
            }

            Func<TResultWrapper> outer = null;

            Func<TResultWrapper> inner = () =>
            {
                return f(outer);
            };

            outer = inner;

            return outer;
        }
    }
}