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
            if(app.Items.Count < 6) {
                Assert.Fail("App initialization failed, expected at least 6 base items");
            }
        }

        /// <summary>
        ///     TestUpdateQuality ensures that the App.UpdateQuality method decreases the
        ///  SellIn and Quality properties of each item in inventory.
        /// </summary>
        [TestMethod]
        public void TestUpdateQuality() {
            App app = new App();

            // Cache current so we can measure the changes
            IList<Item> items = new List<Item>();
            foreach(Item item in app.Items) {
                items.Add(new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality });
            }

            // Call the app.UpdateQuality method
            app.UpdateQuality();

            for(int i = 0; i < items.Count; i++) {
                EvaluateUpdatedItem(items[i], app.Items[i]);
            }
        }

        /// <summary>
        ///     EvaluateUpdatedItem simply routes to the various custom rules below
        /// </summary>
        /// <param name="original">The Item we cached before calling app.UpdateQuality</param>
        /// <param name="updated">The App.Items that was updated</param>
        protected void EvaluateUpdatedItem(Item original, Item updated) {
            if (original.Name == "Sulfuras, Hand of Ragnaros") {
                return;
            }

            if (updated.Quality > 50) {
                Assert.Fail("Item's Quality property exceeds max");
            }

            if (original.SellIn > 0) {
                int dif = original.SellIn - updated.SellIn;
                if (dif != 1) {
                    Assert.Fail("Failed to decrease SellIn property");
                }
            }

            if (original.Name == "Aged Brie") {
                EvaluateAgedBrie(original, updated);
            } else if (original.Name.StartsWith("Backstage passes")) {
                EvaluateBackstagePasses(original, updated);
            } else if (original.Name.StartsWith("Conjured")) {
                EvaluateConjuredItem(original, updated);
            } else {
                EvaluateCommonItem(original, updated);
            }
        }

        /// <summary>
        ///     EvaluateAgedBrie ensures that the quality of aged brie went up
        /// </summary>
        /// <param name="original">The Item we cached before calling app.UpdateQuality</param>
        /// <param name="updated">The App.Items that was updated</param>
        protected void EvaluateAgedBrie(Item original, Item updated) {
            int dif = updated.Quality - original.Quality;
            if (dif != 1) {
                Assert.Fail("Failed to increase Quality property for Aged Brie");
            }
        }

        /// <summary>
        ///     EvaluateBackstagePasses ensures that the quality of any items starting
        ///  with the words Backstage passes (case sensitive) went up
        /// </summary>
        /// <param name="original">The Item we cached before calling app.UpdateQuality</param>
        /// <param name="updated">The App.Items that was updated</param>
        protected void EvaluateBackstagePasses(Item original, Item updated) {
            int dif = updated.Quality - original.Quality;

            if (updated.SellIn == 0) {
                if (updated.Quality != 0) {
                    Assert.Fail("Failed to zero Quality property for Backstage passes, the shows over :(");
                }
            } else if (updated.SellIn < 5) {
                if (dif != 3) {
                    Assert.Fail("Failed to sky rocket Quality of Backstage passes before the show");
                }
            } else if (updated.SellIn < 10) {
                if (dif != 2) {
                    Assert.Fail("Failed to hike up the Quality of Backstage passes as show approaches");
                }
            } else {
                if (dif != 1) {
                    Assert.Fail("Failed to increase the Quality property of Backstage passes");
                }
            }
        }

        /// <summary>
        ///     EvaluateConjuredItem ensures that the quality of any conjured items went
        ///  down twice as fast as common items
        /// </summary>
        /// <param name="original">The Item we cached before calling app.UpdateQuality</param>
        /// <param name="updated">The App.Items that was updated</param>
        protected void EvaluateConjuredItem(Item original, Item updated) {
            if (updated.SellIn > 0) {
                if (original.Quality == 1) {
                    int dif = original.Quality - updated.Quality;
                    if (dif != 1) {
                        Assert.Fail("Failed to decrease Quality property of conjured item");
                    }
                } else if (original.Quality >= 2) {
                    int dif = original.Quality - updated.Quality;
                    if (dif != 2) {
                        Assert.Fail("Failed to decrease Quality property of conjured item");
                    }
                }
            } else {
                if (original.Quality <= 3) {
                    int dif = original.Quality - updated.Quality;
                    if (dif != original.Quality) {
                        Assert.Fail("Failed to decrease Quality property of conjured item");
                    }
                } else if (original.Quality >= 4) {
                    int dif = original.Quality - updated.Quality;
                    if (dif != 4) {
                        Assert.Fail("Failed to decrease Quality property of conjured item");
                    }
                }
            }
        }

        /// <summary>
        ///     EvaluateCommonItem ensures that the quality of a common item degrades as expected
        /// </summary>
        /// <param name="original">The Item we cached before calling app.UpdateQuality</param>
        /// <param name="updated">The App.Items that was updated</param>
        protected void EvaluateCommonItem(Item original, Item updated) {
            if (original.Quality > 0) {
                int dif = original.Quality - updated.Quality;
                if (updated.SellIn > 0) {
                    if (dif != 1) {
                        Assert.Fail("Failed to decrease Quality property of common item");
                    }
                } else {
                    if (dif != 2) {
                        Assert.Fail("Failed to decrease Quality property of common item");
                    }
                }
            }
        }
    }
}
