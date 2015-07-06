using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Newtonsoft.Json;
using NUnit;
using Moq;
using NUnit.Framework;
using Budget.Data.Interfaces;
using Budget.Data.Concrete;
using Budget.Domain.Models;
using Budget.Api.Controllers;
using System.Web.Http;

namespace Budget.Api.Tests.Controllers
{
    [TestFixture]
    public class ItemsControllerTests
    {
        #region setup
        private Mock<IItemRepository> _itemRepository;
        [SetUp]
        public void SetUp()
        {
            _itemRepository = new Mock<IItemRepository>();          
        }
        #endregion

        #region tests
        [Test]
        public void WhenGet_Returns_404_IfNoData()
        {
            // Arrange   
            int fakeId = -10;
            IEnumerable<BudgetItem> fakeItems = GetFakeItems();
            // setup
            _itemRepository.Setup(x => x.GetItem(fakeId)).Returns(fakeItems.FirstOrDefault(p => p.Id == fakeId));

            ItemsController controller = new ItemsController(_itemRepository.Object);

            // Act

            IHttpActionResult actionResult = controller.Get(fakeId);
            // Assert
            Assert.IsNotNull(actionResult, "Result is null");
            Assert.IsInstanceOf(typeof(NotFoundResult), actionResult, "It should return 404");
        }
        [Test]
        public void WhenGet_Returns_Item()
        {
            var fakeId = 1;
            // Arrange   
            IEnumerable<BudgetItem> fakeItems = GetFakeItems();
            // setup
            _itemRepository.Setup(x => x.GetItem(fakeId)).Returns(fakeItems.FirstOrDefault(p => p.Id == fakeId));

            ItemsController controller = new ItemsController(_itemRepository.Object);

            // Act
            var response = controller.Get(fakeId);
            var item = response as OkNegotiatedContentResult<BudgetItem>;
            // Assert

            Assert.IsNotNull(item, "Result is null");
            Assert.IsInstanceOf(typeof(BudgetItem), item.Content, "Wrong Model");
            Assert.AreEqual(fakeId, item.Content.Id, "Got wrong item.");
        }

        [Test]
        public void WhenGetAll_Returns_404_IfNoData()
        {
            // Arrange   
            IEnumerable<BudgetItem> fakeItems = GetNoItems();
            // setup
            _itemRepository.Setup(x => x.GetAll()).Returns(fakeItems);

            ItemsController controller = new ItemsController(_itemRepository.Object);

            // Act

            IHttpActionResult actionResult = controller.Get();
            // Assert
            Assert.IsNotNull(actionResult, "Result is null");
            Assert.IsInstanceOf(typeof(NotFoundResult), actionResult, "It should return 404");
        }

        [Test]
        public void WhenGetAll_Returns_AllItems()
        {
            // Arrange   
            IEnumerable<BudgetItem> fakeItems = GetFakeItems();
            // setup
            _itemRepository.Setup(x => x.GetAll()).Returns(fakeItems);

            ItemsController controller = new ItemsController(_itemRepository.Object);

            // Act
            var response = controller.Get();
            var items = response as OkNegotiatedContentResult<IEnumerable<BudgetItem>>;
            // Assert

            Assert.IsNotNull(items, "Result is null");
            Assert.IsInstanceOf(typeof(IEnumerable<BudgetItem>), items.Content, "Wrong Model");
            Assert.AreNotEqual(3, items.Content.Count(), "Got wrong number of items.");
        }

        [Test]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var controller = new ItemsController(_itemRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new BudgetItem 
                { Id = 25, Notes= "Office Lunch", BudgetLocationId = 2, BudgetSubCategoryId = 2, Amount = 15, DateOccured = DateTime.Now, IsExpense = true, IsFixed = false}
                );
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<BudgetItem>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(25, createdResult.RouteValues["id"]);
        }

