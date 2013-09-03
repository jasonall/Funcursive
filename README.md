Funcursive
==========

Funcursive is a simple framework for creating recursive Actions and Funcs in C#. Let's take a look at a simple example. The following method uses a Func to increment a method variable:

public int Method1()
{
    int x = 0;

    Func<int> f = () =>
    {
        ++x;
        return x;
    };

    int y = f();
    return y;
}


At the end of this method, x and y are both equal to 1.

Now let's expand our Func so it can increment x to whatever value we like:

public int Method1()
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
    return 5;
}


When we run this, x and y both get set to 5.

But loops are for wussies. Let's use FuncR to create a recursive func:

public int Method1()
{
    int x = 0;

    Func<int, int> f = FuncR<int, int>.Create((limit, self) =>
    {
        int v = x;
        if (x < limit)
        {
            ++x;
            v = self(limit);
        }

        return v;
    });

    int y = f(5);
    return y;
}

To create the recursive func, we call FuncR<TValue, TResult>.Create, and pass in a lambda. Notice that the lambda takes in a new parameter, named 'self'. We can use this parameter to recurse back into the Func. When we run this method, x and y still end up with the value 5.

Recursive is cool, but this method runs synchronously, and sync is for wussies. Let's make it async:

public async Task<int> Method1()
{
    int x = 0;

    Func<int, Task<int>> f = FuncR<int, int>.CreateAsync(async (limit, self) =>
    {
        int v = x;
        if (x < limit)
        {
            x = await Task.FromResult<int>(x + 1);
            v = await self(limit);
        }

        return v;
    });

    int y = await f(5);
    return y;
}


Now we've got a bonified asynchronous method, containing an async func, which can call back into itself! 

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

    Func<int, Task> a = ActionR<int>.CreateAsync(async (limit, self) =>
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

public int Method1()
{
    int x = 0;

    int y = FuncR<int, int>.Invoke(5, (limit, self) =>
    {
        int v = x;
        if (x < limit)
        {
            ++x;
            v = self(limit);
        }

        return v;
    });

    return y;
}

This works the same way for Actions, and for asynchronous lambas too!
