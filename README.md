# Qis.App

Qis.App is a console application.
When started it asks a date to fetch stock prices.
If the date entered is valid it returns symbol(s) with the largest daily gain for each exchange in that day.

```
Enter a date:
2020-8-7
```
The return is in json format having exchange as the key value and symbols as the values for that exchange.


```
{"XNYS":["EVR"],"XNAS":["MSFT"]}
```

If there are multiple symbols for the same exchange with the largest daily gain then it returns multiple symbols for that exchange.

The app keeps repeating asking a date until it is stopped.

