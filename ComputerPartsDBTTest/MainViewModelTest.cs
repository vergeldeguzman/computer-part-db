using ComputerParstDb;
using ComputerParstDb.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.DTO;
using Services.Interfaces;
using System.Collections.Generic;

namespace ComputerPartsDBTTest
{
    [TestClass]
    public class MainViewModelTest
    {
        [TestMethod]
        public void AddPart_WithinConditionAndTypeFilters_AddedOnBoundedPartsAndComputerPartsService()
        {
            var mockComputerPartsService = new Mock<IComputerPartsService>();

            const int id = 12345;
            
            mockComputerPartsService
                .Setup(x => x.GetComputerParts(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<ComputerPartDTO>());

            mockComputerPartsService
                .Setup(x => x.Append(It.IsAny<ComputerPartDetailDTO>()))
                .Returns(() => id);

            MainViewModel vm = new MainViewModel(mockComputerPartsService.Object);

            // first part
            vm.Condition = "Good";
            vm.PartType = "Motherboard";

            ComputerPartDetail partDetail = new ComputerPartDetail
            {
                Id = id,
                Condition = "Good",  // Set to Good condition to be within condition filter
                Description = "some description",
                PartType = "Motherboard",
                Price = 1.2M,
                Location = "some location",
                Remarks = "some text"
            };
            vm.AddPart(partDetail);

            mockComputerPartsService.Verify(x => x.Append(It.IsAny<ComputerPartDetailDTO>()), Times.Once());

            Assert.AreEqual(1, vm.BoundedParts.Count);
            Assert.AreEqual(12345, vm.BoundedParts[0].Id);

            // second part
            vm.Condition = "All";
            vm.PartType = "All";

            vm.AddPart(partDetail);

            mockComputerPartsService.Verify(x => x.Append(It.IsAny<ComputerPartDetailDTO>()), Times.Exactly(2));

            Assert.AreEqual(2, vm.BoundedParts.Count);
            Assert.AreEqual(12345, vm.BoundedParts[0].Id);
            Assert.AreEqual(12345, vm.BoundedParts[1].Id);
        }

        [TestMethod]
        public void AddPart_NotWithinConditionFilter_AddedOnComputerPartsService()
        {
            var mockComputerPartsService = new Mock<IComputerPartsService>();

            const int id = 12345;

            mockComputerPartsService
                .Setup(x => x.GetComputerParts(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<ComputerPartDTO>());

            mockComputerPartsService
                .Setup(x => x.Append(It.IsAny<ComputerPartDetailDTO>()))
                .Returns(id);

            MainViewModel vm = new MainViewModel(mockComputerPartsService.Object);
            vm.Condition = "Good";
            vm.PartType = "Motherboard";

            ComputerPartDetail partDetail = new ComputerPartDetail
            {
                Id = id,
                Condition = "Bad", // Set to Bad condition so that it will be filter out
                Description = "some description",
                PartType = "Motherboard",
                Price = 1.2M,
                Location = "some location",
                Remarks = "some text"
            };
            vm.AddPart(partDetail);

            mockComputerPartsService.Verify(x => x.Append(It.IsAny<ComputerPartDetailDTO>()), Times.Once());

            Assert.AreEqual(0, vm.BoundedParts.Count);
        }

        [TestMethod]
        public void AddPart_NotWithinTypeFilter_AddedOnComputerPartsService()
        {
            var mockComputerPartsService = new Mock<IComputerPartsService>();

            const int id = 12345;

            mockComputerPartsService
                .Setup(x => x.GetComputerParts(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<ComputerPartDTO>());

            mockComputerPartsService
                .Setup(x => x.Append(It.IsAny<ComputerPartDetailDTO>()))
                .Returns(id);

            MainViewModel vm = new MainViewModel(mockComputerPartsService.Object);
            vm.Condition = "Bad";
            vm.PartType = "Keyboard";

            ComputerPartDetail partDetail = new ComputerPartDetail
            {
                Id = id,
                Condition = "Bad", 
                Description = "some description",
                PartType = "Motherboard", // Set to Motherboard type so that it is will be fitler out
                Price = 1.2M,
                Location = "some location",
                Remarks = "some text"
            };
            vm.AddPart(partDetail);

            mockComputerPartsService.Verify(x => x.Append(It.IsAny<ComputerPartDetailDTO>()), Times.Once());

            Assert.AreEqual(0, vm.BoundedParts.Count);
        }

        [TestMethod]
        public void EditPart_WithinConditionAndTypeFilters_UpdatedOnBoundedPartsAndComputerPartsService()
        {
            var mockComputerPartsService = new Mock<IComputerPartsService>();

            const int id = 12345;

            mockComputerPartsService
                .Setup(x => x.GetComputerParts(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<ComputerPartDTO>());

            mockComputerPartsService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<ComputerPartDetailDTO>()));

            MainViewModel vm = new MainViewModel(mockComputerPartsService.Object);
            vm.Condition = "Good";
            vm.PartType = "Motherboard";

            ComputerPart part = new ComputerPart
            {
                Id = id,
                Condition = "Good",
                Description = "some description",
                PartType = "Motherboard"
            };
            vm.BoundedParts.Add(part);

            ComputerPartDetail partDetail = new ComputerPartDetail
            {
                Id = part.Id,
                Condition = part.Condition,
                Description = part.Description,
                PartType = part.PartType,
                Price = 1.2M,
                Location = "some location",
                Remarks = "some text"
            };
            vm.EditPart(part, partDetail);

            mockComputerPartsService.Verify(
                x => x.Update(It.Is<int>(n => n == 12345), It.IsAny<ComputerPartDetailDTO>()),
                Times.Once());

            Assert.AreEqual(1, vm.BoundedParts.Count);

            vm.Condition = "All";
            vm.PartType = "All";

            vm.EditPart(part, partDetail);

            mockComputerPartsService.Verify(
                x => x.Update(It.Is<int>(n => n == 12345), It.IsAny<ComputerPartDetailDTO>()),
                Times.Exactly(2));

            Assert.AreEqual(1, vm.BoundedParts.Count);
        }

        [TestMethod]
        public void EditPart_NotWithinConditionFilter_UpdatedOnComputerPartsService()
        {
            var mockComputerPartsService = new Mock<IComputerPartsService>();

            const int id = 12345;

            mockComputerPartsService
                .Setup(x => x.GetComputerParts(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<ComputerPartDTO>());

            mockComputerPartsService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<ComputerPartDetailDTO>()));

            MainViewModel vm = new MainViewModel(mockComputerPartsService.Object);
            vm.Condition = "Good";
            vm.PartType = "Motherboard";

            ComputerPart part = new ComputerPart
            {
                Id = id,
                Condition = "Bad",  // Set to Bad type so that it will be filter out
                Description = "some description",
                PartType = "Motherboard"
            };
            vm.BoundedParts.Add(part);

            ComputerPartDetail partDetail = new ComputerPartDetail
            {
                Id = part.Id,
                Condition = part.Condition,
                Description = part.Description,
                PartType = part.PartType,
                Price = 1.2M,
                Location = "some location",
                Remarks = "some text"
            };
            vm.EditPart(part, partDetail);

            mockComputerPartsService.Verify(
                x => x.Update(It.Is<int>(n => n == 12345), It.IsAny<ComputerPartDetailDTO>()),
                Times.Once());

            Assert.AreEqual(0, vm.BoundedParts.Count);
        }

        [TestMethod]
        public void EditPart_NotWithinTypeFilter_UpdatedOnComputerPartsService()
        {
            var mockComputerPartsService = new Mock<IComputerPartsService>();

            const int id = 12345;

            mockComputerPartsService
                .Setup(x => x.GetComputerParts(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<ComputerPartDTO>());

            mockComputerPartsService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<ComputerPartDetailDTO>()));

            MainViewModel vm = new MainViewModel(mockComputerPartsService.Object);
            vm.Condition = "Good";
            vm.PartType = "Motherboard";

            ComputerPart part = new ComputerPart
            {
                Id = id,
                Condition = "Good",
                Description = "some description",
                PartType = "Keyboard" // Set to Keyboard type so that it will be filter out
            };
            vm.BoundedParts.Add(part);

            ComputerPartDetail partDetail = new ComputerPartDetail
            {
                Id = part.Id,
                Condition = part.Condition,
                Description = part.Description,
                PartType = part.PartType,
                Price = 1.2M,
                Location = "some location",
                Remarks = "some text"
            };
            vm.EditPart(part, partDetail);

            mockComputerPartsService.Verify(
                x => x.Update(It.Is<int>(n => n == 12345), It.IsAny<ComputerPartDetailDTO>()),
                Times.Once());

            Assert.AreEqual(0, vm.BoundedParts.Count);
        }

