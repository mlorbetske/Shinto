using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shinto.Entities;
using System.Collections.Generic;

namespace EntitiesTests
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void EntityTestHashOperations()
        {
            var e = new Entity();
            e.SetId(0);

            var e2 = new Entity();
            e2.SetId(2);

            var entities = new List<Entity>();
            entities.Add(e2);

            Assert.IsTrue(entities.Contains(e2));
            Assert.IsFalse(entities.Contains(e));

            var copy = new Entity();
            copy.SetId(2);
            Assert.IsTrue(entities.Contains(copy));
            Assert.IsTrue(copy.Equals(e2));
            Assert.IsFalse(e.Equals(e2));

        }
    }
}
