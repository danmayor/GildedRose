
// Don't forget to reference Microsoft.VisualStudio.QualityTools.UnitTestFramework
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GildedRose.Console;

namespace GildedRose.Tests {
    /// <summary>
    ///     ItemTests are used to ensure that the Item class of our inventory control system continues
    ///  to function as it is intended.
    /// </summary>
    [TestClass()]
    public class ItemTests {

        /// <summary>
        ///     TestItemProperties ensures that the Item class contains functional properties
        ///  for Name, Quality and SellIn. (In case that silly goblin decides to turn into a gremlin)
        /// </summary>
        [TestMethod]
        public void TestItemProperties() {
            Item gildedItem = new Item();
            gildedItem.Name = "ItemName";
            gildedItem.Quality = 50;
            gildedItem.SellIn = 10;
            
            if(gildedItem.Name != "ItemName") {
                Assert.Fail("Item.Name property failed");
            } else if(gildedItem.Quality != 50) {
                Assert.Fail("Item.Quality property failed");
            } else if(gildedItem.SellIn != 10) {
                Assert.Fail("Item.SellIn property failed");
            }
        }
    }
}
