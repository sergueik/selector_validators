using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SeleniumLocatorValidation.Extensions;

namespace SeleniumLocatorValidation.Test {

	[TestFixture]
	public class ValidatorsTest	{

		private StringBuilder verificationErrors = new StringBuilder();
		/*
		private IWebDriver driver;
		private bool headless = false;
		private WebDriverWait wait;
		private const int wait_seconds = 3;
		private const int window_width = 900;
		private const int window_heght = 800;
		private Actions actions;
		*/
		[OneTimeSetUp]
		public void SetUp()
		{
			/*
			driver = new ChromeDriver(System.IO.Directory.GetCurrentDirectory());
			if (headless) { 
				var option = new ChromeOptions();
				option.AddArgument("--headless");
				driver = new ChromeDriver(option);
			} else {
				driver = new ChromeDriver();
			}

			driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(60);
			// driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(60));
			driver.Manage().Window.Size = new System.Drawing.Size(700, 400);
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(wait_seconds));
			actions = new Actions(driver);
			*/
		}

		[OneTimeTearDown]
		public void TearDown()
		{/*
			try {
				driver.Quit();
			} catch (Exception) {
			} // Ignore cleanup errors
			*/
			Assert.AreEqual("", verificationErrors.ToString());
		}

		// http://gigi.nullneuron.net/gigilabs/data-driven-tests-with-nunit/
		[TestCase("a[@class='main']/b//c[@class='main']")]
		[TestCase("/body//td/following-sibling::td[1]")]
		[TestCase("/tr[0]/../th")]
		// [TestCase("")]
		public void ShouldDetectInvalidCssSelector(String selector){
			Assert.IsFalse(selector.SimpleCssSelectorTestExtensionMethod());
		}
		
		[TestCase("a[@class='main']/b//c[@class='main']")]
		[TestCase("/body//td/following-sibling::td[1]")]
		[TestCase("/tr[0]/../th")]
		// [TestCase("")]
		public void DetectValidXpath(String selector){
			Assert.IsTrue(selector.SimpleXPathTestExtensionMethod());
		}
		
		[TestCase("a.class > b#id  c:nth-of-type(1)")]
		[TestCase("div.class ~ input#id")]
		[TestCase("body > h1[name='hello'] h2:nth-of-type(1) div")]
		[TestCase("form#formid[name$='form'] input.class[name^='Pass']")]
		public void ShouldDetectInvalidXpath(String selector){
			Assert.IsFalse(selector.SimpleXPathTestExtensionMethod());
		}

		[TestCase("a.class > b#id  c:nth-of-type(1)")]
		[TestCase("div.class ~ input#id")]
		[TestCase("body > h1[name='hello'] h2:nth-of-type(1) div")]
		[TestCase("form#formid[name$='form'] input.class[name^='Pass']")]
		public void ShouldDetectValidCssSelector(String selector){
			Assert.IsTrue(selector.SimpleCssSelectorTestExtensionMethod());
		}
	}
}
