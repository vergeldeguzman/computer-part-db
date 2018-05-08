using ComputerParstDb.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerPartsDBTTest
{
    [TestClass]
    public class PartViewModelTest
    {
        [TestMethod]
        public void Indexer_AllFilledUp_NoErrorMessage()
        {
            PartViewModel vm = new PartViewModel
            {
                Description = "some description",
                PartType = "some part",
                Condition = "some condition"
            };
            string error = vm["Type"];
            Assert.AreEqual("", error);
            error = vm["Part"];
            Assert.AreEqual("", error);
            error = vm["Condition"];
            Assert.AreEqual("", error);
        }

        [TestMethod]
        public void Indexer_EmptyDescription_ErrorMessage()
        {
            PartViewModel vm = new PartViewModel
            {
                Description = "",
                PartType = "some part",
                Condition = "some condition"
            };
            string error = vm["Description"];
            Assert.AreEqual("Description is a mandatory field", error);
        }

        [TestMethod]
        public void Indexer_EmptyCondition_ErrorMessage()
        {
            PartViewModel vm = new PartViewModel
            {
                Description = "some description",
                PartType = "some part",
                Condition = ""
            };
            string error = vm["Condition"];
            Assert.AreEqual("Condition is a mandatory field", error);
        }

        [TestMethod]
        public void Indexer_EmptyType_ErrorMessage()
        {
            PartViewModel vm = new PartViewModel
            {
                Description = "some description",
                PartType = "",
                Condition = "some condition"
            };
            string error = vm["PartType"];
            Assert.AreEqual("Type is a mandatory field", error);
        }

        [TestMethod]
        public void PropertyChanged_Description()
        {
            var result = false;

            PartViewModel vm = new PartViewModel();
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

            PartViewModel vm = new PartViewModel();
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

            PartViewModel vm = new PartViewModel();
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

        [TestMethod]
        public void PropertyChanged_Location()
        {
            var result = false;

            PartViewModel vm = new PartViewModel();
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Location")
                {
                    result = true;
                }
            };

            vm.Location = "some text";
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PropertyChanged_Remarks()
        {
            var result = false;

            PartViewModel vm = new PartViewModel();
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Remarks")
                {
                    result = true;
                }
            };

            vm.Remarks = "some text";
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PropertyChanged_Price()
        {
            var result = false;

            PartViewModel vm = new PartViewModel();
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Price")
                {
                    result = true;
                }
            };

            vm.Price = 0.0m;
            Assert.IsTrue(result);
        }
    }
}
