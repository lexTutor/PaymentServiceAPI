### PaymentServiceAPI

##### Allows users make payments to an external service although currently mocked, can be easily extended to allow for integration of actual external payment such as stripe due to the extensible nature of the structure.
##### The project was written with the "Clean" Architectural pattern following Solid principles and OOP.
##### Sqlite was the database of choice to enable general use of the project without any extra database configurations.
##### EntityFrameworkCore was the ORM used to communicate with the database.
##### The project was Unit tested and interfaces were mocked using the MOQ framework and a testing 74.1% coverage was achieved (537 lines of 724). Pictures can be found below.
##### Error Logging was implemented using Nlog.
### NB: Stack trace of error messages are designed go be returned as part of the response object while in the development environment

##### [Link to the ERD](https://drive.google.com/file/d/1Epv-Mg6DUzyyH8yq7ct_Duey5tRwNSmi/view?usp=sharing)
##### [Link to the documentation](https://docs.google.com/document/d/1BzDBQ95DrE0_ioyN739GBZbTNBpiR1gPROw97bNyiNk/edit?usp=sharing)

##### To run this project locally
##### 1) Clone it into your device.
##### 3) Open in Visual studio or VS Code.
##### 2) Run the app either in debug mode or in release mode.

![Screenshot (141)](https://user-images.githubusercontent.com/72900885/122638245-041f8a80-d0eb-11eb-9cbd-917840932c66.png)
![Screenshot (142)](https://user-images.githubusercontent.com/72900885/122638292-3f21be00-d0eb-11eb-80d1-910a130272c3.png)