        [TestMethod]
        public void RemovePart_RemoveOneItem_EmptyBoundedParts()
        {
            var mockComputerPartsService = new Mock<IComputerPartsService>();

            mockComputerPartsService
                .Setup(x => x.GetComputerParts(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<ComputerPartDTO>());

            mockComputerPartsService
                .Setup(x => x.Delete(It.IsAny<int>()));

            MainViewModel vm = new MainViewModel(mockComputerPartsService.Object);

            ComputerPart part = new ComputerPart()
            {
                Id = 12345
            };
            vm.BoundedParts.Add(part);

            vm.RemovePart(part);

            mockComputerPartsService.Verify(x => x.Delete(It.Is<int>(n => n == 12345)), Times.Once());

            Assert.AreEqual(0, vm.BoundedParts.Count);
        }

        [TestMethod]
        public void ReloadBoundedParts_NotEmpty()
        {
            var mockComputerPartsService = new Mock<IComputerPartsService>();

            List<ComputerPartDTO> dtoParts = new List<ComputerPartDTO>();
            dtoParts.Add(
                new ComputerPartDTO()
                {
                    Id = 12345,
                    Condition = "Good",
                    PartType = "Motherboard",
                    Description = "some description A"
                }
            );

            mockComputerPartsService
                .Setup(x => x.GetComputerParts(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(dtoParts);

            MainViewModel vm = new MainViewModel(mockComputerPartsService.Object);

            vm.ReloadBoundedParts();

            Assert.AreEqual(1, vm.BoundedParts.Count);
            Assert.AreEqual(12345, vm.BoundedParts[0].Id);
        }
    }
}
