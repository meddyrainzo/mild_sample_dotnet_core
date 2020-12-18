## Quoters server

This is the backend service for the `Quoters` application. It is a `REST API` backend so expect more server calls than with using a `GraphQL` backend

## Technologies used

- Dapper
- Docker
- Postgresql

:warning: This is not supposed to be a secure WebAPI and it does not symbolize best practices. It is just a sample API I built to mess around with frontend tech

## Things not covered by this API

- Token based authentication. Will usually do this with `IdentityServer4` or `OpenIddict`.
- API versioning
- The api doesn't check if the user exists or not before creating quotes, liking quotes, bookmarking quotes and commenting on quotes (Should be fixed later)
- Integration tests haven't been written
- If `Authentication` and `Authorization` was implemented, they `UserContext` would have been used to get the `currentUserId` used in the `QuotesController` for retrieving quotes and not the ugly `url`

## Steps to run

- To run this backend, simply clone the application and then run `docker-compose up`
- The `Swagger` documentation can be found at `http://localhost:8000/swagger`
