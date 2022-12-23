﻿namespace Masa.Blazor.Docs;

public class CheckboxParameter
{
    public bool Value { get; set; }

    public string? ParameterValue { get; set; }

    public bool IsBoolean { get; set; }

    public CheckboxParameter(string parameterValue, bool isBoolean, bool value = false)
    {
        Value = value;
        IsBoolean = isBoolean;
        ParameterValue = parameterValue;
    }

    public CheckboxParameter()
    {
        IsBoolean = true;
    }
}
