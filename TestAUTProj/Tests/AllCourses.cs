using NUnit.Framework;
using System.Collections;
using System.Configuration;
using TestAUTProj.Base;
using TestAUTProj.Pages;
using TestAUTProj.Utilities;

namespace TestAUTProj.Tests
{
    [TestFixture]
    public class AllCourses:BaseTest
        {
        
        //Data driven testing approach---------------------------------------------------
        /// <summary>
        /// Data Provider
        /// </summary>
        /// <returns></returns>
        //public static ArrayList getCourseSearchData()
        //{
        //    Object[,] testData = ExcelUtil.getTestData("verify_search_course");
        //    ArrayList arrayList = new ArrayList();
        //    for (int i = 0; i < testData.Length; i++)
        //    {
        //        for (int j = 0; j <= testData.Length-1; j++)
        //        {
        //            arrayList.Add(testData[i, j]);
        //        }
        //    }
        //    return arrayList;
        //}
        //public static IEnumerable<TestCaseData> addTestDataConfig()
        //{
        //    string filePath = Utils.getProjectFolderPath("TestData") + "TestData.xlsx";
        //    ExcelUtil.setExcelFile(filePath, "TestData");

        //    foreach (var item in getCourseSearchData())
        //    {
        //        yield return new TestCaseData(item.ToString());
        //    }
        //}


        //[Test, TestCaseSource("addTestDataConfig")]
        [Test]
        public void TC58466_verifySearchCourse()
        {

            nav = loginPage.signIn(Constants.DEFAULT_USERNAME, Constants.DEFAULT_PASSWORD)
                .allCourses();
            searchBarPage = new SearchBarPage(driver);
            resultPage= searchBarPage.course("rest api");
            bool searchResult = resultPage.verifySearchResult();
            Assert.That(searchResult, Is.True);
            CheckPoint.markFinal(Utils.getCurrentTestCaseName(), searchResult, "search Result verification");
        }
        
        [Test]
        public void filterByCategory()
        {
            nav = loginPage.signIn(Constants.DEFAULT_USERNAME, Constants.DEFAULT_PASSWORD)
                .allCourses();
            categoryFilterPage = new CategoryFilterPage(driver);
            resultPage = categoryFilterPage.select("Sotfware Testing");
            bool searchResult = resultPage.verifySearchResult();
            CheckPoint.markFinal(Utils.getCurrentTestCaseName(), searchResult, "search Result verification");
        }
    }
}
