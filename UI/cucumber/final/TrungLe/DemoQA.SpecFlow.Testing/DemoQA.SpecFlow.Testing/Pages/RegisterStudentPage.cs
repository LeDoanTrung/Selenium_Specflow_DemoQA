using Bogus.DataSets;
using DemoQA.SpecFlow.Service.DataObjects;
using DemoQA_Selenium.DataObjects;
using OpenQA.Selenium;
using System.Globalization;
using TMS_Selenium.Library;
using TMS_SpecFlow_Testing.Pages;


namespace DemoQA_Selenium.Pages
{
    public class RegisterStudentPage : BasePage
    {

        //Web Element
        private Element _firstName = new Element(By.Id("firstName"));
        private Element _lastName = new Element(By.Id("lastName"));
        private Element _email = new Element(By.Id("userEmail"));
        private Element _gender(string gender) 
        { 
            return new Element(By.XPath($"//label[.='{gender}']")); 
        }
        private Element _mobile = new Element(By.Id("userNumber"));
        private Element _dateOfBirth = new Element(By.Id("dateOfBirthInput"));
        private Element _yearDropdown = new Element(By.XPath("//select[contains(@class,'year')]"));
        private Element _monthDropdown = new Element(By.XPath("//select[contains(@class,'month')]"));
        private Element _dayOfBirthValue(string month, string day) 
        { 
            return new Element(By.XPath($"//div[contains(@class,'day') and contains(@aria-label,'{month}') and .='{day}']")); 
        }
        private Element _subjectsInput = new Element(By.XPath("//div[@id='subjectsContainer']//input"));
        private Element _hobbyOption(string hobby) 
        { 
            return new Element(By.XPath($"//label[text()='{hobby}']")); 
        }
        private Element _pictureButton = new Element(By.Id("uploadPicture"));
        private Element _address = new Element(By.Id("currentAddress"));
        private Element _stateDropdown = new Element(By.XPath("//div[@id='state']//input[contains(@id,'input')]"));
        private Element _stateOption(string state) 
        { 
            return new Element(By.XPath($"//div[text()='{state}']")); 
        }
        private Element _cityDropdown = new Element(By.XPath("//div[@id='city']//input[contains(@id,'input')]"));
        private Element _submitButton = new Element(By.Id("submit"));
        private Element _modalPopUp = new Element(By.CssSelector("div[role='document']"));
        private Element _modalHeader = new Element(By.CssSelector("div.modal-header"));
        private Element _modalData(string data) 
        { 
            return new Element(By.XPath($"//td[.='{data}']/following-sibling::td")); 
        }
        private Element _closeButton = new Element(By.Id("closeLargeModal"));
        private Element _modalTitle = new Element(By.XPath("//div[text()='Thanks for submitting the form']"));

        //Page Method
        public void InputFirstName(string name)
        {
            _firstName.ClearText();
            _firstName.InputText(name);
        }

        public void InputLastName(string name)
        {
            _lastName.ClearText();
            _lastName.InputText(name);
        }

        public void InputEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                _email.ClearText();
                _email.InputText(email);

            }
        }

        public void SelectGender(string gender)
        {
            _gender(gender).ClickAndScrollToElement();
        }

        public void InputMobileNumber(string number)
        {
            _mobile.ClearText();
            _mobile.InputText(number);
        }

        public void DateOfBirthPick(string dateOfBirth)
        {
            this._dateOfBirth.ClickOnElement();
            SelectDate(dateOfBirth);
        }

        public void SelectDate(string date)
        {
            DateTime parsedDate = DateTime.ParseExact(date, "dd MMMM,yyyy", CultureInfo.InvariantCulture);

            string day = parsedDate.Day.ToString();
            string month = parsedDate.ToString("MMMM", CultureInfo.InvariantCulture);
            string year = parsedDate.Year.ToString();


            _yearDropdown.SelectOptionByText(year);

            _monthDropdown.SelectOptionByText(month);

            _dayOfBirthValue(month, day).ClickOnElement();
        }

        public void SelectSubjects(string subjects)
        {
            if (!string.IsNullOrEmpty(subjects))
            {
                string[] subjectArray = subjects.Split(", ");

                foreach (string subject in subjectArray)
                {
                    _subjectsInput.InputText(subject);
                    _subjectsInput.SendKeys(Keys.Enter);
                }
            }
        }


        public void SelectHobbies(string hobbies)
        {
            if (!string.IsNullOrEmpty(hobbies))
            {
                string[] hobbiesArray = hobbies.Split(", ");

                foreach (string hobby in hobbiesArray)
                {
                    _hobbyOption(hobby).ClickOnElement();
                }
            }
        }

        public void UploadPicture(string imageName)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string imageDirectory = Path.Combine(projectDirectory, "TestData", "Images");

                string formatFilePath = Path.Combine(imageDirectory, imageName);

                if (File.Exists(formatFilePath))
                {
                    _pictureButton.UploadFile(formatFilePath);
                }
                else
                {
                    throw new FileNotFoundException($"File with name {imageName} not found in {imageDirectory}");
                }
            }
        }

        public void InputAddress(string address)
        {
            this._address.ClearText();
            this._address.InputText(address);
        }

        public void SelectState(string state)
        {
            _stateDropdown.InputText(state);
            _stateDropdown.SendKeys(Keys.Enter);
        }

        public void SelectCity(string state, string city)
        {
            if (_stateOption(state).IsElementExist())
            {
                _cityDropdown.InputText(city);
                _cityDropdown.SendKeys(Keys.Enter);
            }
        }

        public void ClickOnSubmitButton()
        {
            _submitButton.ClickOnElement();
        }
        public void RegisterStudent(StudentDTO student)
        {
            InputFirstName(student.FirstName);
            InputLastName(student.LastName);
            InputEmail(student.Email);
            SelectGender(student.Gender);
            InputMobileNumber(student.MobileNumber);
            DateOfBirthPick(student.DateOfBirth);
            SelectSubjects(student.Subjects);
            SelectHobbies(student.Hobbies);
            UploadPicture(student.Picture);
            InputAddress(student.CurrentAddress);
            SelectState(student.State);
            SelectCity(student.State, student.City);
        }

        public bool IsMessageDisplayed()
        {
            return _modalTitle.IsElementDisplayed();
        }

        public StudentFormattedDTO GetStudentInfoFromUI()
        {
            return new StudentFormattedDTO
            {
                FullName = _modalData("Student Name").GetText(),
                Email = _modalData("Student Email").GetText(),
                Gender = _modalData("Gender").GetText(),
                MobileNumber = _modalData("Mobile").GetText(),
                DateOfBirth = _modalData("Date of Birth").GetText(),
                Subjects = _modalData("Subjects").GetText(),
                Hobbies = _modalData("Hobbies").GetText(),
                Picture = _modalData("Picture").GetText(),
                CurrentAddress = _modalData("Address").GetText(),
                StateAndCity = _modalData("State and City").GetText()
            };
        }

        public void VerifyMessageAfterRegistration(string message)
        {
            IsMessageDisplayed().Should().BeTrue();
            _modalTitle.GetText().Should().Be(message);
        }
        public void VerifyStudentInformation(StudentDTO student)
        {
            var expectedStudent = StudentFormattedDTO.FormatStudentDTO(student);
            var actualStudent = GetStudentInfoFromUI();
            actualStudent.Should().BeEquivalentTo(expectedStudent);
        }

    }
}
