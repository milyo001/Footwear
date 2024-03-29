
# Footwear - Brief Information 
<p> :shoe: An open source e-commerce web application build with Angular 11 and ASP .NET Core (using .NET 5) RESTful Web API.
The applicaiton is build with desktop first aproach, but it's fully responsive (with the help of bootstrap 4 grid system, Angular Material UI and CSS Flexbox and Grid).</p>
<p> :open_file_folder: The architecture is a simple "all-in-one" monolith application (entire application is deployed as a single unit). That means as the project's size and complexity grows, the number of files and folders will continue to grow as well. The "all-in-one" pattern is used just because I am developing the project to practice my skills and for fun. </p>
<p> :memo: The application contains few products manually seeded in the database(I will build admin panel soon), which users can select, pick a size and add to a cart. Then the user can review the products in the cart  and increase/decrease product quiantity or delete the product from cart. Then next step is to select delivery address(or import the data from your account's address) and choose payment. The Payment type is cash on delivery or with a credit/debit card (using the Stripe API). After order is made, the user can see all(current/past or delivered) orders, send an sample invoice email and view order details.
</p>
<p> :gift: The application is made mostly for fun and it's usage is not commercial, feel free to copy, download or clone the repo or get some sample code.</p>

# Guide / How to run locally on your machine
1. Download/Clone the repository <br/>
2. Open the API folder and create appsettings.development.json and appsettings.production.json(optional) (present in .gitignore file so you have to create them manually) 
Example below (replace <<>> with your value):
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "<<DATABASE CONNECTION STRING>>"
  },
  "AllowedHosts": "*",
  "AllowedOrigins": {
    "StripeUrl": "https://checkout.stripe.com/",
    "ServerUrl": "https://localhost:44365",
    "ClientUrl": "http://localhost:4200"
  },
  "ApplicationSettings": {
    "ApiUrl": "https://api.testfootwearapp3.azurewebsites.net",
    "ClientUrl": "https://testfootwearapp3.azurewebsites.net",
    /* Change the api and client url to localhost:<<your port>> */
    "EncryptionKey": "MbQeShVmYq3t6w9z$C&F)J@NcRfUjWnZ,
    /* Or generate one here(select 256-bit key): https://www.allkeysgenerator.com/Random/Security-Encryption-Key-Generator.aspx */
    "JWT_Secret": "<<YOUR JWT SECRET KEY HERE>>",
    /* You can get your unique key here: https://jwt.io/introduction */
    "Stripe_Secret": "<<YOUR STRIPE SECRET KEY HERE>>"
    /* You can get your unique key here: https://stripe.com/docs/keys */
  },
  "MailSettings": {
     /* I am using ethereal fake SMTP, you can generate your own fake account here: https://ethereal.email/create */
    "Mail": "assunta.kohler67@ethereal.email",
    "DisplayName": "Assunta Kohler",
    "Password": "TuYAVeQbKGZmqEbsMM",
    "Host": "smtp.ethereal.email",
    "Port": 587
  }
}

