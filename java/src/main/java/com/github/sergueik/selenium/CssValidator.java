package com.github.sergueik.selenium;

import static org.testng.Assert.assertTrue;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * CSS Lexer-style validator for NSelene WebDriver wrapper .net project
 * https://www.w3.org/TR/CSS21/grammar.html
 * @author: Serguei Kouzmine (kouzmine_serguei@yahoo.com)
 */

// based on:
// https://github.com/sam-rosenthal/java-cssSelector-to-xpath/blob/master/src/main/java/org/sam/rosenthal/cssselectortoxpath/utilities/CssSelectorStringSplitter.java
// https://github.com/sam-rosenthal/java-cssSelector-to-xpath/blob/master/src/main/java/org/sam/rosenthal/cssselectortoxpath/utilities/CssElementAttributeParser.java

public class CssValidator {

	private static CssValidator instance = new CssValidator();

	private CssValidator() {
	}

	public static CssValidator getInstance() {
		return instance;
	}

	@SuppressWarnings("unused")
	private boolean debug = false;

	public void setDebug(boolean value) {
		this.debug = value;
	}

//@formatter:off
	private static final String tokenValidator = "^([^ ~+>\\[]*(?:\\[[^\\]]+\\])*)($|\\s*[ ~+>]\\s*[^ ~+>\\[].*$)";
//@formatter:on
//@formatter:off
	private static final String attributeValidator = "(?i)^(-?[_a-z]+[_a-z0-9-]*|\\*)?(#[_a-z0-9-]*)?(\\.[_a-z0-9-]*)?(:[a-z][a-z\\-]*\\([^)]+\\))?(\\[\\s*-?[_a-z]+[_a-z0-9-]*\\s*(\\=|\\~=|\\|=|\\^=|\\$=|\\*=)?\\s*([\"'][-_.#a-z0-9:\\/ ]+[\"']|[-_.#a-z0-9:\\/]+)?\\s*\\])*$";
//@formatter:on

	// control logging
	private boolean reportedTokenValidator = false;
	private boolean reportedAttributeValidator = false;

	public String getTokenValidator() {
		if (debug) {
			if (!reportedTokenValidator) {
				System.err.println("Token validator: " + tokenValidator);
				reportedTokenValidator = true;
			}
		}
		return tokenValidator;
	}

	public String getAttributeValidator() {
		if (debug) {
			if (!reportedAttributeValidator) {
				System.err.println("Attribute validator: " + attributeValidator);
				reportedAttributeValidator = true;
			}
		}
		return attributeValidator;
	}

	private static boolean reportedNavSeparator = false;
	private final String navSeparator = "[ ~+>]";

	public String getNavSeparator() {
		if (debug) {
			if (!reportedNavSeparator) {
				System.err.println("Nav separator: " + navSeparator);
				reportedNavSeparator = true;
			}
		}
		return "^" + navSeparator;
	}

	public boolean comprehensiveTokenTest(String cssSelectorString) {
		Pattern pattern = Pattern.compile(tokenValidator);
		Matcher match = pattern.matcher(cssSelectorString);
		boolean found = false;
		boolean foundToken = true;
		boolean foundRemainder = true;
		List<String> tokenBuffer = new ArrayList<>();
		List<String> tailBuffer = new ArrayList<>();
		int tokenCnt = 0;
		int maxTokenCnt = 100; // paranoid
		while (match.find() && foundToken && foundRemainder
				&& tokenCnt < maxTokenCnt) {

			if (match.group(1) == null || match.group(1) == "") {
				foundToken = false;
			}
			if (match.group(2) == null /* || match.group(2) == "" */ ) {
				foundRemainder = false;
			}

			if (foundToken) {
				String token = match.group(1);
				tokenBuffer.add(token);
				if (debug) {
					System.err.println(String.format("Extracted token = \"%s\"",
							tokenBuffer.get(tokenCnt)));
				}
			}
			// NOTE the difference between cssSelector and XPath tokens: a valid
			// cssSelectoron can not start with
			// DOM nav, so we chop it away explicitly from the remainder
			if (foundRemainder) {
				String remainderWithNavPrefix = match.group(2);
				String remainder = remainderWithNavPrefix.replaceAll("^" + navSeparator,
						"");
				tailBuffer.add(remainder);
				if (debug) {
					System.err.println(
							String.format("Remaining of the CssSelector: \"%s\"", remainder));
				}
				if (remainder.length() == 0) {
					if (debug) {
						System.err.println("Reached the end of the cssSelector string.");
					}
					found = true; // reached the end of the cssSelectorString string.
												// Grammar is matched
				} else {
					match = pattern.matcher(remainder);
				}
			} else {
				if (debug) {
					System.err.println("Remainder of the string fails to match. ");
				}
			}
			tokenCnt++;
		}
		if (found) {
			for (String cssSelectorTokenString : tokenBuffer) {
				if (debug) {
					// NOTE: run assertion without throwing an exception since it could be
					// reached from the
					// comprehensive Negative tests
					try {
						assertTrue(cssSelectorTokenString.matches(attributeValidator),
								String.format("the token \"%s\"to be a valid CssSelector token",
										cssSelectorTokenString));
					} catch (AssertionError e) {

					}
				}
				if (!cssSelectorTokenString.matches(attributeValidator)) {
					found = false;
				}
			}
		}
		return found;
	}
}
