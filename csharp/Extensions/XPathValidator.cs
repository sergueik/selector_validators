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
	public static class XPathValidator {
		private static String TOKEN_EXTRACTOR = "^\\s*(/?/?\\s*[^ /\\[]+(?:\\[[^\\]]+\\])*)($|\\s*//?\\s*[^ /\\[]+.*$)";

		public static Boolean IsValidXPathExpressionExtensionMethod(this string locator) {
			return (new Regex(TOKEN_EXTRACTOR, RegexOptions.IgnoreCase | RegexOptions.Compiled)).IsMatch(locator);
		}

		public static Boolean IsValidExpression(string locator) {
			return (new Regex(TOKEN_EXTRACTOR, RegexOptions.IgnoreCase | RegexOptions.Compiled)).IsMatch(locator);
		}
	}
}
