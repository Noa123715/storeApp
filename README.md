 # Store App - dotNet5783_7729_0796 🛒

# Application Overview
Store App is a simple store application for branded clothing that allows users to register, log in, and shop for products.
Users can browse a catalog of products, add items to their cart, and complete their purchase through the checkout process.
The app provides a seamless shopping experience for users, making it easy to find and purchase the products they need.

# Getting Started
To run the application locally, follow these steps:
- Open your Command Line. (1. Press WinKey + R, 2. Type cmd, 3. Press Enter)
- Clone the repository from GitHub: 

    ```bash
    git clone https://github.com/Noa123715/storeApp.git
    ```

- Double-click on the file dotNet5783_7729_0976.sln. (Visual Studio will open with the project)
- Make sure that the PL file is selected for execution by checking that it appears next to the green arrow. If it is, click on the green arrow, and the project will start running. If not, select the PL file as the chosen file for execution, and then click on the green arrow.

# Usage Instructions
When the application is running, you will be redirected to the home page where you have four options:

1. If you are an administrator, click "administrator" to access the admin's activities.
2. If you are a customer interested in buying and ordering from our store, you can click the "New Order" button.
3. If you want to see a simulation of how orders are processed in our store, feel free to click on the simulation button.
4. If you want to track your order, you can enter your order number and see its status.

### Administrator
For the administrator, there are two possible actions:

1. **Product Management**: Adding a new product, editing, or deleting an existing product.
2. **Order Management**: Tracking existing orders, approving orders, sending them, and handling them.

### Client
For the customer, there are the following options:

1. **Ordering Products**: Viewing all available products in store, adding a product to the cart, and selecting the quantity of the product.
2. **View Cart**: Option to see the cart with all the products selected. In the cart, there are options to delete an item that you changed your mind about ordering or to add more items if you want to order more than one item.
3. **Order Confirmation**: Confirming the details of the order, payment, and entering the details for order.

### Track Your Order
In order tracking, you can see the current status of your order. 
- If the order has not yet left the store, you can change the order details. 
- If the order has not yet arrived at your home, you can change the shipping address. 
- If the order has already arrived at its destination, you can only change your contact details for future orders.

# Explanation:
### Usage of Technologies
The entire project is written in C#.
The database is an XML file, and the UI is written in WPF.

### Architecture
The project is built on a three-tier architecture:
- **Presentation tier:** The top layer responsible for user display, such as product catalogs, order tracking, etc.
- **Application tier:** The business logic layer, controlling the application's functionality and responsible for processing data returned from the presentation and data tiers and communication between them.
- **Data tier:** The data layer, where information is retrieved, updated, and stored independently, not related to the logical layer.

### Design Patterns
The project uses many design patterns, including:
- **Factory -** 
- **Singelton -** 
- **Lazy initialization -** 
- **Thread safe -** 

### About the application name
We decided to name our application dotNet5783_7729_0796:
dotNet, of course, because that's the framework that support C# language.
5783 - because it is the *gematria* of the Hebrew year in which we wrote the project.
7729_0796 - are the endings of our identification numbers.

# Screenshots
- Entering the website:
  <img src="https://github.com/noa123715/storeApp/blob/main/screenshots/home_page.png"> <br><br>
- Admin Product Management:
  <img src="https://github.com/Noa123715/storeApp/blob/main/screenshots/admin_product.png"><br><br>
- Add a Product:
  <img src="https://github.com/noa123715/storeApp/blob/main/screenshots/add_product.png"><br><br>
- Admin Order Management:
  <img src="https://github.com/noa123715/storeApp/blob/main/screenshots/admin_order.png"><br><br>
- The Catalog:
  <img src="https://github.com/noa123715/storeApp/blob/main/screenshots/catalog.png"><br><br>
- Use a filter to find a product on the catalog:
  <img src="https://github.com/noa123715/storeApp/blob/main/screenshots/filter.png"><br><br>
- Confirm the product to the cart:
  <img src="https://github.com/noa123715/storeApp/blob/main/screenshots/product.png"><br><br>
- Successfully add to the cart:
  <img src="https://github.com/noa123715/storeApp/blob/main/screenshots/successfully_added.png"><br><br>
- The Cart:
  <img src="https://github.com/noa123715/storeApp/blob/main/screenshots/cart.png"><br><br>
- Confirm the order and enter the order's details:
  <img src="https://github.com/noa123715/storeApp/blob/main/screenshots/confirm_order.png"><br><br>
- Track the order:
  <img src="https://github.com/noa123715/storeApp/blob/main/screenshots/track_order.png"><br><br>
- Simulator:
  <img src="https://github.com/noa123715/storeApp/blob/main/screenshots/simulator.png"><br><br>

# Bonus:
 - stage 1,2 - TryParse: Program.cs BlTest and DalTest. 
 - stage 3 - Extension Method: Bl and Dal. 
 - stage 4- Thread safe and Lazy initialization and Singelton: DalList. 
 - stage 5 - Observable collection: Po. Transferring information between windows and PO and Style and Binding and Data-contex: PL.
            Option to delete entity objects: PL-BL. 
 - stage 7 - Prograss Bar: Simulator. 
            Transferring information between windows and PO and Style and Binding and Data-contex and IConverter: PL.
<br/><br/>

#### The application was developed by Shoshana Morgenstern & Noa Abecassis in 2023.

# Enjoyable Shopping 🛍️