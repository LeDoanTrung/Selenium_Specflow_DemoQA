Feature: Search Book with multiple results

@Successfully
Scenario Outline: User searchs book successfully with multiple results
	Given there are books named with following title
		| title                                     |
		| Learning JavaScript Design Patterns       |
		| Designing Evolvable Web APIs with ASP.NET |
	And the user is on Book Store page
	When the user inputs the keyword "<keyword>"
	Then all books match with "<keyword>" will be displayed

Examples:
	| keyword |
	| Design  |
	| design  |
