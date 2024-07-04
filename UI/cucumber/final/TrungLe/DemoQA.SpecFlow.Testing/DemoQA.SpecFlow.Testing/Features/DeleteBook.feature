Feature: Delete book successfully

Background:
	Given the user is logged into the application with a "valid_account"
	And there is a book "git_book" in the collection

@Successfully
Scenario Outline: User delete book successfully from his collection	
	And the user is on the Profile page
	When the user searchs book with "<title>"
	And the user clicks on Delete icon
	And the user clicks on OK button
	And the user clicks on OK button of alert "Book deleted."
	And the book is not shown

Examples:
	| title            | 
	| Git Pocket Guide |
