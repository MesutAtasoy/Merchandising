Feature: DeleteProduct
Delete Product Operations

    Scenario: Delete existing product
        Given a random defined product
        When I send the delete product request
        Then the HTTP response is 'OK'

    Scenario: Try to delete non existing product
        Given a random undefined product
        When I send the delete product request
        Then the HTTP response is 'NotFound'
        And the error message is 'Product is not found'