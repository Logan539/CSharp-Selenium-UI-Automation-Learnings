using OpenQA.Selenium;
using static System.Net.Mime.MediaTypeNames;

namespace Course1
{
    public class Class1
    {
        IWebDriver driver;

        //Iwebdriver is a method containing geturl, click methods.

        //IWebDriver driver = new EdgeDriver();
        string[] prd_name = {"iphone X", "Samsung Note 8","Blackberry" };

        public void StartBrowser()
        {
            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            driver = new ChromeDriver();
            //Implicit wait can be defined globally so that we don't have to define it at each step with threa.sleep method.
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://www.rahulshettyacademy.com/loginpagePractise/";
        }

        public void testing()
        {
            //you can use TestContext.Progress.WriteLine(driver.Title); for storing output on Nunit tests log.
            Console.WriteLine(driver.Title);
            Console.WriteLine(driver.Url);
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy1");
            driver.FindElement(By.Id("password")).SendKeys("learning");
            driver.FindElement(By.XPath("//input[@id='terms']//parent::span")).Click();
            //css selector : syntac = tagname[attribute='value']
            //driver.FindElement(By.CssSelector("input"[value='Sign In']"))
            Verify_url();
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();
            //explicit wait is used to target single component for wait
            WebDriverWait wd = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wd.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(driver.FindElement(By.XPath("//input[@id='signInBtn']")), "Sign In"));

            if (driver.Url == "https://www.rahulshettyacademy.com/loginpagePractise/")
            {
                string errmessage = driver.FindElement(By.ClassName("alert-danger")).Text;
                Console.WriteLine(errmessage);
            }
            else
            {
                Console.WriteLine("Login Sucessful!!");
            }
            Console.WriteLine(driver.Url);
        }

        public void Verify_url()
        {
            //how to locate link using linktext
            string exp_link = "https://rahulshettyacademy.com/documents-request";
            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            string hr_link = link.GetAttribute("href");
            Assert.AreEqual(exp_link, hr_link, "Result not found");
        }

        //how to select dropdown values
        public void dropdown()
        {
            IWebElement drpdw = driver.FindElement(By.XPath("//select[@class='form-control']"));
            SelectElement s = new SelectElement(drpdw);
            s.SelectByValue("consult");
            Thread.Sleep(3000);
            s.SelectByIndex(1);
            Thread.Sleep(3000);
        }

        //how to select a radio button
        public void radiobutton()
        {
            //when indexing is there
            //selecting dynamically without using indexing
            IList<IWebElement> rdo = driver.FindElements(By.XPath("//input[@type='radio']"));
            foreach (IWebElement element in rdo)
            {
                if (element.GetAttribute("value").Equals("user"))
                {
                    element.Click();
                }
            }
            WebDriverWait wd = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wd.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("okayBtn")));
            driver.FindElement(By.Id("okayBtn")).Click();
            Thread.Sleep(1000);
            IWebElement usertag = driver.FindElement(By.XPath("//input[@value='user']"));
            string exp_tag = "user";
            string act_tag = usertag.GetAttribute("value");
            Assert.AreEqual(exp_tag, act_tag, "Result not found");

        }

        public void E2Etest()
        {
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Id("password")).SendKeys("learning");
            driver.FindElement(By.XPath("//input[@id='terms']//parent::span")).Click();
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();
            //explicit wait till checkout button is visible on site.
            //using Partiallinktext as the text might get changed over the period
            WebDriverWait wd = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wd.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

            //Two ways to get title for all the app-cards present on site
            //1. By using TagName
            IList<IWebElement> get_prdName = driver.FindElements(By.TagName("app-card"));
            foreach(IWebElement element in get_prdName)
            {
                if (prd_name.Contains(element.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    element.FindElement(By.CssSelector(".card-footer button")).Click();
                }
                else
                {
                    Console.WriteLine("Product "+ element.FindElement(By.CssSelector(".card-title a")).Text+ " not found");
                }
            }
            driver.FindElement(By.PartialLinkText("Checkout")).Click();
            Thread.Sleep(3000);
        }

            //2. By using CSSselector
            /*IList<IWebElement> get_prdName = driver.FindElements(By.CssSelector(".card-title a"));
            foreach (IWebElement element in get_prdName)
            {
                Console.WriteLine(element.Text)
            }*/

        public void StopBrowser()
        {
            driver.Quit();
        }
    }
}