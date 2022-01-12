<img src="https://cdn.pixabay.com/photo/2014/04/02/10/53/shopping-cart-304843_1280.png" width=20% height=20%>

# Footwear - Brief Information 
An open source e-commerce web application build with Angular 11 and ASP .NET Core 5 Rest API. The applicaiton is build with desktop first design pattern, but it's fully responsive(bootstrap 4 grid system and CSS Flexbox). The application contains few products manually seeded in the database, which users can select, pick a size and add to a cart. Then user can check out the products in the cart,select delivery address and choose payment. The Payment type is cash on delivery or with a credit/debit card (using the Stripe API).
The application is made to practice my ASP .NET Core 5 (started from 3.1, upgraded later to .Net) and Angular 11 (started to building it on Angular 8). 

# Addons and Libiries
## [ASP .NET CORE Packages](https://github.com/milyo001/Footwear/blob/main/Footwear/Footwear/Footwear.csproj) 
* AutoMapper 8.1.1
* JwtBearer 5.0.13
* EntityFrameworkCore 5.0.0
* Stripe.net 39.77.0
## [Angular Packages](https://github.com/milyo001/Footwear/blob/main/Footwear/Footwear/ClientApp/package.json)
* Bootstrap 4.5.3
* Ngx-toastr 11.3.3
* Rxjs 6.6.0 
* Fontawesome Icons 5.15
* Angular Animations 11.0.1
* Angular Material 11.0.1


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
| Remember me option  | &#10060; | Remember user details |
| Facebook/Google login  | &#10060; | Log in with social network API |
| Add forget password option | &#10060; | Add forget password option, send email or text message on the phone to reset |
| Add user token id interceptor | &#10004; | Validate if user token data is valid |

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

## Order Features
| Feature  | Coded? | Description |
|----------|:-------------:|:-------------|
| View Orders | &#10060; | View all orders |
| Create cash order | &#10004; | Create pay on delivery order |
| Create paid order | &#10004; | Pay for order with a credit card |

## Tests
| Feature  | Coded? | Description |
|----------|:-------------:|:-------------|
| View Orders | &#10060; | View all orders |
| Create cash order | &#10004; | Create pay on delivery order |
| Create paid order | &#10004; | Pay for order with a credit card |

