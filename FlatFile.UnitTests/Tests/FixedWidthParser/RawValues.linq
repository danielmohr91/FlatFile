<Query Kind="Program">
  <Reference Relative="..\..\bin\Debug\FlatFile.Core.dll">C:\Projects\flatfile\FlatFile.UnitTests\bin\Debug\FlatFile.Core.dll</Reference>
  <Reference Relative="..\..\bin\Debug\FlatFile.FixedWidth.dll">C:\Projects\flatfile\FlatFile.UnitTests\bin\Debug\FlatFile.FixedWidth.dll</Reference>
  <Reference Relative="..\..\bin\Debug\FlatFileParser.dll">C:\Projects\flatfile\FlatFile.UnitTests\bin\Debug\FlatFileParser.dll</Reference>
  <Namespace>FlatFile.Core</Namespace>
  <Namespace>FlatFile.FixedWidth.Implementation</Namespace>
  <Namespace>FlatFile.FixedWidth.Interfaces</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
</Query>

void Main()
{
	var tester = new RawValues();
	tester.GetStringValuesFromLongInvalidLine();
}

public class RawValues
{
	private string testString =
		"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";


	public void GetStringValuesFromValidLine()
	{
		var testLine = "1         test1     test2     ";
		var parser = CreateTestParser();

		var model = parser.ParseLine<StringModel>(testLine);

		if (!model.id.Equals("1         ") || !model.field1.Equals("test1     ") || !model.field2.Equals("test2     "))
		{
			throw new Exception($"Incorrect tuple was read from line. Expected 'test1', actual '{model.field1}'. Expected 'test2', actual '{model.field2}'");
		}
	}

	public void GetStringValuesFromLongInvalidLine()
	{
		var testLine = "1         test1     test2     " + testString;
		var parser = CreateTestParser();

		var model = parser.ParseLine<StringModel>(testLine);

		if (!model.id.Equals("1         ") || !model.field1.Equals("test1     ") || !model.field2.Equals("test2     "))
		{
			throw new Exception($"Incorrect tuple was read from line where line exceeds expected width. Expected 'test1', actual '{model.field1}'. Expected 'test2', actual '{model.field2}'");
		}
	}

	private FixedLengthLineParser CreateTestParser()
	{
		var testModel = new StringModel();

		var fieldSettings = new List<IFixedFieldSettings>
			{
				new FixedFieldSettings
				{
					Index = 1,
					Length = 10,
					PropertyInfo = testModel.GetType().GetProperty("id")
				},
				new FixedFieldSettings
				{
					Index = 1,
					Length = 10,
					PropertyInfo = testModel.GetType().GetProperty("field1")
				},
				new FixedFieldSettings
				{
					Index = 1,
					Length = 10,
					PropertyInfo = testModel.GetType().GetProperty("field2")
				}
			};

		ILayoutDescriptor<IFixedFieldSettings> settings = new FixedLayout<StringModel>(typeof(StringModel), fieldSettings);

		return new FixedLengthLineParser(settings);
	}
}

class StringModel
{
	public string id { get; set; } // TODO: Make this an int, and support field.TypeConverter
	public string field1 { get; set; }
	public string field2 { get; set; }

}