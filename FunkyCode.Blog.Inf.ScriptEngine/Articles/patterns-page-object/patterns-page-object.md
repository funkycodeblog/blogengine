# Design patterns: Page Object

<!-- Id: patterns-page-object  -->
<!-- Categories: Design patterns, Testing  -->
<!-- Date: 20201205  -->

<!-- #header -->
The page object is quite fresh design pattern, much younger than folks from standard suite popularized by Gang of Four (founding fathers), although it is assigned to one of three Go4 pattern types (creational, structural and behavioural). As you quickly observe it is structural design pattern and helps us maintain clean code in user interface automation tests.
<!-- #endheader -->

Automatic UI tests, in general, are about:
1. programmatically editing or clicking user interface controls (commonly web page controls), invoking actions exactly as during manual testing,
2. taking data from controls on pages which are displayed as a result
3. compared this data with expected.

The most popular tool for this stuff is Selenium. Tests can be recorded as macros or programmed.

When I first saw selenium test execution I was really impressed and I thought that do this programmatically is a very challenging task. To my surprise, when I tried it proved to be pretty easy. In fact, although this technology has a low entry threshold, it is quite tricky, especially to write code easy to maintain when the web page layout is changing.

Let’s assume that Google calculator is an application to be automatically tested.

To start with Selenium you need to add references to Selenium.WebDriver (which is a base library) and implementation for desired browsers. You don’t need to write code for every browser (you work on interfaces), but you need a dedicated library when the code is executed.

![01 Nuget](01_nuget.png)

Then write some code in Test Project, and in general, that’s it.

``` csharp
[Test]
public void Calculator_Should_Return_9_For_4_Plus_5()
{
    int expected = 9;
    
    var driver = new ChromeDriver();
    driver.Navigate().GoToUrl("http:/google.com");

    var input = driver.FindElement(By.Name("q"));
    input.SendKeys("calculator");
    input.SendKeys(Keys.Enter);

    var key4 = driver.FindElement(By.Id("cwbt23"));
    var key5 = driver.FindElement(By.Id("cwbt24"));
    var keyPlus = driver.FindElement(By.Id("cwbt46"));
    var keyEquals = driver.FindElement(By.Id("cwbt45"));
    
    key4.Click();
    keyPlus.Click();
    key5.Click();
    keyEquals.Click();

    var output = driver.FindElement(By.Id("cwos"));
    var result = Convert.ToInt32(output.Text);

    Assert.That(result, Is.EqualTo(expected));
    driver.Close();
}
```

But the main problem (and the reason for this post) is that many programmers stay with code like above. Although it is effective and the result is achieved. This approach will create a lot of code redundancy and will be very hard to maintain in future.

Page Object pattern is a candidate for solving beforementioned problems.
It creates an abstraction of the web page but also of page elements and performed actions, web driver code should be removed at least from the first layer and hidden as deep as it could be.

Here’s our abstraction of calculator page, calculator element and action which will be performed in the context of automation tests.

``` csharp
public interface ICalculatorPage
{
    ICalculator Calculator { get; }
}

public interface ICalculator
{
    double GetResult(int first, OperationTypeEnum operation, int second);
    void Clear();
}
```

Next abstraction layer is decomposition that GetResult and Clear actions mean. As you see, still there are no references to Web Driver.

``` csharp
public abstract class CalculatorBase : ICalculator
{
    protected abstract void Click(OperationTypeEnum operationKey);
    protected abstract int GetValueFromOutput();
     
    public void Clear()
    {
        Click(OperationTypeEnum.Clear);
    }

    public int GetResult(int first, OperationTypeEnum operation, int second)
    {
        var key1 = (OperationTypeEnum)first;
        var key2 = (OperationTypeEnum)second;
     
        Click(key1);
        Click(operation);
        Click(key2);
        Click(OperationTypeEnum.Enter);
     
        var result = GetValueFromOutput();
        return result;
    }
}
```

Only in implementation for Google Calculator we have reference to Web Driver and to elements on Google Calculator page.

``` csharp
public class GoogleCalculator : CalculatorBase
    {
      // (...)
 
 
 Dictionary<OperationTypeEnum, string> OperationKeyDictionary => new Dictionary<OperationTypeEnum, string>
        {
            // (...)
            {OperationTypeEnum.n00,"cwbt43"},
            {OperationTypeEnum.Add,"cwbt46"},
            {OperationTypeEnum.Multiply,"cwbt26"},
            {OperationTypeEnum.Divide,"cwbt16"},
            {OperationTypeEnum.Enter, "cwbt45"},
            // (...)
        };
 
        protected override void Click(OperationTypeEnum operationKey)
        {
            var element = _driver.FindElement(By.Id(OperationKeyDictionary[operationKey]));
            element.Click();
        }
 
        protected override int GetValueFromOutput()
        {
            var element = _driver.FindElement(By.Id(OperationKeyDictionary[OperationTypeEnum.Output]));
            return Convert.ToInt32(element.Text);
        }
 
// (...)
}
```

After refactoring, our tests are much more fluent and flexible for adding another cases.

``` csharp
[Test]
[TestCase(4, OperationTypeEnum.Add, 5, 9)]
[TestCase(5, OperationTypeEnum.Multiply, 4, 20)]
[TestCase(9, OperationTypeEnum.Substract, 3, 6)]
[TestCase(6, OperationTypeEnum.Divide, 3, 2)]
public void Calculator(int first, OperationTypeEnum operation, int second, int expected)
{
    _page.Calculator.Clear();

    var result = _page.Calculator.GetResult(first, operation, second);

    Assert.That(result, Is.EqualTo(expected));
}
```

Let’s simulate changes in the layout by moving to a different calculator. Take look at this link:

[http://www.calculatoria.com/](http://www.calculatoria.com/)

To adjust to the new layout we need to add another implementation of CalculatorBase.

Notice that in the layout of Calculatoria output control are not defined id and control must be taken via XPath.

``` csharp
public class CalculatoriaCalculator : CalculatorBase
{
// (...)
 
Dictionary<OperationTypeEnum, string> OperationKeyDictionary => new Dictionary<OperationTypeEnum, string>
{
// (...)
 
{OperationTypeEnum.n08,"btn104"},
{OperationTypeEnum.n09,"btn105"},
{OperationTypeEnum.n00,"btn96"},
{OperationTypeEnum.Add,"btn107"},
{OperationTypeEnum.Clear,"btn27"},
{OperationTypeEnum.Output,"displaysum"}
 
// (...)
};
 
protected override int GetValueFromOutput()
{
var element = _driver.FindElement(By.XPath("//*[@id=\"ocalc\"]/tbody/tr[2]/td/div/input"));
return Convert.ToInt32(element.GetAttribute("value"));
}
 
// (...)
}
```




