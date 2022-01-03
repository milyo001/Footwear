<img src="https://cdn.pixabay.com/photo/2014/04/02/10/53/shopping-cart-304843_1280.png" width=20% height=20%>

# Footwear - Brief Information 
An e-commerce web application build with Angular 11 and .NET 5. The applicaiton is build with desktop first design pattern, but it's fully responsive(bootstrap 4 grid system and CSS Flexbox). The application contains few products manually seeded in the database, which users can select, pick a size and add to a cart. Then user can check out the products in the cart,
select delivery address and choose payment. The Payment type is cash on delivery or with a credit/debit card (using the Stripe API).

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

