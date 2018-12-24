using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using NUnit.Framework;

namespace SeleniumLocatorValidation.Extensions {
	public static class CssSelectorValidator {

		private static string token = null;
		private static Boolean isValid = false;
		private static MatchCollection matches;
		const String TOKEN_EXTRACTOR = "^(?<token>[^ ~+>\\[]*(?:\\[[^\\]]+\\])*)(?<remainder>$|\\s*[ ~+>]\\s*[^ ~+>\\[].*$)";
		const String CSS_TOKEN_CONDITION_EXTRACTOR = "(?i)^(-?[_a-z]+[_a-z0-9-]*|\\*)?(#[_a-z0-9-]*)?(\\.[_a-z0-9-]*)?(:[a-z][a-z\\-]*\\([^)]+\\))?(\\[\\s*-?[_a-z]+[_a-z0-9-]*\\s*(\\=|\\~=|\\|=|\\^=|\\$=|\\*=)?\\s*([\"'][-_.#a-z0-9:\\/ ]+[\"']|[-_.#a-z0-9:\\/]+)?\\s*\\])*$";
		private static Regex tokenSplitterRegex = new Regex(TOKEN_EXTRACTOR, RegexOptions.IgnoreCase | RegexOptions.Compiled);
		private static Regex tokenInspectorRegex = new Regex(CSS_TOKEN_CONDITION_EXTRACTOR, RegexOptions.IgnoreCase | RegexOptions.Compiled);
	
		public static Boolean IsValidCssSelectorExpressionExtensionMethod(this string locator) {
			return (new Regex(TOKEN_EXTRACTOR, RegexOptions.IgnoreCase | RegexOptions.Compiled)).IsMatch(locator);
		}

		public static Boolean IsValidExpression(String locator)
		{
			token = null;
			String reminder = locator;
			while (!String.IsNullOrEmpty(reminder) && tokenSplitterRegex.IsMatch(reminder)) {
				matches = tokenSplitterRegex.Matches(reminder);
				foreach (Match match in matches) {
					if (match.Length != 0) {
						isValid = true;
						foreach (Capture capture in match.Groups["token"].Captures) {
							if (token == null) {
								token = capture.ToString();
								if (isValid) {
									if (!tokenInspectorRegex.IsMatch(token)) {
										isValid = false;
									}
								}
							}
						}
						if (isValid) {
							reminder = null;
							foreach (Capture capture in match.Groups["reminder"].Captures) {
								if (reminder == null) {
									reminder = capture.ToString();
								}
							}
						} else {
							reminder = "";
						}
					}
				}
			}
			return isValid;
		}
	}
}
