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
    public static class ActionR
    {
        /// <summary>
        /// Creates a recursive Action.
        /// </summary>
        /// <param name="a">The inner Action.</param>
        /// <returns>The created Action.</returns>
        public static Action Create(Action<Action> a)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }

            Action outer = null;

            Action inner = () =>
            {
                a(outer);
            };

            outer = inner;

            return outer;
        }

        /// <summary>
        /// Creates an async recursive Action.
        /// </summary>
        /// <param name="a">The inner Action.</param>
        /// <returns>The created Action.</returns>
        public static Func<Task> CreateAsync(Func<Func<Task>, Task> a)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }

            Func<Task> outer = null;

            Func<Task> inner = () =>
            {
                return a(outer);
            };

            outer = inner;

            return outer;
        }

        /// <summary>
        /// Creates and invokes a recursive Action.
        /// </summary>
        /// <param name="a">The inner Action.</param>
        public static void Invoke(Action<Action> a)
        {
            Create(a)();
        }

        /// <summary>
        /// Creates and invokes an async recursive Action.
        /// </summary>
        /// <param name="a">The inner Action.</param>
        /// <returns>Returns the Action as a task.</returns>
        public static Task InvokeAsync(Func<Func<Task>, Task> a)
        {
            return CreateAsync(a)();
        }
    }
}