using System;
using ComputerParstDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerPartsDBTTest
{
    [TestClass]
    public class ComputerPartTest
    {
        [TestMethod]
        public void PropertyChanged_Description()
        {
            var result = false;

            ComputerPart vm = new ComputerPart();
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Description")
                {
                    result = true;
                }
            };

            vm.Description = "some text";
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PropertyChanged_Condition()
        {
            var result = false;

            ComputerPart vm = new ComputerPart();
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Condition")
                {
                    result = true;
                }
            };

            vm.Condition = "some text";
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PropertyChanged_Type()
        {
            var result = false;

            ComputerPart vm = new ComputerPart();
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "PartType")
                {
                    result = true;
                }
            };

            vm.PartType = "some text";
            Assert.IsTrue(result);
        }
    }
}
