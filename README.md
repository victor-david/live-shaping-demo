# Live Shaping Demo
Demonstration of ListCollectionView live shaping (filtering and sorting)

This demo application uses ListCollectionView to demonstrate its live shaping capabilities. The app creates a set of customer objects and two ListCollectionView objects, each bound to a separate DataGrid, and each with a separate filter specification.

When you click the button, a timer randomly changes the customer balance (used for filtering and sorting). As the balances change, customer rows change position and move from DataGrid to DataGrid, depending on which filter they pass.
