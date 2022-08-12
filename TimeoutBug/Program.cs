
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TimeoutBug
{
    class Program
    {
        public static IWebDriver Browser;
        public static WebDriverWait Wait;

        public static TimeSpan Timeout = new TimeSpan(0, 1, 30);

        public static string TargetFiles = @"Resources\TargetFiles";
        public static string Drivers = @"Resources\Drivers";
        public static string CurrentPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        static async Task Main(string[] args)
        {
            #region WebDriver
            var url = "https://www.cjfashion.com/";
            var driversPath = Path.Combine(CurrentPath, Drivers);
            var _chromeService = ChromeDriverService.CreateDefaultService(driversPath);
            var _chromeOptions = new ChromeOptions();
            var options = new List<string>();

            _chromeService.LogPath = Path.Combine(CurrentPath, $"Driver_{DateTime.Now.ToString("yyyyMMddHHmmss")}.log");
            _chromeService.EnableVerboseLogging = true;

            options.Add("--disable-extensions");
            options.Add("start-maximized");
            options.Add("enable-automation");
            options.Add("--no-sandbox");
            options.Add("--disable-infobars");
            options.Add("--disable-dev-shm-usage");
            options.Add("--disable-browser-side-navigation");
            options.Add("--disable-gpu");

            _chromeOptions.AddArguments(options);
            _chromeOptions.AddUserProfilePreference("safebrowsing.enabled", "true");
            _chromeOptions.AddUserProfilePreference("download.default_directory", Path.Combine(CurrentPath, TargetFiles));
            _chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;

            Browser = new ChromeDriver(_chromeService, _chromeOptions, Timeout);

            Wait = new WebDriverWait(Browser, Timeout);
            Browser.Manage().Timeouts().PageLoad = Timeout;
            Browser.Manage().Timeouts().AsynchronousJavaScript = Timeout;
            Browser.Manage().Window.Maximize();

            Browser.Navigate().GoToUrl(url);
            #endregion


            // Accept Cookies
            var acceptCookiesLocators = new Dictionary<string, string>();
            acceptCookiesLocators.Add("Id", "onetrust-accept-btn-handler");
            acceptCookiesLocators.Add("CSS Selector", "");
            acceptCookiesLocators.Add("XPath", "");
            acceptCookiesLocators.Add("Name", "");
            acceptCookiesLocators.Add("Link Text", "");
            PerformClick(acceptCookiesLocators);

            // Wait for Page to Load
            var startingPageNormalLoadedLocators = new Dictionary<string, string>();
            startingPageNormalLoadedLocators.Add("Id", "");
            startingPageNormalLoadedLocators.Add("CSS Selector", ".top_header--search .RexSearchform__input");
            startingPageNormalLoadedLocators.Add("XPath", "\\/\\/input[@name='ft']");
            startingPageNormalLoadedLocators.Add("Name", "ft");
            startingPageNormalLoadedLocators.Add("Link Text", "");
            await WaitForCondition(startingPageNormalLoadedLocators, "2", "00:00:45.000");

            // Click on Login Option
            var loginButtonLocators = new Dictionary<string, string>();
            loginButtonLocators.Add("Id", "");
            loginButtonLocators.Add("CSS Selector", "#header .top_header--login_link");
            loginButtonLocators.Add("XPath", "//a[contains(text(),'Login')]");
            loginButtonLocators.Add("Name", "");
            loginButtonLocators.Add("Link Text", "");
            PerformClick(loginButtonLocators);


            // Wait for Login Options to Show
            var loginOptionsLocator = new Dictionary<string, string>();
            loginOptionsLocator.Add("Id", "loginWithUserAndPasswordBtn");
            loginOptionsLocator.Add("CSS Selector", "#vtexIdUI-auth-selector #loginWithUserAndPasswordBtn");
            loginOptionsLocator.Add("XPath", "//button[@id='loginWithUserAndPasswordBtn']");
            loginOptionsLocator.Add("Name", "ft");
            loginOptionsLocator.Add("Link Text", "");
            await WaitForCondition(loginOptionsLocator, "2", "00:00:45.000");

            // Click on Access using Email and Password
            var loginWithEmailButton = new Dictionary<string, string>();
            loginWithEmailButton.Add("Id", "loginWithUserAndPasswordBtn");
            loginWithEmailButton.Add("CSS Selector", "#vtexIdUI-auth-selector #loginWithUserAndPasswordBtn");
            loginWithEmailButton.Add("XPath", "//button[@id='loginWithUserAndPasswordBtn']");
            loginWithEmailButton.Add("Name", "");
            loginWithEmailButton.Add("Link Text", "");
            PerformClick(loginWithEmailButton);

            await Task.Delay(5500);

            // Enter Email
            var emailInputLocators = new Dictionary<string, string>();
            emailInputLocators.Add("Id", "inputEmail");
            emailInputLocators.Add("CSS Selector", "#vtexIdUI-form-classic-login #inputEmail");
            emailInputLocators.Add("XPath", "//input[@id='inputEmail']");
            emailInputLocators.Add("Name", "");
            emailInputLocators.Add("Link Text", "");
            Input(emailInputLocators, "shamel.ahmon@dissloo.com");

            // Enter Password
            var passwordInputLocators = new Dictionary<string, string>();
            passwordInputLocators.Add("Id", "inputPassword");
            passwordInputLocators.Add("CSS Selector", "#vtexIdUI-form-classic-login #inputPassword");
            passwordInputLocators.Add("XPath", "//input[@id='inputPassword']");
            passwordInputLocators.Add("Name", "");
            passwordInputLocators.Add("Link Text", "");
            Input(passwordInputLocators, "TimeoutBug1");


            // Click on Enter
            var enterButtonLocator = new Dictionary<string, string>();
            enterButtonLocator.Add("Id", "classicLoginBtn");
            enterButtonLocator.Add("CSS Selector", "div > #vtexIdUI-classic-login > #vtexIdUI-form-classic-login #classicLoginBtn");
            enterButtonLocator.Add("XPath", "//button[@id='classicLoginBtn']");
            enterButtonLocator.Add("Name", "");
            enterButtonLocator.Add("Link Text", "");
            PerformClick(enterButtonLocator);

            // Wait for Starting Page to Load as a Logged User
            var loggedPageLocators = new Dictionary<string, string>();
            loggedPageLocators.Add("Id", "");
            loggedPageLocators.Add("CSS Selector", "");
            loggedPageLocators.Add("XPath", "//a[contains(text(),'Olá!')]");
            loggedPageLocators.Add("Name", "");
            loggedPageLocators.Add("Link Text", "");
            await WaitForCondition(loggedPageLocators, "2", "00:00:45.000");

            while (true)
            {
                // Go to Starting Page
                var mainPageLink = new Dictionary<string, string>();
                mainPageLink.Add("Id", "");
                mainPageLink.Add("CSS Selector", "#header > .container img");
                mainPageLink.Add("XPath", "//header[@id='header']/div/div[2]/a/img");
                mainPageLink.Add("Name", "");
                mainPageLink.Add("Link Text", "");
                PerformClick(mainPageLink);

                await Task.Delay(5500);

                // Wait for Starting Page to Load as a Logged User
                await WaitForCondition(loggedPageLocators, "2", "00:00:45.000");

                await Task.Delay(5500);

                // Hover on Login
                var loginHover = new Dictionary<string, string>();
                loginHover.Add("Id", "");
                loginHover.Add("CSS Selector", "#header .top_header--login_link");
                loginHover.Add("XPath", "//header[@id='header']/div/div[3]/div[2]");
                loginHover.Add("Name", "");
                loginHover.Add("Link Text", "");
                PerformMouseHover(loginHover);

                await Task.Delay(5500);

                // Click on Meus Dados
                var meusDadosLocator = new Dictionary<string, string>();
                meusDadosLocator.Add("Id", "");
                meusDadosLocator.Add("CSS Selector", ".top_header--right_conteiner li:nth-child(3) > a");
                meusDadosLocator.Add("XPath", "//a[contains(text(),'Meus dados')]");
                meusDadosLocator.Add("Name", "");
                meusDadosLocator.Add("Link Text", "Vestidos");
                PerformClick(meusDadosLocator);

                await Task.Delay(5500);

                // Wait for Page to Load
                var myDataLoadedLocators = new Dictionary<string, string>();
                myDataLoadedLocators.Add("Id", "");
                myDataLoadedLocators.Add("CSS Selector", "");
                myDataLoadedLocators.Add("XPath", "(//div[contains(@class,'vtex-pageHeader__container pa5 pa7-ns')])[2]");
                myDataLoadedLocators.Add("Name", "");
                myDataLoadedLocators.Add("Link Text", "");
                await WaitForCondition(myDataLoadedLocators, "4", "00:00:45.000");

                await Task.Delay(5500);
            }
        }

        public static IWebElement FindElement(Dictionary<string, string> dicLocators)
        {
            IWebElement element = null;
            var index = 0;

            while (element == null && index < 5)
            {
                element = FindItem(dicLocators.ElementAt(index).Key, dicLocators.ElementAt(index).Value);
                index++;
            }

            return element;
        }

        public static IWebElement FindItem(string type, string locator)
        {
            try
            {
                if (string.IsNullOrEmpty(locator))
                    return null;

                Func<string, By> searchContext = null;

                switch (type)
                {
                    case "Id":
                        searchContext = By.Id; break;
                    case "CSS Selector":
                        searchContext = By.CssSelector; break;
                    case "XPath":
                        searchContext = By.XPath; break;
                    case "Name":
                        searchContext = By.Name; break;
                    case "Link Text":
                        searchContext = By.LinkText; break;
                    default:
                        return null;
                }

                Browser.SwitchTo().DefaultContent();

                return Browser.FindElement(searchContext(locator));
            }
            catch
            {
                return null;
            }
        }

        public static void PerformClick(Dictionary<string, string> dicLocators)
        {
            try
            {
                var element = FindElement(dicLocators);

                element.Click();
            }
            catch { }
        }

        public static void GoToUrl(string url)
        {
            try
            {
                Browser.Navigate().GoToUrl(url);
            }
            catch { }
        }

        public static void Input(Dictionary<string, string> dicLocators, string inputValue)
        {
            try
            {
                var element = FindElement(dicLocators);

                element.SendKeys(inputValue);
            }
            catch { }
        }

        public static void PerformMouseHover(Dictionary<string, string> dicLocators)
        {
            try
            {
                var element = FindElement(dicLocators);
                var actions = new OpenQA.Selenium.Interactions.Actions(Browser);

                actions.MoveToElement(element).Perform();
            }
            catch { }
        }

        public static async Task<bool> WaitForCondition(Dictionary<string, string> dicLocators, string condition, string timeout)
        {
            try
            {
                TimeSpan.TryParse(timeout, out var parsedTimeSpan);
                if (parsedTimeSpan.TotalMilliseconds > 0)
                    Wait.Timeout = parsedTimeSpan;
                else
                    Wait.Timeout = new TimeSpan(0, 1, 30);

                var taskTimeout = Task.Delay(parsedTimeSpan);

                IWebElement element = null;

                while (element == null && !taskTimeout.IsCompleted)
                { 
                    element = FindElement(dicLocators);

                    await Task.Delay(350);
                }

                bool result;
                switch (condition)
                {
                    case "0":
                    {
                        result = Wait.Until(Visible(element));
                    }
                    break;
                    case "2":
                    {
                        result = Wait.Until(Enabled(element));
                    }
                    break;
                    case "4":
                    {
                        result = Wait.Until(Visible(element));
                    }
                    break;
                    case "5":
                    {
                        result = Wait.Until(Invisible(element));
                    }
                    break;
                    default:
                    {
                        result = Wait.Until(Visible(element));
                    }
                    break;
                }

                return result;
            }
            catch { return false; }
        }

        public static Func<IWebDriver, bool> Visible(IWebElement el) => (d) => { try { return el.Displayed; } catch { return false; } };
        public static Func<IWebDriver, bool> Enabled(IWebElement el) => (d) => { try { return el.Enabled; } catch { return false; } };
        public static Func<IWebDriver, bool> Invisible(IWebElement el) => (d) => { try { return el == null || !el.Displayed; } catch { return false; } };
    }
}

