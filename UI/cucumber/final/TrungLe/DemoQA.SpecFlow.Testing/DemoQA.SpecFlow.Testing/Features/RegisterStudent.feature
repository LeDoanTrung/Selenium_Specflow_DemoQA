Feature: Register Student

@AllFields @Successfully
Scenario Outline: Register a student information with all fields successfully
	Given the user is on Student Registration Form page
	When the user inputs the following data into Student Registration Form
		| Key            | Value                          |
		| firstName      | Trung                          |
		| lastName       | Le                             |
		| email          | ledoantrung1999@gmail.com      |
		| gender         | Male                           |
		| mobileNumber   | 0564686019                     |
		| dateOfBirth    | 03 April,1999                  |
		| subjects       | Maths, Arts, English           |
		| hobbies        | Sports, Music                  |
		| picture        | avatar.jpg                     |
		| currentAddress | Co Giang, Phu Nhuan, Hochiminh |
		| state          | Haryana                        |
		| city           | Panipat                        |
	And the user click on Submit button
	Then a successfully message "Thanks for submitting the form" is shown
	And all information of student form is shown