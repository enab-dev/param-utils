param-utils
===========

Small C# Library For Parsing and Typecasting Configuration Settings

This library can be used to safely retrieve configuration settings in a .net environment while casting them safely to their intended type. Casting to string, bool, int32 and int64 is included by default but logic for more types can be added easily in a similar fashion to the existing ones. 

Simple unit tests in NUnit exist to validate the functionality of the existing functionality.

Sample usage for application setting "myFlag" which is a boolean:

```csharp

var myFlag = Params.GetParameter<bool>("myFlag");

```

