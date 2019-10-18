# ToDataTable

### What is this?

ToDataTable is a pair of .Net Core extension methods that allow you to create a .Net [DataTable](https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable?view=netframework-4.8) or a SQL Server [Table-Valued Parameter](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/table-valued-parameters) from a collection of objects, e.g:

```csharp
IEnumerable<SomeClass> enumerable = new List<SomeClass();
DataTable dataTable = enumerable.ToDataTable();
SqlParameter sqlParameter = enumerable.ToSqlParameter("@SqlParameterName","SqlUserDefinedDataTypeName")
```

### How do I get it?

Install ToDataTable via nuget Package Manager:
```
Install-Package ToDataTable -Version 0.1.2
```

or .Net CLI:
```
dotnet add package ToDataTable --version 0.1.2
```

### Give me an example:

Say you had a `Horse` class:

```csharp
public class Horse
{
    public string Name { get; set;}
    public string Occupation { get; set;}
    public int Legs { get; set; }
    public double TopSpeed {get; set; }
}
```

and you created an array of 2 `Horse`s :

```csharp
var horseArray = new[] {
    new Horse {
	    Name = "Dobin",
	    Occupation "Code Horse",
	    Legs = 4,
	    TopSpeed = 18.6
    },
    new Horse {
	    Name = "Plato",
	    Occupation "Curious Gelding",
	    Legs = 4,
	    TopSpeed = 22.3
    }
}
```

You'd use the `ToDataTable()` extension method to create a DataTable from your array:

```csharp
DataTable myDataTable = horseArray.ToDataTable();
```

The resulting table would look like this:

|Name|Occupation|Legs|TopSpeed|
|--|--|--|--|
|Dobin|Code Horse|4|18.6|
|Plato|Curious Gelding|4|22.3|

### Why is this useful?

Sql Table-valued parameters are created in .Net using `DataTable`s. Coding a DataTable from scratch is time consuming and prone to error. These extension methods can create a Datatable or a SqlParameter from a collection of objects in a single line of code. 

### Is it fast?

You bet. The code uses reflection to build methods for creating  `DataTable`s and adding `DataRow`s. The reflection code is only run once per type and  cached to a singleton context object. Caching makes the code run significantly faster than using reflection alone. 

I used [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) to measure the cached methods vs just reflecton. The Benchmark tests are included in the repo.