```

3. Type command 'dotnet restore' to install all missing packages or do it manually

4. For demo payments use this demo card info provided from Stripe API:</br>
    Email: Any*</br>
    Card Number: 4242 4242 4242 4242</br>
    Expiration: Any* / Any*</br>
    CVV: Any*</br>
    Name: Any*</br>
 </br>

*You can put random information but the card number should be the one from above

5. Run with IIS Express or host the application

# Dependencies
## [ASP .NET CORE Packages](https://github.com/milyo001/Footwear/blob/main/Footwear/Footwear/Footwear.csproj) 
* AutoMapper 8.1.1
* JwtBearer 5.0.13
* EntityFrameworkCore 5.0.0
* Stripe.net 39.77.0
* NETCore.MailKit 2.0.3
* Moq 4.16.1
* XUnit 2.4.3
## [Angular Packages](https://github.com/milyo001/Footwear/blob/main/Footwear/Footwear/ClientApp/package.json)
* Bootstrap 4.5.3
* Ngx-toastr 11.3.3
* Rxjs 6.6.0 
* Fontawesome Icons 5.15
* Angular Animations 11.0.1
* Ngx Pagination 5.0.0
* Angular Material 11.0.1
* Karma 5.1.1
* Jasmine 3.6.0

# Functionality
## User Features
| Feature  | Coded? | Description |
|----------|:-------------:|:-------------|
| Register a new user | &#10004; | Register a new user using JWT Authorization token functionality |
| Log in | &#10004; | Log in functionality |
| Change password | &#10004; | Change password functionality |
| Change email/username | &#10004; | Change email/username functionality |
| Add User Information | &#10004; | Add default user billing information |
| Import User Information | &#10004; | Import user information functionality when finalizing order |
| Check/Modify User Information | &#10004; | Check or modify user information, invoice address, first, last name etc. |
| Remember me option  | &#10004; | Remember user details |
| Facebook/Google login  | &#10060; | Log in with social network API |
| Add user token id interceptor | &#10004; | Validate if user token data is valid |
| Implement lazy loading | &#10004; | Lazy-load the user module |

## Product Features 
| Feature  | Coded? | Description |
|----------|:-------------:|:-------------|
| View Products  | &#10004; | View all products functionality  |
| Sort Products  | &#10004; | Sort all products by ascending, descending etc. functionality |
| Filter Products  | &#10004; | Filter all products by gender (man, woman, kids) functionality |
| Change Pages  | &#10004; | Pagination functionality |
| View Product  | &#10004; | View a single product functionality |
| Select Product Size  | &#10004; | Select product size functionality |
| Add Product To Cart | &#10004; | Add the selected product to cart stored in the database |
| Search For Product by Name | &#10060; | Search for specific product |
| Check Available Products | &#10060; | Check if product size is available in the database |
| Implement lazy loading | &#10004; | Lazy-load the product module |

## Cart Features 
| Feature  | Coded? | Description |
|----------|:-------------:|:-------------|
| View All Cart Products | &#10004; | View all cart products for the user |
| Increment Cart Product  | &#10004; | Increment Cart Product |
| Decrement Cart Product Quantity | &#10004; | Decrement cart product quantity  |
| Add New Cart Product When Size Is Diffrent | &#10004; | Create new instance of cart product, only when the size is diffrent, otherwise increase quantity |
| Remove Cart Product | &#10004; | Remove cart product |
| View Cart Product | &#10004; | View Cart Product directly from the cart page |
| Checkout  | &#10004; | Checkout functionality |
| Implement lazy loading | &#10004; | Lazy-load the cart module |

## Order Features
| Feature  | Coded? | Description |
|----------|:-------------:|:-------------|
| Create cash order | &#10004; | Create pay on delivery order |
| Create paid order | &#10004; | Pay for order with a credit card |
| View Orders | &#10004; | View all orders |
| Send email for order | &#10004; | Functionality to send email for order details, currently using fake SMTP |
| Orders pagination condition | &#10004; | Shows pagination only when orders are more than a specific number (currently 10 per page) |
| Reconfirm order | &#10004; | Reconfirm orders before ordering with a stepper |
| View current and delivered orders | &#10004; | View current and delivered orders by changing sections Current/Past Orders |
| View selected order component details | &#10004; | View the selected order full details and implement close details toggler button |
| View ordered products for order | &#10004; | View selected order ordered list of products with accordion functionality |
| Implement Lazy loading | &#10004; | Lazy-load the orders module |

## Tests
| Feature  | Coded? | Description |
|----------|:-------------:|:-------------|
| Test ASP .NET Controller logic | &#10004; | Test the api controllers logic |
| Test ASP .NET Models | &#10060; | Test the validation in models  |
| Test ASP .NET Services | &#10060; | Test the methods in the services  |
| Test ASP .NET StartUp class | &#10060; | Test the application composition root  |
| Test Angular components | &#10060; | Test all components logic |
| Test Angular services | &#10004; | Test all http and application services |
| Test Angular pipes | &#10004; | Test all Angular pipes |
