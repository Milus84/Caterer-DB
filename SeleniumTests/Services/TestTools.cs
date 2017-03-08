﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;


namespace SeleniumTests.Services
{
    public static class TestTools
    {

        public static bool Fehlermeldung_Sichtbarkeitsprüfung(string id, IWebDriver driver, Int16 zeit=0)
        {

            bool present = false;

            try
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(zeit));
                driver.FindElement(By.Id(id));
                present = true;
            }
            catch (NoSuchElementException)
            {
                present = false;
            }
            return present;

        }

        public static string Label_Text_Zurückgeben(string id, IWebDriver driver, Int16 zeit=5)
        {

            string Text = "";
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(zeit));
                driver.FindElement(By.Id(id));
                Text = driver.FindElement(By.Id(id)).Text;

            }
            return Text;

        }

        public static String Textbox_Text_Zurückgeben(string id, IWebDriver driver, Int16 zeit=5)
        {

            String Text = "";
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(zeit));
                driver.FindElement(By.Id(id));
                Text = driver.FindElement(By.Id(id)).GetAttribute("value");

            }
            return Text;

        }

        public static void TestStart_Angemeldete_User_Ausloggen(IWebDriver driver)
        {

            if (Fehlermeldung_Sichtbarkeitsprüfung("DropdownLogin", driver))
            {
                Element_Klicken("DropdownLogin", driver);
                Element_Klicken("Ausloggen", driver);
            }
            else
            {

            }

        }

        public static void Element_Klicken(string id, IWebDriver driver, Int16 zeit=5)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(zeit));
            driver.FindElement(By.Id(id));
            driver.FindElement(By.Id(id)).Click();
        }

        public static void User_Login_Durchführen(String email, String pw, IWebDriver driver, Int16 zeit=5)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(zeit));
            Element_Klicken("DropdownLogout", driver);
            Element_Klicken("loginLinkhead", driver);
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys(email);
            driver.FindElement(By.Id("Passwort")).Clear();
            driver.FindElement(By.Id("Passwort")).SendKeys(pw);
            driver.FindElement(By.XPath("//input[@value='Anmelden']")).Click();

        }

        public static void Daten_In_Textbox_Eingeben(string daten, string id, IWebDriver driver, int zeit=5)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(zeit));
            driver.FindElement(By.Id(id)).Clear();
            driver.FindElement(By.Id(id)).SendKeys(daten);

        }

        public static void Selenium_Wartet_Eine_Sekunde(IWebDriver driver)
        {

            Fehlermeldung_Sichtbarkeitsprüfung("xxx", driver, 1);

        }

        public static void Nutzer_Ausloggen(IWebDriver driver)
        {

            Element_Klicken("DropdownLogin", driver);
            Element_Klicken("Ausloggen", driver);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            driver.FindElement(By.Id("loginLinkbutton"));
            Assert.AreEqual(Hinweise.Startseite, driver.Title);

        }

        public static void TestEnde_Angemeldete_User_Ausloggen_Oder_Startseite_Aufrufen(IWebDriver driver)
        {
            
            if (Fehlermeldung_Sichtbarkeitsprüfung("DropdownLogin", driver))
            {
                Element_Klicken("DropdownLogin", driver);
                Element_Klicken("Ausloggen", driver);
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                driver.FindElement(By.Id("loginLinkbutton"));
                Assert.AreEqual(Hinweise.Startseite, driver.Title);
            }
            else
            {

                Element_Klicken("StartButton", driver);
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                driver.FindElement(By.Id("loginLinkbutton"));
                Assert.AreEqual(Hinweise.Startseite, driver.Title);

            }

        }

        //------------------------------------------------------------------------------------------------------------
        //Navigiert zur Kontaktseite über die Fußzeile.
        public static void KontaktFußzeile(IWebDriver driver)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Element_Klicken("Kontakt", driver);
            Assert.AreEqual(Hinweise.Kontaktseite,Label_Text_Zurückgeben("Ansprechp", driver));

        }

        //Navigiert zur Kontaktseite über die Logoutdropdown.
        public static void KontaktDropdownLogout(IWebDriver driver)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Element_Klicken("DropdownServiceLogout", driver);
            Element_Klicken("DropdownKontaktLogout", driver);
            Assert.AreEqual(Hinweise.Kontaktseite, Label_Text_Zurückgeben("Ansprechp", driver));

        }

        //Navigiert zur Kontaktseite über die Logindropdown.
        public static void KontaktDropdownLogin(IWebDriver driver)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Element_Klicken("DropdownServiceLogin", driver);
            Element_Klicken("DropdownKontaktLogin", driver);
            Assert.AreEqual(Hinweise.Kontaktseite, Label_Text_Zurückgeben("Ansprechp", driver));

        }

        //------------------------------------------------------------------------------------------------------------
        //Navigiert zur Datenschutzseite über die Fußzeile.
        public static void DatenschutzFußzeile(IWebDriver driver)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Element_Klicken("Datenschutz", driver);
            Assert.AreEqual(Hinweise.Datenschutzseite, Label_Text_Zurückgeben("Datenschutzbest", driver));

        }

        //Navigiert zur Datenschutzseite über die Logoutdropdown.
        public static void DatenschutzDropdownLogout(IWebDriver driver)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Element_Klicken("DropdownServiceLogout", driver);
            Element_Klicken("DropdownDatenschutzLogout", driver);
            Assert.AreEqual(Hinweise.Datenschutzseite, Label_Text_Zurückgeben("Datenschutzbest", driver));

        }

        //Navigiert zur Datenschutzseite über die Logindropdown.
        public static void DatenschutzDropdownLogin(IWebDriver driver)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Element_Klicken("DropdownServiceLogin", driver);
            Element_Klicken("DropdownDatenschutzLogin", driver);
            Assert.AreEqual(Hinweise.Datenschutzseite, Label_Text_Zurückgeben("Datenschutzbest", driver));

        }

        //------------------------------------------------------------------------------------------------------------
        //Navigiert zur AGB-Seite über die Fußzeile.
        public static void AGBFußzeile(IWebDriver driver)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Element_Klicken("AGB", driver);
            Assert.AreEqual(Hinweise.AGBseite, Label_Text_Zurückgeben("AllgemGeschäftsbedingungen", driver));

        }

        //Navigiert zur AGB-Seite über die Logoutdropdown.
        public static void AGBDropdownLogout(IWebDriver driver)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Element_Klicken("DropdownServiceLogout", driver);
            Element_Klicken("DropdownAGBLogout", driver);
            Assert.AreEqual(Hinweise.AGBseite, Label_Text_Zurückgeben("AllgemGeschäftsbedingungen", driver));

        }

        //Navigiert zur AGB-Seite über die Logindropdown.
        public static void AGBDropdownLogin(IWebDriver driver)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Element_Klicken("DropdownServiceLogin", driver);
            Element_Klicken("DropdownAGBLogin", driver);
            Assert.AreEqual(Hinweise.AGBseite, Label_Text_Zurückgeben("AllgemGeschäftsbedingungen", driver));

        }

        internal static void TestEnde(IWebDriver driver)
        {
            throw new NotImplementedException();
        }

        //------------------------------------------------------------------------------------------------------------
        //Navigiert zur Impressum-Seite über die Fußzeile.
        public static void ImpressumFußzeile(IWebDriver driver)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Element_Klicken("Impressum", driver);
            Assert.AreEqual(Hinweise.Impressumseite, Label_Text_Zurückgeben("Impr", driver));

        }

        //Navigiert zur Impressum-Seite über die Logoutdropdown.
        public static void ImpressumDropdownLogout(IWebDriver driver)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Element_Klicken("DropdownServiceLogout", driver);
            Element_Klicken("DropdownImpressumLogout", driver);
            Assert.AreEqual(Hinweise.Impressumseite, Label_Text_Zurückgeben("Impr", driver));

        }

        //Navigiert zur Impressum-Seite über die Logindropdown.
        public static void ImpressumDropdownLogin(IWebDriver driver)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Element_Klicken("DropdownServiceLogin", driver);
            Element_Klicken("DropdownImpressumLogin", driver);
            Assert.AreEqual(Hinweise.Impressumseite, Label_Text_Zurückgeben("Impr", driver));

        }

    }
}

