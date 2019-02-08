using System.Collections.Generic;

namespace GildedRose.Console {
    public class App {
        /// <summary>
        ///     Items is a reference to the Program.Items list (take that goblin in the corner)
        /// </summary>
        public IList<Item> Items;

        /// <summary>
        ///     Default Constructor (builds our own Items list)
        /// </summary>
        public App(IList<Item> items = null) {
            if (items == null) {
                items = new List<Item> {
                    new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                    new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Item {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 15,
                            Quality = 20
                    },
                    new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                };
            }

            Items = items;
        }

        /// <summary>
        ///     UpdateQuality will process our end of day rules on our inventory
        /// </summary>
        public void UpdateQuality() {
            foreach(Item item in Items) {
                if(item.Name == "Aged Brie") {
                    UpdateAgedBrie(item);
                } else if (item.Name.StartsWith("Backstage passes")) {
                    UpdateBackstagePasses(item);
                } else if (item.Name.StartsWith("Conjured")) {
                    UpdateConjuredItem(item);
                } else if (item.Name == "Sulfuras, Hand of Ragnaros") {
                    item.SellIn = 0;
                    item.Quality = 80;
                } else {
                    UpdateCommonItem(item);
                }
            }
        }

        /// <summary>
        ///     UpdateAgedBrie will increase the quality of this item every day to
        ///  a max of 50
        /// </summary>
        /// <param name="item">The item to update</param>
        protected void UpdateAgedBrie(Item item) {
            if (item.SellIn > 0) {
                item.SellIn--;
            } else {
                item.SellIn = 0;
            }

            if (item.Quality < 50) {
                item.Quality++;
            }
        }

        /// <summary>
        ///     UpdateBackstagePasses will increase the quality of this item every day
        ///  (more when show is 10 days or less out, even more when 5 days or less out)
        ///  to a max of 50
        /// </summary>
        /// <param name="item">The item to update</param>
        protected void UpdateBackstagePasses(Item item) {
            if (item.SellIn <= 1) {
                item.SellIn = 0;
                item.Quality = 0;
            } else if (item.SellIn <= 5) {
                item.SellIn--;
                item.Quality += 3;
            } else if (item.SellIn <= 10) {
                item.SellIn--;
                item.Quality += 2;
            } else {
                item.SellIn--;
                item.Quality++;
            }

            if (item.Quality > 50) {
                item.Quality = 50;
            }
        }

        /// <summary>
        ///     UpdateConjuredItem will decrease the quality of this item every day
        ///  (twice as fast as a common item). Twice as fast as that when SellIn hits 0
        /// </summary>
        /// <param name="item">The item to update</param>
        protected void UpdateConjuredItem(Item item) {
            if (item.SellIn > 0) {
                item.SellIn--;
                if (item.Quality <= 1) {
                    item.Quality = 0;
                } else {
                    item.Quality -= 2;
                }
            } else {
                item.SellIn = 0;
                if (item.Quality <= 3) {
                    item.Quality = 0;
                } else {
                    item.Quality -= 4;
                }
            }

        }

        /// <summary>
        ///     UpdateCommonItem will decrease the quality of this item every day
        ///  (twice as fast when SellIn hits 0)
        /// </summary>
        /// <param name="item"></param>
        protected void UpdateCommonItem(Item item) {
            if (item.SellIn > 0) {
                item.SellIn--;
                item.Quality--;
            } else {
                item.SellIn = 0;
                if(item.Quality <= 1) {
                    item.Quality = 0;
                } else {
                    item.Quality -= 2;
                }
            }
        }
    }
}
