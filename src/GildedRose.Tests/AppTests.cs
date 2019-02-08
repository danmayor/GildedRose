using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GildedRose.Console;

namespace GildedRose.Tests {
    /// <summary>
    ///     AppTests are used to ensure that the new App class that we create from the existing
    ///  program class works as expected.
    /// </summary>
    [TestClass()]
    public class AppTests {
        /// <summary>
        ///     TestInitialization ensures that creating a new App object using the base constructor
        ///  initializes and populates the base inventory of our little shop
        /// </summary>
        [TestMethod]
        public void TestInitialization() {
            App app = new App();
            if(app.Items.Count < 7) {
                Assert.Fail("App initialization failed, expected at least 7 base items");
            }
        }

        /// <summary>
        ///     TestUpdateQuality ensures that the App.UpdateQuality method decreases the
        ///  SellIn and Quality properties of each item in inventory.
        /// </summary>
        [TestMethod]
        public void TestUpdateQuality() {
            App app = new App();
            List<Item> items = new List<Item>();

            foreach(Item item in app.Items) {
                items.Add(new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality });
            }

            app.UpdateQuality();

            for(int i = 0; i < items.Count; i++) {
                if (items[i].Name != app.Items[i].Name) {
                    Assert.Fail("App.UpdateQuality has caused the name of this item to change");
                } else {
                    int sellInDiff = items[i].SellIn - app.Items[i].SellIn;
                    int qualityDiff = items[i].Quality - app.Items[i].Quality;

                    if(sellInDiff != 1 && items[i].SellIn > 0) {
                        Assert.Fail("App.UpdateQuality failed to decrease SellIn property");
                    }

                    if(qualityDiff != 1 && items[i].Quality > 0) {
                        Assert.Fail("App.UpdateQuality failed to decrease Quality property");
                    }
                }
            }
        }
    }
}
