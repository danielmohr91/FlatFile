﻿# Change Log

## 6-8-18 Code Review Comments

- Used an IDictionary appropriately, since order is important, but not an IList since we're normally querying by key vs. value, and not IEnumerable or IQueryable because order is important. 
- LayoutDescriptor.fields is private because we wanted to manage appending entries ourselves, given the calculations and dependencies that go along with it.
- Used a generic for the model, so that’s decoupled nicely. 
	- If the entity we’re parsing changes, FixedLayoutParser class does not change.
	- If business needs change, no changes needed to FixedlayoutParser. 
- Reviewed changes required to allow for chained expression methods from an existing AppendField method.
	e.g. LayoutDescriptor<TTarget> AppendField<TProperty>(Expression<Func<TTarget, TProperty>> expression, int fieldLength);


## 6-8-18
  Added IFlatFileLayoutDescriptor interface and documentation.
  Moved documentation to interface (vs. implementation). Only implementation specific comments remain.
  Refactored unit test naming and added new class for Type Converter Tests

## 6-11-18
  Thought about support for non-string types and type converters

  To do the above, need to have a working or mocked parser. Started adding a dummy flat file parser for now.
  Added new method to IFlatFileLayoutDescriptor interface to get ordered fields. Could return collection of
  all fields, and order in parser, OR order in LayoutDescriptor. Chose to order in LayoutDescriptor, since
  sorting depends on the implementation of fields (currently Dictionary<int, FixedFieldSetting>).

##  6-12-18
  Resumed on DummyFixedWidthFileParser. Use this in unit test for the type converter, and test converting id
  from string to int. Can either use reflection in the dummy class, or hardcode just for the primitive type.
  probably want to hardcode if type is PrimitiveTypes, else not implemented exception. 

## 6-13-18
  Implemented basic ParseFile method in FixedWidthFileParser
  Created unit tests
      - Created test model for strings and primitive types
      - Generated fixed width test file (StringTest.dat)
      - Overrode equals method and equality operators
      - Successfully implemented and tested parsing into string models

## 6-13-18 - Code Review Comments

1. Simplify error handling. 
 - What's in place is good if handling each exception differently (e.g. retrying on IO failure, 
   vs. something different for FileNotFound). Simply bubbling up a different error message for each one
   is not a good reason. Also, because inner exception isn't set, this actually reduces the information 
   sent up. Could either add custom exceptions, or remove all try  / catches. The purpose of this exercise
   is practicing OOP principles and SOLID practices, so going to skip the exception handling for now.
	- Done (Removed)
2. Use a generic as the return type in IFixedWidthFileParser
 - In `IFixedWidthFileParser`, change `IDictionary<int, TEntity>` to a generic return type.
	- Done
