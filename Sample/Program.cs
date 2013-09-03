namespace Sample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Funcursive;

    /// <summary>
    /// Contains FuncR and ActionR samples.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point into the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            // Run synchronous samples.
            ActionR_SyncSample();
            string value1 = FuncR_SyncSample();

            // Run asynchronous samples.
            bool quit = false;
            string value2 = null;

            ThreadPool.QueueUserWorkItem(async o =>
            {
                await ActionR_AsyncSample();
                value2 = await FuncR_ASyncSample();

                quit = true;
            });

            // Wait for the asynchronous samples to complete.
            while (!quit)
            {
                Thread.Sleep(100);
            }

            // Display the result of the func operations.
            Console.WriteLine(value1);
            Console.WriteLine(value2);
        }

        /// <summary>
        /// Shows usage of the synchronous ActionR method.
        /// </summary>
        public static void ActionR_SyncSample()
        {
            int x = 0;

            // Create a synchronous, recursive action taking one parameter as input.
            Action<int> a = ActionR<int>.Create((value, self) =>
            {
                // Loop recursively until value is reached.
                if (x < value)
                {
                    ++x;
                    self(value);
                }
            });

            // Execute the action with 4 loops.
            a(4);
        }

        /// <summary>
        /// Shows usage of the asynchronous ActionR method.
        /// </summary>
        /// <returns>An async task.</returns>
        public static async Task ActionR_AsyncSample()
        {
            int x = 0;

            // Create an asynchronous, recursive action taking one parameter as input.
            Func<int, Task> a = ActionR<int>.Create(async (value, self) =>
            {
                // Loop recursively until value is reached.
                if (x < value)
                {
                    x = await Task.FromResult<int>(x + 1);
                    await self(value);
                }
            });

            // Execut the action with 4 loops, and wait for it to complete.
            await a(4);
        }

        /// <summary>
        /// Shows usage of the synchronous FuncR method.
        /// </summary>
        /// <returns>The result of the operation.</returns>
        public static string FuncR_SyncSample()
        {
            int x = 0;

            // Create a synchronous, recursive func taking one parameter as input and returning a string.
            Func<int, string> a = FuncR<int, string>.Create((value, self) =>
            {
                // Loop recursively until value is reached.
                string s = x + ",";
                if (x < value)
                {
                    ++x;
                    s += self(value);
                }

                return s;
            });

            // Execute the action with 4 loops, and return the result.
            return a(4);
        }

        /// <summary>
        /// Shows usage of the asynchronous FuncR method.
        /// </summary>
        /// <returns>An async task containing the function result.</returns>
        public static async Task<string> FuncR_ASyncSample()
        {
            int x = 0;

            // Create a asynchronous, recursive func taking one parameter as input and returning a string.
            Func<int, Task<string>> a = FuncR<int, string>.Create(async (value, self) =>
            {
                // Loop recursively until value is reached.
                string s = x + ",";
                if (x < value)
                {
                    x = await Task.FromResult<int>(x + 1);
                    s += await self(value);
                }

                return s;
            });

            // Execute the action with 4 loops, wait for it to complete, and return the result.
            return await a(4);
        }
    }
}
