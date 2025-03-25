# BakeryStoreApp
Microservices architecture showcase

1. Missing functionality
- bulk pricing (I ran out of time due to some personal issues, tried my best to implement & cover as much functionality as I could)

2. How to run
- Navigate to the root folder and run the following command docker.exe compose -f D:\Code\BakeryStoreApp\docker-compose.yml -f D:\Code\BakeryStoreApp\docker-compose.override.yml -p bakerystoreapp up --force-recreate -d --build basketapi catalogapi basketdb catalogdb discountapi discountdb mongoexpresscatalogdb discountgrpc mongoexpressdiscountdb storefront
- Open a browser and navigate to http://localhost:8080

3. Project structure

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

4. Improvements
- there are a lot of improvements, code refactorings that can be done to the project, can be discussed, but I consider they were out of the project scope
