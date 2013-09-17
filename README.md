Funcursive
==========

Funcursive is a simple framework for creating recursive Funcs and Actions in C#.  

Let's take a look at a simple example. The following method creates a func that can increment a local variable to whatever limit we choose:

	public void Method1()
	{
		int x = 0;

		Func<int, int> f = (limit) =>
		{
			while (x < limit)
			{
				++x;
			}

			return x;
		};

		int y = f(5);
	}

When we run Method1, x and y both end up with the value of 5.

But loops are for wussies. Let's use FuncR to create a recursive func:

	public void Method1()
	{
		int x = 0;

		Func<int, int> f = FuncR<int, int>.Create((limit, self) =>
		{
			if (x < limit)
			{
				++x;
				self(limit);
			}

			return x;
		});

		int y = f(5);
	}

Notice that the lambda we pass to FuncR.Create now takes in a second parameter, named 'self'. We can use this parameter to recurse back into the Func. Also notice that FuncR.Create returns a normal Func. The recursion is hidden within the body of the Func.

Recursion is cool, but this method runs synchronously, and sync is for wussies. Let's make it async:

	public async Task Method1()
	{
		int x = 0;

		Func<int, Task<int>> f = FuncR<int, int>.Create(async (limit, self) =>
		{
			if (x < limit)
			{
				x = await Task.FromResult<int>(x + 1);
				v = await self(limit);
			}

			return x;
		});

		int y = await f(5);
	}

Now we've got an asynchronous method, which creates an async func, which can call back into itself! 

If we don't need to return anything from the lambda, we can use an Action rather than a Func:

	public void Method1()
	{
		int x = 0;

		Action<int> a = ActionR<int>.Create((limit, self) =>
		{
			if (x < limit)
			{
				++x;
				self(limit);
			}
		});

		a(5);
	}

And of course, this can be async too:

	public async Task Method1()
	{
		int x = 0;

		Func<int, Task> a = ActionR<int>.Create(async (limit, self) =>
		{
			if (x < limit)
			{
				x = await Task.FromResult(x + 1);
				await self(limit);
			}
		});

		await a(5);
	}

Last but not least, we can use the handy Invoke method if we want to create and invoke a recursive Func all in one go:

	public void Method1()
	{
		int x = 0;

		int y = FuncR<int, int>.Invoke(5, (limit, self) =>
		{
			if (x < limit)
			{
				++x;
				self(limit);
			}

			return x;
		});
	}

