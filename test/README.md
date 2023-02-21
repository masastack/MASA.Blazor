# README.md

## 1、什么是单元测试？

​	测试分为单元测试和集成测试。单元测试主要对单个类的公有属性和方法进行测试，不会涉及依赖对象，所有依赖对象都会用mock对象或者桩对象代替。因此，单元测试的主要目的是测试单个类的工作是否正常。集成测试则不然，需要测试在各个不同的使用场景中，各个类是否协调一致并且工作正常。

## 2、为什么要进行单元测试？

​	单元测试可以保证代码的正确性和稳定性。在测试驱动开发中，测试是先行的。单元测试会给出代码用例，程序的编写必须符合代码用例。在开发过程中，我们难免会对代码进行更改，如果改出了bug，单元测试可以第一时间发现问题。单元测试还可以测出边界情况，程序正常运行时很少涉及边界值，单元测试能够增强程序的健壮性。所以，单元测试是十分重要的。

## 3、如何对Masa.Blazor进行单元测试？

### 2.1 前期准备

​	Masa.Blazor是一个Blazor的组件库。然而，与其它组件库不同，Masa.Blazor是抽象组件库BlazorComponent的一个实现。

​	开发工具：

- [.NET 6 Preview 7](https://dotnet.microsoft.com/download/dotnet/6.0)
- [vs2022](https://visualstudio.microsoft.com/zh-hans/vs/preview/) 

### 2.2 项目结构

```
Masa.Blazor
├── ...
├── src
│   ├── Test
│	│   └── Masa.Blazor.Test 测试项目
│   └──  Masa.Blazor 组件库
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

##### 1、默认样式

```c#
			// Act
			var cut = RenderComponent<MInput<string>>();
			var classes = cut.Instance.CssProvider.GetClass();

			// Assert
			Assert.AreEqual("m-input theme--light", classes);
```

##### 2、IsDisabled样式

```c#
			// Act
			var cut = RenderComponent<TestInput>(props =>
			{
				props.Add(p => p.MockIsDisabled, true);
			});
			var input = cut.Instance;
			var classes = input.CssProvider.GetClass();

			// Assert
			Assert.IsTrue(input.IsDisabled);
			Assert.AreEqual("m-input m-input--is-disabled theme--light", classes);
```

​	这里Mock了一个IsDisabled，主要目的在于测试m-input--is-disabled是否由IsDisabled属性决定，计算属性IsDisabled会另行测试。

#### 2.3.4 测试计算属性

ComputedColor的默认值应该是primary。

```c#
			// Act
			var cut = RenderComponent<TestInput>();

			// Assert
			Assert.AreEqual("primary", cut.Instance.ComputedColor);
```

如果设置了Color则ComputedColor应该是Color

```c#
			// Act
			var cut = RenderComponent<TestInput>(props =>
			{
				props.Add(r => r.Color, color);
			});

			// Assert
			Assert.AreEqual(color, cut.Instance.ComputedColor);
```

#### 2.3.5 测试事件

HandleOnChange会被组件的部件调用，这里只需要测试它是否正常工作。

```c#
			// Arrange
			var cut = RenderComponent<MTextField<string>>();
			var args = new ChangeEventArgs()
			{
				Value = "hello"
			};

			// Act
			await cut.Instance.HandleOnChange(args);

			// Assert
			Assert.AreEqual("hello", cut.Instance.Value);
```

HandleOnChange还会调用ValueChanged,这里测试ValueChanged是否正常工作。

```c#
			// Arrange
			using var factory = new TestEventCallbackFactory();

			var val = "";
			var cut = RenderComponent<MTextField<string>>(props =>
			{
				props.Add(p => p.ValueChanged, factory.CreateEventCallback<string>(v =>
				{
					val = v;
				}));
			});

			// Act
			var args = new ChangeEventArgs()
			{
				Value = "hello"
			};
			await factory.Reciever.InvokeAsync(() => cut.Instance.HandleOnChange(args));

			// Assert
			Assert.AreEqual("hello", cut.Instance.Value);
			Assert.AreEqual("hello", val);
```









