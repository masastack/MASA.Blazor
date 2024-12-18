using System.Collections.Generic;

namespace Masa.Blazor.Test.MobileCascader;

public class CascaderModel
{
    public string Value { get; set; }
    public string Label { get; set; }
    public List<CascaderModel> Children { get; set; }
}

[TestClass]
public class PMobileCascaderTests: TestBase
{
}
