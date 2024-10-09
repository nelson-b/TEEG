# TestAPI_TEE
Note: There can be more addition to the real live application, the features added are subject to test project requirement and time available

**Key technology components used:**
1) .Net Core Web API 6.0
2) Swagger
3) In Memory database (For test purpose in live environment actual SQL server database I would recommend)
4) Fluent Validation
5) Dependency Injection
6) Serilog
7) Repository Pattern
8) CQRS Pattern with Mediatr
9) Error Handling

**Architecture layers**:

1)
Guest.API

This is Gateway layer and entry point. All API request, request validation are made here

Guest.BAL

This business and transformation layer. All logic is handled in this layer

Guest.Infrastructure

This infrastructure layer. All database backend transactions are handled here

