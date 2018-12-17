package com.github.sergueik.selenium;

import static org.testng.Assert.assertTrue;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.testng.annotations.Test;

/**
 * XPAth NBF based Lexer-style validator for NSelene WebDriver wrapper .net project
 * https://www.w3.org/2002/11/xquery-xpath-applets/xpath-jjdoc.html
 * @author: Serguei Kouzmine (kouzmine_serguei@yahoo.com)
 */

// following on (only tokenzation part is finished):
// https://github.com/sam-rosenthal/java-cssSelector-to-xpath/blob/master/src/main/java/org/sam/rosenthal/cssselectortoxpath/utilities/CssSelectorStringSplitter.java
// https://github.com/sam-rosenthal/java-cssSelector-to-xpath/blob/master/src/main/java/org/sam/rosenthal/cssselectortoxpath/utilities/CssElementAttributeParser.java

public class XPathValidator {

	// singleton style
	private static XPathValidator instance = new XPathValidator();

	private XPathValidator() {
	}

	public static XPathValidator getInstance() {
		return instance;
	}

	@SuppressWarnings("unused")
	private boolean debug = false;

	public void setDebug(boolean value) {
		this.debug = value;
	}

// @formatter:off
	private static final String tokenValidator = "^\\s*(/?/?\\s*[^ /\\[]+(?:\\[[^\\]]+\\])*)($|\\s*//?\\s*[^ /\\[]+.*$)";
// @formatter:on
	private static final String attributeValidator = null;

	// control logging
	private boolean reportedTokenValidator = false;
	private boolean reportedAttributeValidator = false;

	public String getTokenValidator() {
		if (debug) {
			if (!reportedTokenValidator) {
				System.err.println("XPath Token validator: " + tokenValidator);
				reportedTokenValidator = true;
			}
		}
		return tokenValidator;
	}

	public String getAttributeValidator() {
		if (debug) {
			if (!reportedAttributeValidator) {
				System.err.println("XPath Attribute validator: " + attributeValidator);
				reportedAttributeValidator = true;
			}
		}
		return attributeValidator;
	}

	public boolean comprehensiveTokenTest(String xpathString) {

		Pattern pattern = Pattern.compile(tokenValidator);
		Matcher match = pattern.matcher(xpathString);

		boolean foundToken = true;
		boolean foundRemainder = true;
		boolean found = false;
		List<String> tokenBuffer = new ArrayList<>();
		List<String> tailBuffer = new ArrayList<>();
		int tokenCnt = 0;
		int maxTokenCnt = 100; // paranoid
		while (match.find() && foundToken && foundRemainder
				&& tokenCnt < maxTokenCnt) {

			if (match.group(1) == null || match.group(1) == "") {
				foundToken = false;
			}
			if (match.group(2) == null || match.group(2) == "") {
				foundRemainder = false;
			}
			if (foundToken) {
				tokenBuffer.add(match.group(1));
				if (debug) {
					System.err.println(String.format("Extracted token: \"%s\"",
							tokenBuffer.get(tokenCnt)));
				}
			}
			if (foundRemainder) {
				String remainder = match.group(2);
				tailBuffer.add(remainder);
				if (debug) {
					System.err.println(
							String.format("Remaining of the xpath: \"%s\"", remainder));
				}
				if (remainder.length() == 0) {
					if (debug) {
						System.err.println("Reached the end of the xpath string.");
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
		// Condition Extractor not implemented in XPathValidator yet
		return found;
	}

}
