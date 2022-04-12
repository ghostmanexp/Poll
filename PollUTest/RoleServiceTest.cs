using NUnit.Framework;
using Poll;
using Poll.Interfaces;
using Poll.Services;

namespace PollUTest
{
    [TestFixture]
    public class RoleServiceTest
    {
        private Main _dbConn;
        private IRoleService _roleService;

        [SetUp]
        public void Setup()
        {
            _dbConn = new Main();
            _roleService = new RoleService(_dbConn);
        }

        [Test]
        public void Test1()
        {
            var lst = _roleService.GetAllAsync();
            Assert.IsNotNull(lst);

            Assert.Pass();
        }
    }
}