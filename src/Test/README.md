# README.md

## 1、什么是单元测试？

​	测试分为单元测试和集成测试。单元测试主要对单个类的公有属性和方法进行测试，不会涉及依赖对象，所有依赖对象都会用mock对象或者桩对象代替。因此，单元测试的主要目的是测试单个类的工作是否正常。集成测试则不然，需要测试在各个不同的使用场景中，各个类是否协调一致并且工作正常。

## 2、为什么要进行单元测试？

​	单元测试可以保证代码的正确性和稳定性。在测试驱动开发中，测试是先行的。单元测试会给出代码用例，程序的编写必须符合代码用例。在开发过程中，我们难免会对代码进行更改，如果改出了bug，单元测试可以第一时间发现问题。单元测试还可以测出边界情况，程序正常运行时很少涉及边界值，单元测试能够增强程序的健壮性。所以，单元测试是十分重要的。

## 3、如何对MASA.Blazor进行单元测试？

### 2.1 前期准备

​	MASA.Blazor是一个Blazor的组件库。然而，与其它组件库不同，MASA.Blazor是抽象组件库BlazorComponent的一个实现。

​	开发工具：

- [.NET 6 rc-2](https://dotnet.microsoft.com/download/dotnet/6.0)
- [vs2022](https://visualstudio.microsoft.com/zh-hans/vs/preview/) 

### 2.2 项目结构

```
MASA.Blazor
├── ...
├── src
│   ├── Test
│	│   └── MASA.Blazor.Test 测试项目
│   └──  MASA.Blazor 组件库
├── ...
```

### 2.3 测试详解

#### 2.3.1 测试框架

- MSTest
- bunit

#### 2.3.2 测试基类

​	TestBase

​	所有的测试类都需要继承TestBase，里面包含组件测试的基础功能。

#### 2.3.3 测试样式

##### 1、class

```c#
		[TestMethod]
        public void RenderNormal()
        {
            // Arrange & Act
            var cut = RenderComponent<MInput<string>>();
            var inputDiv = cut.Find("div");

            // Assert
            Assert.AreEqual(2, inputDiv.ClassList.Length);
            Assert.IsTrue(inputDiv.ClassList.Contains("m-input"));
            Assert.IsTrue(inputDiv.ClassList.Contains("theme--light"));
        }
```

##### 2、style

```c#
		[TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MInput<string>>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-input__slot");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px", style);
        }
```

#### 2.3.4 测试事件

```c#
		[TestMethod]
        public void RenderButtonAndClick()
        {
            // Arrange
            var times = 0;
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.OnClick, args =>
                {
                    times++;
                });
            });

            // Act
            var buttonElement = cut.Find("button");
            buttonElement.Click();

            // Assert
            Assert.AreEqual(1, times);
        }
```

#### 2.3.5 测试插槽

```c#
		[TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-alert__content");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
```









