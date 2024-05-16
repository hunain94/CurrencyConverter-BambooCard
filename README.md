# CurrencyConverter

To run the application
Please run this project in VS 2022 
Debug the project and you will be redirected to http://localhost:5294/swagger/index.html

Three endpoint available on swagger/index to test.

To allow reattmpt to Frankfurt APIs I have used Polly library and have initialized that in the program.cs class.

For exception handling I have written a middleware class in the BAL library.


Assumptions
I have assumed that dates that will be sent as a request param in the historical records will be (yyyy-MM-dd) format.


Enhancements
I would have also implemented logging if I had the time. 