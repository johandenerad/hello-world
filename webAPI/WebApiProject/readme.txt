Instructions:

1. Open 'WebApi/Loans.sln/' in Visual Studio, build the solution and run with IIS Express
2. A browser displaying the API should open, displaying two loans
3. Open the 'Website/Loan Web Api.html' in Chrome
4. In Chrome's DevTools 'Sources' tab, navigate to 'result --> (no domain) --> pen.js
5. Adjust the urls in the $.getJSON and $.ajax calls so that they match the 'https:\\localhost:xxxxx' local address running the API
6. The 'Get All Loans' and 'Find Loan' buttons should now function

Using Fiddler or other web debuggers you can POST and DELETE entries to the API, which the 'Get All Loans' and 'Find Loan' buttons can GET 