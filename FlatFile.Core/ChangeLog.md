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
				- {"Unable to cast object of type 'FlatFile.FixedWidth.Implementation.TypeConverters.IntTypeConverter' to type 'FlatFile.FixedWidth.Interfaces.ITypeConverter`1[System.Object]'."}
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
### CODE REVIEW COMMENTS
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

### Code review comments
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

	CODE REVIEW COMMENTS
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