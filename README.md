# BakeryStoreApp
Microservices architecture showcase


#### The Bakery Problem
Consider a store where items have not only have unit price but also volume prices and sales. For example, cookies may be $1.25 each or six for $6.00 or eight for $6.00 on Fridays.

This showcases a shoppingcart application that accepts a date and an arbitrary ordering of products and then returns the correct total price for an entire shopping cart based on per-unit pricing or volume pricing when applicable.

The products and associated data :
```json
{
  "treats": [
    {
      "id": 1,
      "name": "Brownie",
      "imageURL": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTHdr1eTXEMs68Dx-b_mZT0RpifEQ8so6A1unRsJlyJIPe0LUE2HQ",
      "price": 2.00,
      "bulkPricing": {
        "amount": 4,
        "totalPrice": 7.00
      }
    },
    {
      "id": 2,
      "name": "Key Lime Cheesecake",
      "imageURL": "http://1.bp.blogspot.com/-7we9Z0C_fpI/T90JXcg3YsI/AAAAAAAABn4/EN7u2vMuRug/s1600/key+lime+cheesecake+slice+in+front.jpg",
      "price": 8.00,
      "bulkPricing": null
    },
    {
      "id": 3,
      "name": "Cookie",
      "imageURL": "http://www.mayheminthekitchen.com/wp-content/uploads/2015/05/chocolate-cookie-square.jpg",
      "price": 1.25,
      "bulkPricing": {
        "amount": 6,
        "totalPrice": 6.00
      }
    },
    {
      "id": 4,
      "name": "Mini Gingerbread Donut",
      "imageURL": "https://s3.amazonaws.com/pinchofyum/gingerbread-donuts-22.jpg",
      "price": 0.50,
      "bulkPricing": null
    }
  ]
}
```

Test cases:
1. Without any sales active, add these items: 1 cookie, 4 brownies, 1 cheesecake. The total price
should be $16.25.
2. Without any sales active, add these items: 8 cookies. The total price should be $8.50.
3. Without any sales active, add these items: 1 cookie, 1 brownie, 1 cheesecake, 2 donuts. The
total price should be $12.25.
4. On October 1 2022, add these items: 8 cookies, 4 cheesecakes. The total price should be $32.50.


##### Missing functionality
- bulk pricing (I ran out of time due to some personal issues, tried my best to implement & cover as much functionality as I could)

##### How to run
- Navigate to the root folder and run the following command docker.exe compose -f docker-compose.yml -f docker-compose.override.yml -p bakerystoreapp up --force-recreate -d --build basketapi catalogapi basketdb catalogdb discountapi discountdb mongoexpresscatalogdb discountgrpc mongoexpressdiscountdb storefront
- Open a browser and navigate to http://localhost:8080

##### Project structure

- the solution is built on a microservices architecture and it contains 4 main projects
 a. StoreFront - acts as a Backend for FrontEnd (BFF)
 b. Discount.API - microservice handling discount bounded-context
 c. Basket.API - microservice handling shopping cart
 d. Catalog.API - microservice handling the product catalog

- the Catalog.API project connects to a mongodb database to retrieve & store the products
  --- database data can be viewed by accessing the mongo-express tool (http://localhost:8081)
  --- data is automatically seeded on application startup (CatalogContextSeed.cs)

- the Discount.API project connects to a mongodb database to retrieve & store the discounts
  --- database data can be viewed by accessing the mongo-express tool (http://localhost:8082)
  --- data is automatically seeded on application startup (DiscountContextSeed.cs)

- the Basket.API project connects to a Redis cache for storing the current state of the cart, update the cart on cart changes, etc.

##### Improvements
- there are a lot of improvements, code refactorings that can be done to the project, can be discussed, but I consider they were out of the project scope
