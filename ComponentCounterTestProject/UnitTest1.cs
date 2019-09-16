using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentCounter;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Collections.Generic;

namespace ComponentCounterTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateOracleConnections()
        {
            //Arrange
            DBHelper dbFiat = new DBHelper(Line.EPS_FIAT);

            //Act
            ConnectionState expectedResponse = ConnectionState.Open;
            ConnectionState response = dbFiat.Connect();

            //Assert
            Assert.AreEqual(expectedResponse, response);
        }

        [TestMethod]
        public void GetComponentUsageCountPerCycleTest()
        {
            //Arrange
            DBHelper dbFiat = new DBHelper(Line.EPS_FIAT);
            string itemPartNo = "A0038111";
            string partNo = "00519622680";

            //Act
            //var expectedResponse = 1;
            var componentUsagePerCycle = dbFiat.GetComponentUsageCountPerCycle(itemPartNo, partNo);
            
            //Assert
            //Assert.AreEqual(expectedResponse, componentUsagePerCycle);
            Assert.IsTrue(componentUsagePerCycle > 0 && componentUsagePerCycle < 10);
        }

        [TestMethod]
        public void GetDrawerCountDataTest()
        {
            //Arrange
            DBHelper dbFiat = new DBHelper(Line.EPS_FIAT);
            DateTime dateTimeFrom = DateTime.Parse("29-08-2019 06:00:00");
            DateTime dateTimeTo = DateTime.Parse("29-08-2019 07:00:00");

            //Act
            DataSet dataSetResult = dbFiat.GetDrawerCountData(dateTimeFrom, dateTimeTo);

            //Assert
            Assert.IsNotNull(dataSetResult);
        }

        [TestMethod]
        public void GetProcDataCfgDrawerRecIdTest()
        {
            //Arrange
            DBHelper dbFiat = new DBHelper(Line.EPS_FIAT);

            //Act
            List<int> drawerRecId = dbFiat.GetProcDataCfgDrawerRecIds();

            //Assert
            //Assert.AreEqual(260, drawerRecId);
            Assert.IsNotNull(drawerRecId);
        }
    }
}
