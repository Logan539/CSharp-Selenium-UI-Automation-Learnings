namespace Course1
{
    public class Tests
    {
        public Class1 class1 = new Class1();
        [SetUp]
        public void Setup()
        {
            class1.StartBrowser();
        }

        [Test]
        public void PlayingaroundLogin()
        {
            class1.testing();
        }

        [Test] 
        public void PlayingaroundDropDown()
        {
            class1.dropdown();
        }

        [Test]
        public void playingaroudnRadioButton()
        {
            class1.radiobutton();
        }

        [Test]
        public void E2E()
        {
            class1.E2Etest();
        }

        [TearDown]
        public void CloseBrowser()
        {
           class1.StopBrowser();
        }
    }
}