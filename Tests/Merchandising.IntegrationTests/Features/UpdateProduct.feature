Feature: UpdateProduct
Update product operations

    Scenario: Update existing product
        Given a random defined product
        When I send the update request
        Then the HTTP response is 'OK'
        And return valid updated product

    Scenario: Update existing product without category
        Given a random defined product
        When I send the update request without category
        Then the HTTP response is 'OK'
        And return valid updated product

    Scenario: Trying to update non-existing product
        Given a random undefined product
        When I send the update request with unknown category
        Then the HTTP response is 'Not Found'
        And the error message is 'Product is not found'
        
    Scenario: Trying to update existing product with unknown category
        Given a random defined product
        When I send the update request with unknown category
        Then the HTTP response is 'Not Found'
        And the error message is 'Category is not found'

    Scenario: Try to update existing product without name
        Given a random defined product
        When I send the update request without name
        Then the HTTP response is 'Bad Request'
        And the error message is 'Name is required'
        
    Scenario: Try to update existing product with long name
        Given a random defined product
        When I send the update request with 250 character(s) name
        Then the HTTP response is 'Bad Request'
        And the error message is 'Name must be at most 200 characters'
