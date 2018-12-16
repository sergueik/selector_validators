using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using NUnit.Framework;

namespace SeleniumLocatorValidation.Extensions
{
	public static class XPathValidator
	{
		private static string result = null;
		private static Boolean isValid = false;
		private static Regex regex;
		private static MatchCollection matches;
		private static String TOKEN_EXTRACTOR = "^\\s*(/?/?\\s*[^ /\\[]+(?:\\[[^\\]]+\\])*)($|\\s*//?\\s*[^ /\\[]+.*$)";

		public static Boolean SimpleXPathTestExtensionMethod(this string locator)
		{
			return (new Regex(TOKEN_EXTRACTOR, RegexOptions.IgnoreCase | RegexOptions.Compiled)).IsMatch(locator);
		}

		public static Boolean SimpleTest(string locator)
		{
			return (new Regex(TOKEN_EXTRACTOR, RegexOptions.IgnoreCase | RegexOptions.Compiled)).IsMatch(locator);
		}

		public static Boolean ComprehensiveXPathTestExtensionMethod(this string locator)
		{
			isValid = false;
			// TODO:  iterate over tokens 
			return isValid;
		}
			
		public static Boolean ComprehensiveTest(string locator)
		{
			isValid = false;
			// TODO:  iterate over tokens 
			return isValid;
		}
	}
}
