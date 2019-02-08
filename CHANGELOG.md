# Project Start
First I removed the default unit test as it was simply a place holder. Then I introduced
the change log and new unit tests to representing the existing GildedRose.Console project
classes.

# Step 1 TDD Draft for initial refactor
In this step I draft up an initial design for critical unit tests. These tests are built
for the Item class and a new App class. The UnitTests will not build at this point because
the App class has not yet been introduced. (I believe this to be considered the Arrange step
of a AAA pattern).

In the next commit, I expand on the tests to account for the various quality modification
rules of the shop and introduce the App class that emulates the existing Items collection.
