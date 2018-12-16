using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using NUnit.Framework;

namespace SeleniumLocatorValidation.Extensions {
	public static class CssValidator {

		private static string result = null;
		private static Boolean isValid = false;
		private static Regex regex;
		private static MatchCollection matches;
		private static String TOKEN_EXTRACTOR = "^([^ ~+>\\[]*(?:\\[[^\\]]+\\])*)($|\\s*[ ~+>]\\s*[^ ~+>\\[].*$)";
		private static String CSS_TOKEN_CONDITION_EXTRACTOR = "(?i)^(-?[_a-z]+[_a-z0-9-]*|\\*)?(#[_a-z0-9-]*)?(\\.[_a-z0-9-]*)?(:[a-z][a-z\\-]*\\([^)]+\\))?(\\[\\s*-?[_a-z]+[_a-z0-9-]*\\s*(\\=|\\~=|\\|=|\\^=|\\$=|\\*=)?\\s*([\"'][-_.#a-z0-9:\\/ ]+[\"']|[-_.#a-z0-9:\\/]+)?\\s*\\])*$";
	
		public static Boolean SimpleCssSelectorTestExtensionMethod(this string locator) {
			return (new Regex(TOKEN_EXTRACTOR, RegexOptions.IgnoreCase | RegexOptions.Compiled)).IsMatch(locator);
		}

		public static Boolean SimpleTest(string locator) {
			return (new Regex(TOKEN_EXTRACTOR, RegexOptions.IgnoreCase | RegexOptions.Compiled)).IsMatch(locator);
		}

		public static Boolean ComprehensiveCssSelectorTestExtensionMethod(this string locator) {
			isValid = false;
			return isValid;
		}
			
		public static Boolean ComprehensiveTest(string locator) {
			isValid = false;
			return isValid;
		}
	}
}
