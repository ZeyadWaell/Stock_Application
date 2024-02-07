# Stock Application

## Description
Welcome to the Stock Application! This compact application allows users to engage in stock trading with a straightforward order logic.

### Order Logic
- **Buying:**
  - When a user creates an order to buy for the first time, the application checks the current stock price and the available quantity in the company's stock.
  - If the desired quantity is available, the user can proceed with the purchase.
  - If the quantity is insufficient, the application places an order, waiting for someone to sell, and the user's order is queued for processing.

- **Selling:**
  - When a user decides to sell, the application checks if there's a matching buy order in the market.
  - If a buyer is found at the specified price and quantity or less, the transaction is executed.
  - If no match is found, the application places a sell order, waiting for someone to buy.
  - If the order doesn't match the stock price, the application checks the market for a buyer at the specified price. If found, the transaction is completed; otherwise, it remains pending.

## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Features](#features)
- [Contributing](#contributing)
- [License](#license)