        [Test]
        public void WhenPut_InValidItem_CreatesNewItem()
        {
            // Arrange

            var fakeId = 100;
            var newItem = new BudgetItem { Id = fakeId, Notes = "Office Lunch", BudgetLocationId = 2, BudgetSubCategoryId = 2, Amount = 15, DateOccured = DateTime.Now, IsExpense = true, IsFixed = false };

            IEnumerable<BudgetItem> fakeItems = GetFakeItems();

            _itemRepository.Setup(x => x.GetItem(fakeId)).Returns(fakeItems.FirstOrDefault(p => p.Id == fakeId));
            
            var controller = new ItemsController(_itemRepository.Object);
            // Act            
            IHttpActionResult actionResult = controller.Put(fakeId, newItem);
            // Assert
            Assert.IsNotNull(actionResult);
         
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<BudgetItem>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(fakeId, createdResult.RouteValues["id"]);              
         }

        [Test]
        public void WhenPut_ValidItem_ReturnsOk()
        {
            // Arrange
            int itemId = 2;
            var newItem = new BudgetItem { Id = itemId, Notes = "Office Lunch", BudgetLocationId = 2, BudgetSubCategoryId = 2, Amount = 15, DateOccured = DateTime.Now, IsExpense = true, IsFixed = false };
            IEnumerable<BudgetItem> fakeItems = GetFakeItems();

            _itemRepository.Setup(x => x.GetItem(itemId)).Returns(fakeItems.FirstOrDefault(p => p.Id == itemId));
            _itemRepository.Setup(x => x.UpdateItem(newItem)).Returns(newItem);

            var controller = new ItemsController(_itemRepository.Object);
            
            // Act            
            IHttpActionResult actionResult = controller.Put(2, newItem);
            // Assert
            Assert.IsNotNull(actionResult);

            var item = actionResult as OkNegotiatedContentResult<BudgetItem>;
            Assert.IsNotNull(item);
            Assert.AreEqual(item.Content.BudgetLocationId, 2);


        }
        [Test]
        public void WhenDelete_WithValidId_Returns_Ok()
        {
            var itemId = 2;
            // Arrange   
            IEnumerable<BudgetItem> fakeItems = GetFakeItems();
            var numberOfItemsBeforeDelete = fakeItems.Count();
            // setup
            _itemRepository.Setup(x => x.GetItem(itemId)).Returns(fakeItems.FirstOrDefault(p => p.Id == itemId));
            
            var controller = new ItemsController(_itemRepository.Object);

            // Act

            IHttpActionResult actionResult = controller.Delete(itemId);
            // Assert
            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<BudgetItem>), actionResult);

        }

        [Test]
        public void WhenDelete_WithAnUnknownId_Returns_404()
        {
            var itemId = 1000;
            // Arrange   
            IEnumerable<BudgetItem> fakeArtists = GetFakeItems();
            // setup
            _itemRepository.Setup(x => x.GetItem(itemId)).Returns(fakeArtists.FirstOrDefault(p => p.Id == itemId));

            var controller = new ItemsController(_itemRepository.Object);

            // Act

            IHttpActionResult actionResult = controller.Delete(itemId);
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf(typeof(NotFoundResult), actionResult, "It should return 404");
        }

        #endregion 

        #region privateMethods
        /// <summary>
        /// GetItems
        /// </summary>
        /// <returns>IEnumerable<BudgetItem></returns>
        /// <remarks>Returns list of fake items</remarks>
        private static IEnumerable<BudgetItem> GetFakeItems()
        {
            IEnumerable<BudgetItem> fakeItems = new List<BudgetItem> {
                new BudgetItem{ Id = 1, Notes= "Coffee", BudgetLocationId = 1, BudgetSubCategoryId = 1, Amount = 5, DateOccured = DateTime.Now, IsExpense = true, IsFixed = false },
                new BudgetItem{ Id = 2, Notes= "Lunch", BudgetLocationId = 2, BudgetSubCategoryId = 2, Amount = 10, DateOccured = DateTime.Now, IsExpense = true, IsFixed = false},
             }.AsEnumerable();

            return fakeItems;
        }


        /// <summary>
        /// GetNoItems
        /// </summary>
        /// <returns>null</returns>
        /// <remarks>Returns null list</remarks>
        private static IEnumerable<BudgetItem> GetNoItems()
        {            
            return null;
        }
     
        #endregion
    }
}
