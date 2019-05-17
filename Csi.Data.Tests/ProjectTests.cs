using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace Csi.Data.Tests
{
    /*  Test constructors for Project entity.

        Null_Project_Name and Null_Project_Name_Alt are different ways of writing the same test.
        Null_Project_Name highlights some of the benefits of writing it this way.
        i.e. It all depends on what you want.
     */

    [TestClass, TestCategory("Data"), TestCategory("Project")]
    public class ProjectTests
    {

        [TestMethod]
        [DataTestMethod]
        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null_Project_Name_Alt(string projectName)
        {
            // Arrange

            // Act
            new Project(projectName);

            // Assert - Expects exception
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(null)]
        public void Null_Project_Name(string projectName)
        {
            // Arrange
            Project project = null;

            // Act
            var ex = Assert.ThrowsException<ArgumentNullException>(() =>
                project = new Project(projectName)
            );

            // Assert
            Assert.AreEqual("name", ex.ParamName);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("")]
        [DataRow("A")]
        [DataRow("AB")]
        [DataRow("测试")]
        public void Short_Project_Name(string projectName)
        {
            // Arrange
            Project project = null;

            // Act
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                project = new Project(projectName)
            );

            // Assert
            Assert.AreEqual("name", ex.ParamName);
        }
        
        [TestMethod]
        [DataTestMethod]
        [DataRow("TestProject")]
        [DataRow("ABC")]
        [DataRow("，。/")] // What?!
        [DataRow("亚马逊")]
        [DataRow("测试方案")]
        public void Valid_ProjectName(string projectName)
        {
            // Arrange
            // Act
            Project project = new Project(projectName);

            // Assert
            Assert.IsNotNull(project);
            Assert.IsNotNull(project.Id);
            Assert.AreEqual(projectName, project.Name);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("TestProject", 2018, 12, 31)]
        public void Valid_Project_Name_And_DateTime_Args(string projectName, int year, int month, int day)
        {
            // Arrange
            DateTime projectRegisteredDate = new DateTime(year, month, day);

            // Act
            Project project = new Project(projectName, projectRegisteredDate);

            // Assert
            Assert.IsNotNull(project);
            Assert.IsNotNull(project.Id);
            Assert.AreEqual(projectName, project.Name);
        }

        [TestMethod]
        public void Valid_Empty_Project_Constructor()
        {
            // Arrange
            // Act
            Project project = new Project();

            // Assert
            Assert.IsNotNull(project);
            Assert.IsNotNull(project.Id);
        }

    }
}