3. If re-throwing an exception, set the inner exception.
 - Likely removing all error handling for now (per #1), so this may be a non-issue
	- Done (removed error handling for now)
4. ParseFile should return a generic (similar to #2).
	- Done, returned TFile
5. I implemented one or more interfaces. Use these interfaces as the types now instead of concrete types
	- Done
6. Repeat above for all complex types (e.g. IList instead of list)
 - While I'm at it, I want to think through data structures more. e.g. ICollection vs. IDictionary for
   the entities being returned from ParseFile
 - Update List to IList, Dictionary to IDictionary, etc...
 - Level set on data structures. Think through the types used, and if each one is a good fit. Why or why not? 
	- IList is used once in GetOrderedFields
		- GetOrderedFields is invoked once in GetModelFromLine.
			- Nowhere is Add / Remove needed. This should be a collection instead. IEnumerable wouldn't honor the
			  custom order. ICollection honors the custom order, IList also does, but we don't need the extras. 
			- In GetOrderedFields, could call Enumerable.ToList() or cast to ICollection<T>. ToList creates a  
			  cached copy, so chose the cast instead. 
				- Switched to ToList, the cast generated an InvalidCastException. Can't cast a
				  WhereSelectEnumerableIterator to ICollection<IFixedFieldSetting>
	- No more lists are used anywhere in solution. 

## 6-14-18
 - Removed all Try / Catches.
 - Documented a few pre-conditions that should be checked for instead.
 - Updated FixedWidthFileParser and the interface to use a generic for the return type of ParseFile
 - Removed 'Peek' from ParseFile implementation
 - Started updating unit tests from Dictionary to Collection for return type from ParseFile

## 6-19-18
- Documentation
- Updated to depend on interfaces, not implementations. (Updated concrete types to interfaces where possible in FixedWidthFileParser, LayoutDescriptor, and unit tests)
	- e.g. LayoutDescriptor field in FixedWidthFileParser, is now IFlatFileLayoutDescriptor
	- Changed types from FixedFieldSetting to IFixedFieldSetting (except where instantiating). Maybe ninject down the road? 

## 6-26-18
- Thought through data structures in solution. 
- Only one list is used, changed this to a collection in GetOrderedFields, since we only care about the order.
  No list operations (Add / Remove, etc...) are required. 

## 6-27-18
- Cleaned up unit tests
- Started thinking through type converter api
	- Would like built in converters for primitive types, and custom converter exposed for mapping to complex objects
	- On IFlatFileLayoutDescriptor, could add "AsBool" to be used after AppendField. 
		- e.g.  IFlatFileLayoutDescriptor<TTarget> AsBool<TProperty>();
		- Nothing tells me that this is for the previously appended field though. Probably want to add the
		  converter in AppendField extension method.
		- If option is exposed, do we pass an enum for which basic type we try converting to, and also accept an 
		  expression for a custom converter? 
	- Thinking .WithTypeConverter is a better option and more readable. Perhaps an api like the following
		- .AppendField(x => x.id, fieldLength).WithTypeConverter<StringConverter>()
		- signature: IFlatFileLayoutDescriptor<TTarget> WithTypeConverter<TTypeConverter>();
- Polyfilled C# 6 safe equals method in DummyStringModel. Unit tests pass, can now build in C# 6 / VS 2015


## 6-27-18 - Code Review Comments
1. Leave ParseFile's return type as ICollection. Don't switch to IEnumerable, since we use the "Add" method internally. 
	- Remove class level generic TFile
	- Just return an ICollection (already in place) of type TEntity in ParseFile
		- e.g. Interface for ParseFile is simply ICollection<T> ParseFile. Not TFile (that allows ICollection to be any type via a second generic)
	- DONE
2. ParseFile implementation is good. 
	- +1 on reading line by line via the stream. ReadLine from stream preferable over ReadAllLines, so whole file isn't in memory
3. Cache OrderedFields either in LayoutDescriptor, or in a variable above the loop in GetModelFromLine
	- DONE
		- Thought best to cache in LayoutDescriptor, in so cached for any others calls to get ordered fields going forward. 
		- Saved as class level field. Marked this dirty in the Add method, so recalculated after field is appended
*		- LEE - should this be a public property instead of a method? 
4. Remove tentative WithLayoutDescriptor extension method in FlatFileLayoutDescriptor. 
	- Implement type converter by convention for primitive types, then go from there w/ the api. 
		- Implement convention based type converter for primitive types. Use built in parse or tryparse methods. 
		  Use type in existing PropertyInfo, don't make user pass in the type a second time. 
			- Remember, DateTime is NOT a primitive type :)
		- As a rule of thumb, favor CONVENTION over CONFIGURATION.

## 6-28-81
- Refactored unit tests. 
- Removed virtual method calls from constructor (ParserTestBase)
- Shared patterns and parsing for string tests and primitive type tests

## 7-2-18
- Implemented default TypeConverter for primitive types w/ reflection and built in Parse method.
- Implemented boolean specific type converter to account for 0 and 1, and uppercase True / False strings. 
- Implemented unit tests for boolean type converter and number of rows matching input file

## 7-3-18
- Changed ParseFile to right trim string fields. This should eventually be configurable. 
- Added GetHashCode to both test models. Used R# default for now. 
- Started thinking through a generic type converter
	- Added unit test for DateTime. Works with generic Parse method. 
	- Need to think through locale (MM/DD/YYYY vs. DD/MM/YYYY etc...)
	- Need to add additional tests for four digit years, and other formats

## 7-5-18
- Added unit tests for DateTime Converter. 
	- Tested Dates, DateTimes, two vs. four digit years, w/ and w/o milliseconds, and for FormatException on invalid month.

## 7-6-18	
- Started planning out interface for allowing custom type converters
- Started a TypeConverter for a test enum (Days of week). Following documentation on MSND for type converters: https://msdn.microsoft.com/en-us/library/ayybcxe5.aspx

## 7-9-18
- Tested conversions to Day enum from single character codes, abbreviated days, full days, and bad input

## 7-10-18
- Added unit tests for custom type converter
	- Compared collection of parsed rows from new test file to expected. 
	- Gave consideration to the following:			
		- string to enum check
		- primitive type and string regression check within the same model

## 7-16-18
- Added field.TypeConverter and tied up with AppendField
- Added conditional in FixedWidthFileParser to choose the default or custom type converter
- Tied up loose ends, unit tests for converting string to enum w/ custom type convert all pass. 
- Perhaps change the boolean type converter from IPrimitiveTypeConverter to ITypeConverter. 
	- All that is needed is the dictionary from 1, 0 and bad casing on "true" and "false", to "true" and "false". 
	- The rest is shared with the generic type converter. No changes recommended. 

## 7-18-18
### CODE REVIEW COMMENTS
- The if condition for booleans is a violation of the open closed principle.
	- Make BooleanTypeConverter a fully fledged type converter (like DummyEnumTypeConverter) that extends TypeConverter
- Follow this same pattern for the other primitive types (and possible others, like DateTime)
- Don't use by default in Parse method, but add the appropriate TypeConverter in AppendField using a known dictionary of converters above.
	- It's OK to have lots of small classes, it's good when you're using composition. What's bad is lots of small classes that traverse down and down a call stack to a shared method that handles 81 different things
	- Building with small classes is good. 

To do's
	- Change PrimitiveTypeConverter (formerlly GenericTypeConverter) to a TypeConverter for each supported type, extending TypeConverter, and perhaps implementing my ITypeConverter interface. ITypeConverter interface might no longer be necessary. 
	- Use a Dictionary of known types, and in AppendField add a type converter from this dictionary (if available) if none is passed in
	- Dictionary should be <Type, Object>, where the object is the newed up type converter. They can be shared instances. No need to use reflection to new up type converter for each field (would be the case if using Dictionary<Type, Type>)

## 7/19/18
- Quote of the day: "Once the requirements do change though it’s quite likely that they will change in a similar way again later on"
	- Implement with this in mind.
- Added type converters for each primitive type. Removed if condition for bools, and the generic converter (that uses reflection to call "Parse").
- Tied parsing and unit tests for the above

## 7/23/18
- Filled in unit tests for remaining primitive types.
- Need to think through if the new test methods checking type add much value - just checking if the model property is of expected type.

## 7/24/18
- Added boundary conditions to list of expected values. 
	- Next, either duplicate in a test file or (preferred), write a simple class to write LayoutDescriptors to a file

## 7/25/18
- Started automating writing a test file given a collection of expected rows and layout descriptor.
	- See FixedWidthFileWriter.cs

- Tie up test w/ GetExpectedRows

## 7/25/18
###CODE REVIEW COMMENTS
- depend on abstractions, not concrete implementations. When we extend the .net TypeConverter is this a violation? 
- Remove true true, false / false from the dictionary. if not found, then we just have the tolower value.
- Probably don't make the TypeConverters extend TypeConverter. Perhaps leverage the TypeConverter code, but don't extend it
- Not thinking through the api's / interfaces fully... very quick to jump on "there's already a typeconverter so no need to think through the api"
	- think through the api, that should drive the implementation
	- Keep ITypeConverter interface, think through only what I need (e.g. ConvertFromString), and only use that. 
		- only need ConvertFromString method
		- overcomplicating things to extend the .net typeconverter. keep it simple. think through the api.
			- don't just look what's available and use it. Design the api, and go from there. 
		- Sbyte, etc... can be skipped. If the file actually has individual bytes, the user should probably be deciding how to convert them. 
			- only worry about the primary ones, datetime, decimal, int, etc... 
			- only use double. for decimal, float, etc... just use decimaltypeconverter and cast to decimal
			- do the easy, straighforward ones. Leave the rest up to the enduser. 
- e.g. 
	Before: 
		StringTypeConverter : TypeConverter, ITypeConverter
	After: 
		StringTypeConverter : ITypeConverter
- LayoutDescriptor
	- shouldn't have knowledge of all the type converters. This should be an abstraction
	- violation of single responsibility principle, and open / closed principle
		- if a new type converter was created, LayoutDescriptor would have to change with current implementation
		- only depend on abstractions in LayoutDescriptor

simplify type TypeConverters
- think through which ones I want to handle, get rid of all the rest
	- awkward to convert string to sybte, etc... skip
	- provided end user ability to configure this themself (ITypeConverter and AppendField method that accepts one)
Think hard about the api / interface for the TypeConverters, make sure it's what I want it to be... e.g. return object in ConvertFromString? What about a Generic Type instead on the ITypeConverter class? 

Don't worry about deadlines



## 7/26/18
- Removed uncommon primitive types.
- No longer extending .net TypeConverter. Kept it simple, and only implemented pre-existing ITypeConverter. 
	- Changes in progress, perhaps using a generic for return type of ITypeConverter. If too messy, will make it an object. 

## 7/31/18
- Removed extra entry from dictionary
- ITypeConverter only defines "ConvertFromString"
	- Should ITypeConverter return a generic, or an object? 
	- Can't mix generics in a collection, so would need to introduce a new base interface and use this instead.
		- At that point, how much benefit is there to using a generic? 
		- Played around with some different options. Used <object> as the type, too messy. 
			- using the empty base interface works well for keeping all the other interfaces and method signatures clean, but when using "ConvertFromString" it becomes problematic
			- If it was an abstract class, might be easier than a base interface

## 8/1/18
- Thought through implications of using a generic for ITypeConverter
	- Could make things compile pretty cleanly with a base interface without the generic.
		- Gets messy with having a collection of mixed types. 
		- Could cast to ITypeConverter<object> or ITypeConverter<dynamic>, but that feels so wrong
		- Also, while it compiles, it would fail at runtime.
			- {"Unable to cast object of type 'FlatFile.FixedWidth.Implementation.TypeConverters.IntTypeConverter' to type 'FlatFile.FixedWidth.Interfaces.ITypeConverter [System.Object]'."}
- The main place we use the type converter, we already know the expected return type (through field.PropertyInfo.PropertyType). Not a huge advantage, and makes things messy. 
	- besides, modelProperty.SetValue expects and object for the converted value. Anything more specific is unnecessary.
- Removed byte and sbyte from unit tests


## 8/6/18
- Thought through quick and dirty File Writer
	- What about doubles that exceed field length (e.g. 1.33333333333333333333333333E+100)
	- This would truncate to 1.3333333333 with a shorter column width, and loose the exponent. Would rather get exception than bad data.
	- Threw exception if value length exceeds configured field length. Eventually could use a type converter here.

## 8/7/18
- Generated test file for primitive types

## 8/8/18
- Hit the following exception: 
  FileNotFoundException Microsoft.VisualStudio.TestPlatform.MSTest.TestAdapter.dll
- Can't run any tests. Cleaned / rebuilt, then closed / reopened solution with no success. Restarted visual studio, now running successfully. 
	- Details: https://developercommunity.visualstudio.com/content/problem/222892/filenotfoundexception-microsoftvisualstudiotestpla.html
- Started looking into failed unit test for parsing double. 
	- Can repro in linqpad. This seems strange: double.Parse(double.MaxValue.ToString()); fails
		- OverflowException
		- Value was either too large or too small for a Double.
		- Subtracting small amounts doesn't help. Subtracting 1E300 works: 
		  double.Parse((double.MaxValue - 1E300).ToString());
		- Resume here looking into this unexpected language behavior...
		- Can't be a language bug, but sure looks that way: 
			- Parse throw OverFlow Exception when: "s represents a number that is less than MinValue or greater than MaxValue."
 			  https://msdn.microsoft.com/en-us/library/fd84bdyt(v=vs.110).aspx
			  Where "MaxValue" is Double.MaxValue (https://msdn.microsoft.com/en-us/library/system.double.maxvalue(v=vs.110).aspx)
	- Here's what's happening...
		- double.MaxValue.ToString() is 1.79769313486232E+308
		- double.MaxValue is actually   1.7976931348623157E+308
		- ToString() is rounding, just over the threshold. Makes sense.

## 8/8/18
### Code Review Comments
- Remove truthy / falsey dictionary from boolean type converter
- Remove IPrimitiveTypeConverter interface (no longer used, forgot to delete it)
- Reviewed the "double.Parse(double.MaxValue.ToString());" question. Documented above. 

## 8/9/18
- Removed unused IPrimitiveTypeConverter interface
- Removed 0 and 1 from boolean type converter. Stay away from truthy falsey type conversions for the default converter. 
- Started on tying up code so unit tests all pass. 
	- Resume here: 
		floatTest
		expected: -0.214748368
		actual: -0.2147484

## 8/10/18
- Looked into issue truncating float when writing out the test file.
- Resume on unit test _____________Should_TruncateFloatToTenDigits_When_GetTruncatedFloatingPointNumberIsCalled
  also, the truncating logic should probably be in the layout desciptor for file writers? 

## 8/14/18
- Thought through float and decimal conversion / rounding behavior as a user of the application. 
- Ran a test file. Everything works as one would expect, except for float and double
	- float and double precision isn't exact. Thinking through how this should work. As an example:
	  Expected 
		Name	Value	Type
		boolTest	TRUE	bool
		decimalTest	7.92282E+28	decimal
		doubleTest	1.7976931248623157E+308	double
		floatTest	3.40E+38	float
		id	0	int
		intTest	2147483647	int
		longTest	9.22337E+18	long
		shortTest	32767	short
		stringTest	"Test 1"	string
		uintTest	4294967295	uint
		ulongTest	1.84467E+19	ulong
		ushortTest	65535	ushort
	   Actual
		Name	Value	Type
		boolTest	TRUE	bool
		decimalTest	7.92282E+28	decimal
		doubleTest	1.79769312486232E+308	double
		floatTest	3.40E+38	float
		id	0	int
		intTest	2147483647	int
		longTest	9.22337E+18	long
		shortTest	32767	short
		stringTest	"Test 1"	string
		uintTest	4294967295	uint
		ulongTest	1.84467E+19	ulong
		ushortTest	65535	ushort
	- Expected: 
		doubleTest	1.7976931248623157E+308
		floatTest	3.40282245E+38
	- Actual: 
		doubleTest	1.79769312486232E+308 (Rounded)
		floatTest	3.402822E+38 (Rounded)
	- The input file matches "actual", the issue was with generating the test file. Just need to shave some precision off.
		- The precision used in generating the test file isn't supported by the language. e.g.
			var test = 1.7976931248623157E+308;
			test.Dump(); // 1.79769312486232E+308
			test.ToString().Dump(); // 1.79769312486232E+308
		- Note the rounding above, even without ToString().
- Fixed the first row. Resume on fixing the matching on the second row (run unit tests to generate the exception)
	- Message: CollectionAssert.AreEqual failed. (Element at index 2 do not match.)


## 8/15/18
- All unit tests now pass. 
	- Updated expected values for floats, too many digits of precision were used so rounding occured. 

## 8/15/18

### Code Review Comments - 8/15/18
- Fix instances of ITypeConverter<object> - generic should be a primitive type, or the Day enum. Not object
	// Make a factory to get the type converter, or change ITypeConverter<object> to just object
	private readonly IDictionary<Type, ITypeConverter<object>> typeConverters;
- Fix AppendField
	IFlatFileLayoutDescriptor<TTarget> AppendField<TProperty>(Expression<Func<TTarget, TProperty>> expression, int fieldLength, ITypeConverter<object> typeConverter);
	// ITypeConverter<object> is wrong here, should be TProperty? 
- If there are any instances of "ITypeConverter<object>" something is wrong, should be TProperty or similar.
- Remove all instances of "ITypeConverter<object>"
	- AppendField method - clean up generics in this method and in the whole class. 
- type converter generics, should NOT be object, should be primitive types

- Started a few of the above changes. Resume here on object vs. ITypeConverter<TProperty> 

## 8/16/18
- Started thinking through options for making the TypeConverter strongly typed (vs. object). 
  Get's tricky because the generic type is not known at compile time for the default type converters.

## 8/27/18
- Thinking through how to best handle TypeConverters with a runtime generic. 

## 8/28/18
- Added linqpad script for above question.

## 8/29/18
- Added quick abstract factory linqpad script for vehicles to get some ideas / practice generics w/ factory pattern

## Code Review Comments - 8/29/18
	- In the boolean type converter, just let the error bubble up
	- Don't worry about the return type for the concrete type converters. Perhaps pass in type T on the fixed field settingsetting?
	- Continue on with the Type Converters

## 8/31/18
- Changed the generic for a collection of ITypeConverters or IFixedFieldSettings to object, since the generics are of assorted types, not all of type TProperty
- Compiles, but messy for collections of IFixedFieldSetting. Could cast generic type to object, but probably going to fail during runtime.

## 9/4/18
- Added comments in LayoutDescriptor on the challenges of using a generic field inside a collection. 
	-   // This will throw runtime exception. Setting is of type TProperty. 
        // Trying to the fixedfieldsetting add to the collection of fields, that are currently of type <object>
        // I can't think of a way to strongly type the collection of fields. 
        //     - Would typically use a generic, but can't mix generics in a collection, right? 
        //     - e.g. at element zero of an array, can't have field<bool>, element one field<string>, etc...
        // Generated an example here: C:\Projects\FlatFile\FlatFile.FixedWidth\IdeaSandbox\CollectionWithGeneric.linq

## 9/5/18
- See line 119 in LayoutDescriptor for example of issue
	- Each field has a TypeConverter of a generic type. No problem right? Use a generic on IFixedFieldSetting - but IFixedFieldSetting 
	  can't mix generics, because it's a collection: 
	  IDictionary<int, IFixedFieldSetting<object>> fields; 
	  Used object as an example for now to get it to compile when IFixedFieldSetting requires a generic to TProperty as return
	  type of the type converter.
- We could perhaps remove the generic from IFixedFieldSetting, but that tells us the return type of the TypeConverter field: 
  ITypeConverter<TProperty> TypeConverter { get; set; }
- One workaround is to use properties - GenericCollectionWithProperties.linq

## 9/6/18
- Finished static factory for convention based type converters
- Added unit test for int converter. Other primitive types need unit tests next. 

## 9/10/18
- Added unit tests for all other primitive type defaults for the convention based type converters

## 9/12/18
- Thought about the collection of mixed generic types. I don't see a way around using a non-generic base class, or 
  non-generic interface that the collection uses as the type. Can cast as needed, and perhaps a type in the base class
  to keep the casting clean. 
- Created a dictionary of mixed generics that returns a strongly typed object (e.g. TypeConverter<int>)
	- See: C:\Projects\FlatFile\FlatFile.FixedWidth\IdeaSandbox\CollectionWithNonGenericBaseClass.linq
- Created an example for TypeConverter factory

## 9/13/18
- Wrote some throwaway code for list and dictionary type converter lookups
- will implement this in test suite next: C:\Projects\FlatFile\FlatFile.FixedWidth\IdeaSandbox\CollectionOfTypedFields.linq

## 9/17/18
- Thought about using a base class or base interface with dynamic return type for `GetConvertedString` method. 
	- Could depend on interface or abstract class with `dynamic` return type, and then keep generics as is

## 9/19/18
- Implemented base interface with dynamic return type for GetConvertedString
- Implemented abstract base class for `TypeConverter` that implements `ITypeConverter<T>` and `ITypeConverterBase`
	- `ITypeConverter<T>` remains unchanged
	- `ITypeConverterBase` is new, and has `dynamic` return type (vs. type `T`). 
		- Other interfaces (e.g. `IFixedFieldSetting`) depend on this `dynamic` return type, vs. type `T`, which now allows collections
		  since the collection is all of type dynamic, vs. mixed generics
	- Updated misc loose ends so all 48 unit tests now pass again.

### Code Review Thoughts
- Interesting approach using the base clase, and relying on the dynamic method instead
	- Consider changing dynamic to object
	- Consider renaming one of the methods `ConvertFromString` so we aren't using conflicting method names in `ITypeConverter` and `ITypeConverterBase`
- Going forward, wrap up TODOs in the codebase, then revisit original code kata problem statement

## 9/25/18
	- Removed unneeded comments

## 10/10/18
- Added data munging project to test in a real world scenario (using [code kata #4](http://codekata.com/kata/kata04-data-munging/) as an example)
- Added custom type converter for ints, stripped out non digits. 
	- TODO: Test above
- Last row of totals needs to be ignored. Since reading as a stream, we don't at the beginning if it's the last row.
	- Could add new option `SkipLastRow`, and peek ahead when parsing stream.
	- Probably cleaner, run the flat file through a pre-processor and remove the totals row. 

### Code Review Comments - 10/10/18
1. Differentiate by `ITypeConverter` vs. `ITypeConverterBase` by namespace... not by base and not base
	- e.g. in C# collections, there is a collections namespace and generic namespace
		- [Collections](https://docs.microsoft.com/en-us/dotnet/api/system.collections?view=netframework-4.7.2)
		- [Generic Collections](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic?view=netframework-4.7.2)
	- Use same naming convention for type converters
2. `FixedFieldSetting`, generic is now unused
	- Remove the generic here. This will have a cascading effect - will be able to remove generics from several methods and classes now
3. On `LayoutDescriptor` > `Add`, maybe `ITypeConverterBase` as param type, not `TypeConverter` with generic. Then can remove generic on the other `Add` method
4. On `GetTypeConverter`, probably return non-generic `ITypeConverterBase` vs. `TypeConverter<T>`
	- Probably make the Generic a parameter instead... Type type as param vs. a generic. More readable.
	- Since returning the non-generic base, can return each without a cast now as well :)
5. There's another strategy vs. the generic and non generic version. Could use a container with the type stored under, say `GetResource`. I think it's called a Monad? 
6. Remove uneeded comments

## 10/15/18
- Moved ITypeConverter to new `...Interfaces.Generic` namespace. Renamed `ITypeConverterBase` to `ITypeConverter`
- Removed generic from `FixedFieldSetting`
	- Removed generic from method call
	- Removed generic from second `LayoutDescriptor.Add` method
		- The generic is needed in methods above `Add` in the call stack, because TProperty is used as return type for `TypeConverter`
- Resume on #3 tomorrow. 

## 10/16/18
- Removed generic from `ITypeConverter` in `LayoutDescriptor` > `Add`. Now no generic is needed on `Add` method.
- Removed generic from `GetTypeConverter`. Returning `ITypeConverter` (no generic), and Type T is now passed as a parameter instead of a generic.
	- Since returning `ITypeConverter` base (not generic), was able to remove all the casts as well.
- Removed unecessary comments
- Added curly braces back on single line `if` statements, and corrected R# settings. 
- Cleaned namespaces. 
	
## 10/17/18
- Pre-processed file to remove unneeded summary row
- Added unit test to confirm custom type converter (for dirty int) is working correctly, and that collection from weather.dat matches expected
- Background:
	- The totals row is problematic. 
	  Reading line by line, unsure of what is last row since using a stream - it's not trivial to simply pass flag for IgnoreLastRow
	  Is there a better way? 
	  These totals at the bottom should be ignored: 
	  `mo  82.9  60.5  71.7    16  58.8       0.00              6.9          5.3`
	  Perhaps want to run file through a pre-processor? 
	- Used a file-preprocessor
	- Added new unit test to confirm importer reads in from flat file correctly
		- Custom Type Converter strips out the asterisks from boundary values
		- Correctly reads into collection. This collection matches the mocked collection, based on the weather.dat import file
- Started on Min / Max spread report

## Code Review Comments - 10/18/18
1. Rename `Type T` in `GetTypeConverter` from Type T to something different. 
	- `Type T` is the convention used for generics. Don't re-use it for the parameter naming
2. Thoughts on skipping rows starting with _x_ being a pre-processor step, or something new built into flat file parsing? 
	- Perhaps `SkipRowsWith` as an option on flat file parser. 
	- More flexible would be something like a `TestForSkip` object _(similar to TypeConverter)_
		- Don't provide a default implementation. If `TestForSkip` is null, just skip.
3. Same question as above on skipping columns. Other libraries I've seen the workaround is to name a new column, say `garbage1`, `garbage2`, etc... for the width you want to skip. 
   Would you suggest similar approach, or add a new option to skip columns? 
	- Probably a new option for field... `shouldSkip` perhaps to skip columns? 

_Tomorrow - resume on reports. Also the two new settings overviewed above._


## 10/18/18 - 10/24/18
- Refactored parameter naming in `GetTypeConverter`. Was `T`, now `fieldType`.
- Though through skipping rows
	- `TestForSkip` similar to `TypeConverter`, but not on `IFlatFileLayoutDescriptor` via `AppendField` 
	- Similar functionality (e.g. `SkipFirstRow`, `SkipBlankRows` are on `ParseFile` method. will keep consistent)
	- Added `ITestForSkip` as parameter on `IFixedWidthFileParser` > `ParseFile`
		- Tricky, because this is a row by row basis. Same rule for each row though, same for file. Should be good. 
	- Implemented `ITestForSkip` in `ParseFile` in unit tests for weather reporting
		- Renamed internal / private `ParseFile` to `ParseFileHelper`, too many method overloads were confusing to read.
- Implemented ShouldSkip / ITestForSkip and tested successfully with weather report


## Code Review Comments - 10/24/18
- FlatFile
	- **add field to column (`IFixedFieldSetting`), add new property for ShouldSkip**
	- change `ITestForSkip` to include ignorefirstrow and ignoreblankrows. Then can get rid of overloaded ParseFile method, and just have one
		- Will need `rowNumber` for this
- Data Munging
	- in shouldSkip, include a row number as well (say you want to exclude all odd rows, skip first row, etc...)
		- if this was a live API, would keep `ParseFile` as is for backwards compatability, and add [Depricated] decrator on the old method

## 10/25/18
- Added `ShouldSkip` to `IFixedFieldSetting`, and implementation
	- Added `shouldSkip` as parameter to `AddField`, but only for one of the overloaded methods. The `AppendField` method that takes an ITypeConverter should never be skipped. 

## 10/26/18
- Started on unit tests for `FixedFieldSettingTests`

## 10/29/18
- Added feature to ignore columns
- Added unit test for skipping blank rows
- Unit tests in progress for SkipBlankRows, SkipColumns, SkipHeaderRow, and TestForSkip

## 10/30/18
- Added the following tests
	- `Should_SkipIgnoredField_When_AppendIgnoredFieldIsUsed`
	- `Should_SkipBlankRows_When_BlankRowSettingIsEnabled`
	- `Should_SkipHeaderRow_When_SkipHeaderSettingIsEnabled`
- Removed infinite recursion in `MinMax` report. I thought I could set a property like this: 
```
	private IList<int> differences
	{
	    get => differences ?? (differences = GetDifferences());
	    set { }
	}
```
- Nope, infinite recursion. Used a backing property for now. 

## 10/31/18
- This is no longer user friendly `parser.ParseFile(null, false, true); // Skip blank rows`, there are up to three optional paramters. Should have one test for skip object, or similar
	- Removed all skip related options. Generalized using TestForSkip interface instead. 
		- e.g. In `IFixedWidthFileParser` > `ParseFile`, removed optional parameters `bool ignoreFirstRow = false` and `bool ignoreBlankRows = false`.
		- method signature is cleaner. Was `ParseFile(testForSkip, true, true)` now `ParseFile(testForSkip)`

## Code Review Comments - 10/31/18
- Don't use a helper for both skip first row and blank rows
	- do one per class
- consider removing both the implementations for testforskip
	- Trivial to implement, the user can do it themselves. 
- increment row number outside the if statement


## 11/7/18
- Removed helpers, except for SkipBlankRows and SkipFirstRow
	- Only one skip rule per class
- In `FixedWidthFileParser.ParseFileHelper` moved `rowNumber++` outside if statement
- Resumed on weather report
- Implemented MinMax report
	- ordered all points be absolute difference
- Tested successfully in `Should_GetMaxMatchingExpected_When_MinMaxReportIsRun` and `Should_GetMinMatchingExpected_When_MinMaxReportIsRun`
- Not sure why stack overflow in ReportMinMax if this is used: 
```
// Infinite Recursive Loop... fix this
private IList<int> differences
{
    get => differences ?? (differences = GetDifferences());
    set { }
}
```
- Current workflow
	- Import file with `WeatherImporter`
		- Input: weather.dat
		- Output: collection of `Point`s
		- Overview: 
			- Reads in list of points using `WeatherImporter`
				- Only `Id`, `Max`, and `Min` are parsed out. 
			- Defines `WeatherReportSkipDefinitions` for skipping header, blank rows, and summary row
			- Defines `DirtyIntTypeConverter` for getting only digits in the integer columns (e.g. we don't want the asterisk denoting min / max value in a column)
	- Report Min / Max with `ReportMinMax`
		- Input: collection of `Point`s
		- Output: report object. Can get min or max using the following: 
			- `GetMinSpread`
				- Just returns `FirstOrDefault` from `points` (sorted list, cached)
			- `GetMaxSpread`
				- Just returns `LastOrDefault` from `points` (sorted list, cached)

### Code Review Comments - 11/7/18
- Can trim asterisks instead of choosing IsDigit
	- see `Trim` overload 
- Consider renaming x and y in `Point` to `high` and `low`. Maybe `Id` to `DayNumber`. Meaningful variable names vs. abstraction and re-usability. Hmmm... 
- Move the import stuff in `Should_ImportFlatFileToModel_When_LayoutDescriptorIsDefined` to the `WeatherImporter` class
	- Think through if `ITestForSkip` objects would be better in the `ILayoutDescriptor` vs. a parameter in `GetRows` method. 
- Asked about stack overflow (detailed above). The property with the getter and setter is syntactic sugar, it's really a method - therefore when I return `differences`, that calls the method, and so forth, ad infinitum.  

## 11/8/18
- Moved `WeatherReportSkipDefinitions` into `WeatherImporter`, and added method `GetWeatherSpreads` that wraps `GetRows` with the custom `ITestForSkip` object (`WeatherReportSkipDefinitions`)
- Changed from `IsDigit` filter with subsequent `Trim`, to simply `Trim('*', ' ')`, since only asterisks and spaces need to be stripped at the moment. 
- Refactored `IPoint` and `Point` to `IDailyTemperature`, and `DailyTemperature`. 
	- Refactored fields from `Id`, `X`, and `Y` to meaningful names for day id, high / low temp. 
- Thought through moving `ITestForSkip` object into `ILayoutDescriptor` vs. param to `GetRows`. Example currently in `GetWeatherSpreads`
	- Moved `ITestForSkip` object into `ILayoutDescriptor` vs. param to `GetRows`
	- Cleaned up now unecessary `ITestForSkip` parameters, and helper methods to support skipping or not skipping
- All unit tests pass. Resume here. Add new test to ensure collection of skip definitions can be used successfully. 

## 11/9/18
- Tested skipping with multiple `ITestForSkip` objects defined with `WithSkipDefinition`. 
- Example: 
```
var layout = new LayoutDescriptor<DummyStringModel>()
				.AppendField(x => x.Id, 5)
				.AppendField(x => x.Field1, 15)
				.AppendField(x => x.Field2, 15)
				.AppendField(x => x.Field3, 15)
				.WithSkipDefinition(new SkipBlankRows())
				.WithSkipDefinition(new SkipFirstRow());
```
- Added `LeagueScore`
- Added `LeagueStandingsImporter`
- Added `LeagueStandingsSkipDefinitions`
	- Skipped where `rowNumber == 0 || row.StartsWith("   --") || string.IsNullOrWhiteSpace(row);`
- Started layout descriptor
```
var layout = new LayoutDescriptor<LeagueScore>()
                .AppendIgnoredField(7) // Id
                .AppendField(x => x.TeamName, 16)
                .AppendIgnoredField(20)
                .AppendField(x => x.GoalsFor, 4)
                .AppendIgnoredField(3)
                .AppendField(x => x.GoalsAgainst, 6)
                .WithSkipDefinition(new LeagueStandingsSkipDefinitions());
```

## 11/12/18
- Mocked expected result for league standings, and tested `LeagueStandingsImporter` against `football.dat`
- Updated `ReportMinMax` to work with `Type T` vs. just `IEnumerable<IDailyTemperatures>`
- Added report for scoring spreads with `ReportMinMaxScore`

## 11/13/18
- Refactored `ReportMinMaxScore` and `ReportMinMaxTemperature`
	- _Is A_ vs. _Has A_ relationship? 
		- Each is a report, should probably inherit. 
		- However, not each is a report of `Tuple<int, int>`, they are strongly typed. 
		  To make inheritance clean would have to sacrifice readability of property names in `LeagueScore` and `DailyTemperatures` models, and method names in `ReportMinMaxScore` and `ReportMinMaxTemperature`. 
		- To maintain descriptive property and method names, used composition for now. Each of the two reports has a readonly field for `MinMaxReport`
		- Readability and Maintanability would be hurt by further refactoring (e.g. using tuples instead of models with friendly property names, and `GetMin` vs. something like `GetSmallestTemperatureSpread`)
- Refactored report names
- Used pattern matching in Equals method on new models. No build warnings or info level messages left. 

## 11/14/18

#### Part Three: DRY Fusion
- Take the two programs written previously and factor out as much common code as possible, leaving you with two smaller programs and some kind of shared functionality.
	- Already somewhat done. Formally think this through. 
	- _See 'Is A' vs. 'Has A' thoughts from 11/13/18._
		- Could factor out more common code (e.g. don't wrap the Min and Max reporting methods), but this would sacrifice readability. 
		- Same goes towards eliminating models and using tuples in the Soccer Report and Weather Report

#### Kata Questions
- To what extent did the design decisions you made when writing the original programs make it easier or harder to factor out common code?
	- Importer
		- Importing the flat file is all in a separate library (`FlatFileParser`). 
		- Using this is as simple as defining a layout descriptor with the column widths and field names. 
			- Optionally, one or more skip definitions may be used as well to pass over rows that are irrelevant or otherwise invalid 
			- Also, a custom type converter may override the defaults. This is the case for the `WeatherImporter`, `DirtyIntTypeConverter` is used for trimming and converting the strings (with asterisks) to integers. 
		- The above provided a clean separation of concerns.
			- The only "Importing" related code for the weather report, was the column definitions, rules for skipping weather specific rows, and the rule for converting dirty integers
			- This made the design decisions for the second report unambiguous and straightforward. 
	- Report
		- The `WeatherReport` sorted the weather model by the max and min temperature spreads. 
			- To get the max or min spread, just returned the first or last element of sorted collection. 
		- This is the same exact pattern needed for the Soccer Report, just different names. 
			- Refactored the min / max reporting aspect into `MinMaxReport`. Wrapped the min / max methods with friendly method names, and passed in list of tuples vs. the user friendly models.
			- Selecting out the tuples isn't terribly user-friendly. The rest maintains high readability, at the cost composing and wrapping methods vs. inheriting a base class with no extra methods for the equivilent of Min and Max methods. 
- Was the way you wrote the second program influenced by writing the first?
	- Yes, as stated above, this is broken into small, re-usable components with well designed interfaces. There was a single, unambiguous way to implement the file parsing for the second. 
	- Could have used composition or inheritence for the report portion. Raised an interesting design decision between less code and greater readability (using models with friendly property names, and descriptive method names in `WeatherReport` and `SoccerReport`)
- Is factoring out as much common code as possible always a good thing? Did the readability of the programs suffer because of this requirement? How about the maintainability?
	- No. I could have refactored out more common code (skipped models in the reports, and eliminated both reporting methods in the two report classes), but readability and maintainability would have suffered. 
	- Readability is still high, with descriptive model properties and descriptive method names in both reports. 
		- Readability would suffer if any more common code was abstracted out from `WeatherReport` and `SoccerReport`



## Code Review Comments - 11/14/18
- Background
	- Added SoccerReport 
		- new model
		- new layout descriptor
		- added unit tests
	- Summary of changes can be seen in [PR #11](https://github.com/danielmohr91/FlatFile/commit/d5e221e6b93b31eb19d72e9bca4629457f0abe87 )
	- Summarized Kata Questions above. Walk through design decisions. 
- Ask opinion on wrapping `MinMaxReport` with composition
	- Feels dirty to select out tuples. 
	- Also, very little complexity. Just sorting a list based on the absolute difference between two things. That four line query is somewhat abstracted already (using `Math.Abs`). 
		- Same applies to getting min and max, the abtraction is simply returning `FirstOrDefault` and `LastOrDefault` on a cached copy of `differences`, which is the sorted list from above. 
	- Response below:

> Agreed ^
> Remove `MinMaxReport` and put the query back in `WeatherReport` and `SoccerReport`
> Just that last change, then good for Code Kata 4. No further changes recommended. 

- Next up, [Code Kata 5](http://codekata.com/kata/kata05-bloom-filters/), [bloom filter](https://ipowerinfinity.wordpress.com/2008/03/02/bloom-filters-designing-a-spellchecker/) for spell checking!

## 11/28/18
- Refactored `MinMaxReport` out. No longer composing `SoccerScoreReport` and `WeatherReport` with the shared sorting and reporting on min max. 
- Soccer and Weather report are more readable, but at the cost of a duplicate query, and similar logic in both for min and max (just a one liner). 
- No further changes recommented. Unit tests pass. 
- Moving on to [Code Kata 5](http://codekata.com/kata/kata05-bloom-filters/)

## 11/29/18
- Updated weather report to use `Tuple` with `IDailyTemperature` and `int` _(for the temperature difference)_, since we need the day number for the new console app
- Added new project for console app
	- Started implementation for console app. 
	- Resume here, getting the file path for weather.dat

## 11/30/18
- Added path to weather.dat
- Next, implement IComparable in temperatures



- Make a console app to run this, then Code Kata 5