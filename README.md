# Book API

##Project Overview

A REST API built with .NET Core 8, SQL Server, and Entity Framework Core & Dapper for managing books. The API provides sorting, stored procedure support, batch inserts, and price calculations.

##Tech Stack

1. .NET Core 8
2. Entity Framework Core & Dapper
3. SQL Server
4. Swagger 

##Setup Instructions

1. Clone the Repository
2. Configure Database (appsettings.json)
3. Run the API

##API Endpoints

1. Get Books Sorted by Publisher & Author: GET /api/books/sorted-by-publisher
2. Get Books Sorted by Author: GET /api/books/sorted-by-author
3. Get Total Price of Books: GET /api/books/total-price
4. Bulk Insert Books: POST /api/books/bulk-insert
5. Get Books Sorted by Publisher & Author by using SP: GET /api/books/sorted-by-publisher-sp
6. Get Books Sorted by Author by using SP: GET /api/books/sorted-by-author-sp

