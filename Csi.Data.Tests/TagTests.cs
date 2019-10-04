using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace Csi.Data.Tests
{
    /*  Test constructors for Tag entity.

        Null_Project_Name and Null_Project_Name_Alt are different ways of writing the same test.
        Null_Project_Name highlights some of the benefits of writing it this way.
        i.e. It all depends on what you want.
     */

    [TestClass, TestCategory("Data"), TestCategory("Tag")]
    public class TagTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Empty_Tag()
        {
            // Arrange
            // N/A

            // Act
            Tag rec = new Tag();

            // Assert - Expects rec is not null
            Assert.IsNotNull(rec);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(null)]
        public void Null_Tag_Text(string text)
        {
            // Arrange
            Tag tag = null;

            // Act
            var ex = Assert.ThrowsException<ArgumentNullException>(() =>
                tag = new Tag(text)
            );

            // Assert
            Assert.AreEqual("text", ex.ParamName);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("")]
        [DataRow("A")]
        [DataRow("AB")]
        [DataRow("测试")]
        public void Short_Tag_Text(string text)
        {
            // Arrange
            Tag tag = null;

            // Act
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                tag = new Tag(text)
            );

            // Assert
            Assert.AreEqual("text", ex.ParamName);
        }
        
        [TestMethod]
        [DataTestMethod]
        [DataRow("TestProject")]
        [DataRow("ABC")]
        [DataRow("，。/")] // What?!
        [DataRow("亚马逊")]
        [DataRow("测试方案")]
        public void Valid_Tag_Text(string text)
        {
            // Arrange
            // Act
            Tag tag = new Tag(text);

            // Assert
            Assert.IsNotNull(tag);
            Assert.IsNotNull(tag.Id);
            Assert.AreEqual(text, tag.Text);
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