# Exercise 7

**Instructions:** Please complete the following requirements ensuring your solution meets all of them. Going above and beyond the requirements is great so long as all of the requirements are met. Before coding anything, please read all of the requirements first. If you are unsure about anything, please ask the instructor.

## Requirements

**Requirement 1.** Create a new component named Car Edit Row. The Car Edit Row is similar to Car View Row except the columns for make, model, year, color and price are empty input fields.  Do not make the Id an input field. In the last column, there should be two buttons: "Save" and "Cancel". Do not implement the logic to do the save and cancel, but display the buttons.

FOR REQUIREMENT 1: When the Car Edit Row is displayed, do **NOT** prepopulate the fields with the data of the car being edited.

**Requirement 2.** Add a button labeled "Edit" to the last column of the Car View Row component. When the button is clicked the row on which it is clicked changes to a Car Edit Row component. Only one row is editable at a time. Your data structure should only support editing one row at a time.

**Bonus Requirement**

Look up the `OnInitializedAsync` lifecycle method. Using this method, set the values of the form's model to the `Car` parameter passed into the `<CarEditRow>` component.

Ensure it works!