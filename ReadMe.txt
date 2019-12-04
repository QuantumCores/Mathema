LINK https://github.com/QunatumCore/Mathema

ARCHITECTURE

I have split Solution into Presentation layer and "Backend" layer. 

In Backend there are Algorithms project with logic, Interfaces so that third party users can add their own parsers or expression builders. 
Models and Shared are also in separate projects as they might be used all over solution without necessity to reference for example Algorithms.
There are also test. Test are extremaly important!

I applied KISS (Keep It Simple Stupid) and YAGNI (You Aint Gonna Need It) rules in this solution. There is only one console thread using ExpressionBuilder and RPNParser and 
both classes are stateless. That's why my Parse abd Build methods are static. If I were to change presentation layer to WCF or WebApi I would then make these methods 
nonstatic and create Interfaces for both classes.

SOLUTION

I started with a simple idea of reading input strings and recognizing characters as symbols (numbers, operators etc.). To make this easily extendible I used 
Reverse Polish Notation and Shunting Yard Algorithm to keep operations in correct order. I split Parsing, Expression building and Expression executionin two separate 
processes to add in future Expression classification with variables and Solution methods with variables. As I was working on this project it was extremaly simple to add 
functions, new operators and it will be very easy to add constants (which you can already see in Shared project).

