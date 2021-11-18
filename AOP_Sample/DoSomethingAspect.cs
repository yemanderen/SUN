using Castle.DynamicProxy;
using System;

public class DoSomethingAspect : IInterceptor
{
    public static int a=0;
    public void Intercept(IInvocation invocation)
    {
        try
        {
            DoSomething();
            invocation.Proceed();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    void DoSomething()
    {
        Console.WriteLine(++a);
    }
}