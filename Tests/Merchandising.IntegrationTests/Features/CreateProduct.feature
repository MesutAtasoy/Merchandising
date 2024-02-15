Feature: CreateProduct
Create Product Operations

    Scenario: Adding a random product with category
        When I send the create product request
        Then the HTTP response is 'Created'
        And return valid created product

    Scenario: Adding a random product without category
        When I send the create product request without category
        Then the HTTP response is 'Created'
        And return valid created product
        
    Scenario: Adding a random product with unknown category
        When I send the create product request with unknown category 
        Then the HTTP response is 'Not Found'
        And the error message is 'Category is not found'
        
    Scenario: Trying to add a random product without name
        When I send the create product request without name
        Then the HTTP response is 'Bad Request'
        And the error message is 'Name is required'
        
    Scenario: Trying to add a random product with long name
        When I send the create product request with 250 character(s) name
        Then the HTTP response is 'Bad Request'
        And the error message is 'Name must be at most 200 characters'
        