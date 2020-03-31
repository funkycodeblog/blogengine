# Hello CodeDOM

<!-- Id: codedom  -->
<!-- Categories: C# -->
<!-- Date: 20200319 -->

<!-- #header -->
CodeDOM is a part of .NET Framework which allows us to generate, compile and use code at runtime. It's quite sophisticated and I've been thinking that it was art for art's sake till I found it applied to real case scenario.
<!-- #endheader -->

Let’s consider the following scenario. Although simplified and changed it is based on true story.

We had built and currently support the application for our customer. One of the features is calculating bonus salaries for his employees.
It is based on a formula, and let’s say it depends on seniority, current salary, grate, rating and number of certificates. The formula was implemented according to the requirements, but problem is that our customer continuously asks us for changes in this formula. It rather boring and doesn’t pay so we need to invent something.

So what about expose this formula for customers admin and make it up to them to update this formula whenever they want. That’s pretty easy, but how to handle this formula?
The first (and maybe the best) attitude is to create a parser for formula. But it can be tricky (provided there’s nothing open source), and when there’s no budget for such a solution we can think of CodeDOM technology.

### What is CodeDOM?

CodeDOM technology enables us to dynamically compile and run .NET assembly out of string containing .NET code. It’s a little bit obsolete and currently replaced by Roslyn, but I still can be usable.  You can read more here:

https://docs.microsoft.com/pl-pl/dotnet/framework/reflection-and-codedom/dynamic-source-code-generation-and-compilation

Assebly can be generated and written on disk, but we take advantage of the possibility of storing assembly in memory.

So idea is to expose function content to the customer and let him edit this formula himself. Then his code will be compiled and calculations will be performed by function from dynamically compiled code.

How it will be done:

1. The user code snippet will be taken from the client web app.
2. The user code snippet will be combined with a template with C# code
3. The code will be compiled into assembly and assembly will be stored in memory
4. Type containing our function will be taken via reflection.
5. Function with formula will be invoked via reflection.

Here’s is what came from the client web app:

``` code
salary * 0.25m + (seniority / numberOfCertificates)
```

Here’s our code template:

``` csharp
const string CLASS_TEMPLATE = @"
 
using System;
 
namespace CodeDom
{
  
    public class BonusCalculator : IBonusCalculator
    {      
         public decimal Calculate(int seniority, decimal salary, int grade, double rating, int numberOfCertificates)
         {
             return @OUR_CUSTOMER_CODE ;
         }
    }
}";
```

Altogether with the snippet, it will make fully fledged code.

Let’s create a contract:

``` csharp
public interface IBonusCalculator
{
    decimal Calculate(int seniority, decimal salary, int grade, double rating, int numberOfCertificates);
}
```

Instead of compile-time implementation we  wil create implementation of this interface in run-time.

We need to inject type of class contaning formula function, so we need constructor. Lets’s create base clase for all run-time future implementation.

``` csharp
public abstract class CodeDomTypeWrapper
{
    protected Type _wrappedType { get; set; }
    protected object _instance { get; set; }

    public CodeDomTypeWrapper(Type wrappedType)
    {
        _wrappedType = wrappedType;
        _instance = Activator.CreateInstance(_wrappedType);
    }
}
```

And finally here’s implementation of our IBonusCalculator:

``` csharp
public class BonusCalculatorTypeWrapper : CodeDomTypeWrapper, IBonusCalculator
{
    MethodInfo _calculateMethod;

    public BonusCalculatorTypeWrapper(Type matchType)
        : base(matchType)
    {
        _calculateMethod = _wrappedType.GetMethod(nameof(IBonusCalculator.Calculate));
    }
    
    public decimal Calculate(int seniority, decimal salary, int grade, double rating, int numberOfCertificates)
    {
        decimal result = (decimal)_calculateMethod.Invoke(_instance, new object[] { seniority, salary, grade, rating, numberOfCertificates });       
        return result;
    }
  
}
```

We stil do not have our assembly, so here is clue of this blog post. As you see it’s not a rocket science. Compilation takes place in single line of code.

``` csharp
public class CodeDomFactory
{
    public static TWrapper Compile<TWrapper>(string code, string typeFullName) where TWrapper : CodeDomTypeWrapper
    {
        var parameters = new CompilerParameters
        {
            GenerateExecutable = false,
            GenerateInMemory = true
        };

        // adding referenced assemblies
        // our in-memory assembly got implementation of IBonusCalculator,
        // so reference to assembly that contains IBonusCalculator must be also added
        var @interface = typeof(TWrapper).GetInterfaces()[0];
        parameters.ReferencedAssemblies.Add(@interface.Assembly.Location);
        parameters.ReferencedAssemblies.Add("System.dll");
        parameters.ReferencedAssemblies.Add("System.Linq.dll");
         
        var codeDomProvider = CodeDomProvider.CreateProvider("CSharp");

        // here we are compiling our assembly
        var compilerResults = codeDomProvider.CompileAssemblyFromSource(parameters, code);

        if (compilerResults.Errors.HasErrors)
        {
            foreach (CompilerError error in compilerResults.Errors)
            {
                if (error.IsWarning)
                    continue;
                
                // TODO: handle errors
                return null;
            }
        }

        // let's get our created assembly and get
        Assembly assembly = compilerResults.CompiledAssembly;
        Type wrappedType = assembly.GetType(typeFullName);
        
        var codeDomWrapper = (TWrapper)Activator.CreateInstance(typeof(TWrapper), new object[] { wrappedType });
        
        return codeDomWrapper;
    }
}
```

Here is how everything is used. Of course it is simplified for purpose of quick understanding what all is about.

``` csharp
static void Main(string[] args)
{
    int seniority = 6;
    decimal salary = 15000;
    int grade = 9;
    double rating = 9.2;
    int numberOfCertificates = 5;

     // this is our customer code that came via HTTP from editor exposed to customer
     string clientCode = "salary * 0.25m + (seniority / numberOfCertificates)";
 
     // we are creatting full fledged class with customers part
     var classCode = CLASS_TEMPLATE.Replace("@OUR_CUSTOMER_CODE", clientCode);

     // magic lays here
     var calculator = CodeDomFactory.Compile<BonusCalculatorTypeWrapper>(classCode,"CodeDom.BonusCalculator");

     // then we use
     decimal bonus = calculator.Calculate(seniority, salary, grade, rating, numberOfCertificates);

 }
``` 

