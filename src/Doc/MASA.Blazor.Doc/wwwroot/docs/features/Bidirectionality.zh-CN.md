---
order: 1
title: 双向性(LTR/RTL)
---

MASA.Blazor 支持 RTL (从右至左) 语言.

## 使用

```c#
@page "/rtlSwitch"
@inject GlobalConfig GlobalConfig

<button @onclick="SetRTL">从右至左</button>
<button @onclick="SetLTR">从左至右</button>
<button @onclick="Switch">切换</button>

@code{
	void SetRTL()
	{
		GlobalConfig.RTL = true;
	}

	void SetLTR()
	{
		GlobalConfig.RTL = false;
	}

	void Switch()
	{
		GlobalConfig.RTL = !GlobalConfig.RTL;
	}
}
```
