using System;
using BlazorComponent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test;

[TestClass]
public class ObservableObjectTests
{
    [TestMethod]
    public void Test()
    {
        var numbers = new[] { 1, 2, 3, 4 };
        numbers.ForEach(n =>
        {
            if (n > 2)
            {
                return;
            }
            
            Console.WriteLine(n);
        });

    }
    
    [TestMethod]
    public void ObservableObject_SimpleAssignment()
    {
        var options = new ObservableOptions();
        options.Page = 1;

        Assert.AreEqual(options.Page, 1);
    }

    [TestMethod]
    public void ObservableNestObject_SimpleAssignment()
    {
        var optionsWrapper = new ObservableNestOptions();
        optionsWrapper.ObservableOptions = new ObservableOptions
        {
            Page = 1
        };

        Assert.AreEqual(optionsWrapper.ObservableOptions.Page, 1);
    }

    [TestMethod]
    public void ObservableNestObject_UpdateByAction()
    {
        Action<ObservableOptions> action = options => { options.Page = 1; };

        var optionsWrapper = new ObservableNestOptions();
        action(optionsWrapper.ObservableOptions);

        Assert.AreEqual(optionsWrapper.ObservableOptions.Page, 1);
    }

    [TestMethod]
    public void ObservableNestObject_SimpleNestAssignment()
    {
        var optionsWrapper = new ObservableNestOptions();
        optionsWrapper.ObservableOptions.Page = 1;

        Assert.AreEqual(optionsWrapper.ObservableOptions.Page, 1);
    }

    private class ObservableOptions : ObservableObject
    {
        public int Page
        {
            get => GetValue<int>();
            set => SetValue(value);
        }
    }

    private class ObservableNestOptions : ObservableObject
    {
        public ObservableOptions ObservableOptions
        {
            get => GetValue(new ObservableOptions());
            set => SetValue(value);
        }
    }
}
