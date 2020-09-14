# Qis.App

Qis.App is a console application.
When started it asks a date to fetch stock prices.
If the date entered is valid it returns symbol(s) with the largest daily gain for each exchange.
The return is in json format having exchange as the key value and symbols as the values for that exchange.
If there are multiple symbols for the same exchange with the largest daily gain then it return multiple symbols for that exchange.


