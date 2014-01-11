using System;
using System.Configuration;
using NUnit.Framework;
using ParamUtils;

namespace ParamUtilsTests
{
	[TestFixture]
	public class ParamsTests
	{
		[Test, Category("general case")]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void TestCaseMissingParam ()
		{
			Params.GetParameter<string> ("randomParameter");
		}

		[Test, Category("string")]
		public void TestCaseString ()
		{
			var testStr = Params.GetParameter<string> ("stringTest");
			Assert.AreEqual ("blah", testStr);
		}

		[Test, Category("bool")]
		public void TestCaseBoolTrue ()
		{
			var testBool = Params.GetParameter<bool> ("boolTrueTest");
			Assert.AreEqual (true, testBool);
		}

		[Test, Category("bool")]
		public void TestCaseBoolFalse ()
		{
			var testBool = Params.GetParameter<bool> ("boolFalseTest");
			Assert.AreEqual (false, testBool);
		}

		[Test, Category("bool")]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void TestCaseBoolBad ()
		{
			Params.GetParameter<bool> ("boolBadTest");
		}

		[Test, Category("int32")]
		public void TestCaseInt ()
		{
			var testInt = Params.GetParameter<int> ("intTest");
			Assert.AreEqual (11, testInt);
		}

		[Test, Category("int32")]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void TestCaseBadInt ()
		{
			Params.GetParameter<int> ("intBadTest");
		}

		[Test, Category("int64")]
		public void TestCaseInt64 ()
		{
			var testInt = Params.GetParameter<Int64> ("int64Test");
			Assert.AreEqual (12, testInt);
		}

		[Test, Category("int64")]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void TestCaseBadInt64 ()
		{
			Params.GetParameter<int> ("int64BadTest");
		}
	}
}

